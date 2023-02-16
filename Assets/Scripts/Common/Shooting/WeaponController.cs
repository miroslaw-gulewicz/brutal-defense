using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private SpriteLibrary _library;

    public void Fire()
    {
        _animator.SetTrigger("Fire");
    }

    public SpriteLibraryAsset SpriteLibrary { set => _library.spriteLibraryAsset = value; }
}
