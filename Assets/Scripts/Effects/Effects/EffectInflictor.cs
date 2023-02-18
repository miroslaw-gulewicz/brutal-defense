using System;
using UnityEngine;
namespace Effect
{
    public interface EffectInflictor : IDescribable
    {
        IAffected.EffectType EffectType { get; }

        InflictorSourceKey inflictorSourceKey { get; }

        EffectInflictorAgent EffectAgent { get; }

        IEffectContextData Attachffect(IEffectContextHolder mono);

        float UpdateInflictor(IDestructable destructable, IEffectContextHolder effectContextHolder);

        void StopEffect(IEffectContextHolder mono);

        public interface InflictorSourceKey
        {
            string Name { get; }
        }


        //TODO: replacement for IEffectContextData
        [Serializable]
        public struct EffectContext
        {
            [SerializeField]
            public float[] data;

            public EffectContext(float[] data)
            {
                this.data = data;
            }
        }
    }
}