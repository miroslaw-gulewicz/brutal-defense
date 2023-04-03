using Effect;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpawnEnemyObjectsCommand",
	menuName = "ScriptableObjects/Commands/SpawnEnemyObjectsCommand")]
public class SpawnEnemyObjectsCommand : SpawnObjectCommand
{
	[SerializeField] EnemyObject enemyObject;


	public override void DoCommand(IEffectContextHolder context)
	{
		GameObject gameObject = ObjectCacheManager._Instance.GetObject(enemyObject.Prefab.gameObject);
		gameObject.transform.position = context.Mono.transform.position;
		var enemy = gameObject.GetComponent<Enemy>();
		enemy.EnemyObject = enemyObject;

		if (context.Mono.TryGetComponent(out Enemy parentEnemy))
			enemy.Initialize(parentEnemy);
		


		if(context.Mono.TryGetComponent(out Movement movement))
			enemy.GetComponent<Movement>().Initialize(movement);
		
		gameObject.SetActive(true);
		parentEnemy.DestroyCallBack(IDestructable.StatusSource.SPAWNED, enemy);
	}
}