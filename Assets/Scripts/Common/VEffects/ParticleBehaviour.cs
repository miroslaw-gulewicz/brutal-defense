using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehaviour : MonoBehaviour
{
	[SerializeField] private float disableAfter = 0.5f;
	ParticleSystem ps;

	void Awake()
	{
		ps = GetComponent<ParticleSystem>();
	}

	private IEnumerator DisableAfter(float v)
	{
		yield return new WaitForSeconds(v);
		gameObject.SetActive(false);
	}

	private void OnEnable()
	{
		if (disableAfter > 0)
			StartCoroutine(DisableAfter(disableAfter));
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}
}