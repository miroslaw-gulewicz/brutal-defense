using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
	[SerializeField] GameObject _coinPrefab;

	[SerializeField] GameObject _bonesDropEffect;

	[SerializeField] GameObject _towerDestroyEffectPrefab;

	internal void SpawnCoin(Vector3 position, int number = 1)
	{
		SpawnPrefab(_coinPrefab, position);
	}

	internal void BonesEffect(Vector3 position, int number = 1)
	{
		for (int i = 0; i < number; i++)
		{
			SpawnPrefab(_bonesDropEffect, position);
		}
	}

	public void TowerExplosion(Vector3 position)
	{
		SpawnPrefab(_towerDestroyEffectPrefab, position);
	}


	private void SpawnPrefab(GameObject prefab, Vector3 position)
	{
		GameObject go = ObjectCacheManager._Instance.GetObject(prefab);
		go.transform.position = position;
		go.SetActive(true);
	}
}