using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlaceBehaviour : MonoBehaviour, IHighlightable, ISelectable
{
    [SerializeField]
    protected HighlightTrigger _highlightTrigger;

    [SerializeField]
    protected SelectableBehaviour _selectableBehaviour;

    [SerializeField]
    private ProximityTriggerBehaviour _proximityTriggerBehaviour;

    private TurretBehaviour placedTurret;

    public TurretBehaviour PlacedTurret { get => placedTurret; set => placedTurret = value; }


    private void Awake()
    {
        SetSelected(false);
        _proximityTriggerBehaviour = GetComponent<ProximityTriggerBehaviour>();
        _proximityTriggerBehaviour.TrigerEnterCallback = TriggerEntered;
        _proximityTriggerBehaviour.TrigerExitCallback = TriggerEntered;
    }

    private void TriggerEntered(Collider obj)
    {
        if (obj.TryGetComponent<TurretBehaviour>(out TurretBehaviour turret))
        if (turret == placedTurret) 
            turret = null;

        PlacedTurret = turret;  
        gameObject.SetActive(turret == placedTurret);
    }

    public void HighLight(bool highlighted)
    {
       _highlightTrigger.gameObject.SetActive(highlighted);
    }

    public void SetSelected(bool selected)
    {
        _selectableBehaviour.gameObject.SetActive(selected);
    }
}
