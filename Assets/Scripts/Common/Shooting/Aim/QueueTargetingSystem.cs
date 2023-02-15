
using System.Collections.Generic;
using UnityEngine;

namespace Aim
{
    public class QueueTargetingSystem : TargetingSystem
    {
        List<Agent> targetQueue = new List<Agent>();

        private NextTarget nextTarget;

        public QueueTargetingSystem(NextTarget nextTarget = NextTarget.FIRST)
        {
            this.nextTarget = nextTarget;
        }

        public override void Setup(TargetingSystem targetingSystem)
        {
            base.Setup(targetingSystem);
            TargetEnters(targetingSystem.Target);
        }

        public override void TargetEnters(Agent gameObject)
        {
            if (targetQueue.Contains(gameObject)) return;

            targetQueue.Add(gameObject);

            if (target == null)
                target = gameObject;
        }

        internal override void TargetExits(Agent gameObject)
        {
            targetQueue.Remove(gameObject);

            if(GameObject.ReferenceEquals(target, gameObject))
            {
                if (targetQueue.Count > 0)
                {
                    target = targetQueue[nextTarget == NextTarget.FIRST ? 0 : targetQueue.Count - 1];
                }
                else
                    target = null;
            }
            
        }

        public enum NextTarget
        {
            FIRST, LAST
        }
    }

}
