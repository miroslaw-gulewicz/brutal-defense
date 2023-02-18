using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{

    public class PlayerProgressMonitor : MonoBehaviour
    {
        public static PlayerProgressMonitor Instance { get; private set; }
        public int BodyCount => _playersProgressData.KillCount;

        [SerializeField]
        private PlayerProgressData _playersProgressData;

        public float _sessionTime = 0;

        public void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        private void Start()
        {
            PlayerProgressMonitor.Instance.LoadProgress();
        }

        public void RegisterKill()
        {
            _playersProgressData.KillCount++;
        }

        public void LoadProgress()
        {
            PlayerProgressData.SaveData savedData = SaveSystem.LoadPlayerData();
            _playersProgressData.KillCount = savedData._killCount;
            _playersProgressData.PlayTime = savedData._playTime;
        }

        public void SaveProgress()
        {
            SaveSystem.SavePlayer(_playersProgressData);
        }
    }
}