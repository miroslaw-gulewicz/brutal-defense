using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDefinition", menuName = "ScriptableObjects/LevelDefinition")]
public class LevelDefinition : ScriptableObject
{
    [SerializeField]
    private string levelName;

    [SerializeField]
    private WaveDefinition[] waves;

    [SerializeField]
    private Sprite levelCover;

    [SerializeField]
    private int _startCoins;

    public string LevelName { get => levelName; }
    public WaveDefinition[] Waves { get => waves; }
    public Sprite LevelCover { get => levelCover; }
    public int StartCoins { get => _startCoins; }
}