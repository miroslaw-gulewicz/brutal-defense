using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolTip : MonoBehaviour
{
	public static ToolTip instance => _instance;
	private static ToolTip _instance;

	public TMPro.TextMeshProUGUI _tooltipText;

	private void Awake()
	{
		_instance = this;
		Hide();
	}

	public void SetText(string tooltipText)
	{
		_tooltipText.text = tooltipText;
	}

	internal void Show(Vector2 pos)
	{
		gameObject.transform.position = pos;
		gameObject.SetActive(true);
	}

	internal void Hide()
	{
		gameObject.SetActive(false);
	}
}