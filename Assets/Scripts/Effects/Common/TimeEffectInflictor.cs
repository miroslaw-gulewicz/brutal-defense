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

    public override float UpdateInflictor(IDestructable destructable, IEffectContextHolder effectContextHolder)
    {
        TimeEffectContext context = GetTimeContextData(effectContextHolder);
        if(context == null)
        {
            Debug.LogError("No ContextData " + this.name);
            return 0;
        }

        if (context.effectTime > 0 && Time.time >= context._lastUpdateTime + context.interval)
        {
            context._lastUpdateTime = Time.time;
            DoEffect(destructable, effectContextHolder);
            context.effectTime -= context.interval;
        }


        if (context.effectTime > 0)
            return context._lastUpdateTime + context.interval;
        else return 0;
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

        [NonSerialized]
        public float _lastUpdateTime = 0;

        [SerializeField]
        public float effectTime = 6f;

        public TimeEffectContext(TimeEffectContext effectContext)
        {
            this.effectTime = effectContext.effectTime;
            this.interval = effectContext.interval;
            this._lastUpdateTime = -100f;
        }
    }
}