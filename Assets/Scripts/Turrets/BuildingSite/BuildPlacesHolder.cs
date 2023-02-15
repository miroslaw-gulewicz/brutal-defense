using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlacesHolder : MonoBehaviour, IHighlitableObjectHolder
{
    [SerializeField]
    private LayerMask _buildPlacesLayer;

    public int Layer => _buildPlacesLayer.value;

    List<BuildPlaceBehaviour> _buildPlaces = new List<BuildPlaceBehaviour>();

    public void ForEachObject(Action<IHighlightable> cmd)
    {
        _buildPlaces.ForEach(cmd);
    }

    // Start is called before the first frame update
    void Start()
    {
        _buildPlaces.AddRange(FindObjectsOfType<BuildPlaceBehaviour>());
        ForEachObject((higlightable) => higlightable.HighLight(false));
    }
}
