using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PanelSMS : PanelGame {
    public Text lb20, lb30;
    public GameObject objList9029;

    public static PanelSMS instance = null;

    void OnEnable () {
        instance = this;
    }
    // Use this for initialization
    void Start () {
        instance = this;

        if(lb20 != null)
            lb20.text = " = " + BaseInfo.formatMoneyDetailDot (BaseInfo.gI ().sms10) + " " + Res.MONEY_VIP;
        if(lb30 != null)
            lb30.text = " = " + BaseInfo.formatMoneyDetailDot (BaseInfo.gI ().sms15) + " " + Res.MONEY_VIP;
    }

    // Update is called once per frame
    void Update () {
    }

    public void NhanTin (string sms, string dauso, string thongbao) {
        PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("",thongbao, delegate {
#if UNITY_EDITOR
            PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("","Soạn tin theo cú pháp: " + sms + " gửi đến " + dauso, delegate { });
#else
			 GameControl.instance.sendSMS(dauso, sms);
#endif
        });
    }

    public void onClick10 () {
        SoundManager.instance.startClickButtonAudio ();
        string tb = "Nhắn tin để nạp " + Res.MONEY_VIP_UPPERCASE + " " + BaseInfo.formatMoneyDetailDot (BaseInfo.gI ().sms10) + " (phí 10k)?";
        string sms = BaseInfo.gI ().syntax10 + " " + BaseInfo.gI ().mainInfo.userid;
        string ds = BaseInfo.gI ().port10;
        this.NhanTin (sms, ds, tb);
    }

    public void onClick15 () {
        SoundManager.instance.startClickButtonAudio ();
        string tb = "Nhắn tin để nạp " + Res.MONEY_VIP_UPPERCASE + " " + BaseInfo.formatMoneyDetailDot (BaseInfo.gI ().sms15) + " (phí 15k)?";
        string sms = BaseInfo.gI ().syntax15 + " " + BaseInfo.gI ().mainInfo.userid;
        string ds = BaseInfo.gI ().port15;
        this.NhanTin (sms, ds, tb);
    }

    public void onClick9029 (string name, long money, string sys, short port) {
        SoundManager.instance.startClickButtonAudio ();

        string tb = "Nhắn tin để nạp " + BaseInfo.formatMoneyDetailDot (money) + " " +  Res.MONEY_VIP_UPPERCASE + " (phí " + name + ")?";
        string sms = sys + "##" + BaseInfo.gI ().mainInfo.userid;
        string ds = port + "";
        NhanTin (sms, ds, tb);
    }

    public void addList9029 (List<Item9029> list) {
        LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Item9029", (obj) => {
            GameObject obj9029 = obj;
            obj9029.transform.SetParent(objList9029.transform);
            obj9029.transform.localScale = Vector3.one;
            obj9029.GetComponent<Item9029>().setText(list[0].name, list[0].sys, list[0].port, list[0].money);
            for (int i = 1; i < list.Count; i++)
            {
                GameObject obj90 = Instantiate(obj9029);
                obj90.transform.SetParent(objList9029.transform);
                obj90.transform.localScale = Vector3.one;
                obj9029.GetComponent<Item9029>().setText(list[i].name, list[i].sys, list[i].port, list[i].money);
            }
        });
    }
}
