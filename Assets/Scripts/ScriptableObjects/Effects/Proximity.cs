using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Proximity", menuName = "ScriptableObjects/Effect/Proximity")]
public class Proximity : EffectInflictorAgent
{
    [SerializeField] protected ProximityEffectInflictor _proximityAffectorPrefab;

    [SerializeField] BaseEffectInflictor[] _inflictors;

    [SerializeField] bool cancelEffectOnExit = false;

    [SerializeField] float proximity;
    protected override GameObject CreateEffect(GameObject target)
    {
        IProximityInflictor proximityAffector = Instantiate(_proximityAffectorPrefab, target.transform);

        proximityAffector.transform.SetParent(target.transform, false);

        proximityAffector.Inflictors = _inflictors;
        proximityAffector.CancelEffectOnExit = cancelEffectOnExit;  
        proximityAffector.EffectRadius = proximity;

        return proximityAffector.gameObject;
    }
}