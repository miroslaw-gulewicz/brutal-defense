using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescribleBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Func<string> _getDescription;

	private void Awake()
	{
		var desc = GetComponent<IDescrible>();
		if (desc != null)
			_getDescription = desc.GetActionDescription;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		ToolTip.instance.SetText(_getDescription.Invoke());
		ToolTip.instance.Show(eventData.position);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		ToolTip.instance.Hide();
	}

	private void OnDisable()
	{
		ToolTip.instance.Hide();
	}

	public interface IDescrible
	{
		string GetActionDescription();
	}
}