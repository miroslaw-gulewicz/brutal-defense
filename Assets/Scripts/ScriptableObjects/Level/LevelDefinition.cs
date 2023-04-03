using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDefinition", menuName = "ScriptableObjects/LevelDefinition")]
public class LevelDefinition : ScriptableObject
{
	[SerializeField] private string levelName;

	[SerializeField] private WaveDefinition[] waves;

	[SerializeField] private Sprite levelCover;

	[SerializeField] private int _startCoins;

	[SerializeField] private Vector3[] _wayPoints;

	[SerializeField] private Vector3[] _turretPlacements;

	[SerializeField] private short _playerMaxLives;

	public string LevelName
	{
		get => levelName;
	}

	public WaveDefinition[] Waves
	{
		get => waves;
	}

	public Sprite LevelCover
	{
		get => levelCover;
	}

	public int StartCoins
	{
		get => _startCoins;
	}

	public Vector3[] WayPoints
	{
		get => _wayPoints;
		set => _wayPoints = value;
	}
	
	public Vector3[] TurretPlacements
	{
		get => _turretPlacements;
		set => _turretPlacements = value;
	}
	public short PlayerMaxLives => _playerMaxLives;
}