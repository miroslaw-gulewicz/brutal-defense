using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
	public Transform target;
	public float smooth = 5.0f;
	void Update()
	{
		transform.position = Vector3.Lerp(transform.position, target.position, Time.unscaledDeltaTime * smooth);
	}

}
