using UnityEngine.Events;
namespace Effect
{
    public interface IAffected : IEffectEventSource
    {
        void ApplyEffect(EffectInflictor[] inflictors);
        void CancelEffect(EffectInflictor[] inflictors);

        public enum EffectType : int
        {
            NONE, STUN, BLEED, FREEZE, BURN, POISION, PERMANENT
        }
    }
}