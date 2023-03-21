using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCacheManager : MonoBehaviour, IObjectSuplier
{
	#region Singleton

	private static ObjectCacheManager __Instance;

	public static ObjectCacheManager _Instance
	{
		get => __Instance;
	}

	#endregion

	[SerializeField] ObjectCacheDefs[] defs;

	Dictionary<UnityEngine.Object, ObjectPool> prefabPools;

	private readonly Func<ObjectCacheDefs, UnityEngine.Object> ToKey = e => e.key == null ? e.prefab : e.key;
	private readonly Func<ObjectCacheDefs, ObjectPool> ToElement = e => new ObjectPool(e.prefab, e.count);

	public void Awake()
	{
		__Instance = this;
		prefabPools = defs.ToDictionary(keySelector: ToKey, elementSelector: ToElement);
	}

	public GameObject GetObject(UnityEngine.Object key, GameObject prefab = null, bool activate = true)
	{
		if (prefabPools.TryGetValue(key, out var pool))
		{
			var obj = pool.GetObject();
			if (obj == null)
			{
				Debug.LogWarning("Not enough objects in pool " + key.name);
				pool.Resize();
				obj = pool.GetObject();
			}

			if (activate)
				obj.SetActive(true);
			return obj;
		}
		else
		{
			Debug.LogWarning("Pool not exists for " + key.name);
			if (prefab != null)
			{
				Debug.Log("Creating pool for" + prefab.name);
				CreateNewPool(new ObjectCacheDefs()
				{
					key = key,
					prefab = prefab,
					count = 5
				});
			}
		}

		return null;
	}

	public GameObject GetObject(UnityEngine.Object key, bool activate = true)
	{
		return GetObject(key, null, activate);
	}

	public GameObject GetObject(UnityEngine.Object key)
	{
		return GetObject(key, null, true);
	}

	public void CreateNewPool(ObjectCacheDefs definition)
	{
		if (prefabPools.ContainsKey(definition.key) || prefabPools.ContainsKey(definition.prefab))
		{
			Debug.LogWarning("Cache exists for " + definition.key + " or " + definition.prefab);
		}

		prefabPools.Add(ToKey.Invoke(definition), ToElement.Invoke(definition));
	}

	[Serializable]
	public class ObjectCacheDefs
	{
		[SerializeField] public UnityEngine.Object key;

		[SerializeField] public GameObject prefab;

		[SerializeField] public int count;
	}
}