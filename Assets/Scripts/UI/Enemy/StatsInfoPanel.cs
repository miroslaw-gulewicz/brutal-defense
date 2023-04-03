using System;
using System.Collections.Generic;
using Effect;
using UnityEngine;
using UnityEngine.UI;
using static IDestructable;

public class StatsInfoPanel : MonoBehaviour
{
	[SerializeField] private TMPro.TextMeshProUGUI _displayName;

	[SerializeField] private Image _image;

	[SerializeField] private StatItem _statItemPrefab;

	[SerializeField] private StatItem _statItemInflictorPrefab;

	[SerializeField] private GameObject _content;

	[SerializeField] private BasicStatViewConfig[] statsConfig;

	[SerializeField] private ResistanceStatViewConfig[] resistanceStatViewConfig;

	[SerializeField] private WeaponStatViewConfig[] weaponStatViewConfig;

	private Dictionary<System.Object, StatItem> _statItems;

	private List<StatItem> _descriptionItems;

	private void Awake()
	{
		_descriptionItems = new List<StatItem>();
		_statItems = new Dictionary<System.Object, StatItem>();
		foreach (var item in statsConfig)
		{
			StatItem statItem = Instantiate(_statItemPrefab, _content.transform);
			StatItem.Init(statItem, item.tooltip, Color.white);
			statItem.gameObject.SetActive(false);
			_statItems.Add(item.Key, statItem);
		}

		foreach (var item in resistanceStatViewConfig)
		{
			StatItem statItem = Instantiate(_statItemPrefab, _content.transform);
			StatItem.Init(statItem, item.tooltip, Color.white);
			statItem.gameObject.SetActive(false);
			_statItems.Add(item.Key, statItem);
		}

		foreach (var item in weaponStatViewConfig)
		{
			StatItem statItem = Instantiate(_statItemPrefab, _content.transform);
			StatItem.Init(statItem, item.tooltip, Color.white);
			statItem.gameObject.SetActive(false);
			_statItems.Add(item.Key, statItem);
		}

		for (int i = 0; i < 4; i++)
		{
			_descriptionItems.Add(Instantiate(_statItemInflictorPrefab, _content.transform));
		}

		gameObject.SetActive(false);
	}

	public void Display(string name, Sprite sprite, BasicStatsHolder stats, ResistanceHolder resistance,
		EffectInflictor[] enemyObjectSelfInflictors, Weapon weapon = null)
	{

		_displayName.text = name;

		if (weapon != null) DisplayWeaponInfo(weapon);

		foreach (var item in _statItems.Values)
			item.gameObject.SetActive(false);		
		
		foreach (var item in _descriptionItems)
			item.gameObject.SetActive(false);

		foreach (var item in stats)
		{
			ShowStat(item.stat, item.value);
		}

		foreach (var item in resistance)
		{
			ShowStat(item.damageType, item.damageReduction);
		}

		_image.sprite = sprite;

		List<StatItem>.Enumerator enumerator = _descriptionItems.GetEnumerator();

		foreach (var item in enemyObjectSelfInflictors)
		{
			enumerator.MoveNext();
			var displayItem = enumerator.Current;
			displayItem.SetValue(item.Description());
			displayItem.gameObject.SetActive(true);
		}

		gameObject.SetActive(true);
	}

	private void DisplayWeaponInfo(Weapon weapon)
	{
		ShowStat(WeaponInfo.DAMAGE, weapon.Projectile.Damage);
		ShowStat(WeaponInfo.RANGE, weapon.Range);
	}

	private void ShowStat(System.Object key, float value)
	{
		if (value == 0 || !_statItems.TryGetValue(key, out StatItem enemyStatItem)) return;

		enemyStatItem.gameObject.SetActive(true);
		enemyStatItem.SetValue(value);
	}

	public void Close()
	{
		gameObject.SetActive(false);
	}

	public enum WeaponInfo
	{
		DAMAGE, RANGE
	}

	[Serializable]
	public class WeaponStatViewConfig : StatViewConfig
	{
		[SerializeField] public WeaponInfo weaponStat;

		public override object Key => weaponStat;
	}

	[Serializable]
	public class ResistanceStatViewConfig : StatViewConfig
	{
		[SerializeField] public DamageType resistance;

		public override object Key => resistance;
	}

	[Serializable]
	public class BasicStatViewConfig : StatViewConfig
	{
		[SerializeField] public StatEnum stat;

		public override object Key => stat;
	}

	public class StatViewConfig
	{
		[SerializeField] public string tooltip;

		[SerializeField] public Sprite sprite;

		[SerializeField] public Color color;

		public virtual System.Object Key => null;
	}
}