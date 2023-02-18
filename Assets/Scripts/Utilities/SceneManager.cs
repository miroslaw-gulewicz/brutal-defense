using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class SceneManager : MonoBehaviour
{
    public static SceneManager _Instance { get; private set; }
    public SceneManager __Instance { get; private set; }
    public LevelDefinition LevelDefinition { get => _levelDefinition; }

    [SerializeField]
    private string _menuScene;

    [SerializeField]
    private string _levelSceneName;

    private LevelDefinition _levelDefinition;

    private float _gameSpeed = 1f;

    public float GameSpeed { get { return _gameSpeed; } set { _gameSpeed = value; } }

    private void Awake()
    {
        if(FindObjectsOfType<SceneManager>().Length == 1)
            _Instance = this;

        UnityEngine.SceneManagement.SceneManager.sceneUnloaded += SceneUnloaded;
    }

    private void SceneUnloaded(UnityEngine.SceneManagement.Scene arg0)
    {
        PlayerProgressMonitor.Instance.SaveProgress();
    }

    public void LoadLevelScene(LevelDefinition levelDefinition)
    {
        _levelDefinition = levelDefinition;
        UnityEngine.SceneManagement.SceneManager.LoadScene(_levelSceneName);
    }

    public void LoadMainMenuScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(_menuScene);
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = _gameSpeed;
    }
}
