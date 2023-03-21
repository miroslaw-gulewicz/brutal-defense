using Aim;
using System;
using System.Collections.Generic;
using UnityEngine;
using static TargetingMethodPanel;

public class TargetingMethodPanel : CollectionDisplayPanel<TowerShootingStrategyDropdownData, TargetingSystemUI>
{
	public event Action<TowerShootingStrategyDropdownData> ValueChanged;

	public void DisplayTargetingMethods(List<TowerShootingStrategyDropdownData> methods)
	{
		_selectedObjectManager.ClearSelection();
		foreach (var item in uiElements)
		{
			Destroy(item.gameObject);
		}

		uiElements.Clear();
		_elementToUi.Clear();
		foreach (var method in methods)
		{
			TargetingSystemUI targetingSystemUi = Instantiate(prefab, _panelRoot.transform);
			targetingSystemUi.Display(method.targeting, method.text, () => TargetingSelected(method));
			uiElements.Add(targetingSystemUi);
			_elementToUi.Add(method, targetingSystemUi);
		}
	}

	private void TargetingSelected(TowerShootingStrategyDropdownData method)
	{
		ValueChanged?.Invoke(method);
	}

	internal void Select(TowerShootingStrategyDropdownData targetingSystemType)
	{
		_selectedObjectManager.SelectObject(_elementToUi[targetingSystemType]);
	}

	[Serializable]
	public class TowerShootingStrategyDropdownData : TMPro.TMP_Dropdown.OptionData
	{
		[SerializeField] internal TargetingSystemType targeting;
	}
}