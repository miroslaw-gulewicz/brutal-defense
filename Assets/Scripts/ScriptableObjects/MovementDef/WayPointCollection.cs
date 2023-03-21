using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WayPointCollection", menuName = "ScriptableObjects/WayPointCollection")]
public class WayPointCollection : ScriptableObject
{
	[SerializeField] private Vector3[] position;

	public Vector3[] Position
	{
		get => position;
	}
}