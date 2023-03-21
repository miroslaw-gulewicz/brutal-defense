using Aim;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoUI : MonoBehaviour, ITab
{
	[SerializeField] private EconomyManager economyManager;

	[SerializeField] private TurretBehaviour _currentTurret;

	[SerializeField] private ObjectPlacementControl _objectPlacementControl;

	[SerializeField] private Canvas _towerInfoCanvas;

	[SerializeField] private StatsInfoPanel _towerStatsPanel;

	[SerializeField] private PayButton sellTowerButton;

	[SerializeField] private PayButton upgradeTowerButton;

	[SerializeField] private Button _targetIngButton;

	[SerializeField] private Button _closeTowerInfo;

	[SerializeField] private UpgradeTowerButton upgradeTowerControl;

	[SerializeField] private ShootingStrategyPanel _shootingStrategyPanel;

	public event Action OnCloseTab;

	private TabPanelUI tabPanelUI;

	public void Awake()
	{
		tabPanelUI = new TabPanelUI();
		tabPanelUI.AddTab(_shootingStrategyPanel);

		sellTowerButton.onClick.AddListener(SellTower);
		upgradeTowerButton.onClick.AddListener(DoUpgrade);
		_closeTowerInfo.onClick.AddListener(() => OnCloseTab?.Invoke());
		_targetIngButton.onClick.AddListener(ShowTargetingPanel);
		_shootingStrategyPanel.OnCloseTab += OnTargetPanelClose;
	}

	private void OnTargetPanelClose()
	{
		_towerStatsPanel.gameObject.SetActive(true);
		tabPanelUI.CloseAll();
		_towerInfoCanvas.gameObject.SetActive(true);
	}

	private void ShowTargetingPanel()
	{
		tabPanelUI.ActivateTab(_shootingStrategyPanel);
		_towerStatsPanel.gameObject.SetActive(false);
		_towerInfoCanvas.gameObject.SetActive(false);
	}

	private void Start()
	{
		tabPanelUI.CloseAll();
	}

	private void DoUpgrade()
	{
		economyManager.UpgradeTower(_currentTurret);
		DisplayTurretInfo(_currentTurret);
	}

	private void SellTower()
	{
		economyManager.SellTower(_currentTurret);
		OnCloseTab?.Invoke();
		_objectPlacementControl.WorldObjectSelectionManager.Deselect(_currentTurret);
	}

	public void Hide()
	{
		_towerInfoCanvas.gameObject.SetActive(false);
		_shootingStrategyPanel.gameObject.SetActive(false);
	}

	public void Show()
	{
		_towerInfoCanvas.gameObject.SetActive(true);
		_towerStatsPanel.gameObject.SetActive(true);
	}

	public void DisplayTurretInfo(TurretBehaviour turret)
	{
		_currentTurret = turret;
		var turretDef = _currentTurret.TurretObject;

		_towerStatsPanel.Display(turretDef.Sprite, turretDef.BasicStats, turretDef.Resistance, turretDef as Weapon);
		sellTowerButton.Value = economyManager.TurretValue(_currentTurret);

		upgradeTowerControl.CurrentTower = turret;
		var nextUpgrade = economyManager.NextLevelTowerValue(_currentTurret);
		upgradeTowerButton.gameObject.SetActive(nextUpgrade != -1 && nextUpgrade <= economyManager.Coins);
		if (nextUpgrade != -1)
		{
			upgradeTowerButton.Value = nextUpgrade;
		}

		_shootingStrategyPanel.SetShootinObject(turret.GetComponent<Shooting>());
	}
}