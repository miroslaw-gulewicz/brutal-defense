using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DescribleBehaviour;

public class UpgradeTowerButton : MonoBehaviour, IDescrible
{
	TurretBehaviour _currentTower;

	[SerializeField] TurretSpawner _turretSpawner;

	public TurretBehaviour CurrentTower
	{
		set => _currentTower = value;
	}

	public string GetActionDescription()
	{
		return _turretSpawner.GetNextLevelDescription(_currentTower);
	}
}