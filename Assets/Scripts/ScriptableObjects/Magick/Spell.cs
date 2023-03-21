using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "ScriptableObjects/Magick/Spell")]
public class Spell : ScriptableObject
{
	[SerializeField] private string _spellName;

	[SerializeField] private EffectInflictorAgent _effectAgent;

	[SerializeField] private BaseEffectInflictor[] _inflictors;

	[SerializeField] private Sprite _sprite;

	[Range(1, 360)] [SerializeField] private float _cooldown;

	[SerializeField] private GameObject _gizmoPrefab;

	[SerializeField] LayerMask targetsAgentLayer;

	public string SpellName
	{
		get => _spellName;
	}

	public EffectInflictorAgent EffectAgent
	{
		get => _effectAgent;
	}

	public Sprite Sprite
	{
		get => _sprite;
	}

	public float Cooldown
	{
		get => _cooldown;
	}

	public GameObject GizmoPrefab
	{
		get => _gizmoPrefab;
	}

	public LayerMask TargetsAgentLayer
	{
		get => targetsAgentLayer;
	}

	public BaseEffectInflictor[] Inflictors
	{
		get => _inflictors;
	}
}