using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;

    public static SoundManager Instance => _instance;

    [SerializeField]
    public AudioSource audioSource;

    [SerializeField]
    public AudioClip[] clips;

    private void Awake()
    {
        _instance = this;
    }

    public void PlayAudio(SoundType soundType)
    {
        audioSource.PlayOneShot(clips[Random.Range(0, clips.Length)]);
    }

    public enum SoundType
    {
        MONSTER_KILLED
    }

}
