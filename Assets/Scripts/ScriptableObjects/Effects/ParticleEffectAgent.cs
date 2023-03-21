using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticleEffectAgent", menuName = "ScriptableObjects/ParticleEffectAgent")]
public class ParticleEffectAgent : EffectInflictorAgent
{
	[SerializeField] ParticleSystem m_ParticleSystem;

	public ParticleSystem ParticleSystem
	{
		get => m_ParticleSystem;
	}

	protected override GameObject CreateEffect(GameObject gameObject)
	{
		ParticleSystem particleSystem = Instantiate(m_ParticleSystem);

		return particleSystem.gameObject;
	}
}