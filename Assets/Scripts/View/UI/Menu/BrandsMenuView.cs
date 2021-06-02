using UnityEngine;

public class BrandsMenuView : BaseMenuView
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;
    [Header("Elements")]
    [SerializeField] private GameObject _itemsGrid;

    private UIController _controller;
    private string _path = "ItemsPrefabs/BrandItem";


    private void Awake()
    {
        FindMyController();
        GeneralEvents.Current.OnBrandsLoaded += SetUpMenu;
    }

    public override void Hide()
    {
        if (!IsShow) return;
        _panel.gameObject.SetActive(false);
        IsShow = false;
    }
    public override void Show()
    {
        if (IsShow) return;
        _panel.gameObject.SetActive(true);
        IsShow = true;
    }

    public void FindMyController()
    {
        if (_controller == null)
        {
            _controller = FindObjectOfType<MainController>().GetController<UIController>();
        }
        _controller.AddView(this);
    }

    private void SetUpMenu(Brands brands)
    {
        foreach (BrandItem brand in brands.items)
        {
            BrandItemView view = Instantiate(Resources.Load<BrandItemView>(_path));
            view.transform.SetParent(_itemsGrid.transform);
            view.SetUp(brand);
        }
    }
}