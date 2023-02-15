using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDestructable;

public class WaveManager : MonoBehaviour, IHighlitableObjectHolder
{
    [SerializeField]
    private bool autoStart;

    [SerializeField]
    private WaveDefinition waveDefinition;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private EnemyDestination enemyDestination;

    [SerializeField]
    public LayerMask layer;

    public int Layer => layer.value;

    public event Action OnEnemyCountChanged;

    private event Action WaveCompleted;

    public event Action OnWaveBegin;

    [SerializeField]
    private WayPoint[] waypoints;

    public event Action<DestroyedSource, Enemy> EnemyDestroyedEvent;

    private List<Enemy> _spawnedEnemies = new List<Enemy>();

    public WaveDefinition WaveDefinition { get => waveDefinition; set => waveDefinition = value; }
    public EnemySpawner EnemySpawner { get => enemySpawner; set => enemySpawner = value; }

    public int CurrentEnemies { get => _spawnedEnemies.Count; }

    public int EnemiesRemains { get => waveDefinition.EnemiesCount - unitsReleased; }

    int unitIndex;
    int unitsReleased;


    Dictionary<DestroyedSource, int> EnemiesDeathsStat = new Dictionary<DestroyedSource, int>();


    public void Start()
    {
        EnemySpawner.SpawnCompleted += OnSpawningComplete;
        EnemySpawner.OnEnemySpawned += EnemySpawner_OnEnemySpawned;
        enemyDestination.OnEnemyReached += EnemyDestroyed;

        EnemiesDeathsStat.Add(DestroyedSource.KILLED, 0);
        EnemiesDeathsStat.Add(DestroyedSource.SAVED, 0);

        _spawnedEnemies.AddRange(FindObjectsOfType<Enemy>());

        if(autoStart)
         BegintWave();
    }

    private void EnemySpawner_OnEnemySpawned(Enemy enemy)
    {
        Movement movable = enemy.GetComponent<Movement>();
        movable.Waypoints = waypoints;
        movable.Initialize();

        enemy.DestroyCallBack = EnemyDestroyed;
        enemy.name =   "Wave Enemy " + unitsReleased.ToString();

        _spawnedEnemies.Add(enemy);
        unitsReleased++;
        OnEnemyCountChanged?.Invoke();

    }

    private void OnDestroy()
    {
        EnemySpawner.SpawnCompleted -= OnSpawningComplete;
        EnemySpawner.OnEnemySpawned -= EnemySpawner_OnEnemySpawned;
        enemyDestination.OnEnemyReached -= EnemyDestroyed;
    }

    [ContextMenu("Begin Wave")]
    public void BegintWave()
    {
        OnWaveBegin.Invoke();
        NextWave();
    }

    
    private void NextWave()
    {
        SetNextWaveDef();
        StartWave();

        unitIndex++;
    }

    internal List<EnemyObject> WaveEnemiesDefinition()
    {
        return waveDefinition.WaveUnits.Select(x => x.EnemyDef).Distinct().ToList();
    }

    private void OnSpawningComplete()
    {
        if(unitIndex >= WaveDefinition.WaveUnits.Length)
        {
            
            // end;
            return;
        }

        NextWave();
    }

    private void StartWave()
    {
        EnemySpawner.StartSpawn();
    }

    private void SetNextWaveDef()
    {
        WaveDefinition.WaveUnit waveUnit = WaveDefinition.WaveUnits[unitIndex % WaveDefinition.WaveUnits.Length];
        EnemySpawner.SpawnCount = waveUnit.Quantity;
        EnemySpawner._enemyDef = waveUnit.EnemyDef;
        EnemySpawner.SpawnInterval = waveUnit.SpawnIntervalSeconds;
    }

    public void Pause()
    {

    }

    public void Reset()
    {
        unitIndex = 0;
        unitsReleased = 0;
    }

    private void EnemyDestroyed(DestroyedSource source, Enemy enemy)
    {
        EnemyDestroyedEvent?.Invoke(source, enemy);
        _spawnedEnemies.Remove(enemy);
        EnemiesDeathsStat[source]++;
        OnEnemyCountChanged?.Invoke();
        enemy.gameObject.SetActive(false);
    }

    public void ForEachObject(Action<IHighlightable> cmd)
    {
        _spawnedEnemies.ForEach(cmd);
    }

    
}
