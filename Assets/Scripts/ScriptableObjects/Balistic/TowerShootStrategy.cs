using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TowerShootStrategy", menuName = "ScriptableObjects/TowerShootStrategy")]
public class TowerShootStrategy : ScriptableObject
{
    public virtual void Setup(IAimable aimable)
    {
        
    }

    public virtual void TargetEnters(IAimable aimable, GameObject gameObject)
    {
        if (!aimable.Target)
            aimable.Target = gameObject;
    }

    internal virtual void TargetExits(IAimable aimable, GameObject gameObject)
    {
        if (GameObject.ReferenceEquals(aimable.Target, gameObject))
            aimable.Target = null;
    }

    internal virtual GameObject ObtainTarget(IAimable turretBehaviour)
    {
        return turretBehaviour.Target;
    }
}