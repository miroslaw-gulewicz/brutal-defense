using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace So.Common
{
    public class ItemDatabase<T> : ScriptableObject, ISerializationCallbackReceiver
    {
        public T[] items;
        public Dictionary<T, int> GetId = new Dictionary<T, int>();
        public Dictionary<int, T> GetItem = new Dictionary<int, T>();

        public void OnAfterDeserialize()
        {
            GetId = new Dictionary<T, int>();
            GetId.Clear();
            GetItem.Clear();
            for (int i = 0; i < items.Length; i++)
            {
                GetId.Add(items[i], i);
                GetItem.Add(i, items[i]);
            }
        }

        public void OnBeforeSerialize()
        {
            //nop
        }
    }
}