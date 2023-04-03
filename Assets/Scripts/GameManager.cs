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

	[SerializeField] private RoadMeshCreator roadMeshCreator;
	public BasicStatsHolder BasicStats => basicStats;

	public event Action OnStatsChanged;

	[SerializeField] [Range(0, 10)] private float gameSpeed;

	private void Awake()
	{
		waveManager.EnemyDestroyedEvent += EnemyDestroyedEvent;
		waveManager.WaveUnitChanged += OnWaveUnitChanged;
		_turretSpawner.DestroyCallBack += TowerDestroyed;
	}

	private void OnWaveUnitChanged(WaveDefinition.WaveUnit obj)
	{

	}

	[ContextMenu("Setup Level")]
	private void Start()
	{
		RestoreGameSpeed();
		SetupLevel(_levelDefinition);
	}

	[ContextMenu("Set Game Speed")]
	public void RestoreGameSpeed()
	{
		Time.timeScale = gameSpeed;
	}

	public void SetGameSpeed(float gameSpeed)
	{
		Time.timeScale = gameSpeed;
	}

	private void TowerDestroyed(IDestructable.StatusSource arg1, TurretBehaviour arg2)
	{
		effectsManager.TowerExplosion(arg2.transform.position);
	}

	public void SetupLevel(LevelDefinition levelDefinition)
	{
		economyManager.Coins = levelDefinition.StartCoins;
		waveManager.WaveDefinition = levelDefinition.Waves[0];
		BezierPath bezierPath = new BezierPath(levelDefinition.WayPoints, false, PathSpace.xz);
		waveManager.Path = new VertexPath(bezierPath, transform);
		roadMeshCreator.textureTiling = waveManager.Path.length;
		roadMeshCreator.CreateRoad(waveManager.Path);
		_turretSpawner.SpawnBuildingPlaces(levelDefinition.TurretPlacements);
		basicStats.CurrentHp = levelDefinition.PlayerMaxLives;
		basicStats.StartHP = levelDefinition.PlayerMaxLives;
		waveManager.Restart();
	}

	public void RestartLevel()
	{
		waveManager.Restart();
		economyManager.Coins = _levelDefinition.StartCoins;
		_turretSpawner.ClearTurrets();
		basicStats.CurrentHp = _levelDefinition.PlayerMaxLives;
		OnStatsChanged?.Invoke();
	}

	public void StartWave()
	{
		waveManager.AutoStart = true;
		waveManager.BegintWave();
	}

	private void EnemyDestroyedEvent(IDestructable.StatusSource source, Enemy enemy)
	{
		switch (source)
		{
			case IDestructable.StatusSource.KILLED:
				effectsManager.SpawnCoin(enemy.gameObject.transform.position);
				effectsManager.BonesEffect(enemy.gameObject.transform.position, 5);
				economyManager.AddCoin(enemy.EnemyObject.Coins);
				SoundManager.Instance.PlayAudio(SoundManager.SoundType.MONSTER_KILLED);
				PlayerProgressMonitor.Instance.RegisterKill();
				break;
			case IDestructable.StatusSource.SAVED:
				BasicStats.CurrentHp -= 1;
				break;
		}

		OnStatsChanged?.Invoke();
	}

	[ContextMenu("SaveSystem")]
	public void SaveSystem()
	{
		_levelDefinition.WayPoints = Array.ConvertAll(waveManager.Waypoints, input => input.Position);
		_levelDefinition.TurretPlacements = Array.ConvertAll(FindObjectsOfType<BuildPlaceBehaviour>(), input => input.transform.position);
		_levelDefinition.SetDirty();
	}
}