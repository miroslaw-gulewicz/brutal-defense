using System.Collections.Generic;
using UnityEngine;

public class TabPanelUI
{
    private List<ITab> tabs = new List<ITab>();

    private ITab active;

    public void AddTab(ITab tab)
    {
        if (tabs.Contains(tab)) return;

        tabs.Add(tab);
    }

    public void ActivateTab(ITab tab)
    {
        if(Object.ReferenceEquals(tab, active)) return;

        tabs.ForEach(tab => tab.Hide());
        active = tab;
        active.Show();
    }

    internal void CloseAll()
    {
        tabs.ForEach(tab => tab.Hide());
        active = null;
    }
}
