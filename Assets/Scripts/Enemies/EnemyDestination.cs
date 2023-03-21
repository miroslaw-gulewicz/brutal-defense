using System;
using UnityEngine;
using static IDestructable;

public class EnemyDestination : MonoBehaviour
{
	public event Action<DestroyedSource, Enemy> OnEnemyReached;

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.TryGetComponent<Enemy>(out Enemy enemy))
			OnEnemyReached?.Invoke(DestroyedSource.SAVED, enemy);
	}
}