using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private WaveManager waveManager;

    [SerializeField]
    EffectsManager effectsManager;

    [SerializeField]
    EconomyManager economyManager;

    public event Action OnStatsChanged;

    [SerializeField]
    public TurretSpawner _turretSpawner;

    public LevelDefinition _levelDefinition;

    [SerializeField]
    private BasicStatsHolder basicStats;

    public BasicStatsHolder BasicStats { get => basicStats; }


    private void Awake()
    {
        waveManager.EnemyDestroyedEvent += EnemyDestroyedEvent;
        _turretSpawner.DestroyCallBack += TowerDestroyed;
    }

    private void Start()
    {
        SetupLevel(_levelDefinition);
    }

    private void TowerDestroyed(IDestructable.DestroyedSource arg1, TurretBehaviour arg2)
    {
        effectsManager.TowerExplosion(arg2.transform.position);
    }

    public void SetupLevel(LevelDefinition levelDefinition)
    {
        economyManager.Coins = levelDefinition.StartCoins;
        waveManager.WaveDefinition = levelDefinition.Waves[0];
    }

    private void EnemyDestroyedEvent(IDestructable.DestroyedSource source, Enemy enemy)
    {
        switch (source)
        {
            case IDestructable.DestroyedSource.KILLED:
                effectsManager.SpawnCoin(enemy.gameObject.transform.position);
                effectsManager.BonesEffect(enemy.gameObject.transform.position);
                economyManager.AddCoin(enemy.EnemyObject.Coins);
                SoundManager.Instance.PlayAudio(SoundManager.SoundType.MONSTER_KILLED);
                break;
            case IDestructable.DestroyedSource.SAVED:
                BasicStats.CurrentHp -= 1;
                break;
        }

        OnStatsChanged?.Invoke();
    }
}
