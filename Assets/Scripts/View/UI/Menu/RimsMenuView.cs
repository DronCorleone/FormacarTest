using UnityEngine;
using UnityEngine.UI;

public class RimsMenuView : BaseMenuView
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;
    [Header("Elements")]
    [SerializeField] private Button _buttonBack;
    [SerializeField] private GameObject _itemsGrid;

    private UIController _controller;
    private string _path = "ItemsPrefabs/RimItem";


    private void Awake()
    {
        FindMyController();

        _buttonBack.onClick.AddListener(UIEvents.Current.ButtonBrandMenu);

        GeneralEvents.Current.OnRimsLoaded += SetUpMenu;
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

    private void SetUpMenu(Rims rims)
    {
        RimItemView[] oldRims = GetComponentsInChildren<RimItemView>(true);
        for (int i = 0; i < oldRims.Length; i++)
        {
            Destroy(oldRims[i].gameObject);
        }

        foreach (RimItem rim in rims.items)
        {
            RimItemView view = Instantiate(Resources.Load<RimItemView>(_path));
            view.transform.SetParent(_itemsGrid.transform);
            view.SetUp(rim);
        }
    }
}