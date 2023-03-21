using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : IObjectProvider
{
	GameObject[] _objectCache;

	GameObject _objectPrefab;

	public ObjectPool(Object key, GameObject prefab, int size)
	{
		_objectPrefab = prefab;
		_objectCache = new GameObject[size];

		for (int i = 0; i < size; i++)
		{
			var obj = GameObject.Instantiate(_objectPrefab);
			_objectCache[i] = obj;
			ReturnToPool(obj);
		}
	}

	public ObjectPool(GameObject prefab, int size) : this(prefab, prefab, size)
	{
	}

	public GameObject GetObject()
	{
		for (int i = 0; i < _objectCache.Length; i++)
		{
			if (!_objectCache[i].gameObject.activeSelf)
			{
				return _objectCache[i];
			}
		}

		return null;
	}


	public void ReturnToPool(GameObject instance)
	{
		instance.gameObject.SetActive(false);
	}

	internal void Resize(int additionalSize = 5)
	{
		var newCache = new GameObject[_objectCache.Length + additionalSize];
		int index = 0;
		for (; index < _objectCache.Length; index++)
			newCache[index] = _objectCache[index];
		for (; index < newCache.Length; index++)
		{
			newCache[index] = GameObject.Instantiate(_objectPrefab);
			ReturnToPool(newCache[index]);
		}

		_objectCache = newCache;
	}
}

public interface IObjectProvider
{
	GameObject GetObject();
	void ReturnToPool(GameObject instance);
}