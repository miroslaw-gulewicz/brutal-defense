using Effect;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EffectAgentsLevelsCollection", menuName = "ScriptableObjects/EffectAgentsLevelsCollection")]
public class EffectAgentsLevelsCollection : ScriptableObject, ISerializationCallbackReceiver
{
    [SerializeField]
    TypesAgentLevels[] typesAgentLevels;

    Dictionary<IAffected.EffectType, Dictionary<int, EffectInflictorAgent>> dictionary;

    public void OnAfterDeserialize()
    {
        dictionary = typesAgentLevels.ToDictionary(keySelector: e => e.effectType, elementSelector: e => e.agentLevels.ToDictionary(i => i.level, i => i.agent));
    }

    public void OnBeforeSerialize()
    {

    }

    public EffectInflictorAgent GetEfectAgent(IAffected.EffectType effectType, int level)
    {
        return dictionary[effectType][level+1];
    }

    [Serializable]
    public class TypesAgentLevels
    {
        [SerializeField]
        internal IAffected.EffectType effectType;

        [SerializeField]
        internal AgentLevels[] agentLevels;
    }

    [Serializable]
    public class AgentLevels
    {
        [SerializeField]
        internal int level;
        [SerializeField]
        internal EffectInflictorAgent agent;
    }
}