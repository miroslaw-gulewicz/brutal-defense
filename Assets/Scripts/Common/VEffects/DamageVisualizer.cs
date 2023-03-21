using Effect;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static Effect.IAffected;

public class DamageVisualizer : MonoBehaviour
{
	[SerializeField] private GameObject _damageIndicatorPrefab;

	[SerializeField] private GameObject _effectVisualizerPrefab;

	[SerializeField] private DamageColor[] _colors;

	[SerializeField] private EffectColor[] _effectColors;

	private Dictionary<IDestructable.DamageType, Color> _damageType2Color;

	private Dictionary<EffectType, Color> _effectType2Color;

	private void Awake()
	{
		_damageType2Color = _colors.ToDictionary(k => k.damage, k => k.color);
		_effectType2Color = _effectColors.ToDictionary(k => k.effect, k => k.color);
	}

	public void DisplayDamageInfo(Vector3 position, IDestructable.DamageType damageType, short amount)
	{
		GameObject go = ObjectCacheManager._Instance.GetObject(_damageIndicatorPrefab);
		go.transform.position = position;
		DamageInfo damageInfo = go.GetComponent<DamageInfo>();
		if (_damageType2Color.TryGetValue(damageType, out Color color))
		{
			damageInfo.UpdateDamage(color, amount);
		}
		else
		{
			Debug.Log("No color for " + damageType);
		}

		go.SetActive(true);
	}

	[Serializable]
	public class DamageColor
	{
		public Color color;
		public IDestructable.DamageType damage;
	}

	[Serializable]
	public class EffectColor
	{
		public Color color;
		public EffectType effect;
	}

	internal void DisplayEffectInfo(Vector3 position, EffectInflictor inflictor)
	{
		GameObject go = ObjectCacheManager._Instance.GetObject(_effectVisualizerPrefab);
		go.transform.position = position;
		DamageInfo damageInfo = go.GetComponent<DamageInfo>();
		if (_effectType2Color.TryGetValue(inflictor.EffectType, out Color color))
			damageInfo.UpdateText(color, inflictor.inflictorSourceKey.Name);
		go.SetActive(true);
	}
}