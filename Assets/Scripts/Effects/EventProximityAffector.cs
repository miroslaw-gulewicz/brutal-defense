using Effect;
using UnityEngine;

public class EventProximityAffector : ProximityEffectInflictor, IEventListener
{
    [SerializeField]
    private SpawnEnemyObjectsCommand _command;

    private event IEventListener.CancelEventHandler _proximityEventCancel;

    public SpawnEnemyObjectsCommand Command { set => _command = value; }

    protected override void TriggerEnter(Collider other)
    {
        base.TriggerEnter(other);
        if (other.TryGetComponent(out IEffectEventSource eventSource))
            eventSource.RegisterEventCallback(this, OnEventReised);
    }

    protected override void TriggerExit(Collider other)
    {
        if(!_cancelEffectOnExit && other.TryGetComponent(out IEffectEventSource eventSource))
            eventSource.UnRegisterEventCallback(this, OnEventReised);

        base.TriggerExit(other);
    }

    protected void OnEventReised(IEffectContextHolder contextHolder)
    {
        _command.DoCommand(contextHolder);    
    }

    private void OnDestroy()
    {
        _proximityEventCancel?.Invoke(this);
    }

    private void OnDisable()
    {
        _proximityEventCancel?.Invoke(this);
    }


    public void RegisterCancelEvent(IEventListener.CancelEventHandler handler)
    {
        _proximityEventCancel += handler;
    }

    public void UnRegisterCancelEvent(IEventListener.CancelEventHandler handler)
    {
        _proximityEventCancel -= handler;
    }
}
