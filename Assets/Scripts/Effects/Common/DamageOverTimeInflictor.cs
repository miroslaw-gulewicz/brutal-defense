using Effect;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageOverTimeInflictor",
	menuName = "ScriptableObjects/Inflictors/DamageOverTimeInflictor")]
public class DamageOverTimeInflictor : TimeEffectInflictor
{
	[SerializeField] public DamageOverTimeInflictorDescriptor inflictorDescriptor;

	[SerializeField] public IDestructable.DamageType damageType;


	[SerializeField] public short damage;

	public override string Description()
	{
		if (!customDescription)
			return inflictorDescriptor.EffectDescription(this);
		else
			return base.Description();
	}

	public override void StopEffect(IEffectContextHolder mono)
	{
		mono.RemoveContextData(this);
	}

	protected override IEffectContextData AttachData(IEffectContextHolder mono, TimeEffectContext timeEffectContext)
	{
		mono.PutContextData(this, timeEffectContext);
		return timeEffectContext;
	}

	protected override void DoEffect(IDestructable destructable, IEffectContextHolder effectContextHolder)
	{
		destructable.TakeDamage(damageType, damage);
	}

	protected override TimeEffectContext GetTimeContextData(IEffectContextHolder effectContextHolder)
	{
		effectContextHolder.GetContextData(this, out IEffectContextData contextData);
		return (TimeEffectContext)contextData;
	}

	protected override void SaveTimeContextData(IEffectContextHolder mono, TimeEffectContext timeEffectContext)
	{
		mono.PutContextData(this, timeEffectContext);
	}
}