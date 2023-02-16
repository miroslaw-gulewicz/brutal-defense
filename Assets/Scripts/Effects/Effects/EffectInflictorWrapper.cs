using Effect;
public class EffectInflictorWrapper : EffectInflictor
{
    public IAffected.EffectType EffectType => IAffected.EffectType.NONE;

    public EffectInflictorAgent EffectAgent => _effectAgent;

    public EffectInflictor.InflictorSourceKey inflictorSourceKey => throw new System.NotImplementedException();

    public EffectInflictorAgent _effectAgent;
    EffectInflictorWrapper(EffectInflictorAgent agent)
    {
        _effectAgent = agent;
    }

     public IEffectContextData Attachffect(IEffectContextHolder mono)
    {
        return null;
    }

    public float UpdateInflictor(IDestructable destructable, IEffectContextHolder effectContextHolder)
    {
        throw new System.NotImplementedException();
    }

    public void StopEffect(IEffectContextHolder mono)
    {
        throw new System.NotImplementedException();
    }

    public string Description()
    {
        throw new System.NotImplementedException();
    }
}
