using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentEnemiesInfoUI : MonoBehaviour
{
    [SerializeField]
    private EnemyStatsInfoPanel _enemyStatsInfoPanel;

    [SerializeField]
    private Canvas mainCanvas;

    [SerializeField]
    TMPro.TextMeshProUGUI current;

    [SerializeField]
    TMPro.TextMeshProUGUI remains;

    [SerializeField]
    WaveManager waveManager;

    [SerializeField]
    private WorldObjectSelectionManager WorldObjectSelectionManager;

    private SelectObjectsManager<Enemy> enemySelection;



    public void Start()
    {
        enemySelection = new SelectObjectsManager<Enemy>();
        waveManager.OnWaveBegin += RefreshStats;
        waveManager.OnEnemyCountChanged += RefreshStats; 
        WorldObjectSelectionManager.OnObjectSelected += OnObjectSelected;

    }

    private void OnObjectSelected(GameObject obj)
    {
        if (obj == null) enemySelection.ClearSelection();

        if (!obj.TryGetComponent(out Enemy enemy)) return;

        enemySelection.SelectObject(enemy);

        if(enemySelection.IsObjectSelected())
        {
            _enemyStatsInfoPanel.Display(enemySelection.GetSelectedValue().EnemyObject);
        }
        else
        {
            _enemyStatsInfoPanel.Close();
        }
    }

    private void RefreshStats()
    {
        current.text = waveManager.CurrentEnemies.ToString();
        remains.text = waveManager.EnemiesRemains.ToString();
    }
}
