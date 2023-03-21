using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretObjectDef", menuName = "ScriptableObjects/TurretObjectDef")]
public class TurretObjectDef : Weapon
{
	[Header("Turret")] [SerializeField] private int cost;

	[SerializeField] private short hp;

	[SerializeField] private short buildTime;

	[SerializeField] private Sprite sprite;

	[SerializeField] private string towerName;

	[SerializeField] private ResistanceHolder resistance;

	[SerializeField] private BasicStatsHolder basicStats;

	public short BuildTime
	{
		get => buildTime;
	}

	public short Hp
	{
		get => hp;
	}

	public Sprite Sprite
	{
		get => sprite;
		set => sprite = value;
	}

	public int Cost
	{
		get => cost;
		set => cost = value;
	}

	public string TowerName
	{
		get => towerName;
	}

	public ResistanceHolder Resistance
	{
		get => resistance;
	}

	public BasicStatsHolder BasicStats
	{
		get => basicStats;
	}
}