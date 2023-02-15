using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public float Range { get => _range; }
    public float FireSpeed { get => fireSpeed; }
    public Projectile Projectile { get => projectile; }
    public BaseEffectInflictor[] Inflictors { get => inflictors; }
}