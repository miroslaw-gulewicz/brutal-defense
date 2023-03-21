using UnityEngine;
using Effect;

public class IProximityInflictor : ProximityTrigger
{
	[SerializeField] protected BaseEffectInflictor[] _inflictors;

	public BaseEffectInflictor[] Inflictors
	{
		set => _inflictors = value;
	}

	protected override void TriggerEnter(Collider other)
	{
	}

	protected override void TriggerExit(Collider other)
	{
	}
}