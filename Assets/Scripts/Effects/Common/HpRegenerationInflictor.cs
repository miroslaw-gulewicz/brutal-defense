using Effect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HpRegenerationInflictor", menuName = "ScriptableObjects/Inflictors/HpRegenerationInflictor")]
public class HpRegenerationInflictor : BaseEffectInflictor
{
    [SerializeField]
    public short heal = 1;

    public override IEffectContextData Attachffect(IEffectContextHolder mono)
    {
        return null;
    }

    public override string Description()
    {
        return "";
    }

    public override void StopEffect(IEffectContextHolder mono)
    {

    }

    public override float UpdateInflictor(IDestructable destructable, IEffectContextHolder effectContextHolder)
    {
        destructable.TakeDamage(IDestructable.DamageType.HEAL, (short)-heal);
        return 0;
    }
}