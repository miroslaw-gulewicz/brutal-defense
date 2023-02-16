using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effect;
//[CreateAssetMenu(fileName = "BasicEffectAgentInflictor", menuName = "ScriptableObjects/Inflictor/BasicEffectAgentInflictor")]
public class BasicEffectAgentInflictor : BaseEffectInflictor
{
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
        return 0;
    }
}