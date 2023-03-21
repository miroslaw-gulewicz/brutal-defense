using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellButton : MonoBehaviour
{
	[SerializeField] private Spell _spell;

	[SerializeField] TMPro.TextMeshProUGUI textMeshProUGUI;

	[SerializeField] private bool _cooldown;

	private Button _button;

	private CooldownTimer _cooldownTimer;

	public Action<Spell> OnSpellSelected { set; private get; }

	public Spell Spell
	{
		get => _spell;
	}

	[ContextMenu("Init")]
	private void Awake()
	{
		_button = GetComponent<Button>();
		_button.onClick.AddListener(() => OnSpellSelected(_spell));
		textMeshProUGUI.text = _spell.SpellName;
		_cooldownTimer = GetComponent<CooldownTimer>();
	}

	public void StartCooldown()
	{
		if (!_cooldown) return;

		_button.enabled = false;
		_cooldownTimer.StartTimout(_spell.Cooldown, SetEnabled);
	}

	public void SetEnabled()
	{
		_button.enabled = true;
	}
}