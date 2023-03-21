using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SimpleSlider : MonoBehaviour
{
	[SerializeField] TextMeshProUGUI valueText;

	[SerializeField] int minValue;
	[SerializeField] int maxValue;

	[SerializeField] Slider slider;

	[SerializeField] Action<float> onValueChanged;

	public float Value
	{
		get { return slider.value; }
		set
		{
			slider.value = value;
			UpdateValueText(value);
		}
	}

	public Action<float> OnValueChanged
	{
		get => onValueChanged;
		set => onValueChanged = value;
	}

	private void Awake()
	{
		slider.minValue = minValue;
		slider.maxValue = maxValue;

		slider.onValueChanged.AddListener((val) =>
		{
			OnValueChanged?.Invoke(val);
			UpdateValueText(val);
		});
	}

	private void UpdateValueText(float val)
	{
		valueText.text = ((int)val).ToString();
	}
}