using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelSetting : PanelGame {
    private bool isNhacNen = true;
    private bool isAutoReady = true;
    private bool isNhanLoiMoi = true;
    private bool isVibra = true;
    public Image nhacNenImg;
    public Image autoReadyImg;
    public Image nhanLoiMoiImg;
    public Image vibraImg;

    void OnEnable() {
        //nhanloimoichoi.isOn = BaseInfo.gI().isNhanLoiMoiChoi;
        //nhacnen.isOn = BaseInfo.gI().isSound;
        //autoready.isOn = BaseInfo.gI().isAutoReady;
       // base.OnEnable();
    }

    //public void onChangeVL() {
    //    nhanloimoichoi.isOn = BaseInfo.gI().isNhanLoiMoiChoi;
    //}

    public void clickNhacNen() {
        SoundManager.instance.startClickButtonAudio();
        isNhacNen = !isNhacNen;
        LoadSprite(isNhacNen, nhacNenImg);
        PlayerPrefs.SetInt("sound", isNhacNen ? 0 : 1);
        PlayerPrefs.Save();
        BaseInfo.gI().isSound = isNhacNen;

    }
    public void clickRung() {
        SoundManager.instance.startClickButtonAudio();
        isVibra = !isVibra;
        LoadSprite(isVibra, vibraImg);
        BaseInfo.gI().isVibrate = isVibra;
    }

    public void clickNhanLoiMoiChoi() {
        SoundManager.instance.startClickButtonAudio();
        isNhanLoiMoi = !isNhanLoiMoi;
        LoadSprite(isNhanLoiMoi, nhanLoiMoiImg);
        BaseInfo.gI().isNhanLoiMoiChoi = isNhanLoiMoi;
    }
    public void clickAutoReady() {
        SoundManager.instance.startClickButtonAudio();
        isAutoReady = !isAutoReady;
        LoadSprite(isAutoReady, autoReadyImg);
        BaseInfo.gI().isAutoReady = isAutoReady;
    }

    private void LoadSprite(bool isOn, Image bg) {
        if (isOn)
            LoadAssetBundle.LoadSprite(bg, "ui", "icon_on");
        else
            LoadAssetBundle.LoadSprite(bg, "ui", "icon_off");
    }

    public void CloseSetting() {
        GetComponent<UIPopUp>().HideDialog();
    }
}

