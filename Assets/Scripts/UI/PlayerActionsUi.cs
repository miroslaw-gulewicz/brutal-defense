using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionsUi : MonoBehaviour
{
    [SerializeField]
    private Button buildButton;

    [SerializeField]
    private Button magicButton;

    [SerializeField]
    private BuildTowerButton _selectedTowerButton;

    [SerializeField]
    private Canvas canvasPlayerActions;

    [SerializeField]
    private TowerBuildingMenuUI towerBuildingMenuUI;

    [SerializeField]
    private SpellBookUI spellBookUI;

    [SerializeField]
    private TowerInfoUI towerInfoUI;

    [SerializeField]
    private TabPanelUI tabs;

    [SerializeField]
    private WorldObjectSelectionManager WorldObjectSelectionManager;

    private SelectObjectsManager<TurretBehaviour> turretSelection;

    private LayerMask _default;

    public void Awake()
    {
        turretSelection = new SelectObjectsManager<TurretBehaviour>();
        tabs = new TabPanelUI();

        tabs.AddTab(towerBuildingMenuUI);
        tabs.AddTab(spellBookUI);

        buildButton.onClick.AddListener(HideButtons);
        buildButton.onClick.AddListener(() => tabs.ActivateTab(towerBuildingMenuUI));

        magicButton.onClick.AddListener(HideButtons);
        magicButton.onClick.AddListener(() => tabs.ActivateTab(spellBookUI));

        _selectedTowerButton.Action = (btb) => ShowTurretDetails();

        tabs.CloseAll();

        towerBuildingMenuUI.cancelSelectionButton.onClick.AddListener(ShowButtons);
        towerBuildingMenuUI.cancelSelectionButton.onClick.AddListener(tabs.CloseAll);

        spellBookUI.CancelSpellBookButton.onClick.AddListener(ShowButtons);
        spellBookUI.CancelSpellBookButton.onClick.AddListener(tabs.CloseAll);

        WorldObjectSelectionManager.OnObjectSelected += OnObjectSelected;
        _default = WorldObjectSelectionManager.LayerMask;
        towerInfoUI.Hide();
        spellBookUI.Hide();
    }

    private void OnObjectSelected(GameObject obj)
    {
        if(obj == null) turretSelection.ClearSelection();
        
        if (!obj.TryGetComponent(out TurretBehaviour turret)) return;
        
        turretSelection.SelectObject(turret);
        
        if(turretSelection.IsObjectSelected())
        {
            _selectedTowerButton.TowerObjectDef = turret.TurretObject;
            _selectedTowerButton.SetSelected(true);
        } else
        {
            towerInfoUI.Hide();
        }

        _selectedTowerButton.gameObject.SetActive(turretSelection.IsObjectSelected());
    }

    public void ShowTurretDetails()
    {
        towerInfoUI.DisplayTurretInfo(turretSelection.GetSelectedValue());
        towerInfoUI.Show();
    }

    public void ShowButtons()
    {
        RestoreSelection();
        ActivateButtons(true);
    }

    public void RestoreSelection()
    {
        WorldObjectSelectionManager.LayerMask = _default;
    }

    public void HideButtons()
    {
        RestoreSelection();
        ActivateButtons(false);
    }

    private void ActivateButtons(bool active)
    {
        canvasPlayerActions.gameObject.SetActive(active);
    }
}
