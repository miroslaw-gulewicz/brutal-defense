using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static IDestructable;

public class StatsInfoPanel : MonoBehaviour
{
	[SerializeField] private Image _image;

	[SerializeField] private StatItem _statItemPrefab;

	[SerializeField] private GameObject _content;

	[SerializeField] private BasicStatViewConfig[] statsConfig;

	[SerializeField] private ResistanceStatViewConfig[] resistanceStatViewConfig;


	private Dictionary<System.Object, StatItem> _statItems;

	private void Start()
	{
		_statItems = new Dictionary<System.Object, StatItem>();
		foreach (var item in statsConfig)
		{
			StatItem statItem = Instantiate(_statItemPrefab, _content.transform);
			StatItem.Init(statItem, item.sprite, item.tooltip, item.color);
			statItem.gameObject.SetActive(false);
			_statItems.Add(item.Key, statItem);
		}

		foreach (var item in resistanceStatViewConfig)
		{
			StatItem statItem = Instantiate(_statItemPrefab, _content.transform);
			StatItem.Init(statItem, item.sprite, item.tooltip, item.color);
			statItem.gameObject.SetActive(false);
			_statItems.Add(item.Key, statItem);
		}

		gameObject.SetActive(false);
	}

	public void Display(Sprite sprite, BasicStatsHolder stats, ResistanceHolder resistance, Weapon weapon = null)
	{
		gameObject.SetActive(true);
		foreach (var item in _statItems.Values)
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

		if (weapon == null) return;
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