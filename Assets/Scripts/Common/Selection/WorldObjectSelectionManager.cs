using System;
using UnityEngine;
using UnityEngine.InputSystem.UI;

public class WorldObjectSelectionManager : MonoBehaviour
{
	public event Action<GameObject> OnObjectSelected;

	public LayerMask LayerMask
	{
		get => layerMask;
		set => layerMask = value;
	}

	private PlayerWorldInteractionControls _playerWorldInteractionControls;

	[SerializeField] private Camera mainCamera;

	[SerializeField] private LayerMask layerMask;

	private Vector2 _position;

	private RaycastHit[] _hit = new RaycastHit[1];

	private Ray _ray;

	[SerializeField] private InputSystemUIInputModule uiInputSystem;

	private float lastClick = 0;

	[SerializeField] private bool doubleClickToSelect;

	[Range(0, 0.5f)] [SerializeField] private float doubleClickTreshold = 0.3f;

	public Vector2 ClickPosition
	{
		get => _position;
	}

	private void Awake()
	{
		_playerWorldInteractionControls = new PlayerWorldInteractionControls();
		_playerWorldInteractionControls.PlayerWorldInteractions.WorldClick.performed += OnWorldClick;
	}

	private void OnWorldClick(UnityEngine.InputSystem.InputAction.CallbackContext clickCallback)
	{
		if (uiInputSystem.IsPointerOverGameObject(0)) return;
		if (!clickCallback.performed || clickCallback.ReadValue<float>() != 1) return;

		_position = _playerWorldInteractionControls.PlayerWorldInteractions.MousePosition.ReadValue<Vector2>();
		if (IsSelectClick())
		{
			TrySelectObject(ref _position);
			lastClick = 0;
		}

		lastClick = Time.unscaledTime;
	}

	private bool IsSelectClick()
	{
		if (doubleClickToSelect)
			return Time.unscaledTime - lastClick <= doubleClickTreshold;
		else
			return true;
	}

	protected void TrySelectObject(ref Vector2 position)
	{
		_ray = mainCamera.ScreenPointToRay(position);
		if (Physics.RaycastNonAlloc(_ray,  _hit, 100, LayerMask, QueryTriggerInteraction.Collide) > 0)
		{
			OnObjectSelected?.Invoke(_hit[0].transform.gameObject);
		}
	}

	public void Select(GameObject obj)
	{
		OnObjectSelected?.Invoke(obj);
	}

	public void Deselect(GameObject obj)
	{
		OnObjectSelected?.Invoke(obj);
	}

	private void OnEnable()
	{
		_playerWorldInteractionControls?.Enable();
	}

	private void OnDisable()
	{
		_playerWorldInteractionControls?.Disable();
	}
}