using UnityEngine;

namespace Aim
{
	public class ManualTargetingSystem : TargetingSystem
	{
		public GameObject ManualTarget
		{
			set => target = value;
		}

		public override void Setup(TargetingSystem targetingSystem)
		{
			if (GameObject.ReferenceEquals(target, targetingSystem.Target))
				target = targetingSystem.Target;
		}

		public override void TargetEnters(GameObject gameObject)
		{
		}

		internal override void TargetExits(GameObject gameObject)
		{
		}
	}
}