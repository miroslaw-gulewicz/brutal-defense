using Effect;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageInflictor", menuName = "ScriptableObjects/Inflictors/DamageInflictor")]

public class DamageInflictor : BaseEffectInflictor
{

    [SerializeField]
    public IDestructable.DamageType damageType;


    [SerializeField]
    public short damage;

    public override IEffectContextData Attachffect(IEffectContextHolder mono)
    {
        return null;
    }

    public override string Description()
    {
        return damage + " " + damageType + " damage ";  
    }

    public override void StopEffect(IEffectContextHolder mono)
    {

    }

    public override float UpdateInflictor(IDestructable destructable, IEffectContextHolder effectContextHolder)
    {
        destructable.TakeDamage(damageType, damage);
        return 0;
    }
}
