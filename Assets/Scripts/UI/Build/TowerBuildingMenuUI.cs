using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuildingMenuUI : MonoBehaviour, ITab
{
	[Tooltip("Object provides callback info for turret placement on map")] [SerializeField]
	public ObjectPlacementControl placementControl;

	[SerializeField] TurretSpawner turretSpawner;

	[SerializeField] public Button cancelSelectionButton;

	[SerializeField] public Button applyBuilding;

	[SerializeField] EconomyManager economyManager;

	[Tooltip("Object acts as pointer")] [SerializeField]
	GameObject turretGizmoPrefab;

	[SerializeField] LayerMask _turretPlacementLayer;

	TurretObjectDef turretObjectDef;

	[SerializeField] Canvas towersCanvas;

	[SerializeField] Canvas buttonsCanvas;

	[SerializeField] StatsInfoPanel _statsInfoPanel;

	BuildTowerButton[] buildTowerButtons;

	private SelectObjectsManager<BuildTowerButton> buttonSelection = new SelectObjectsManager<BuildTowerButton>(true);

	public event Action OnCloseTab;


	private void Awake()
	{
		buildTowerButtons = GetComponentsInChildren<BuildTowerButton>();
		foreach (var item in buildTowerButtons)
		{
			item.Action = OnTowerSelect;
		}

		applyBuilding.onClick.AddListener(() => DoBuild(placementControl.Gizmo));
		cancelSelectionButton.onClick.AddListener(() => { OnCloseTab?.Invoke(); });
		economyManager.OnCoinsChanged += OnCoinsChanged;
	}

	private void Start()
	{
		EnableButtons(false);
		CancelBuildingSelection();
	}

	private void OnCoinsChanged()
	{
		bool canBuy;
		bool enableBuy = true;
		;
		foreach (var towerButton in buildTowerButtons)
		{
			canBuy = economyManager.HasSufficientCoins(towerButton.TowerObjectDef.Cost);
			towerButton.SetEnabled(canBuy);
			enableBuy &= canBuy;
		}

		applyBuilding.gameObject.SetActive(enableBuy);
	}

	public void ActivateBuildMenu()
	{
		towersCanvas.gameObject.SetActive(true);
		buttonsCanvas.gameObject.SetActive(true);
		cancelSelectionButton.gameObject.SetActive(true);

		OnCoinsChanged();
	}

	private void CancelBuildingSelection()
	{
		EnableButtons(false);
		towersCanvas.gameObject.SetActive(false);
		buttonsCanvas.gameObject.SetActive(false);
		placementControl.Reset();
		buttonSelection.ClearSelection();
	}

	private void EnableButtons(bool enabled)
	{
		cancelSelectionButton.gameObject.SetActive(enabled);
		applyBuilding.gameObject.SetActive(enabled);
		buttonsCanvas.gameObject.SetActive(enabled);
	}

	private void DoBuild(GameObject placement)
	{
		if (!economyManager.HasSufficientCoins(turretObjectDef.Cost)) return;

		BuildPlaceBehaviour bpb;

		if (!placement.TryGetComponent(out bpb)) return;

		if (bpb.PlacedTurret != null) return;

		Debug.Log("Building tower");
		//placementControl.Reset();
		economyManager.Buy(turretObjectDef.Cost);
		placementControl.WorldObjectSelectionManager.Select(turretSpawner.SpawnTurret(turretObjectDef,
	placement.transform.position).gameObject);
	}

	private void OnTowerSelect(BuildTowerButton obj)
	{
		bool canBuy = economyManager.HasSufficientCoins(obj.TowerObjectDef.Cost);
		applyBuilding.enabled = canBuy;
		buttonSelection.SelectObject(obj);

		if (canBuy)
		{
			placementControl.Reset();
			placementControl.Setup(OnPlacementChoosen, _turretPlacementLayer);
			turretObjectDef = obj.TowerObjectDef;
			applyBuilding.gameObject.SetActive(canBuy);
		}

		buttonsCanvas.gameObject.SetActive(true);
		towersCanvas.gameObject.SetActive(true);
		// cancelBuildButton.gameObject.SetActive(true);

		obj.SetSelected(true);
	}

	private void OnPlacementChoosen(GameObject obj)
	{
		DoBuild(obj);
	}

	private void CancelTower()
	{
		CancelBuildingSelection();
		foreach (var item in buildTowerButtons)
		{
			item.SetEnabled(true);
			//cancelBuildButton.gameObject.SetActive(false);
			cancelSelectionButton.gameObject.SetActive(true);
			towersCanvas.gameObject.SetActive(true);
			buttonsCanvas.gameObject.SetActive(true);
		}
	}

	public void Show()
	{
		ActivateBuildMenu();
	}

	public void Hide()
	{
		CancelBuildingSelection();
	}
}