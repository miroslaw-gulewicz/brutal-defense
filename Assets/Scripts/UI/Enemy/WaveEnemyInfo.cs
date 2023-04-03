using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WaveEnemyInfo : MonoBehaviour, ISelectable, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private Image _enemyImage;

	[SerializeField] private TextMeshProUGUI _enemiesCount;

	[SerializeField] private Button _infoButton;

	[SerializeField] private GameObject _selectableIndicator;

	private UnityAction<WaveEnemyInfo> _selectCallback;

	public void DisplayInfo(Sprite sprite, int count, UnityAction<WaveEnemyInfo> selectCallback)
	{
		_enemyImage.sprite = sprite;
		_selectCallback = selectCallback;
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

	public string GetActionDescription()
	{
		return "";
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		_selectCallback?.Invoke(null);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_selectCallback?.Invoke(this);
	}
}