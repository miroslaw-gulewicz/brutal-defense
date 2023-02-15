using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static DescribleBehaviour;

[RequireComponent(typeof(Button))]
public class BuildTowerButton : MonoBehaviour, IDescrible
{
    [SerializeField]
    TurretObjectDef towerObjectDef;

    Button button;

    Action<BuildTowerButton> action;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private TMPro.TextMeshProUGUI description;

    [SerializeField]
    private TMPro.TextMeshProUGUI _cost;

    public Action<BuildTowerButton> Action { get => action; set => action = value; }
    public TurretObjectDef TowerObjectDef { get => towerObjectDef; set => towerObjectDef = value; }

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        if(_image == null)
        {
            _image = GetComponent<Image>();
        }
        _image.sprite = towerObjectDef.Sprite;
        description.text = towerObjectDef.TowerName;
        _cost.text = towerObjectDef.Cost.ToString();
    }

    private void OnClick()
    {
        Action.Invoke(this);
    }

    public void SetEnabled(bool enabled)
    {
        button.interactable = enabled;
    }

    public string getActionDescription()
    {
        return towerObjectDef.TowerName;
    }
}
