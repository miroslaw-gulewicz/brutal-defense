using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatItem : MonoBehaviour
{
	[SerializeField] private TMPro.TextMeshProUGUI _statName;

	[SerializeField] private TMPro.TextMeshProUGUI _statsText;

	public static void Init(StatItem statItem, string statName, Color color)
	{
		statItem._statName.color = color;
		statItem._statName.text = statName;
		statItem._statsText.color = color;
	}

	public void SetValue(float value)
	{
		_statsText.text = value.ToString();
	}

	public void SetValue(string value)
	{
		_statsText.text = value;
	}
}