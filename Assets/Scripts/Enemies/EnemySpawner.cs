using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDestructable;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] public Transform _enemiesWorldParent;

	[SerializeField] public EnemyObject _enemyDef;

	DamageVisualizer _visualizer;

	[SerializeField] private bool initExisting;

	public void Awake()
	{
		_visualizer = FindObjectOfType<DamageVisualizer>();

		if (initExisting)
		{
			foreach (var enemy in FindObjectsOfType<Enemy>())
			{
				InitExistingEnemies(enemy);
			}
		}
	}

	private void InitExistingEnemies(Enemy enemy)
	{
		enemy.DamageVisualizer = _visualizer;
		enemy.Initialize();

		if (enemy.TryGetComponent(out Shooting shooting))
		{
			shooting.Weapon = enemy.EnemyObject.Weapon;
			shooting.Initialize();
		}
	}

	public Enemy SpawnEnemy(GameObject prefab, Vector3 position, EnemyObject enemyObject)
	{
		GameObject obj = ObjectCacheManager._Instance.GetObject(prefab, false);

		if (obj == null)
			obj = Instantiate(_enemyDef.Prefab.gameObject);
		obj.transform.SetParent(_enemiesWorldParent, false);
		obj.transform.localPosition = position;
		var enemy = obj.GetComponent<Enemy>();
		enemy.EnemyObject = enemyObject;
		InitExistingEnemies(enemy);

		obj.SetActive(true);
		return enemy;
	}

	[ContextMenu("SpawnEnemy")]
	public void Spawn()
	{
		SpawnEnemy(_enemyDef.Prefab.gameObject, Vector3.zero, _enemyDef);
	}


	[ContextMenu("Pause")]
	public void Pause()
	{
		Time.timeScale = 0f;
	}

	[ContextMenu("Resume")]
	public void Resume()
	{
		Time.timeScale = 1f;
	}
}