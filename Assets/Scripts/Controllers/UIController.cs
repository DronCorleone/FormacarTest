using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : BaseController
{
    private BrandsMenuView _brandsMenu;
    private RimsMenuView _rimsMenu;

    public UIController(MainController main) : base (main)
    {

    }

    public override void Initialize()
    {
        base.Initialize();

        GameObject.Instantiate(Resources.Load<GameObject>("UIPrefab/UI"));

        SwitchUI(UIState.Brands);
    }

    public void AddView(BrandsMenuView view)
    {
        _brandsMenu = view;
    }
    public void AddView(RimsMenuView view)
    {
        _rimsMenu = view;
    }

    private void SwitchUI(UIState state)
    {
        switch (state)
        {
            case UIState.Brands:
                _brandsMenu.Show();
                _rimsMenu.Hide();
                break;
            case UIState.Rims:
                _brandsMenu.Hide();
                _rimsMenu.Show();
                break;
        }
    }
}