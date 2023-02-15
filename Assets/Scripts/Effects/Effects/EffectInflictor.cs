using UnityEngine;
namespace Effect
{
    public interface EffectInflictor : IDescribable
    {
        IAffected.EffectType EffectType { get; }

        InflictorSourceKey inflictorSourceKey { get; }

        EffectInflictorAgent EffectAgent { get; }

        IEffectContextData Attachffect(IEffectContextHolder mono);

        bool UpdateInflictor(IDestructable destructable, IEffectContextHolder effectContextHolder);

        void StopEffect(IEffectContextHolder mono);

        public interface InflictorSourceKey
        {
            string Name { get; }
        }
    }
}