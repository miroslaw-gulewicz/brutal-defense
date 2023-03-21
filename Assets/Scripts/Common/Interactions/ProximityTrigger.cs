using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProximityTrigger : MonoBehaviour
{
	[SerializeField] ProximityTriggerBehaviour _proximityTriggerBehaviour;

	[SerializeField] private RangeHighlight _rangeHighlight;

	[SerializeField] protected bool _cancelEffectOnExit;

	public bool CancelEffectOnExit
	{
		set => _cancelEffectOnExit = value;
	}

	[SerializeField] protected SphereCollider _sphereCollider;

	public float EffectRadius
	{
		set
		{
			_sphereCollider.radius = value;
			_rangeHighlight.RangeRadius = value;
		}
	}


	protected virtual void Awake()
	{
		_proximityTriggerBehaviour = GetComponentInChildren<ProximityTriggerBehaviour>();

		_proximityTriggerBehaviour.TrigerEnterCallback = TriggerEnter;
		_proximityTriggerBehaviour.TrigerExitCallback = TriggerExit;
	}

	protected abstract void TriggerEnter(Collider other);

	protected abstract void TriggerExit(Collider other);
}