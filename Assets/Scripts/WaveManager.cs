using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDestructable;
using static WaveDefinition;
using PathCreation;

public class WaveManager : MonoBehaviour, IHighlitableObjectHolder
{
	[SerializeField] private bool autoStart;

	[SerializeField] private WaveDefinition waveDefinition;

	[SerializeField] private EnemySpawner enemySpawner;

	private float lastSpawned;

	private int spawnCount;

	public event Action<WaveUnit> WaveUnitChanged;

	public event Action<Enemy> OnWaveEnemySpawned;

	Coroutine spawningCoroutine;

	[SerializeField] private float spawnInterval;

	[SerializeField] private EnemyDestination enemyDestination;

	[SerializeField] public LayerMask layer;

	public int Layer => layer.value;

	public event Action OnEnemyCountChanged;

	public event Action WaveCompleted;

	public event Action OnWaveBegin;

	public event Action<WaveDefinition> OnWaveChanged;

	[SerializeField] private bool _randomUnits;

	[SerializeField] private WayPoint[] waypoints;

	public event Action<StatusSource, Enemy> EnemyDestroyedEvent;

	private List<Enemy> _spawnedEnemies = new List<Enemy>();

	public WaveDefinition WaveDefinition
	{
		get => waveDefinition;
		set
		{
			waveDefinition = value;
			OnWaveChanged?.Invoke(value);
		}
	}

	public EnemySpawner EnemySpawner
	{
		get => enemySpawner;
		set => enemySpawner = value;
	}

	public int CurrentEnemies
	{
		get => _spawnedEnemies.Count;
	}

	public int EnemiesRemains
	{
		get => waveDefinition.EnemiesCount - unitsReleased;
	}

	public int SpawnCount
	{
		get => spawnCount;
		set => spawnCount = value;
	}

	public float SpawnInterval
	{
		set => spawnInterval = value;
	}

	public bool AutoStart
	{
		get => autoStart;
		set => autoStart = value;
	}

	public WayPoint[] Waypoints
	{
		get => waypoints;
		set { waypoints = value; }
	}

	public VertexPath Path { get; internal set; }
	public WaveDefinition[] Waves { get; internal set; }

	[SerializeField] private int unitIndex;


	private int unitsReleased;

	Dictionary<StatusSource, int> EnemiesDeathsStat = new Dictionary<StatusSource, int>();

	private WaveDefinition.WaveUnit _currentWaveUnit;

	private void Awake()
	{
		enemyDestination.OnEnemyReached += EnemyStatusChanged;

		EnemiesDeathsStat.Add(StatusSource.KILLED, 0);
		EnemiesDeathsStat.Add(StatusSource.SAVED, 0);

		_spawnedEnemies.AddRange(FindObjectsOfType<Enemy>());
	}


	public IEnumerator SpawnEnemyCoorutine()
	{
		while (spawnCount > 0)
		{
			SpawnWaveEnemy();
			spawnCount--;
			yield return new WaitForSeconds(spawnInterval);
		}

		if(_currentWaveUnit.ContinueSpawning)
		{
			NextWave();
		}

	}

	[ContextMenu("Spawn")]
	public void SpawnWaveEnemy()
	{
		var enemy = enemySpawner.SpawnEnemy(EnemySpawner._enemyDef.Prefab.gameObject, Vector3.zero,
			EnemySpawner._enemyDef);
		EnemySpawned(enemy);
		lastSpawned = Time.fixedTime;
	}

	private void EnemySpawned(Enemy enemy)
	{
		Movement movable = enemy.GetComponent<Movement>();
		movable.Path = Path;
		movable.Distance = 0;
		movable.Destination = enemyDestination.transform.position;
		movable.Initialize();

		enemy.DestroyCallBack = EnemyStatusChanged;
		enemy.name = "Wave Enemy " + unitsReleased.ToString();

		_spawnedEnemies.Add(enemy);
		unitsReleased++;
		OnEnemyCountChanged?.Invoke();
		OnWaveEnemySpawned?.Invoke(enemy);
	}

	private void OnDestroy()
	{
		enemyDestination.OnEnemyReached -= EnemyStatusChanged;
	}

	[ContextMenu("Begin Wave")]
	public void BegintWave()
	{
		if (unitIndex == WaveDefinition.WaveUnits.Length)
		{
			WaveCompleted?.Invoke();
			return;
		}

		OnWaveBegin.Invoke();
		NextWave();
	}


	private void NextWave()
	{
		SetNextWaveDef();
		spawningCoroutine = StartCoroutine(SpawnEnemyCoorutine());

		unitIndex++;
	}

	internal List<EnemyObject> WaveEnemiesDefinition()
	{
		return waveDefinition.WaveUnits.Select(x => x.EnemyDef).Distinct().ToList();
	}

	public void SetNextWaveDef()
	{
		_currentWaveUnit = WaveDefinition.WaveUnits[WaveUnit()];
		spawnCount = _currentWaveUnit.Quantity;
		spawnInterval = _currentWaveUnit.SpawnIntervalSeconds;
		EnemySpawner._enemyDef = _currentWaveUnit.EnemyDef;
		WaveUnitChanged?.Invoke(_currentWaveUnit);
	}

	private int WaveUnit()
	{
		if (_randomUnits)
			return UnityEngine.Random.Range(0, WaveDefinition.WaveUnits.Length);
		else
			return unitIndex % WaveDefinition.WaveUnits.Length;
	}

	[ContextMenu("Restart")]
	public void Restart()
	{
		unitIndex = 0;
		unitsReleased = 0;

		_spawnedEnemies.ForEach(enemy => { enemy.gameObject.SetActive(false); });
		_spawnedEnemies.Clear();
		OnEnemyCountChanged?.Invoke();
	}

	private void EnemyStatusChanged(StatusSource source, Enemy enemy)
	{
		if (StatusSource.SPAWNED == source)
			_spawnedEnemies.Add(enemy);
		else
			StartCoroutine(InactivateAfter(source, enemy));
	}

	private IEnumerator InactivateAfter(StatusSource source, Enemy enemy)
	{
		IPreDestroyListener[] preDestroyListeners = enemy.GetComponentsInChildren<IPreDestroyListener>();
		for (int i = 0; i < preDestroyListeners.Length; i++)
		{
			preDestroyListeners[i].OnPreDestroy();
		}

		yield return new WaitForEndOfFrame();
		yield return new WaitForEndOfFrame();

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