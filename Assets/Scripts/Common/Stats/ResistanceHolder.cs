using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using static ResistanceHolder;
using System.Collections;

[Serializable]
public class ResistanceHolder : IResistanceHolder, ISerializationCallbackReceiver, IEnumerable<ResistanceDef>
{
	[SerializeField] List<ResistanceDef> resistanceStats;

	private Dictionary<IDestructable.DamageType, float> resistances;

	public float this[IDestructable.DamageType index]
	{
		get
		{
			float res = 0;
			resistances.TryGetValue(index, out res);
			return res;
		}
	}

	public ResistanceHolder(ResistanceHolder resistance)
	{
		this.resistances = resistance.resistances.ToDictionary(rd => rd.Key, rd => rd.Value);
	}

	public IEnumerator<ResistanceDef> GetEnumerator()
	{
		return resistanceStats.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return resistanceStats.GetEnumerator();
	}

	public void OnBeforeSerialize()
	{
		//nop
	}

	[Serializable]
	public class ResistanceDef
	{
		[SerializeField] internal IDestructable.DamageType damageType;

		[SerializeField] [Range(-100, 100)] internal float damageReduction;
	}

	public void OnAfterDeserialize()
	{
		if (resistanceStats != null)
			resistances = resistanceStats.ToDictionary(rd => rd.damageType, rd => rd.damageReduction);
	}
}