using Effect;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SelfEffectInflictorAgent", menuName = "ScriptableObjects/Effect/SelfEffectInflictorAgent")]
public class SelfEffectInflictorAgent : EffectInflictorAgent, IEventListener
{
	[SerializeField] private SpawnEnemyObjectsCommand[] spawnEnemyObjectsCommands;

	public void RegisterCancelEvent(IEventListener.CancelEventHandler handler)
	{

	}

	public void UnRegisterCancelEvent(IEventListener.CancelEventHandler handler)
	{
		
	}

	protected override GameObject CreateEffect(GameObject target)
	{
		if (target.TryGetComponent(out IEffectEventSource eventSource))
			eventSource.RegisterEventCallback(this, OnEventReised);

		return null;
	}

	private void OnEventReised(IEffectContextHolder holder)
	{
		Array.ForEach(spawnEnemyObjectsCommands, (c) => c.DoCommand(holder));
	}
}