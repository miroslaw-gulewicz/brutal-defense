using Effect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static IDestructable;
using static MovementStrategy;

public class Agent : MonoBehaviour, IDestructable, IAffected, IHighlightable, ISelectable
{
    protected EffectManager _effectManager;
    protected BasicStatsManager _statsManager = new BasicStatsManager();

    [SerializeField]
    protected HighlightTrigger highlightTrigger;

    [SerializeField]
    protected SelectableBehaviour selectableBehaviour;

    [SerializeField]
    protected DamageVisualizer _damageVisualizer;

    [SerializeField]
    protected IndicatorBarBehaviour _hpBar;

    [SerializeField]
    protected SpriteRenderer _sprite;

    public virtual void SetSelected(bool selected)
    {
        selectableBehaviour.gameObject.SetActive(selected);
    }

    void IDestructable.TakeDamage(IDestructable.DamageType damageType, short amount)
    {
        _effectManager.TakeDamage(damageType, amount);
    }

    void IAffected.ApplyEffect(EffectInflictor[] inflictors)
    {
        _effectManager.ApplyEffect(inflictors);
    }

    public void CancelEffect(EffectInflictor[] inflictors)
    {
        _effectManager.CancelEffect(inflictors);
    }

    public void RegisterEventCallback(IEventListener source, UnityAction<IEffectContextHolder> callback)
    {
        _effectManager.RegisterEventCallback(source, callback);
    }

    public void UnRegisterEventCallback(IEventListener source, UnityAction<IEffectContextHolder> callback)
    {
        _effectManager.UnRegisterEventCallback(source, callback);
    }

    public virtual void HighLight(bool highlighted)
    {
        highlightTrigger.gameObject.SetActive(highlighted);
    }

    protected void OnDamageTaken(IDestructable.DamageType damageType, short amount)
    {
        if (gameObject.activeSelf)
            _damageVisualizer?.DisplayDamageInfo(transform.position, damageType, amount);
    }

    protected void OnEffectApplied(EffectInflictor inflictor)
    {
/*        if (gameObject.activeSelf)
            _damageVisualizer?.DisplayEffectInfo(transform.position, inflictor);*/
    }

    public virtual Object ObjectDefinition => null;

    public BasicStatsHolder BasicStats => _statsManager.BasicStatsHolder;
}
