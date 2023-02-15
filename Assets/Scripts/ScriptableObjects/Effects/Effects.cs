using Effect;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effects", menuName = "ScriptableObjects/Effects")]
public class Effects : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    EffectModifier[] effects;

    Dictionary<IAffected.EffectType, EffectInflictorAgent> effectAgents;


    public EffectInflictorAgent this[IAffected.EffectType idx]
    {

        // using get accessor
        get
        {
            return effectAgents[idx];
        }
    }

    public void OnAfterDeserialize()
    {
        effectAgents = effects.ToDictionary(keySelector: ef => ef.effectType, elementSelector: ef => ef.agent);
    }

    public void OnBeforeSerialize()
    {
    }

    [Serializable]
    public class EffectModifier
    {
        [SerializeField]
        internal IAffected.EffectType effectType;

        [SerializeField]
        internal EffectInflictorAgent agent;
    }
}