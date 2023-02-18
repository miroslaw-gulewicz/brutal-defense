using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class DisabledTriggerBehaviuour : MonoBehaviour
{
    List<IDisabledTrigger> triggeredObjects = new List<IDisabledTrigger>();

    Collider Collider;

    public static void TriggerEntered(IDisabledTrigger disabledTrigger, Collider collider)
    {
        DisabledTriggerBehaviuour comp;
        if (collider.TryGetComponent<DisabledTriggerBehaviuour>(out comp))
        {
            if (!comp.triggeredObjects.Contains(disabledTrigger))
                comp.triggeredObjects.Add(disabledTrigger);
        }
    }

    private void Awake()
    {
        Collider = GetComponent<Collider>();
    }
    private void OnDisable()
    {
        NotifyColliders();
    }

    private void OnDestroy()
    {
        NotifyColliders();
    }

    private void NotifyColliders()
    {
        triggeredObjects.ForEach(t => t.OnObjectInRangeDisabled(Collider));
        triggeredObjects.Clear();
    }

    public interface IDisabledTrigger
    {
        void OnObjectInRangeDisabled(Collider collider);
    }
}
