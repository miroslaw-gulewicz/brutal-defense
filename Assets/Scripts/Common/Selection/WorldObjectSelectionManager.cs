using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class WorldObjectSelectionManager : MonoBehaviour
{   
    public event Action<GameObject> OnObjectSelected;

    public LayerMask LayerMask { get => layerMask; set => layerMask = value; }

    private PlayerWorldInteractionControls _playerWorldInteractionControls;

    [SerializeField]
    private Camera mainCamera;

    [SerializeField]
    private LayerMask layerMask;

    [SerializeField]
    private LayerMask _uiLayerMask;

    private Vector2 _position;

    private RaycastHit _hit;

    private Ray _ray;

    [SerializeField]
    private GameObject[] agentHolders;

    [SerializeField]
    private InputSystemUIInputModule uiInputSystem;

    // Layer , element on layer container
    private Dictionary<int, IHighlitableObjectHolder> agents;

    private float lastClick = 0;

    [Range(0, 0.5f)]
    [SerializeField]
    private float doubleClickTreshold = 0.3f;

    public Vector2 CursorPosition { get => _position ; }

    private void Awake()
    {
        _playerWorldInteractionControls = new PlayerWorldInteractionControls();
        _playerWorldInteractionControls.PlayerWorldInteractions.WorldClick.performed += OnWorldClick;
        agents = agentHolders.ToDictionary(x => x.GetComponent<IHighlitableObjectHolder>().Layer, x => x.GetComponent<IHighlitableObjectHolder>());
    }

    private void OnWorldClick(UnityEngine.InputSystem.InputAction.CallbackContext clickCallback)
    {
        if (uiInputSystem.IsPointerOverGameObject(0)) return;
        if (!clickCallback.performed || clickCallback.ReadValue<float>() != 1) return;

        Debug.Log("Clicked " + clickCallback.ToString());
        _position = _playerWorldInteractionControls.PlayerWorldInteractions.MousePosition.ReadValue<Vector2>();
        if (IsDoubleClick())
        {
            TrySelectObject(ref _position);
            lastClick = 0;
        }

        lastClick = Time.time;
    }

    private bool IsDoubleClick()
    {
        return Time.time - lastClick <= doubleClickTreshold;
    }

    protected void TrySelectObject(ref Vector2 position)
    {
        _ray = mainCamera.ScreenPointToRay(position);
       if (Physics.Raycast(_ray, out _hit, 100, LayerMask, QueryTriggerInteraction.Collide))
        {
            Debug.Log("Selected: " + _hit.transform.gameObject.name);
            OnObjectSelected.Invoke(_hit.transform.gameObject);
        } 
    }

    public void Select(TurretBehaviour currentTurret)
    {
        OnObjectSelected.Invoke(currentTurret.gameObject);
    }

    public void Deselect(TurretBehaviour currentTurret)
    {
        OnObjectSelected.Invoke(currentTurret.gameObject);
    }

    private void OnEnable()
    {
        _playerWorldInteractionControls?.Enable();
    }

    private void OnDisable()
    {
        _playerWorldInteractionControls?.Disable();
    }

    public void HighlightAgents(int layer, bool highlighted)
    {
        if(agents.TryGetValue(layer, out var agentHolder)){
            agentHolder.ForEachObject(agent => agent.HighLight(highlighted));
        } else
        {
            Debug.LogWarning("No object for highlight on layer " + layer);
        }
    }

    [ContextMenu("Highlight")]
    public void Highlight()
    {
        HighlightAgents(LayerMask.value, true);
    }

    [ContextMenu("UnHighlight")]
    public void UnHighlight()
    {
        HighlightAgents(LayerMask.value, false);
    }
}
