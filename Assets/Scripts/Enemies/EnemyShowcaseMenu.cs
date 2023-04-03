using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class EnemyShowcaseMenu : MonoBehaviour
{
	[SerializeField] private WaveManager _waveManager;

	[SerializeField] private WaveDefinition _waveDefinition;

	[SerializeField] private MovementStrategy _enemyMovementStrategy;

	[SerializeField] private WorldObjectSelectionManager _objectSelectionManager;

	[SerializeField] private EffectsManager effectsManager;

	void Start()
	{
		_waveManager.OnWaveEnemySpawned += OnEnemySpawned;
		_waveManager.EnemyDestroyedEvent += OnEnemyDestroyed;
		_objectSelectionManager.OnObjectSelected += OnEnemyBrutallyDoubleClicked;

		_waveManager.WaveDefinition = _waveDefinition;

		StartShowcase();
	}

	private void OnEnemyBrutallyDoubleClicked(GameObject obj)
	{
		if (obj.TryGetComponent(out Enemy enemy))
			enemy.DoDestroy(IDestructable.StatusSource.KILLED);
	}

	private void OnEnemySpawned(Enemy obj)
	{
		Movement movable = obj.GetComponent<Movement>();
		movable.MovementStrategy = _enemyMovementStrategy;

		Vector3 enemyPosition = new Vector3(-4, 0, -7);
		obj.transform.position = enemyPosition;
		movable.FaceDirection(0 < enemyPosition.x);
		movable.Initialize();
	}

	private void OnEnemyDestroyed(IDestructable.StatusSource source, Enemy enemy)
	{
		StartShowcase();
		if (source == IDestructable.StatusSource.KILLED)
		{
			PlayerProgressMonitor.Instance.RegisterKill();
			effectsManager.BonesEffect(enemy.transform.position, 5);
			effectsManager.SpawnCoin(enemy.transform.position);
		}
	}

	[ContextMenu("Stat Showcase")]
	private void StartShowcase()
	{
		_waveManager.SetNextWaveDef();
		_waveManager.SpawnWaveEnemy();
	}
}