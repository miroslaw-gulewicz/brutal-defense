using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SimpleToggle : MonoBehaviour
{
	public const string ON = "On";
	public const string OFF = "Off";

	[SerializeField] TextMeshProUGUI valueText;

	[SerializeField] Toggle toggle;

	[SerializeField] Action<bool> onValueChanged;

	[SerializeField] public bool reverse;

	public bool Value
	{
		get { return GetCalculatedValue(toggle.isOn); }
		set
		{
			toggle.isOn = GetCalculatedValue(value);
			UpdateValueText(toggle.isOn);
		}
	}

	public Action<bool> OnValueChanged
	{
		get => onValueChanged;
		set => onValueChanged = value;
	}

	private void Awake()
	{
		toggle.onValueChanged.AddListener((val) =>
		{
			OnValueChanged?.Invoke(GetCalculatedValue(val));
			UpdateValueText(val);
		});
	}

	private bool GetCalculatedValue(bool val)
	{
		if (reverse) return !val;
		return val;
	}

	private void UpdateValueText(bool val)
	{
		valueText.text = GetCalculatedValue(val) ? ON : OFF;
	}
}