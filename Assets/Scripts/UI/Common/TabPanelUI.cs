using System;
using System.Collections.Generic;
using UnityEngine;

public class TabPanelUI
{
	private List<ITab> tabs = new List<ITab>();

	private ITab active;

	public event Action<ITab> OnActivateTab;

	public void AddTab(ITab tab)
	{
		if (tabs.Contains(tab)) return;

		tabs.Add(tab);
	}

	public void ActivateTab(ITab tab)
	{
		if (UnityEngine.Object.ReferenceEquals(tab, active)) return;

		CloseAll();
		active = tab;
		active.Show();
		OnActivateTab?.Invoke(active);
	}

	internal void CloseAll()
	{
		tabs.ForEach(tab => tab.Hide());
		active = null;
	}
}