using Effect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "ScriptableObjects/Projectile")]
public class Projectile : ScriptableObject
{
	[SerializeField] private short damage;

	[SerializeField] private GameObject impactEffect;

	[SerializeField] private IDestructable.DamageType damageType;

	[SerializeField] private IAffected.EffectType effect;

	[SerializeField] private Sprite sprite;

	[SerializeField] private BasicStatsHolder _stats;

	public short Damage
	{
		get => damage;
	}

	public GameObject ImpactEffectPrefab
	{
		get => impactEffect;
	}

	public IDestructable.DamageType DamageType
	{
		get => damageType;
	}

	public Sprite Sprite
	{
		get => sprite;
	}

	public BasicStatsHolder Stats
	{
		get => _stats;
		set => _stats = value;
	}
}