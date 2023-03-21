using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WaveDefinition", menuName = "ScriptableObjects/WaveDefinition")]
public class WaveDefinition : ScriptableObject, ISerializationCallbackReceiver
{
	[SerializeField] public int maxEscapedEnemies;

	[SerializeField] private WaveUnit[] waveUnits;

	[SerializeField] private WayPointCollection _wayPoints;

	private int enemiesCount;

	public WaveUnit[] WaveUnits
	{
		get => waveUnits;
	}

	public int EnemiesCount
	{
		get => enemiesCount;
		set => enemiesCount = value;
	}

	public void OnAfterDeserialize()
	{
		enemiesCount = waveUnits.Sum(e => e.Quantity);
	}

	public void OnBeforeSerialize()
	{
	}

	[Serializable]
	public class WaveUnit
	{
		[SerializeField] EnemyObject enemyDef;

		[SerializeField] int quantity;

		[SerializeField] private float _spawnInterval;

		[SerializeField] bool continueSpawning;

		public int Quantity
		{
			get => quantity;
		}

		public EnemyObject EnemyDef
		{
			get => enemyDef;
		}

		public float SpawnIntervalSeconds
		{
			get => _spawnInterval;
		}

		public bool ContinueSpawning
		{
			get => continueSpawning;
		}
	}
}