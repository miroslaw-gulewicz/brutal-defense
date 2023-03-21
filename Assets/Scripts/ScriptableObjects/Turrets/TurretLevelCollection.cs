using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TurretLevelCollection", menuName = "ScriptableObjects/TurretLevelCollection")]
public class TurretLevelCollection : ScriptableObject
{
	[SerializeField] TowerLevelDefinition[] turretLevels;

	public TowerLevelDefinition[] TurretLevels
	{
		get => turretLevels;
	}

	[Serializable]
	public class TowerLevelDefinition
	{
		[SerializeField] int level;

		[SerializeField] TurretObjectDef definition;

		public TurretObjectDef Definition
		{
			get => definition;
		}

		public int Level
		{
			get => level;
		}
	}
}