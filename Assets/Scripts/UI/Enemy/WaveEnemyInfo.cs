using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveEnemyInfo : MonoBehaviour
{
    [SerializeField]
    private Image _enemyImage;

    [SerializeField]
    private TextMeshProUGUI _enemiesCount;

    public void DisplayInfo(Sprite sprite, int count)
    {
        _enemyImage.sprite = sprite;
        UpdateEnemiesCount(count);
    }

    public void UpdateEnemiesCount(int enemyCount)
    {
        _enemiesCount.text = enemyCount.ToString();
    }
}
