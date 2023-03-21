using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuController : MonoBehaviour, IMenu
{
	[SerializeField] private LevelDefinitionsDatabase _levelDefinitions;

	[SerializeField] private Canvas _menuCanvas;

	[SerializeField] private GameObject _levelsContentDisplay;

	[SerializeField] private Button _returnBtn;

	[SerializeField] private LevelTileController _tileControllerPrefab;


	private void Start()
	{
		foreach (var level in _levelDefinitions.items)
		{
			LevelTileController levelTile = Instantiate(_tileControllerPrefab, _levelsContentDisplay.transform);
			LevelTileController.Init(levelTile, level, OnLevelSelected);
		}
	}

	private void OnLevelSelected(LevelDefinition selectedLevel)
	{
		SceneManager._Instance.LoadLevelScene(selectedLevel);
	}

	public void Close()
	{
		_menuCanvas.gameObject.SetActive(false);
	}

	public void Display()
	{
		_menuCanvas.gameObject.SetActive(true);
	}
}