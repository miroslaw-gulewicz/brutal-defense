using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProgressData", menuName = "ScriptableObjects/Player/PlayerProgressData")]
public class PlayerProgressData : ScriptableObject
{
	[SerializeField] private int _killCount;

	[SerializeField] private float _playTime;

	public float PlayTime
	{
		get => _playTime;
		set => _playTime = value;
	}

	public int KillCount
	{
		get => _killCount;
		set => _killCount = value;
	}


	[Serializable]
	public struct SaveData
	{
		public int _killCount;
		public float _playTime;
	}
}