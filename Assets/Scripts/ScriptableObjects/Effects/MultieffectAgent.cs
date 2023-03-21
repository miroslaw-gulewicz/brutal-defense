using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MultieffectAgent", menuName = "ScriptableObjects/MultieffectAgent")]
public class MultieffectAgent : EffectInflictorAgent
{
	[SerializeField] private GameObject _agentPrefab;

	[SerializeField] EffectInflictorAgent[] effectAgents;

	protected override GameObject CreateEffect(GameObject mono)
	{
		GameObject empty;

		if (_agentPrefab)
		{
			empty = Instantiate(_agentPrefab);
		}
		else
		{
			empty = new GameObject();
		}

		empty.name = this.name + "Agent Effect Root";

		for (int i = 0; i < effectAgents.Length; i++)
		{
			effectAgents[i].ApplyEffect(empty);
		}

		return empty;
	}
}