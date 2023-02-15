using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "QueueShotStrategy", menuName = "ScriptableObjects/QueueShotStrategy")]
public class QueueShotStrategy : TowerShootStrategy
{


    public override void Setup(IAimable aimable)
    {
        aimable.Targets = new List<GameObject>();
    }

    public override void TargetEnters(IAimable aimable, GameObject gameObject)
    {
        List<GameObject> targets = aimable.Targets as List<GameObject>;
        targets.Add(gameObject);
        if(aimable.Target == null)
            aimable.Target = gameObject;
    }

    internal override void TargetExits(IAimable aimable, GameObject gameObject)
    {
        List<GameObject> targets = aimable.Targets as List<GameObject>;

        if (aimable.Target == gameObject)
            aimable.Target = targets.Find(p => true);

        targets.Remove(gameObject);
    }

    internal override GameObject ObtainTarget(IAimable turretBehaviour)
    {
        return turretBehaviour.Target;
    }
}