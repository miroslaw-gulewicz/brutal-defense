using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DescribleBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Func<string> _getDescription;

    private void Awake()
    {
        var desc = GetComponent<IDescrible>();
        if (desc != null)
            _getDescription = desc.getActionDescription;
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

    public interface IDescrible
    {
        string getActionDescription();
    }
}
