using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "InflictorDescriptor", menuName = "ScriptableObjects/InflictorDescriptor")]
public class DamageOverTimeInflictorDescriptor : ScriptableObject
{
    public string DamageType(DamageOverTimeInflictor inflictor)
    {
        return inflictor.EffectType.ToString();
    }

    public string EffectDescription(DamageOverTimeInflictor inflictor)
    {
        int damage = inflictor.damage;
        float time = inflictor.timeContextData.effectTime ;
        float interval = inflictor.timeContextData.interval;
        string dmgType = DamageType(inflictor);
        return $" {damage * 1/interval} {dmgType} damage/s for {time} seconds"; 
    }

    public class Description
    {
        public string name;
    }
}