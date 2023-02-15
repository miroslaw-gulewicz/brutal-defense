using System;
using UnityEngine;

[Serializable]
public class BasicStatsManager : IStatMutable
{
    [SerializeField]
    private BasicStatsHolder basicStatsHolder;
    
    [SerializeField]
    private IResistanceHolder resistanceHolder;

    public BasicStatsManager()
    {
    }

    public BasicStatsManager(BasicStatsHolder basicStatsHolder, IResistanceHolder resistanceHolder)
    {
        this.BasicStatsHolder = basicStatsHolder;
        this.ResistanceHolder = resistanceHolder;
    }

    public BasicStatsHolder BasicStatsHolder { get => basicStatsHolder; set => basicStatsHolder = value; }
    public IResistanceHolder ResistanceHolder { get => resistanceHolder; set => resistanceHolder = value; }

}
