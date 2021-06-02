using System.Collections;
using System;
using UnityEngine;

public class GeneralEvents : MonoBehaviour
{
    public static GeneralEvents Current;

    private void Awake()
    {
        Current = this;
    }


    public Action<Brands> OnBrandsLoaded;
    public void BrandsLoaded(Brands brands)
    {
        OnBrandsLoaded?.Invoke(brands);
    }
}