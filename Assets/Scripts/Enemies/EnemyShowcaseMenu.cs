using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class EnemyShowcaseMenu : MonoBehaviour
{
    [SerializeField]
    private EnemySpawner _enemySpawner;

    [SerializeField]
    private WaveDefinition _waveDefinition;

    [SerializeField]
    private MovementStrategy _enemyMovementStrategy;

    [SerializeField]
    private EnemyDestination _enemyDestination;

    [SerializeField]
    private WorldObjectSelectionManager _objectSelectionManager;

    [SerializeField]
    private EffectsManager effectsManager;

    int index = 0;

    void Start()
    {
        _enemySpawner.OnWaveEnemySpawned += OnEnemySpawned;
        _enemyDestination.OnEnemyReached += OnEnemyDestroyed;
        _objectSelectionManager.OnObjectSelected += OnEnemySelected;
        StartShowcase();
    }

    private void OnEnemySelected(GameObject obj)
    {
        if(obj.TryGetComponent(out Enemy enemy))
        {
            effectsManager.BonesEffect(enemy.transform.position);
            effectsManager.SpawnCoin(enemy.transform.position);
            OnEnemyDestroyed(IDestructable.DestroyedSource.KILLED, enemy);
        }
            
    }

    private void OnEnemySpawned(Enemy obj)
    {
        Movement movable = obj.GetComponent<Movement>();
        movable.MovementStrategy = _enemyMovementStrategy;
        movable.Destination = _enemyDestination.transform.position;

        Vector3 enemyPosition = new Vector3(-4,  0, -7);
        obj.transform.position = enemyPosition;
        movable.FaceDirection(0 < enemyPosition.x);
        movable.Initialize();
    }

    private void OnEnemyDestroyed(IDestructable.DestroyedSource source, Enemy enemy)
    {
        enemy.DoDestroy(source);
        StartShowcase();

        if(source == IDestructable.DestroyedSource.KILLED)
            PlayerProgressMonitor.Instance.RegisterKill();
    }

    private void SetupRandomEnemy()
    {
        WaveDefinition.WaveUnit waveUnit = _waveDefinition.WaveUnits[index++%_waveDefinition.WaveUnits.Length];
        _enemySpawner._enemyDef = waveUnit.EnemyDef;
        _enemySpawner.SpawnCount = 1;
    }

    [ContextMenu("Stat Showcase")]
    private void StartShowcase()
    {
        SetupRandomEnemy();
        _enemySpawner.StartSpawn();
    }
}
