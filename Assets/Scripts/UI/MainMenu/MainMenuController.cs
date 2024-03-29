using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
	[SerializeField] private Button _playBtn;

	[SerializeField] private Button _leaderBoardBtn;

	[SerializeField] private Button _settingsBtn;

	[SerializeField] private Button _creditsBtn;

	[SerializeField] private Button _returnBtn;

	[SerializeField] private Button _exitBtn;

	[SerializeField] private LevelMenuController _levelMenuController;

	[SerializeField] private CreditsMenuController _creditsMenuController;

	[SerializeField] private SettingsMenuController _settingsMenuController;

	[SerializeField] private LeaderBoardMenuController _leaderBoardMenuController;

	[SerializeField] private Canvas _menuCanvas;

	private IMenu _activeMenu;

	private void Awake()
	{
		_playBtn.onClick.AddListener(() => { CloseActiveAndDisplay(_levelMenuController); });

		_settingsBtn.onClick.AddListener(() => { CloseActiveAndDisplay(_settingsMenuController); });

		_creditsBtn.onClick.AddListener(() => { CloseActiveAndDisplay(_creditsMenuController); });

		_leaderBoardBtn.onClick.AddListener(() => { CloseActiveAndDisplay(_leaderBoardMenuController); });

		_returnBtn.onClick.AddListener(CloseActive);

		_exitBtn.onClick.AddListener(Application.Quit);

		CloseActive();

		_settingsMenuController.Close();
		_creditsMenuController.Close();
		_levelMenuController.Close();
		_leaderBoardMenuController.Close();

#if UNITY_WEBGL || UNITY_EDITOR
        _exitBtn.gameObject.SetActive(false);
#endif
    }

    private void CloseActiveAndDisplay(IMenu menu)
	{
		_activeMenu?.Close();
		_activeMenu = menu;
		_activeMenu?.Display();
		_menuCanvas.gameObject.SetActive(false);
		_returnBtn.gameObject.SetActive(true);
	}

	private void CloseActive()
	{
		_activeMenu?.Close();
		_activeMenu = null;
		_returnBtn.gameObject.SetActive(false);
		_menuCanvas.gameObject.SetActive(true);
	}
}