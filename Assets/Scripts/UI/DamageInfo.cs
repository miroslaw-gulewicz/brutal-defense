using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo : MonoBehaviour
{
	[SerializeField] TMPro.TextMeshPro damageValue;
	[SerializeField] TMPro.TextMeshPro damageSignPlus;
	[SerializeField] TMPro.TextMeshPro damageSignMinus;

	private static string[] _damageStringCache;
	private void Awake()
	{
		if (_damageStringCache != null) return;

		_damageStringCache = new string[100];

		for (int i = 0; i < 100; i++)
		{
			_damageStringCache[i] = i.ToString();
		}
	}

	internal void UpdateDamage(Color color, int amount)
	{
		damageValue.color = color;
		var negative = amount < 0;
		var absDmg = Math.Abs(amount);

		if (absDmg < _damageStringCache.Length)
			damageValue.text = _damageStringCache[absDmg];

		damageSignMinus.gameObject.SetActive(negative);
		damageSignPlus.gameObject.SetActive(!negative);
		damageSignPlus.color = color;
		damageSignMinus.color = color;
	}

	public void UpdateText(Color color, string text)
	{
		damageValue.color = color;

		damageValue.text = text;
	}
}