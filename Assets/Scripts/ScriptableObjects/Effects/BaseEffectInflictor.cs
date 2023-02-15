using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effect;

public abstract class BaseEffectInflictor : ScriptableObject, EffectInflictor
{
    [SerializeField]
    protected IAffected.EffectType _effectType;

    [SerializeField]
    protected EffectInflictorAgent _effectAgent;

    public  IAffected.EffectType EffectType => _effectType;
    
    public EffectInflictorAgent EffectAgent => _effectAgent;

    public EffectInflictor.InflictorSourceKey inflictorSourceKey => _key;

    private EffectInflictorKey _key;
    public abstract IEffectContextData Attachffect(IEffectContextHolder mono);

    public abstract void StopEffect(IEffectContextHolder mono);

    public abstract bool UpdateInflictor(IDestructable destructable, IEffectContextHolder effectContextHolder);

    public abstract string Description();

    private void OnEnable()
    {
        _key = new EffectInflictorKey(_effectType, name);
    }

    public class EffectInflictorKey : EffectInflictor.InflictorSourceKey
    {
        IAffected.EffectType EffectType;
        string inflictorName;

        public EffectInflictorKey(IAffected.EffectType effectType, string name)
        {
            EffectType = effectType;
            inflictorName = name;
        }

        string EffectInflictor.InflictorSourceKey.Name  => inflictorName; 

    }
}