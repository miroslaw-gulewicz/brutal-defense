using PathCreation;
using PathCreation.Examples;
using System;
using UnityEngine;
using Utilities;

public class GameManager : MonoBehaviour
{
	[SerializeField] private WaveManager waveManager;

	[SerializeField] private EffectsManager effectsManager;

	[SerializeField] private EconomyManager economyManager;

	[SerializeField] private TurretSpawner _turretSpawner;

	[SerializeField] private LevelDefinition _levelDefinition;

	[SerializeField] private BasicStatsHolder basicStats;

	[SerializeField] RoadMeshCreator roadMeshCreator;

	public BasicStatsHolder BasicStats
	{
		get => basicStats;
	}

	public event Action OnStatsChanged;

	[SerializeField] [Range(0, 10)] private float gameSpeed;

	private void Awake()
	{
		waveManager.EnemyDestroyedEvent += EnemyDestroyedEvent;
		waveManager.WaveUnitChanged += OnWaveUnitChanged;
		waveManager.WaveCompleted += OnWaveCompleted;
		_turretSpawner.DestroyCallBack += TowerDestroyed;
	}

	private void OnWaveUnitChanged(WaveDefinition.WaveUnit obj)
	{
	}

	private void OnWaveCompleted()
	{
	}

	[ContextMenu("Setup Level")]
	private void Start()
	{
		SetGameSpeed();
		SetupLevel(_levelDefinition);
	}

	[ContextMenu("Set Game Speed")]
	public void SetGameSpeed()
	{
		Time.timeScale = gameSpeed;
	}

	private void TowerDestroyed(IDestructable.DestroyedSource arg1, TurretBehaviour arg2)
	{
		effectsManager.TowerExplosion(arg2.transform.position);
	}

	public void SetupLevel(LevelDefinition levelDefinition)
	{
		economyManager.Coins = levelDefinition.StartCoins;
		waveManager.WaveDefinition = levelDefinition.Waves[0];
		BezierPath bezierPath = new BezierPath(levelDefinition.WayPoints, false, PathSpace.xz);
		waveManager.Path = new VertexPath(bezierPath, transform);
		roadMeshCreator.CreateRoad(waveManager.Path);
		waveManager.Restart();
	}

	public void StartWave()
	{
		waveManager.AutoStart = true;
		waveManager.BegintWave();
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
				PlayerProgressMonitor.Instance.RegisterKill();
				break;
			case IDestructable.DestroyedSource.SAVED:
				BasicStats.CurrentHp -= 1;
				break;
		}

		OnStatsChanged?.Invoke();
	}

	[ContextMenu("SaveSystem")]
	public void Waypoints()
	{
		Vector3[] newWayPoints = new Vector3[waveManager.Waypoints.Length];
		int i = 0;
		foreach (var waypoint in waveManager.Waypoints)
		{
			newWayPoints[i++] = waypoint.Position;
		}

		_levelDefinition.WayPoints = newWayPoints;
		_levelDefinition.SetDirty();
	}
}