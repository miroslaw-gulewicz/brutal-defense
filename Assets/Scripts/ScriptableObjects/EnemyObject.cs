using Effect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

[CreateAssetMenu(fileName = "EnemyObject", menuName = "ScriptableObjects/EnemyObject")]
public class EnemyObject : ScriptableObject
{
    [SerializeField]
    string enemyName;

    [SerializeField]
    short exp;

    [SerializeField]
    private Sprite sprite;

    [SerializeField]
    int coins;

    [SerializeField]
    private Enemy prefab;

    [SerializeField]
    private SpriteLibraryAsset spriteLibrary;

    [SerializeField]
    private Weapon weapon;

    [SerializeField]
    private Effects defaultEffects;

    [SerializeField]
    private ResistanceHolder resistance;

    [SerializeField]
    private BasicStatsHolder basicStats;

    [SerializeField]
    private BaseEffectInflictor[] effectInflicors;

    public Sprite Sprite { get => sprite; }
    public Effects DefaultEffects { get => defaultEffects; }
    public string EnemyName { get => enemyName; }
    public BasicStatsHolder BasicStats { get => basicStats; }
    public ResistanceHolder Resistance { get => resistance; }
    public int Coins { get => coins; }
    public EffectInflictor[] SelfInflictors { get => effectInflicors; }
    public Weapon Weapon { get => weapon; }
    public SpriteLibraryAsset SpriteLibrary { get => spriteLibrary; }
    public Enemy Prefab { get => prefab; }
}