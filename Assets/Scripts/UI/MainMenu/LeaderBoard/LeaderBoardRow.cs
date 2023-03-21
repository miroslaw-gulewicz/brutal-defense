using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardRow : MonoBehaviour
{
	[SerializeField] private TMPro.TextMeshProUGUI _posText;
	[SerializeField] private TMPro.TextMeshProUGUI _nameText;
	[SerializeField] private TMPro.TextMeshProUGUI _KillsText;

	internal void UpdateData(int position, string displayName, int statValue)
	{
		_posText.text = position.ToString();
		_nameText.text = displayName;
		_KillsText.text = statValue.ToString();
	}
}