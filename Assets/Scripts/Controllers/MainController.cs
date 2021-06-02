using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MainController : MonoBehaviour
{
    [SerializeField] private string _brandsURL;

    private string _rimsURL;
    private int _brandID;

    private List<BaseController> _controllers = new List<BaseController>();
    private UIController _uiController;

    private Brands _brands;
    private Rims _rims;


    private void Awake()
    {
        _uiController = new UIController(this);

        StartCoroutine(GetBrandsJSON());

        GeneralEvents.Current.OnLoadRims += LoadRims;
    }

    private void Start()
    {
        foreach (BaseController controller in _controllers)
        {
            if (controller is IInitialize)
            {
                controller.Initialize();
            }
        }
    }

    public void Update()
    {
        foreach (BaseController controller in _controllers)
        {
            if (controller is IExecute)
            {
                controller.Execute();
            }
        }
    }

    public void AddController(BaseController controller)
    {
        if (!_controllers.Contains(controller))
        {
            _controllers.Add(controller);
        }
    }

    public void RemoveController(BaseController controller)
    {
        if (_controllers.Contains(controller))
        {
            _controllers.Remove(controller);
        }
    }

    public T GetController<T>() where T : BaseController
    {
        foreach (BaseController obj in _controllers)
        {
            if (obj.GetType() == typeof(T))
            {
                return (T)obj;
            }
        }
        return null;
    }

    #region JSON Management
    private IEnumerator GetBrandsJSON()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(_brandsURL))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("Oops! Something wrong with loading brands");
            }
            else
            {
                ProcessBrandsJSON(www.downloadHandler.text);
            }
        }
    }
    private void ProcessBrandsJSON(string text)
    {
        _brands = JsonUtility.FromJson<Brands>(text);
        GeneralEvents.Current.BrandsLoaded(_brands);
    }

    private IEnumerator GetRimsJSON(string url)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("Oops! Something wrong with loading brands");
            }
            else
            {
                ProcessRimsJSON(www.downloadHandler.text);
            }
        }
    }
    private void ProcessRimsJSON(string text)
    {
        _rims = JsonUtility.FromJson<Rims>(text);
        GeneralEvents.Current.RimsLoaded(_rims);
    }
    private void LoadRims(int id)
    {
        _rimsURL = $"https://test.formacar.ru/json/data/31/UWheels/UWheelList_{id}_17.json";
        StartCoroutine(GetRimsJSON(_rimsURL));
    }
    #endregion
}