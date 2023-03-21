using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionDisplayPanel<oBj, uiElem> : MonoBehaviour where uiElem : ISelectable
{
	[SerializeField] protected GameObject _panelRoot;

	[SerializeField] protected uiElem prefab;

	protected List<uiElem> uiElements = new List<uiElem>();
	protected Dictionary<oBj, uiElem> _elementToUi = new Dictionary<oBj, uiElem>();
	protected SelectObjectsManager<uiElem> _selectedObjectManager = new SelectObjectsManager<uiElem>();
}