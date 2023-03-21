using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveEnemyInfo : MonoBehaviour, ISelectable
{
	[SerializeField] private Image _enemyImage;

	[SerializeField] private TextMeshProUGUI _enemiesCount;

	[SerializeField] private GameObject _selectableIndicator;

	public void DisplayInfo(Sprite sprite, int count)
	{
		_enemyImage.sprite = sprite;
		UpdateEnemiesCount(count);
	}

	public void SetSelected(bool selected)
	{
		_selectableIndicator.SetActive(selected);
	}

	public void UpdateEnemiesCount(int enemyCount)
	{
		_enemiesCount.text = enemyCount.ToString();
	}
}