using System;
using UnityEngine;
using UnityEngine.UI;
using static DescribleBehaviour;

[RequireComponent(typeof(Button))]
public class BuildTowerButton : MonoBehaviour, IDescrible, ISelectable
{
    [SerializeField]
    TurretObjectDef towerObjectDef;

    Button button;

    Action<BuildTowerButton> action;

    [SerializeField]
    private Image _image;

    [SerializeField]
    private Image _weaponImage;

    [SerializeField]
    private TMPro.TextMeshProUGUI description;

    [SerializeField]
    private TMPro.TextMeshProUGUI _cost;

    [SerializeField]
    private SelectableBehaviour selectableBehaviour;

    public Action<BuildTowerButton> Action { get => action; set => action = value; }
    public TurretObjectDef TowerObjectDef { get => towerObjectDef; set { towerObjectDef = value; Init(); } }

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        if(_image == null)
        {
            _image = GetComponent<Image>();
        }
        Init();
    }

    private void Init()
    {
        _image.sprite = towerObjectDef.Sprite;
        description.text = towerObjectDef.TowerName;
        _cost.text = towerObjectDef.Cost.ToString();
        _weaponImage.sprite = towerObjectDef.WeaponSpriteLibrary.GetSprite("Idle", "1");
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

    public void SetSelected(bool selected)
    {
        selectableBehaviour.gameObject.SetActive(selected);
    }
}
