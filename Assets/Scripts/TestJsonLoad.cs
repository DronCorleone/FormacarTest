using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestJsonLoad : MonoBehaviour
{
    [SerializeField] private string _brandURL;
    [SerializeField] private string _rimsURL;


    private void Start()
    {
        StartCoroutine(GetData());
    }

    IEnumerator GetData()
    {
        UnityWebRequest www = UnityWebRequest.Get(_brandURL);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning("Something wrong with URL");
        }
        else
        {
            ProcessJson(www.downloadHandler.text);
        }
    }

    private void ProcessJson(string text)
    {
        Brands items = JsonUtility.FromJson<Brands>(text);
        
        foreach (BrandItem item in items.items)
        {
            Debug.Log($"{item.id}");
        }
    }
}