using Aim;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TargetingSystemUI : MonoBehaviour, ISelectable
{
	[SerializeField] private TargetingSystemType _targetingSystemType;

	[SerializeField] private TextMeshProUGUI _systemName;

	[SerializeField] private GameObject _selectionIndicator;

	public TargetingSystemType TargetingSystemType
	{
		get => _targetingSystemType;
		set => _targetingSystemType = value;
	}

	public void Display(TargetingSystemType systemType, string systemName,
		UnityEngine.Events.UnityAction OnValueChanged)
	{
		_targetingSystemType = systemType;
		_systemName.text = systemName;
		GetComponent<Button>().onClick.AddListener(OnValueChanged);
	}

	public void SetSelected(bool selected)
	{
		_selectionIndicator.SetActive(selected);
	}
}