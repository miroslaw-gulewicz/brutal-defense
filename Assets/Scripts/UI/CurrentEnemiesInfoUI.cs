using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveDefinition;

public class CurrentEnemiesInfoUI : MonoBehaviour
{
	[SerializeField] private StatsInfoPanel _enemyStatsInfoPanel;

	[SerializeField] private Canvas mainCanvas;

	[SerializeField] TMPro.TextMeshProUGUI current;

	[SerializeField] TMPro.TextMeshProUGUI remains;

	[SerializeField] WaveManager waveManager;

	[SerializeField] private WorldObjectSelectionManager WorldObjectSelectionManager;

	[SerializeField] private WaveDefinitionPanel _waveDefinitionPanel;

	public void Awake()
	{
		_waveDefinitionPanel.OnWaveEnemyHovered = OnObjectHovered;
		waveManager.OnWaveBegin += RefreshStats;
		waveManager.OnEnemyCountChanged += RefreshStats;
		waveManager.OnWaveChanged += WaveChanged;
		waveManager.WaveUnitChanged += OnWaveUnitChanged;
	}

	private void OnWaveUnitChanged(WaveUnit waveUnit)
	{
		_waveDefinitionPanel.SelectPanel(waveUnit);
	}

	private void WaveChanged(WaveDefinition wave)
	{
		_waveDefinitionPanel.DisplayWave(wave);
	}

	private void OnObjectHovered(EnemyObject enemyObject)
	{
		if(enemyObject == null)
		{
			_enemyStatsInfoPanel.gameObject.SetActive(false);
			return;
		}

		_enemyStatsInfoPanel.Display(enemyObject.EnemyName, enemyObject.Sprite, enemyObject.BasicStats, enemyObject.Resistance, enemyObject.SelfInflictors, enemyObject.Weapon);
	}

	private void RefreshStats()
	{
		current.text = waveManager.CurrentEnemies.ToString();
		remains.text = waveManager.EnemiesRemains.ToString();
	}
}