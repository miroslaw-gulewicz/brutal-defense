using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageInfo : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshPro damageValue;

    public static readonly string PLUS = "+";
    public static readonly string MINUS = "-";

    internal void UpdateDamage(Color color, short amount)
    {
        damageValue.color = color;
        if (IsHealing(amount))
            damageValue.text = PLUS + (-amount).ToString();
        else
            damageValue.text = MINUS + amount.ToString();
    }

    public void UpdateText(Color color, string text)
    {
        damageValue.color = color;
        damageValue.text = text;
    }

    private static bool IsHealing(short amount)
    {
        return amount < 0;
    }
}
