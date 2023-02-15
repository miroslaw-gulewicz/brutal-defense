using Effect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventTriggeredEffectInflictor", menuName = "ScriptableObjects/EventTriggeredEffectInflictor")]
public class EventTriggeredEffectInflictor : BaseEffectInflictor
{
    public override IEffectContextData Attachffect(IEffectContextHolder mono)
    {
        EventTriggerInflictorContext data = new EventTriggerInflictorContext();
        mono.PutContextData(this, data);
        return data;
    }

    public override string Description()
    {
        return "";
    }

    public override void StopEffect(IEffectContextHolder mono)
    {

    }

    public override bool UpdateInflictor(IDestructable destructable, IEffectContextHolder effectContextHolder)
    {
        return false;
    }

    public class EventTriggerInflictorContext : IEffectContextData
    {

    }
}