using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyInfoUI : MonoBehaviour
{
    [SerializeField]
    EconomyManager economyManager;

    [SerializeField]
    GameManager gameManager;

    [SerializeField]
    TMPro.TextMeshProUGUI gold;

    [SerializeField]
    TMPro.TextMeshProUGUI maxMana;

    [SerializeField]
    TMPro.TextMeshProUGUI currentMana;

    [SerializeField]
    TMPro.TextMeshProUGUI maxHp;

    [SerializeField]
    TMPro.TextMeshProUGUI currentHp;


    private void Start()
    {
        gameManager.OnStatsChanged += OnStatsChanged;
        economyManager.OnCoinsChanged += OnStatsChanged;
        OnStatsChanged();
    }

    private void OnStatsChanged()
    {
        gold.text = economyManager.Coins.ToString();
        maxHp.text = gameManager.BasicStats[StatEnum.HP].ToString();
        currentHp.text = gameManager.BasicStats[StatEnum.CURRENT_HP].ToString();
        maxMana.text = gameManager.BasicStats[StatEnum.MANA].ToString();
        currentMana.text = gameManager.BasicStats[StatEnum.CURRENT_MANA].ToString(); 
    }
}
