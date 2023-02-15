using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effect;

[CreateAssetMenu(fileName = "StatsModifierInflictor", menuName = "ScriptableObjects/Inflictors/StatsModifierInflictor")]
public class StatsModifierInflictor : TimeEffectInflictor
{
    [SerializeField]
    StatModInflictorDescriptor descriptor;

    [SerializeField]
    public StatModifierContext statModifierContext;


    public override void StopEffect(IEffectContextHolder mono)
    {
        mono.GetContextData(this, out IEffectContextData context);
        if (context == null) return;
        mono.RemoveContextData(this);
        StatModifierContext statModContext = (StatModifierContext)context;
        mono.BasicStats[(int)statModifierContext.Stat].Value += statModContext.RestoreValue;
        Debug.Log("Restored " + statModContext.Stat + " " + statModContext.RestoreValue + " to " + mono.BasicStats[(int)statModifierContext.Stat].value);

    }

    protected override void DoEffect(IDestructable destructable, IEffectContextHolder effectContextHolder)
    {
        //na
    }

    protected override TimeEffectContext GetTimeContextData(IEffectContextHolder effectContextHolder)
    {
        effectContextHolder.GetContextData(this, out IEffectContextData contextData);
        return ((StatModifierContext)contextData).TimeEffect;
    }

    protected override void SaveTimeContextData(IEffectContextHolder mono, TimeEffectContext timeEffectContext)
    {
        IEffectContextData contextData;
        mono.GetContextData(this, out contextData);
        ((StatModifierContext)contextData).TimeEffect = timeEffectContext;
    }

    public override string Description()
    {
       return descriptor.EffectDescription(this);
    }

    protected override IEffectContextData AttachData(IEffectContextHolder mono, TimeEffectContext timeEffectContext)
    {
        BasicStatsHolder.Stat stat = mono.BasicStats[(int)statModifierContext.Stat];
        if (stat == null) return null;
        float statValue = mono.BasicStats[statModifierContext.Stat];
        float decreaseValue = statValue * (statModifierContext.PercentDecrease / 100f);

        var statModContext = new StatModifierContext()
        {
            Stat = statModifierContext.Stat,
            RestoreValue = decreaseValue,
            TimeEffect = timeEffectContext,
        };

        mono.BasicStats[(int)statModifierContext.Stat].Value -= statModContext.RestoreValue;
        mono.PutContextData(this, statModContext);

        Debug.Log("Affected " + statModContext.Stat + " " + statModContext.RestoreValue + " to " + mono.BasicStats[(int)statModifierContext.Stat].Value);

        return statModContext;
    }

    [Serializable]
    public class StatModifierContext : IEffectContextData
    {
        [SerializeField]
        StatEnum stat;

        [SerializeField]
        [Range(-100f, 100f)]
        float percentDecrease;

        float restoreValue;

        TimeEffectContext timeEffect;

        public StatEnum Stat { get => stat; set => stat = value; }
        public float PercentDecrease { get => percentDecrease; set => percentDecrease = value; }
        public float RestoreValue { get => restoreValue; set => restoreValue = value; }
        public TimeEffectContext TimeEffect { get => timeEffect; set => timeEffect = value; }
    }
}