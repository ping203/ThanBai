using UnityEngine;
using System.Collections;
using emsandacom.Popup;

public class PopupAndLoadingScript : MonoBehaviour {
    public static PopupAndLoadingScript instance;
    public PopupViewScript popup;
    public GameObject objLoading;
    public Toast toast;
    public GameObject objParent;

    void Awake() {
        instance = this;
    }

	// Use this for initialization
	public void LoadPopupAndLoading () {
	    if(popup == null)
        {
            LoadAssetBundle.LoadPrefab("prefabs", "PanelPopup", (obj) => {
                popup = obj.GetComponent<PopupViewScript>() ;
                popup.transform.SetParent(objParent.transform);
                popup.transform.localPosition = Vector3.zero;
                popup.transform.localScale = Vector3.one;
                //popup.gameObject.SetActive(false);
            });
        }

        if (objLoading == null)
        {
            LoadAssetBundle.LoadPrefab("prefabs", "PanelWait", (obj) => {
                objLoading = obj;
                objLoading.transform.SetParent(objParent.transform);
                objLoading.transform.localPosition = Vector3.zero;
                objLoading.transform.localScale = Vector3.one;
                objLoading.SetActive(false);
            });
        }

        if (toast == null)
        {
            LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "toast", (toastPre) =>
            {
                toast = toastPre.GetComponent<Toast>();
                toast.transform.SetParent(objParent.transform);
                toast.transform.localPosition = Vector3.zero;
                toast.transform.localScale = Vector3.one;
                toast.gameObject.SetActive(false);
            });
        }
    }
	
	public void ShowPopup () {
        popup.gameObject.SetActive(true);
	}

    public void HidePopup()
    {
        popup.gameObject.SetActive(false);
    }

    public void ShowLoading()
    {
        objLoading.SetActive(true);
    }

    public void HideLoading()
    {
        objLoading.SetActive(false);
    }

    //void OnApplicationQuit()
    //{
    //    NetworkUtil.GI().cleanNetwork();
    //}
}
