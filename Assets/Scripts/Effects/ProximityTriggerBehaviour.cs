using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DisabledTriggerBehaviuour;

[RequireComponent(typeof(Collider))]
public class ProximityTriggerBehaviour : MonoBehaviour, IDisabledTrigger, IPreDestroyListener
{
	public Action<Collider> TrigerExitCallback { get; set; }
	public Action<Collider> TrigerEnterCallback { get; set; }
	public Action<Collider> TrigerStayCallback { get; set; }

	[SerializeField] public SphereCollider _collider;

	public float Range
	{
		set => _collider.radius = value;
	}

	public void OnObjectInRangeDisabled(Collider collider)
	{
		TrigerExitCallback(collider);
	}

	private void OnTriggerExit(Collider collision)
	{
		DisabledTriggerBehaviuour.TriggerExited(this, collision);
		TrigerExitCallback?.Invoke(collision);
	}

	private void OnTriggerStay(Collider collision)
	{
		TrigerStayCallback?.Invoke(collision);
	}

	private void OnTriggerEnter(Collider other)
	{
		DisabledTriggerBehaviuour.TriggerEntered(this, other);
		TrigerEnterCallback?.Invoke(other);
	}

	public void OnPreDestroy()
	{
		_collider.radius = 0;
	}
}