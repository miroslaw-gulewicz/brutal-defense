using Aim;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static TargetingMethodPanel;

public class ShootingStrategyPanel : MonoBehaviour, ITab
{
	[SerializeField] private GameObject _manualTargetingGizmo;

	[SerializeField] private LayerMask _manualTargetingLayers;

	[SerializeField] private TargetingMethodPanel _targetingMethodPanel;

	[SerializeField] private Button _closeTargetingSetup;

	[SerializeField] private ObjectPlacementControl _objectPlacementControl;

	[SerializeField] private WaveManager _waveManager;

	[SerializeField] private EnemySelectPanel _enemySelectPanel;

	[SerializeField] private ShootingStrategySwitcher _turretShootingStrategySwitcher;

	[SerializeField] private Button _cancelTargetingSetup;

	[SerializeField] private List<TowerShootingStrategyDropdownData> strategies;

	private Shooting _currentShooting;

	public event Action OnCloseTab;

	public void Awake()
	{
		_targetingMethodPanel.ValueChanged += OnStrategyChanged;
		_targetingMethodPanel.DisplayTargetingMethods(strategies);

		_enemySelectPanel.TargetSelected += _enemySelectPanel_TargetSelected;
		_cancelTargetingSetup.onClick.AddListener(ClosePanel);
	}

	private void ClosePanel()
	{
		_objectPlacementControl.Reset();
		OnCloseTab?.Invoke();
	}

	private void OnStrategyChanged(TowerShootingStrategyDropdownData strategy)
	{
		_enemySelectPanel.gameObject.SetActive(false);
		var targetingStrategyType = strategy.targeting;

		_turretShootingStrategySwitcher.Shooting = _currentShooting;

		_turretShootingStrategySwitcher.SystemType = targetingStrategyType;
		_targetingMethodPanel.Select(strategy);
		switch (targetingStrategyType)
		{
			case TargetingSystemType.PRIORITY:
				_enemySelectPanel.gameObject.SetActive(true);
				_enemySelectPanel.DisplayEnemies(_waveManager.WaveEnemiesDefinition());
				_enemySelectPanel.SelectTile(_turretShootingStrategySwitcher.Shooting.TargetingSystem.TargetDefinition);
				break;
			case TargetingSystemType.MANUAL:
				_objectPlacementControl.Setup(_manualTargetingGizmo, OnManualTargetSelected, _manualTargetingLayers,
					false, true);
				break;
			default:
				_turretShootingStrategySwitcher.SetupTargetingSystem();
				break;
		}
	}

	private void _enemySelectPanel_TargetSelected(EnemyObject obj)
	{
		_turretShootingStrategySwitcher.EnemyObject = obj;
		_turretShootingStrategySwitcher.SetupTargetingSystem();
	}

	private void OnManualTargetSelected(GameObject target)
	{
		Debug.Log(target);
		_turretShootingStrategySwitcher.CommonTarget = target;
		_turretShootingStrategySwitcher.SetupTargetingSystem();
		_objectPlacementControl.Reset();
	}

	public void SetShootinObject(Shooting shooting)
	{
		_currentShooting = shooting;
		TargetingSystemType targetingSystemType =
			TargetingSystemFactory.AsTargetingSystemType(shooting.TargetingSystem);
		_targetingMethodPanel.Select(strategies.Find(s => s.targeting == targetingSystemType));
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
		_enemySelectPanel.gameObject.SetActive(false);
	}
}