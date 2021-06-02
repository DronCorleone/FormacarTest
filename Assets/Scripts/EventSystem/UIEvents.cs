using System;
using UnityEngine;

public class UIEvents : MonoBehaviour
{
    public static UIEvents Current;

    private void Awake()
    {
        Current = this;
    }


    public Action<int> OnButtonOpenBrand;
    public void ButtonOpenBrand(int id)
    {
        OnButtonOpenBrand?.Invoke(id);
        Debug.Log(id);
    }

    public Action OnButtonBrandMenu;
    public void ButtonBrandMenu()
    {
        OnButtonBrandMenu?.Invoke();
    }
}