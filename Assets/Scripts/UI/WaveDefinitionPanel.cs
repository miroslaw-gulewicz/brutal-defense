using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveDefinition;

public class WaveDefinitionPanel : CollectionDisplayPanel<WaveUnit, WaveEnemyInfo>
{
	public void Awake()
	{
		uiElements.AddRange(_panelRoot.GetComponentsInChildren<WaveEnemyInfo>());
	}

	internal void DisplayWave(WaveDefinition waveDef)
	{
		_selectedObjectManager.ClearSelection();
		foreach (var item in uiElements)
		{
			Destroy(item.gameObject);
		}

		uiElements.Clear();
		_elementToUi.Clear();
		foreach (var wave in waveDef.WaveUnits)
		{
			WaveEnemyInfo waveEnemyInfo = Instantiate(prefab, _panelRoot.transform);
			waveEnemyInfo.DisplayInfo(wave.EnemyDef.Sprite, wave.Quantity);
			uiElements.Add(waveEnemyInfo);
			_elementToUi.Add(wave, waveEnemyInfo);
		}
	}

	public void SelectPanel(WaveUnit waveunit)
	{
		_selectedObjectManager.SelectObject(_elementToUi[waveunit]);
	}

	public void UpdateWaveEnemyProgress(WaveUnit info, int enemiesLeft)
	{
		WaveEnemyInfo waveEnemyInfo = _elementToUi[info];
		waveEnemyInfo.UpdateEnemiesCount(enemiesLeft);
	}
}