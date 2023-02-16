using UnityEngine;

public class SelectObjectsManager<T> where T : ISelectable
{
    private T obj;

    private static T notSelectedReference = default(T);

    private bool selectionEnabled = true;
    private bool _skipIfSelected;

    public SelectObjectsManager() : this (false)
    {
        
    }

    public SelectObjectsManager(bool skipIfSelected)
    {
        this.obj = notSelectedReference;
        this._skipIfSelected = skipIfSelected;
    }

    public void SelectObject(T newSelectedObject)
    {
        if (!selectionEnabled) return;

        obj?.SetSelected(false);

        if (!Object.Equals(obj, newSelectedObject))
        {
            obj = newSelectedObject;

            obj.SetSelected(true);
        }
        else
        {
            obj = notSelectedReference;
        }
    }

    public T GetSelectedValue()
    {
        return obj;
    }

    internal void ClearSelection()
    {
        obj?.SetSelected(false);
        obj = notSelectedReference;
    }

    internal bool IsObjectSelected()
    {
        return !Object.ReferenceEquals(obj, notSelectedReference);
    }

    internal void SetSelectionEnabled(bool selectionEnabled)
    {
        this.selectionEnabled = selectionEnabled;
        if (!selectionEnabled) ClearSelection();
    }


}
