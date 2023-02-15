using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelTileController : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _levelName;

    [SerializeField]
    private Button _playButton;

    [SerializeField]
    private Image _score;


    public static void Init(LevelTileController tile, LevelDefinition level, Action<LevelDefinition> onLevelSelected)
    {
        tile._levelName.text = level.LevelName;
        tile._playButton.onClick.AddListener(() => onLevelSelected(level));
        tile._score.fillAmount = 0.6f;
    }
}
