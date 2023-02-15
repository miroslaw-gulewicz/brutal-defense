using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatModInflictorDescriptor", menuName = "ScriptableObjects/StatModInflictorDescriptor")]
public class StatModInflictorDescriptor : ScriptableObject
{
    public string DamageType(StatsModifierInflictor inflictor)
    {
        return inflictor.EffectType.ToString();
    }

    public string EffectDescription(StatsModifierInflictor inflictor)
    {
        StatEnum stat = inflictor.statModifierContext.Stat;
        float percentDecrease = inflictor.statModifierContext.PercentDecrease;
        float time = inflictor.timeContextData.effectTime;
        return $" - {percentDecrease} % {stat} for {time} seconds";
    }
}