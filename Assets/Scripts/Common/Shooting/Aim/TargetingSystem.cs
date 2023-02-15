using UnityEngine;

namespace Aim
{
    public class TargetingSystem
    {
        protected Agent target;

        internal int TargetingSystemID { get; set; }

        public Object TargetDefinition;

        public virtual Agent Target { get => target; }

        public virtual void Setup(TargetingSystem targetingSystem)
        {
            target = targetingSystem.Target;
        }

        public virtual void TargetEnters(Agent gameObject)
        {
            if (!target)
                target = gameObject;
        }

        internal virtual void TargetExits(Agent gameObject)
        {
            if (GameObject.ReferenceEquals(target, gameObject))
                target = null;
        }
    }

}