using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacementControl : MonoBehaviour
{
	[SerializeField] private WorldObjectSelectionManager worldObjectSelectionManager;

	[SerializeField] private Camera mainCamera;

	[SerializeField] private RaycastHit hit;

	[SerializeField] private GameObject gizmo;

	public GameObject Gizmo
	{
		get => gizmo;
		set
		{
			if (gizmo != null) gizmo.SetActive(false);
			gizmo = value;
		}
	}

	public WorldObjectSelectionManager WorldObjectSelectionManager
	{
		get => worldObjectSelectionManager;
	}

	public Action<GameObject> TargetAquired;

	[SerializeField] private LayerMask _interactionLayerMask;

	private bool passGizmoWhenNoObject;

	public void Setup(Action<GameObject> onObjectPlaced, int interactionLayerMask, bool higlightObjectsOnGrid = true)
	{
		Setup(null, onObjectPlaced, interactionLayerMask, higlightObjectsOnGrid, false);
	}


	public void Setup(GameObject gizmoPrefab, Action<GameObject> targetAquired, int interactionLayerMask,
		bool higlightObjectsOnGrid)
	{
		Setup(gizmoPrefab, targetAquired, interactionLayerMask, higlightObjectsOnGrid, false);
	}

	public void Setup(GameObject gizmoPrefab, Action<GameObject> targetAquired, int interactionLayerMask,
		bool higlightObjectsOnGrid, bool passGizmoWhenNoObject)
	{
		this.passGizmoWhenNoObject = passGizmoWhenNoObject;
		if (gizmoPrefab != null)
			Gizmo = ObjectCacheManager._Instance.GetObject(gizmoPrefab);
		else
			Gizmo = null;

		_interactionLayerMask = interactionLayerMask;
		TargetAquired = targetAquired;
		worldObjectSelectionManager.OnObjectSelected += OnTargetAquired;
		worldObjectSelectionManager.LayerMask = interactionLayerMask;
		if (higlightObjectsOnGrid)
			worldObjectSelectionManager.HighlightAgents(interactionLayerMask, true);
	}

	public void Reset()
	{
		worldObjectSelectionManager.HighlightAgents(_interactionLayerMask, false);
		worldObjectSelectionManager.OnObjectSelected -= OnTargetAquired;
		Gizmo = null;
		//gameObject.SetActive(false);
	}

	private void OnTargetAquired(GameObject gameObject)
	{
		if (Gizmo == null)
		{
			TargetAquired?.Invoke(gameObject);
			return;
		}

		if (gameObject.TryGetComponent<ISelectable>(out var selectable))
		{
			TargetAquired.Invoke(gameObject);
		}
		else if (passGizmoWhenNoObject)
		{
			TargetAquired.Invoke(gizmo);
		}
	}

	void LateUpdate()
	{
		if (gizmo == null) return;

		Ray ray = mainCamera.ScreenPointToRay(worldObjectSelectionManager.CursorPosition);

		if (Physics.Raycast(ray, out hit, 100.0f, _interactionLayerMask))
		{
			gizmo.transform.position = hit.point;
		}
	}
}