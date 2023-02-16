using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

[CreateAssetMenu(fileName = "Weapon", menuName = "ScriptableObjects/Weapon", order = 0)]
public class Weapon : ScriptableObject
{
    [Header("Weapon")]

    [SerializeField]
    protected float _range;

    [SerializeField]
    protected float fireSpeed;

    [SerializeField]
    protected Projectile projectile;

    [SerializeField]
    protected BaseEffectInflictor[] inflictors;

    [SerializeField]
    private SpriteLibraryAsset _weaponSpriteLibrary;

    public float Range { get => _range; }
    public float FireSpeed { get => fireSpeed; }
    public Projectile Projectile { get => projectile; }
    public BaseEffectInflictor[] Inflictors { get => inflictors; }
    public SpriteLibraryAsset WeaponSpriteLibrary { get => _weaponSpriteLibrary; }
}