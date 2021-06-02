using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DownloaderMenuView : BaseMenuView
{
    [Header("Panel")]
    [SerializeField] private GameObject _panel;
    [Header("Elements")]
    [SerializeField] private Slider _slider;

    private UIController _controller;


    private void Awake()
    {
        FindMyController();

        GeneralEvents.Current.OnDownloadFile += DownloadFile;
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

    public void DownloadFile(string url)
    {
        StartCoroutine(GetFile(url));
    }

    private IEnumerator GetFile(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            AsyncOperation request = www.SendWebRequest();

            while (!www.isDone)
            {
                _slider.value = request.progress;
                yield return null;
            }

            UIEvents.Current.DownloadComplete();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("Oops! Something wrong with loading image of brand");
            }
            else
            {
                File.WriteAllBytes(Application.persistentDataPath, www.downloadHandler.data);
            }
        }
    }
}
