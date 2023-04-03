using Effect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TimingEffectInflictor", menuName = "ScriptableObjects/TimingEffectInflictor")]
public class TimingEffectInflictor : TimeEffectInflictor
{
	[SerializeField] BaseEffectInflictor inflictor;

	public override string Description()
	{
		return inflictor.Description();
	}

	public override void StopEffect(IEffectContextHolder mono)
	{
		inflictor.StopEffect(mono);
		mono.RemoveContextData(this);
	}

	protected override IEffectContextData AttachData(IEffectContextHolder mono, TimeEffectContext timeEffectContext)
	{
		mono.PutContextData(this, timeEffectContext);

		return timeEffectContext;
	}

	protected override void DoEffect(IDestructable destructable, IEffectContextHolder effectContextHolder)
	{
		inflictor.UpdateInflictor(destructable, effectContextHolder);
	}

	protected override TimeEffectContext GetTimeContextData(IEffectContextHolder effectContextHolder)
	{
		effectContextHolder.GetContextData(this, out IEffectContextData contextData);
		return ((TimeEffectContext)contextData);
	}

	protected override void SaveTimeContextData(IEffectContextHolder effectContextHolder,
		TimeEffectContext timeEffectContext)
	{
		effectContextHolder.PutContextData(this, timeEffectContext);
	}
}