using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSummaryMenu : MonoBehaviour
{
	[SerializeField] private Canvas _canvas;

	[SerializeField] private GameObject _defeatedEffects;

	[SerializeField] private PlayerActionsUi _playerActionsUi;

	[SerializeField] private TowerBuildingMenuUI _towerBuildingMenuUI;

	[SerializeField] private GameObject _ingameMenu;

	[SerializeField] private GameManager _gameManager;

	[SerializeField] private WaveManager _waveManager;

	[SerializeField] private EconomyInfoUI _conomyInfoUi;

	[SerializeField] private GameObject _victoryObjects;

	[SerializeField] private GameObject _defeatObjects;

	[SerializeField] private Score _score;

	[SerializeField]int wavesToComplete = 0;

	[SerializeField] public Button _startWaveButton;

	private void Awake()
	{
		_waveManager.OnWaveBegin += OnWaveBegin;
		_waveManager.OnWaveChanged += OnWaveDefinitionChanged;
		_waveManager.WaveUnitChanged += OnWaveUnitChanged;
		_canvas.gameObject.SetActive(false);
	}

	private void OnWaveUnitChanged(WaveDefinition.WaveUnit obj)
	{
		wavesToComplete--;
		if(wavesToComplete == 0)
		{
			_waveManager.OnEnemyCountChanged += OnEnemyCountChanged;
		}
	}

	private void OnWaveDefinitionChanged(WaveDefinition obj)
	{
		wavesToComplete = obj.WaveUnits.Length;
	}

	private void OnWaveBegin()
	{
		Restore();
	}

	private void OnPlayerStatsChanged()
	{
		if(_gameManager.BasicStats.CurrentHp <= 0)
		{
			Show(false);
		}
	}

	private void OnEnemyCountChanged()
	{
		if (_waveManager.CurrentEnemies == 0)
		{
			Show(_gameManager.BasicStats.CurrentHp > 0);
		}
	}

	public void Restore()
	{
		_canvas.gameObject.SetActive(false);
		_defeatedEffects.SetActive(false);
		ActivateUiElements(true);
		_defeatedEffects.SetActive(false);
		_victoryObjects.SetActive(false);
		_gameManager.OnStatsChanged += OnPlayerStatsChanged;
		_gameManager.RestoreGameSpeed();
	}

	public void Show(bool isVictorious)
	{

		_gameManager.OnStatsChanged -= OnPlayerStatsChanged;
		_waveManager.OnEnemyCountChanged -= OnEnemyCountChanged;
		_canvas.gameObject.SetActive(true);
		_defeatedEffects.SetActive(!isVictorious);
		ActivateUiElements(false);
		_defeatObjects.SetActive(!isVictorious);
		_victoryObjects.SetActive(isVictorious);
		_score.gameObject.SetActive(isVictorious);

		if (!isVictorious)
			_gameManager.SetGameSpeed(0.25f);

		_playerActionsUi.ShowButtons();
		_playerActionsUi.HideButtons();
	}

	private void ActivateUiElements(bool active)
	{
		_conomyInfoUi.gameObject.SetActive(active);

		_playerActionsUi.gameObject.SetActive(active);
	}
}
