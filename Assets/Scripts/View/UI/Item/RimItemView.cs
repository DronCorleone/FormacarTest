using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RimItemView : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image _image;
    [SerializeField] private Text _title;
    [SerializeField] private Button _buttonDownload;

    private RimItem _rim;


    public void SetUp(RimItem rim)
    {
        _rim = rim;
        StartCoroutine(GetImage(rim.image.src));
        _title.text = rim.model;
        _buttonDownload.onClick.RemoveAllListeners();
        _buttonDownload.onClick.AddListener(() => UIEvents.Current.ButtonDownloadFile(rim.bundle));
    }

    private IEnumerator GetImage(string url)
    {
        using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("Oops! Something wrong with loading image of brand");
            }
            else
            {
                SetUpImage(DownloadHandlerTexture.GetContent(www));
            }
        }
    }

    private void SetUpImage(Texture2D texture)
    {
        _image.sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), Vector2.zero);
        _image.rectTransform.sizeDelta = new Vector2(_rim.image.width, _rim.image.height);
    }
}