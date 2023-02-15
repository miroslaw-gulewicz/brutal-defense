using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WayPointMovementStrategy;

[RequireComponent(typeof(Collider))]
public class WayPoint : MonoBehaviour, IWayPoint
{
    public Vector3 Position => transform.position;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IWayPointAgent wayPointAgent))
            wayPointAgent.WayPointReached(this);
    }
}
