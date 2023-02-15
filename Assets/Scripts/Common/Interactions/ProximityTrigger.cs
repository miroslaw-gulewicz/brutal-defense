using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProximityTrigger : MonoBehaviour
{
    ProximityTriggerBehaviour _proximityTriggerBehaviour;

    [SerializeField]
    protected bool _cancelEffectOnExit;

    public bool CancelEffectOnExit { set => _cancelEffectOnExit = value; }

    public float EffectRadius { set => _sphereCollider.radius = value; }

    [SerializeField]
    protected SphereCollider _sphereCollider;


    protected virtual void Awake()
    {

        _proximityTriggerBehaviour = GetComponentInChildren<ProximityTriggerBehaviour>();

        _proximityTriggerBehaviour.TrigerEnterCallback = TriggerEnter;
        _proximityTriggerBehaviour.TrigerExitCallback = TriggerExit;
    }

    protected abstract void TriggerEnter(Collider other);

    protected abstract void TriggerExit(Collider other);
}
