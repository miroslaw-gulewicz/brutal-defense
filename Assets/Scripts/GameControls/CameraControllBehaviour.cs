using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControllBehaviour : MonoBehaviour
{
	RTSCameraImput inputs;

	[SerializeField] Vector2 lastRreadPosition = Vector2.zero;

	bool canRotate = false;
	bool canMove = false;

	[SerializeField] private Vector3 desiredPos = Vector3.zero;

	private float desiredRot = 0;
	public float rotSpeed = 250;
	public float damping = 10;

	public float dragSpeed = 15f;

	[SerializeField] private float _processDelay = 0.2f;
	private float _timer = 0.0f;


	void Awake()
	{
		inputs = new RTSCameraImput();

		inputs.PlayerActions.HoldClick.performed += HoldClick_performed;
		inputs.PlayerActions.HoldClick.canceled += HoldClick_canceled;

		inputs.PlayerActions.DragClick.performed += DragClick_performed;
		inputs.PlayerActions.DragClick.canceled += DragClick_canceled;
	}

	private void DragClick_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		canMove = false;
	}

	private void DragClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		canMove = true;
		lastRreadPosition = inputs.PlayerActions.MouseLook.ReadValue<Vector2>();
	}

	private void HoldClick_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		canRotate = false;
	}

	private void HoldClick_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
	{
		canRotate = true;
		lastRreadPosition = inputs.PlayerActions.MouseLook.ReadValue<Vector2>();
	}

	private void OnEnable()
	{
		inputs.Enable();
	}

	private void OnDisable()
	{
		inputs.Disable();
	}


	// Update is called once per frame
	void LateUpdate()
	{
		if (!(canRotate ^ canMove)) return;

		_timer += Time.deltaTime;

		if (_timer < _processDelay) return;
		_timer -= _processDelay;

		Vector2 _directionRead = inputs.PlayerActions.MouseLook.ReadValue<Vector2>();
		if (canRotate)
		{
			desiredRot += (_directionRead - lastRreadPosition).x * rotSpeed * Time.deltaTime;

			var desiredRotQ = Quaternion.Euler(transform.eulerAngles.x, desiredRot, transform.eulerAngles.z);
			transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * damping);
		}

		if (canMove) // move x
		{
			desiredPos = transform.position - transform.forward * (_directionRead - lastRreadPosition).y;
			desiredPos.y = transform.position.y;
			transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * dragSpeed);
		}

		if (canMove) // move z
		{
			desiredPos = transform.position - transform.right * (_directionRead - lastRreadPosition).x;
			desiredPos.y = transform.position.y;
			transform.position = Vector3.Lerp(transform.position, desiredPos, Time.deltaTime * dragSpeed);
		}

		lastRreadPosition = _directionRead;
	}
}