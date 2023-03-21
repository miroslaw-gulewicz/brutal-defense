using System;

public interface ITab
{
	public event Action OnCloseTab;
	public void Show();
	public void Hide();
}