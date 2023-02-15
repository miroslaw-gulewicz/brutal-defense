using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DescribleBehaviour;

public class EnemyMiniatureTile : MonoBehaviour, IDescrible
{
    [SerializeField]
    public Image _img;

    [SerializeField]
    public TMPro.TextMeshProUGUI _nameText;

    [SerializeField]
    public Button tilePressControll;

    private EnemyObject _enemyObject;

    public EnemyObject EnemyObject { get => _enemyObject;}

    public static void Init(EnemyMiniatureTile tile, EnemyObject enemy, Action<EnemyObject> clickCallback)
    {
        tile._img.sprite = enemy.Sprite;
        tile._nameText.text = enemy.EnemyName;
        tile.tilePressControll.onClick.RemoveAllListeners();
        tile.tilePressControll.onClick.AddListener(() => clickCallback(enemy));
        tile._enemyObject = enemy;
    }

    public string getActionDescription()
    {
        return _enemyObject.EnemyName;
    }

    internal void SetSelected(bool selected)
    {
        GetComponent<Image>().enabled = selected;
    }
}
