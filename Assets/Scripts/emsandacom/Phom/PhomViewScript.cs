using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using AppConfig;
using UnityEngine.UI;

public class PhomViewScript : MonoBehaviour {

    public static PhomViewScript instance;
    public GameObject bg_change_scene;
    public GameObject objBatDauBtn;
    public GameObject objSanSangBtn;
    public GameObject objDatCuocBtn;
    public Image lockImg;
    private bool isLock = false;

    void Awake() {
        instance = this;
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(UnLoadScene());
        
	}

    private IEnumerator UnLoadScene() {
        yield return new WaitForEndOfFrame();
        if (SceneManager.GetSceneByName(Res.ROOM_NAME) != null)
            SceneManager.UnloadScene(Res.ROOM_NAME);
        if (SceneManager.GetSceneByName(Res.SETTING_NAME) != null)
            SceneManager.UnloadScene(Res.SETTING_NAME);
    }

    public void onJoinTableSuccess(string message) {
        if (BaseInfo.gI().isView)
        {
            objBatDauBtn.SetActive(false);
            objSanSangBtn.gameObject.SetActive(false);
            objDatCuocBtn.gameObject.SetActive(false);
            lockImg.gameObject.SetActive(false);
            return;
        }

        if (BaseInfo.gI().mainInfo.nick.Equals(message))
        {//neu mh la chu ban
            objBatDauBtn.gameObject.SetActive(true);
            objSanSangBtn.gameObject.SetActive(false);
            objDatCuocBtn.gameObject.SetActive(true);
            lockImg.gameObject.SetActive(true);
        }
    }

    public void ClickLock() {
        isLock = !isLock;
        if (isLock)
            LoadAssetBundle.LoadSprite(lockImg, "ui", "icon_lock");
        else
            LoadAssetBundle.LoadSprite(lockImg, "ui", "icon_unlock");
    }

    public void ClickSetting() {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene(Res.SETTING_AB, Res.SETTING_NAME);
    }

    public void BackToRoom() {
        PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", ClientConfig.Language.GetText("ingame_exit"), delegate
        {
            PopupAndLoadingScript.instance.ShowLoading();
            if (BaseInfo.gI().isView)
            {
                SendData.onOutView();
            }
            else
            {
                SendData.onOutTable();
            }
            //BaseInfo.gI().regOuTable = true;
        });
    }

    public void LoadRoom() {
        bg_change_scene.SetActive(true);
        LoadAssetBundle.LoadScene(Res.ROOM_AB, Res.ROOM_NAME);
    }

    public void SetActiveBg() {
        bg_change_scene.SetActive(false);
    }
}
