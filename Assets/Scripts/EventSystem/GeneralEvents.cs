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


    public Action OnLoadBrands;
    public void LoadBrands()
    {
        OnLoadBrands?.Invoke();
    }

    public Action<int> OnLoadRims;
    public void LoadRims(int brandID)
    {
        OnLoadRims?.Invoke(brandID);
    }

    public Action<Brands> OnBrandsLoaded;
    public void BrandsLoaded(Brands brands)
    {
        OnBrandsLoaded?.Invoke(brands);
    }

    public Action<Rims> OnRimsLoaded;
    public void RimsLoaded(Rims rims)
    {
        OnRimsLoaded?.Invoke(rims);
    }

    public Action<string> OnDownloadFile;
    public void DownloadFile(string url)
    {
        OnDownloadFile?.Invoke(url);
    }
}