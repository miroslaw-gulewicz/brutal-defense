using Aim;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingStrategySwitcher : MonoBehaviour
{
	[SerializeField] private TargetingSystemType _systemType;

	[SerializeField] private EnemyObject _enemyObject;

	private GameObject _commonTarget;

	[SerializeField] private Shooting _shooting;

	[SerializeField] private bool autoInit;

	public Shooting Shooting
	{
		get => _shooting;
		set { _shooting = value; }
	}

	public void Start()
	{
		if (autoInit) SetupTargetingSystem();
	}

	public EnemyObject EnemyObject
	{
		get => _enemyObject;
		set => _enemyObject = value;
	}

	public TargetingSystemType SystemType
	{
		get => _systemType;
		set => _systemType = value;
	}

	public GameObject CommonTarget
	{
		get => _commonTarget;
		set => _commonTarget = value;
	}

	[ContextMenu("SetupTargetingSystem")]
	public void SetupTargetingSystem()
	{
		var targetingSystem = Shooting.TargetingSystem;
		Shooting.TargetingSystem = TargetingSystemFactory.Supply(SystemType);
		TargetingSystemType targetingSystemType =
			TargetingSystemFactory.AsTargetingSystemType(Shooting.TargetingSystem);
		switch (targetingSystemType)
		{
			case TargetingSystemType.PRIORITY:
				PriorityTargetingSystem priorityTargetingSystem = Shooting.TargetingSystem as PriorityTargetingSystem;
				priorityTargetingSystem.TargetDefinition = EnemyObject;
				priorityTargetingSystem.Setup(targetingSystem);
				break;
			case TargetingSystemType.MANUAL:
				ManualTargetingSystem manualTargetingSystem = Shooting.TargetingSystem as ManualTargetingSystem;
				manualTargetingSystem.ManualTarget = _commonTarget;
				break;


			default: break;
		}
	}
}