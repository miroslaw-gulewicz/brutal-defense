using UnityEngine;


public abstract class EffectInflictorAgent : ScriptableObject
{
	public enum ApplyStrategy
	{
		TARGET,
		TARGET_POSITION
	}

	[SerializeField] protected ApplyStrategy _applyStrategy;

	public ApplyStrategy ApplyEffectStrategy
	{
		get => _applyStrategy;
	}

	protected abstract GameObject CreateEffect(GameObject gameObject);

	public GameObject ApplyEffect(GameObject gameObject)
	{
		GameObject effectObject = CreateEffect(gameObject);
		ApplySEffectHierarhy(gameObject, effectObject);
		return effectObject;
	}


	protected void ApplySEffectHierarhy(GameObject target, GameObject gameObject)
	{
		if (_applyStrategy == ApplyStrategy.TARGET)
		{
			gameObject.transform.SetParent(target.transform, false);
		}
		else
		{
			gameObject.transform.SetParent(null, true);
			gameObject.transform.position = target.transform.position;
		}
	}
}