using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SelectableBehaviour : MonoBehaviour
{
	[SerializeField] public UnityEvent<bool> OnSelect;

	private void OnEnable()
	{
		OnSelect?.Invoke(true);
	}

	private void OnDisable()
	{
		OnSelect?.Invoke(false);
	}
}