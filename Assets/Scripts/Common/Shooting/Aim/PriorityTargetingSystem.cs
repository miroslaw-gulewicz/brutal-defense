
using UnityEngine;

namespace Aim {

    public class PriorityTargetingSystem : TargetingSystem
    {
        private QueueTargetingSystem _queueTargetingSystem;

        public override Agent Target => _queueTargetingSystem.Target;

        public override void Setup(TargetingSystem targetingSystem)
        {
            _queueTargetingSystem = new QueueTargetingSystem(QueueTargetingSystem.NextTarget.FIRST);
            if (targetingSystem != null &&  MatchesTarget(targetingSystem.Target))
                _queueTargetingSystem.Setup(targetingSystem);
        }

        public override void TargetEnters(Agent gameObject)
        {
            if (MatchesTarget(gameObject))
                _queueTargetingSystem.TargetEnters(gameObject);
        }

        internal override void TargetExits(Agent gameObject)
        {
            if (MatchesTarget(gameObject))
                _queueTargetingSystem.TargetExits(gameObject);
        }

        public bool MatchesTarget(Agent agent)
        {
            if (agent == null || agent.ObjectDefinition == null) return false;
            if (TargetDefinition == null) return false;
            return Object.ReferenceEquals(TargetDefinition, agent.ObjectDefinition);
        }
    }
}
