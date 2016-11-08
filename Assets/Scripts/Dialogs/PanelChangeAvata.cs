using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class PanelChangeAvata : PanelGame {
    public GameObject tblAva;
    //public GameObject btnAva;

    public bool isLoad = true;

    // Use this for initialization
    void Start() {
        StartCoroutine(loadAva());
    }

    public IEnumerator loadAva() {
        yield return new WaitForEndOfFrame();
        if (isLoad) {
            LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Button_Avata", (prefabsAB) => {
                for (int i = 0; i < Res.AVATA_COUNT; i++) {
                    GameObject btn = Instantiate(prefabsAB) as GameObject;
                    btn.transform.parent = tblAva.transform;
                    btn.transform.localScale = Vector3.one;
                    LoadAssetBundle.LoadSprite(btn.GetComponent<Button>().image, Res.AS_AVATA, (i + 1) + "");
                    btn.name = "" + (i + 1);
                    btn.GetComponent<Button>().onClick.AddListener(delegate {
                        ClickAva(btn);
                    });
                }
            });
            isLoad = false;
        }
    }

    public void ClickAva(GameObject name) {
        SoundManager.instance.startClickButtonAudio();
        int index = Convert.ToInt32(name.name);
        BaseInfo.gI().mainInfo.idAvata = index;
        SendData.onUpdateAvata(index);
        onHide();
    }

    public void CloseChangeAvatar() {
        GetComponent<UIPopUp>().HideDialog();
    }
}
