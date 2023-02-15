using UnityEngine;

public interface ISelectable
{
    public GameObject gameObject { get; }
    void SetSelected(bool selected);
}
