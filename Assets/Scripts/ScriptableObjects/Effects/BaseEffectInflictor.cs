using UnityEngine;
using Effect;
using System;

public abstract class BaseEffectInflictor : ScriptableObject, EffectInflictor
{
	protected static int UniqueIndexingVar = 0;

	[SerializeField] protected bool customDescription;

	[SerializeField] protected IAffected.EffectType _effectType;

	[SerializeField] protected EffectInflictorAgent _effectAgent;

	[SerializeField] protected string _customDescription;

	public IAffected.EffectType EffectType => _effectType;

	public EffectInflictorAgent EffectAgent => _effectAgent;

	public EffectInflictor.InflictorSourceKey inflictorSourceKey => _key;

	[SerializeField] private EffectInflictorKey _key;
	public abstract IEffectContextData Attachffect(IEffectContextHolder mono);

	public abstract void StopEffect(IEffectContextHolder mono);

	public abstract float UpdateInflictor(IDestructable destructable, IEffectContextHolder effectContextHolder);

	public virtual string Description()
	{
		return _customDescription;	
	}

	private void OnEnable()
	{
		_key = new EffectInflictorKey(_effectType, name);
	}

	[Serializable]
	public class EffectInflictorKey : EffectInflictor.InflictorSourceKey
	{
		[SerializeField] int EffectUniqueId;

		string inflictorName;

		public EffectInflictorKey(IAffected.EffectType effectType, string name)
		{
			EffectUniqueId = UniqueIndexingVar++;
			inflictorName = name;
		}

		string EffectInflictor.InflictorSourceKey.Name => inflictorName;
	}
}