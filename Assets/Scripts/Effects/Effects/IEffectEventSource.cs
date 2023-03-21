using Effect;
using UnityEngine.Events;

public interface IEffectEventSource
{
	void RegisterEventCallback(IEventListener source, UnityAction<IEffectContextHolder> callback);
	void UnRegisterEventCallback(IEventListener source, UnityAction<IEffectContextHolder> callback);
}