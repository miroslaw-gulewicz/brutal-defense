using Effect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "TimingEffectInflictor", menuName = "ScriptableObjects/TimingEffectInflictor")]
public class TimingEffectInflictor : TimeEffectInflictor
{
    [SerializeField]
    BaseEffectInflictor inflictor;



    public override string Description()
    {
        return "";//inflictorDescriptor.EffectDescription(this);
    }

    public override void StopEffect(IEffectContextHolder mono)
    {
        inflictor.StopEffect(mono);
        mono.RemoveContextData(this);
    }

    protected override IEffectContextData AttachData(IEffectContextHolder mono, TimeEffectContext timeEffectContext)
    {
        TimingEffectStats timingEffectStats = new TimingEffectStats();
        timingEffectStats.EffectContextData = inflictor.Attachffect(mono);
        mono.PutContextData(this, timingEffectStats);
        base.Attachffect(mono);

        return timingEffectStats;
    }

    protected override void DoEffect(IDestructable destructable, IEffectContextHolder effectContextHolder)
    {
        inflictor.UpdateInflictor(destructable, effectContextHolder);
    }

    protected override TimeEffectContext GetTimeContextData(IEffectContextHolder effectContextHolder)
    {
        effectContextHolder.GetContextData(this, out IEffectContextData contextData);
        return ((TimingEffectStats)contextData).TimeEffect;
    }

    protected override void SaveTimeContextData(IEffectContextHolder effectContextHolder, TimeEffectContext timeEffectContext)
    {
        effectContextHolder.GetContextData(this, out IEffectContextData contextData);
        ((TimingEffectStats)contextData).TimeEffect = timeEffectContext;
    }

    public class TimingEffectStats : IEffectContextData
    {
        IEffectContextData effectContextData;

        TimeEffectContext timeEffect;

        public TimeEffectContext TimeEffect { get => timeEffect; set => timeEffect = value; }
        public IEffectContextData EffectContextData { get => effectContextData; set => effectContextData = value; }
    }
}