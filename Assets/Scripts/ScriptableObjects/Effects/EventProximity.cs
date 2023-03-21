using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EventProximityAffector", menuName = "ScriptableObjects/EventProximityAffector")]
public class EventProximity : EffectInflictorAgent
{
	[SerializeField] private EventProximityAffector _eventproximityAffectorPrefab;

	[SerializeField] private bool cancelEffectOnExit = false;

	[SerializeField] private SpawnEnemyObjectsCommand _spawnObjectsCommand;

	[SerializeField] private float proximity;

	protected override GameObject CreateEffect(GameObject target)
	{
		EventProximityAffector proximityAffector = Instantiate(_eventproximityAffectorPrefab);
		proximityAffector.CancelEffectOnExit = cancelEffectOnExit;
		proximityAffector.Command = _spawnObjectsCommand;
		proximityAffector.EffectRadius = proximity;
		return proximityAffector.gameObject;
	}
}