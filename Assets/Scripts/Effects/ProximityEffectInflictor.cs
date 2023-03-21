using Effect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityEffectInflictor : IProximityInflictor
{
	protected override void TriggerEnter(Collider other)
	{
		if (other.TryGetComponent(out IAffected affector))
		{
			affector.ApplyEffect(_inflictors);
			Debug.Log("Object Affected " + other);
		}
	}

	protected override void TriggerExit(Collider other)
	{
		if (!_cancelEffectOnExit) return;

		if (other.TryGetComponent(out IAffected affector))
		{
			affector.CancelEffect(_inflictors);
			Debug.Log("Object effect exited " + other);
		}
	}
}