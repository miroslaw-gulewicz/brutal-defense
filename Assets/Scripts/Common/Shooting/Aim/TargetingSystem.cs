using UnityEngine;

namespace Aim
{
	public class TargetingSystem
	{
		protected GameObject target;

		internal int TargetingSystemID { get; set; }

		public Object TargetDefinition;

		public virtual GameObject Target
		{
			get => target;
		}

		public virtual void Setup(TargetingSystem targetingSystem)
		{
			target = targetingSystem.Target;
		}

		public virtual void TargetEnters(GameObject gameObject)
		{
			if (!target)
				target = gameObject;
		}

		internal virtual void TargetExits(GameObject gameObject)
		{
			if (GameObject.ReferenceEquals(target, gameObject))
				target = null;
		}
	}
}