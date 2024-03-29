using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Effect.EffectInflictor;
using static IDestructable;

namespace Effect
{
	public class EffectManager : IDestructable, IAffected, IEffectContextHolder
	{
		public Action<DamageType, short> DamageTakenCallback;
		public Action<EffectInflictor> OnEffectAppliedCallback;
		public Effects DefaultEffects { get; internal set; }

		private readonly BasicStatsManager _statsManager;
		private readonly MonoBehaviour _mono;

		private readonly Dictionary<InflictorSourceKey, EffectInflictor> _effects =
			new Dictionary<InflictorSourceKey, EffectInflictor>();

		private readonly Dictionary<InflictorSourceKey, GameObject> _effectObjects =
			new Dictionary<InflictorSourceKey, GameObject>();

		private readonly Dictionary<InflictorSourceKey, IEffectContextData> _effectContexts =
			new Dictionary<InflictorSourceKey, IEffectContextData>();

		private readonly List<InflictorSourceKey> _effectsToRemoveKeys = new List<InflictorSourceKey>(7);

		private EffectInflictor[] _effectInflictors = new EffectInflictor[10];

		public event UnityAction<IEffectContextHolder> _hpBelow0Callback;
		private bool _isDead = false;

		public BasicStatsHolder BasicStats => _statsManager.BasicStatsHolder;

		public MonoBehaviour Mono => _mono;

		float _nextUpdate;

		public EffectManager(BasicStatsManager statsManager, MonoBehaviour mono)
		{
			this._statsManager = statsManager;
			this._mono = mono;
		}

		internal void ClearEffects()
		{
			foreach (var item in _effects)
			{
				item.Value.StopEffect(this);
				if (_effectObjects.TryGetValue(item.Key, out GameObject effect))
					GameObject.Destroy(effect);
			}

			_effects.Clear();
			_effectObjects.Clear();
			_effectContexts.Clear();
		}

		public void AddEffect(EffectInflictor inflictor)
		{
			if (_isDead || !CanAddEffect(inflictor)) return;

			ApplyEffect(inflictor);
			OnEffectAppliedCallback(inflictor);
		}

		private bool CanAddEffect(EffectInflictor inflictor)
		{
			return !_effects.ContainsKey(inflictor.inflictorSourceKey);
		}

		private void ApplyEffect(EffectInflictor inflictor)
		{
			IEffectContextData effectContextData = inflictor.Attachffect(this);
			if (effectContextData == null)
			{
				inflictor.UpdateInflictor(this, this);
			}
			else
			{
				_effects.Add(inflictor.inflictorSourceKey, inflictor);
				_nextUpdate = -10;
			}

			EffectInflictorAgent agent = inflictor.EffectAgent;
/*            if (agent == null)
                agent = DefaultEffects[inflictor.EffectType];*/
			if (!agent) return;


			GameObject effectGo = agent.ApplyEffect(_mono.gameObject);
			if (effectGo != null /*&& inflictor.EffectType != IAffected.EffectType.NONE*/)
				_effectObjects[inflictor.inflictorSourceKey] = effectGo;
		}

		public void CancelEffect(EffectInflictor[] inflictors)
		{
			for (int i = 0; i < inflictors.Length; i++)
			{
				_effectsToRemoveKeys.Add(inflictors[i].inflictorSourceKey);
			}

			_nextUpdate = -10;
		}

		public void ApplyEffect(EffectInflictor[] inflictors)
		{
			for (int i = 0; i < inflictors.Length; i++)
				AddEffect(inflictors[i]);
		}

		public void OnUpdate()
		{
			if (Time.time < _nextUpdate) return;
			float tmpMainNextUpdate = _nextUpdate + 100;
			_effects.Values.CopyTo(_effectInflictors, 0);
			for (int i = 0; i < _effectInflictors.Length; i++)
			{
				EffectInflictor effectInflictor = _effectInflictors[i];
				if (effectInflictor == null) continue;

				float inflictorNextUpdate = effectInflictor.UpdateInflictor(this, this);
				if (effectInflictor.EffectType != IAffected.EffectType.PERMANENT && inflictorNextUpdate <= 0)
				{
					_effectsToRemoveKeys.Add(effectInflictor.inflictorSourceKey);
					effectInflictor.StopEffect(this);
				}
				else
				{
					if (inflictorNextUpdate < tmpMainNextUpdate)
						tmpMainNextUpdate = inflictorNextUpdate;

					_effectInflictors[i] = null;
				}
			}

			_nextUpdate = tmpMainNextUpdate;


			foreach (InflictorSourceKey effectKey in _effectsToRemoveKeys)
			{
				if (_effectObjects.TryGetValue(effectKey, out GameObject obj))
				{
					GameObject.Destroy(obj);
					_effectObjects[effectKey] = null;
				}

				if (_effects.TryGetValue(effectKey, out EffectInflictor effect))
					effect.StopEffect(this);

				_effects.Remove(effectKey);
			}

			_effectsToRemoveKeys.Clear();

			for (int i = 0; i < _effectInflictors.Length; i++)
			{
				_effectInflictors[i] = null;
			}
		}

		public void TakeDamage(DamageType damageType, short amount)
		{
			if (amount < 0)
			{
				_statsManager.BasicStatsHolder.CurrentHp -= amount;
				DamageTakenCallback.Invoke(damageType, (short)amount);
				return;
			}

			float resistance = _statsManager.ResistanceHolder[damageType];

			short totalDamage = (short)(amount - ((short)Math.Ceiling(amount * (resistance / 100f))));

			if (totalDamage > 0)
			{
				_statsManager.BasicStatsHolder.CurrentHp -= totalDamage;
				DamageTakenCallback?.Invoke(damageType, totalDamage);

				if (_statsManager.BasicStatsHolder.CurrentHp <= 0 && !_isDead)
				{
					_isDead = true;
					_hpBelow0Callback?.Invoke(this);
				}
			}
		}

		public void GetContextData(EffectInflictor inflictor, out IEffectContextData data)
		{
			if (!_effectContexts.TryGetValue(inflictor.inflictorSourceKey, out data))
			{
				Debug.Log("No context for inflictor " + inflictor.ToString());
				data = null;
				return;
			}
		}

		public void PutContextData(EffectInflictor inflictor, IEffectContextData data)
		{
			_effectContexts[inflictor.inflictorSourceKey] = data;
		}

		public void RemoveContextData(EffectInflictor inflictor)
		{
			_effectContexts.Remove(inflictor.inflictorSourceKey);
		}

		public void RegisterEventCallback(IEventListener source, UnityAction<IEffectContextHolder> callback)
		{
			_hpBelow0Callback += callback;
			source.RegisterCancelEvent(EventCanceled);
		}

		public void UnRegisterEventCallback(IEventListener source, UnityAction<IEffectContextHolder> callback)
		{
			source.UnRegisterCancelEvent(EventCanceled);
			_hpBelow0Callback = null;
		}

		private void EventCanceled(IEventListener source)
		{
			_hpBelow0Callback -= null;
		}
	}
}