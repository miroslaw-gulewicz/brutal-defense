using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStatItem : MonoBehaviour, DescribleBehaviour.IDescrible
{
    [SerializeField]
    private Image _img;

    [SerializeField]
    private TMPro.TextMeshProUGUI _statsText;

    private string _tooltip;

    public static void Init(EnemyStatItem statItem, Sprite sprite, string tooltip, Color color)
    {
        statItem._img.sprite = sprite;
        statItem._img.color = color;
        statItem._statsText.color = color;
        statItem._tooltip = tooltip;
    }

    public string getActionDescription()
    {
        return _tooltip;
    }

    public void SetValue(float value)
    {
        _statsText.text = value.ToString();
    }
}
