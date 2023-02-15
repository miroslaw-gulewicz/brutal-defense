using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static IDestructable;

public class EnemyStatsInfoPanel : MonoBehaviour
{
    [SerializeField]
    private Image _image;

    [SerializeField]
    private EnemyStatItem _enemyStatItemPrefab;

    [SerializeField]
    private GameObject _content;

    [SerializeField]
    private BasicStatViewConfig[] statsConfig;

    [SerializeField]
    private ResistanceStatViewConfig[] resistanceStatViewConfig;

    private Dictionary<System.Object, EnemyStatItem> enemyStatItems;

    public void Start()
    {
        enemyStatItems = new Dictionary<System.Object, EnemyStatItem>();
        foreach (var item in statsConfig)
        {
            EnemyStatItem enemyStatItem = Instantiate(_enemyStatItemPrefab, _content.transform);
            EnemyStatItem.Init(enemyStatItem, item.sprite, item.tooltip, item.color);
            enemyStatItem.gameObject.SetActive(false);
            enemyStatItems.Add(item.Key, enemyStatItem);
        }


        foreach (var item in resistanceStatViewConfig)
        {
            EnemyStatItem enemyStatItem = Instantiate(_enemyStatItemPrefab, _content.transform);
            EnemyStatItem.Init(enemyStatItem, item.sprite, item.tooltip, item.color);
            enemyStatItem.gameObject.SetActive(false);
            enemyStatItems.Add(item.Key, enemyStatItem);
        }
        
        gameObject.SetActive(false); 
    }

    public void Display(EnemyObject enemy)
    {   
        gameObject.SetActive(true);
        foreach (var item in enemyStatItems.Values)
            item.gameObject.SetActive(false);

        _image.sprite = enemy.Sprite;
        foreach (var item in enemy.BasicStats)
        {
            ShowStat(item.stat, item.value);
        }

        foreach (var item in enemy.Resistance)
        {
            ShowStat(item.damageType, item.damageReduction);
        }
    }

    private void ShowStat(System.Object key, float value)
    {

        if (value == 0 || !enemyStatItems.TryGetValue(key, out EnemyStatItem enemyStatItem)) return;

        enemyStatItem.gameObject.SetActive(true);
        enemyStatItem.SetValue(value);
    }

    public void Close()
    {
        gameObject.SetActive(false);
    }

    [Serializable]
    public class ResistanceStatViewConfig : StatViewConfig
    {
        [SerializeField]
        public DamageType resistance;

        public override object Key => resistance;
    }
    
    [Serializable]
    public class BasicStatViewConfig : StatViewConfig
    {
        [SerializeField]
        public StatEnum stat;

        public override object Key => stat;
    }

    public class StatViewConfig
    {

        [SerializeField]
        public string tooltip;

        [SerializeField]
        public Sprite sprite;

        [SerializeField]
        public Color color;

        public virtual System.Object Key => null;
    }
}
