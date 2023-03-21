using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OptionsManager : MonoBehaviour
{
	private static OptionsManager __Instance;
	public static OptionsManager Instance => __Instance;

	[SerializeField] SimpleSlider gameSpeed;

	[SerializeField] List<GameOptionFieldDefinition> gameOptionFieldDefinitions;


	private void Awake()
	{
		if (__Instance)
			__Instance = this;
	}


	private void Start()
	{
		gameOptionFieldDefinitions.ForEach(definition => definition.valueChanged.AddListener((val) =>
		{
			ChnageOptionValue(definition.key, val);
		}));


		LoadUserSettings();
	}


	public void ChnageOptionValue(string key, float value)
	{
		GameOptionFieldDefinition gameOptionFieldDefinition =
			gameOptionFieldDefinitions.Find(opt => opt.key.Equals(key));
		PlayerPrefs.SetFloat(Settings.settingsGameSpeed, value);
		gameOptionFieldDefinition.valueChanged.Invoke(value);
	}

	private void LoadUserSettings()
	{
		gameSpeed.Value = PlayerPrefs.GetFloat(Settings.settingsGameSpeed, 1);
	}


	[Serializable]
	public class GameOptionFieldDefinition
	{
		[SerializeField] string name;
		[SerializeField] public string key;

		[SerializeField] SimpleSlider prefab;

		[SerializeField] public UnityEvent<float> valueChanged;
	}

	public enum OptionFieldDef
	{
		SLIDER,
		BOOL
	}

	[Serializable]
	public class GameOptions
	{
		float volueme;
		float brightness;
	}
}