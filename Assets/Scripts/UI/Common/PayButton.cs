using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PayButton : Button
{
	[Header("Pay Button")] [SerializeField]
	int value;

	[SerializeField] TMPro.TextMeshProUGUI valueText;

	public int Value
	{
		get => value;
		set
		{
			this.value = value;
			valueText.text = value.ToString();
		}
	}
}