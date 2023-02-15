using Effect;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class TimeEffectInflictor : BaseEffectInflictor
{
    [SerializeField]
    public TimeEffectContext timeContextData;


    protected abstract TimeEffectContext GetTimeContextData(IEffectContextHolder effectContextHolder);
    protected abstract void SaveTimeContextData(IEffectContextHolder mono, TimeEffectContext timeEffectContext);

    public override bool UpdateInflictor(IDestructable destructable, IEffectContextHolder effectContextHolder)
    {
        TimeEffectContext context = GetTimeContextData(effectContextHolder);
        if(context == null)
        {
            Debug.LogError("No ContextData");
            return false;
        }

        if (context.effectTime > 0 && Time.time >= context.lastInflictTime + context.interval)
        {
            context.lastInflictTime = Time.time;
            DoEffect(destructable, effectContextHolder);
        }

        context.effectTime -= Time.deltaTime;
        return context.effectTime > 0;
    }

     

    protected abstract void DoEffect(IDestructable destructable, IEffectContextHolder effectContextHolder);

    public override IEffectContextData Attachffect(IEffectContextHolder mono)
    {        
        return AttachData(mono, new TimeEffectContext(timeContextData));
    }

    protected abstract IEffectContextData AttachData(IEffectContextHolder mono, TimeEffectContext timeEffectContext);


    [Serializable]
    public class TimeEffectContext : IEffectContextData
    {
        [SerializeField]
        public float interval = 0.5f;

        public float lastInflictTime = 0;

        [SerializeField]
        public float effectTime = 6f;

        public TimeEffectContext(TimeEffectContext effectContext)
        {
            this.effectTime = effectContext.effectTime;
            this.interval = effectContext.interval;
            this.lastInflictTime = effectContext.lastInflictTime;
        }
    }
}