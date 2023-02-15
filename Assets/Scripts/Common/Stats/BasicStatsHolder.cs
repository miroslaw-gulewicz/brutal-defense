using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using static BasicStatsHolder;
using UnityEngine.Events;

[Serializable]
public class BasicStatsHolder : ISerializationCallbackReceiver, IEnumerable<Stat>
{
    [SerializeField]
    private Stat[] stats;

    private Dictionary<StatEnum, Stat> statDict;

    public float this[StatEnum stat] 
     {
        get
        {
            Debug.Log(stat);
            statDict.TryGetValue(stat, out Stat val);
            
            return val.value;
        }
     }

    public Stat this[int stat]
    {
        get
        {
            statDict.TryGetValue((StatEnum)stat, out Stat val);
            return val;
        }
    }


    public event Action CurrentHpUpdated;
    public event Action AttackSpeedUpdated;
    public event Action MovingSpeedUpdated;

    public BasicStatsHolder(BasicStatsHolder template)
    {
        statDict = template.statDict.ToDictionary(st => st.Key, st => new Stat(st.Value));
        Stat stat;

        if (statDict.TryGetValue(StatEnum.ATTACK_SPEED, out stat))
            stat.valueChanged = () => AttackSpeedUpdated?.Invoke();

        if(statDict.TryGetValue(StatEnum.MOVING_SPEED, out stat))
            stat.valueChanged = () => MovingSpeedUpdated?.Invoke();

        if (statDict.TryGetValue(StatEnum.CURRENT_HP, out stat))
            stat.valueChanged = () => CurrentHpUpdated?.Invoke();
    }

    public float StartHP { get => this[(int)StatEnum.HP].value;  }
    public float CurrentHp { 
        get => this[(int)StatEnum.CURRENT_HP].value; 
        set  {
            this[(int)StatEnum.CURRENT_HP].Value = value; 
        } 
    }
    public float Speed { get => this[(int)StatEnum.MOVING_SPEED].value;}

    public float AttackSpeed { 
        get => this[(int)StatEnum.ATTACK_SPEED].value;
        set
        {
            this[(int)StatEnum.ATTACK_SPEED].Value = value;
        }
    }

    public void OnAfterDeserialize()
    {
        if (stats != null)
            statDict = stats.ToDictionary(st => st.stat);
    }

    public void OnBeforeSerialize()
    {

    }

    public IEnumerator<Stat> GetEnumerator()
    {
        return statDict.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return statDict.Values.GetEnumerator();
    }

    [Serializable]
    public class Stat
    {
        [SerializeField]
        public StatEnum stat;
        [SerializeField]
        public float value;

        public Action valueChanged;

        public Stat(Stat stat)
        {
            this.stat = stat.stat;
            this.value = stat.value;
            this.valueChanged = stat.valueChanged;
        }

        public float Value { get => value; set {
                this.value = value;
                valueChanged?.Invoke();
            }
        }
    }
}
