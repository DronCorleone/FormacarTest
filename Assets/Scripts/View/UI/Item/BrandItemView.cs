using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class BrandItemView : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image _image;
    [SerializeField] private Text _title;
    [SerializeField] private Button _buttonOpenBrand;

    private float _imageReduce = 5f;


    public void SetUp(BrandItem item)
    {
        StartCoroutine(GetImage(item.image));
        _title.text = item.title;
        _buttonOpenBrand.onClick.RemoveAllListeners();
        _buttonOpenBrand.onClick.AddListener(() => UIEvents.Current.ButtonOpenBrand(item.id));
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
        _image.rectTransform.sizeDelta = new Vector2(texture.width / _imageReduce, texture.height / _imageReduce);
    }
}