using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObjectSuplier
{
    public GameObject GetObject(Object key, GameObject prefab = null, bool activate = true);
}