using System;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Current;

    private void Awake()
    {
        Current = this;
    }


    public Action<int> OnButtonRimsMenu;
    public void ButtonRimsMenu(int id)
    {
        OnButtonRimsMenu?.Invoke(id);
    }

    public Action OnButtonBrandMenu;
    public void ButtonBrandMenu()
    {
        OnButtonBrandMenu?.Invoke();
    }

    public Action<string> OnButtonDownloadFile;
    public void ButtonDownloadFile(string url)
    {
        OnButtonDownloadFile?.Invoke(url);
    }

    public Action OnDowloadComplete;
    public void DownloadComplete()
    {
        OnDowloadComplete?.Invoke();
    }
}