using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsMenuController : MonoBehaviour, IMenu
{
    [SerializeField]
    private Canvas _menuCanvas;

    public void Close()
    {
        _menuCanvas.gameObject.SetActive(false);
    }

    public void Display()
    {
        _menuCanvas.gameObject.SetActive(true);
    }
}
