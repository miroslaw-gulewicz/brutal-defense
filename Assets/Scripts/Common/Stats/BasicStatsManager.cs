using System;
using UnityEngine;

[Serializable]
public class BasicStatsManager : IStatMutable
{
	[SerializeField] private BasicStatsHolder basicStatsHolder;

	[SerializeField] private ResistanceHolder resistanceHolder;

	public BasicStatsManager()
	{
	}

	public BasicStatsManager(BasicStatsHolder basicStatsHolder, ResistanceHolder resistanceHolder)
	{
		this.BasicStatsHolder = basicStatsHolder;
		this.ResistanceHolder = resistanceHolder;
	}

	public BasicStatsHolder BasicStatsHolder
	{
		get => basicStatsHolder;
		set => basicStatsHolder = value;
	}

	public ResistanceHolder ResistanceHolder
	{
		get => resistanceHolder;
		set => resistanceHolder = value;
	}
}