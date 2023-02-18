using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WaveDefinition;

public class WaveDefinitionPanel : MonoBehaviour
{
    [SerializeField]
    private WaveEnemyInfo waveEnemyInfoPrefab;

    [SerializeField]
    private GameObject _panelRoot;

    private List<WaveEnemyInfo> _waveEnemiesInfoPanels = new List<WaveEnemyInfo>();
    private Dictionary<WaveUnit, WaveEnemyInfo> _waveEnemies = new Dictionary<WaveUnit, WaveEnemyInfo>();

    public void Awake()
    {
        _waveEnemiesInfoPanels.AddRange(_panelRoot.GetComponentsInChildren<WaveEnemyInfo>());
    }

    internal void DisplayWave(WaveDefinition waveDef)
    {
        foreach (var item in _waveEnemiesInfoPanels)
        {
            Destroy(item.gameObject);
        }
        _waveEnemiesInfoPanels.Clear();
        _waveEnemies.Clear();
        foreach(var wave in waveDef.WaveUnits)
        {
            WaveEnemyInfo waveEnemyInfo = Instantiate(waveEnemyInfoPrefab, _panelRoot.transform);
            waveEnemyInfo.DisplayInfo(wave.EnemyDef.Sprite, wave.Quantity);
            _waveEnemiesInfoPanels.Add(waveEnemyInfo);
            _waveEnemies.Add(wave, waveEnemyInfo);
        }
    }

    public void UpdateWaveEnemyProgress(WaveUnit info, int enemiesLeft)
    {
        WaveEnemyInfo waveEnemyInfo = _waveEnemies[info];
        waveEnemyInfo.UpdateEnemiesCount(enemiesLeft);
    }
}
