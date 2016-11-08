using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using DG.Tweening;

public class PanelTheCao : PanelGame {
    public InputField ip_masothe, ip_serithe;
    int typeCard = -1;
    public Transform dropdownContainer;
    private bool isDropDown;
    public Text currentMangTxt;
    public GameObject objListTheCao;

    void Start () {
        infoTygia ();
        clickToggle(2);
    }

    public void ClickDropDown() {
        SoundManager.instance.startClickButtonAudio();
        isDropDown = !isDropDown;
        if(isDropDown)
            dropdownContainer.transform.DOScaleY(1, 0.2f);
        else
            dropdownContainer.transform.DOScaleY(0, 0.2f);
    }

   public void clickToggle(int type) {
        SoundManager.instance.startClickButtonAudio();
        typeCard = type;
        CloseDropDown();
        switch (type) {
            case 0:
                currentMangTxt.text = "Mobifone";
                break;
            case 1:
                currentMangTxt.text = "Vinaphone";
                break;
            case 2:
                currentMangTxt.text = "Viettel";
                break;
        }
    }

    public void CloseDropDown() {
        isDropDown = false;
        dropdownContainer.transform.DOScaleY(0, 0.2f);
    }

    public void ClickNapTheCao () {
        SoundManager.instance.startClickButtonAudio ();
        //	OnSubmit ();

        //switch (tenMang)
        //{
        //    case "Mobiphone":
        //        typeCard = 0;
        //        break;
        //    case "Vinaphone":
        //        typeCard = 1;
        //        break;
        //    case "Viettel":
        //        typeCard = 2;
        //        break;
        //}
        if (!string.IsNullOrEmpty(ip_masothe.text)
            || ip_masothe.text.Length > 15) {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Mã số thẻ không hợp lệ!");
            return;
        }

        if(/*typeCard != 4 &&*/ (!string.IsNullOrEmpty(ip_serithe.text))) {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Quý khách hãy nhập vào số Serial");
            return;
        }
        doRequestChargeMoneySimCard (BaseInfo.gI ().mainInfo.nick, typeCard, ip_masothe.text, ip_serithe.text);
        PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Hệ thống đang xử lý!");
    }

    public void TuChoi () {
        SoundManager.instance.startClickButtonAudio ();
        ip_masothe.text = "";
        ip_serithe.text = "";
    }

    private void doRequestChargeMoneySimCard (string userName, int type,
            string cardCode, string series) {
        Message m;
        m = new Message (CMDClient.PAYCARD);
        try {
            m.writer ().WriteUTF (userName);
            m.writer ().WriteShort ((short) type);
            m.writer ().WriteUTF (cardCode);
            m.writer ().WriteUTF (series);
            NetworkUtil.GI ().sendMessage (m);
        } catch(Exception e) {
            Debug.LogException (e);
        }
    }

    public void infoTygia() {
        LoadAssetBundle.LoadPrefab("prefabs", "ItemMenhGia", (obj) => {
            GameObject menhgia = obj;
            menhgia.transform.SetParent(objListTheCao.transform);
            menhgia.transform.localScale = Vector3.one;
            menhgia.GetComponent<Text>().text = BaseInfo.formatMoneyDetailDot(BaseInfo.gI().list_tygia[0].menhgia) + " VND = " + BaseInfo.formatMoneyDetailDot(BaseInfo.gI().list_tygia[0].xu) + " " + Res.MONEY_VIP;
            //Debug.LogError("list_tygia " + BaseInfo.gI().list_tygia.Count + (menhgia == null));
            for (int i = 1; i < BaseInfo.gI().list_tygia.Count; i++)
            {
                GameObject mg = Instantiate(menhgia);
                mg.transform.SetParent(objListTheCao.transform);
                mg.transform.localScale = Vector3.one;
                TyGia tg = (TyGia)BaseInfo.gI().list_tygia[i];
                mg.GetComponent<Text>().text = BaseInfo.formatMoneyDetailDot(tg.menhgia) + " VND = " + BaseInfo.formatMoneyDetailDot(tg.xu) + " " + Res.MONEY_VIP;
            }
        });
        
    }
}
