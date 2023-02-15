using UnityEngine;
namespace Effect
{
    public interface IEffectContextHolder
    {
        MonoBehaviour Mono { get; }

        BasicStatsHolder BasicStats { get; }

        void GetContextData(EffectInflictor inflictor, out IEffectContextData data);

        void PutContextData(EffectInflictor inflictor, IEffectContextData data);

        void RemoveContextData(EffectInflictor inflictor);
    }
}