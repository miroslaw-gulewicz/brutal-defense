using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static IDestructable;

public class EnemyDestination : MonoBehaviour
{
    public event Action<DestroyedSource, Enemy> OnEnemyReached;

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy;
        if(other.gameObject.TryGetComponent<Enemy>(out enemy))
        {
            OnEnemyReached?.Invoke(DestroyedSource.SAVED, enemy);
        }
    }
}
