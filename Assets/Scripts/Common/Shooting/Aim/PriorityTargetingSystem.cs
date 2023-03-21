using UnityEngine;

namespace Aim
{
	public class PriorityTargetingSystem : TargetingSystem
	{
		private QueueTargetingSystem _queueTargetingSystem;

		public override GameObject Target => _queueTargetingSystem.Target;

		public override void Setup(TargetingSystem targetingSystem)
		{
			_queueTargetingSystem = new QueueTargetingSystem(QueueTargetingSystem.NextTarget.FIRST);
			if (targetingSystem != null && targetingSystem.Target != null &&
			    targetingSystem.Target.TryGetComponent<Agent>(out Agent agent))
				if (MatchesTarget(agent))
					_queueTargetingSystem.Setup(targetingSystem);
		}

		public override void TargetEnters(GameObject gameObject)
		{
			if (gameObject.TryGetComponent<Agent>(out Agent agent))
				if (MatchesTarget(agent))
					_queueTargetingSystem.TargetEnters(gameObject);
		}

		internal override void TargetExits(GameObject gameObject)
		{
			if (gameObject.TryGetComponent<Agent>(out Agent agent))
				if (MatchesTarget(agent))
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