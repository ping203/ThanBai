using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PanelNapChuyenXu : PanelGame {
    public static PanelNapChuyenXu instance;
    //public PanelSMS panelSMS;
    //public UIGrid grid;
    private Image currentBgTap;
    public Image thecaoImg;
    public Image smsImg;
    public Image chuyenxuImg;
    public Image iapImg;
    private GameObject currentObj;
    public GameObject objTheCao;
    public GameObject objSMS;
    public GameObject objChuyenXu;
    public GameObject objList9029;
    public GameObject objIAP;

    //public Toggle tg_thecao, tg_sms, tg_sms9029, tg_doichip, tg_doixu;
    //bool is_thecao, is_sms, is_sms9029, is_doichip, is_doixu;
    // Use this for initialization
    void Awake() {
        instance = this;
    }

    void Start() {
        currentBgTap = thecaoImg;
        currentObj = objTheCao;
    }

    public void ClickThecao() {
        SetBG(thecaoImg);
        SetObj(objTheCao);
    }

    public void ClickSMS() {
        SetBG(smsImg);
        SetObj(objSMS);
        if (objList9029.transform.childCount == 0 && BaseInfo.gI().list9029.Count > 0) {
            PanelSMS.instance.addList9029(BaseInfo.gI().list9029);
        }
    }

    public void ClickChuyenXu() {
        SetBG(chuyenxuImg);
        SetObj(objChuyenXu);
    }

    public void ClickIAP() {
        SetBG(iapImg);
        SetObj(objIAP);
    }

    public void Clcik9029() {
        //Debug.LogError ("Clcik9029");
        //List<Item9029> tem = new List<Item9029> ();
        //for(int i = 0; i < 3; i++) {
        //    Item9029 it = new Item9029 ();
        //    string name = "name_" + i.ToString ();
        //    string syntax = "syntax_" + i.ToString ();
        //    short port = (short)i;
        //    long money = 100;

        //    it.name = name;
        //    it.sys = syntax;
        //    it.port = port;
        //    it.money = money;

        //    tem.Add (it);
        //}
        //Debug.LogError ("tem count: " + tem.Count);
        //PanelSMS.instance.addList9029 (tem);
        //string net = UnityPluginForWindowPhone.Class1.getDeviceNetworkInformation();
        //switch (net) {
        //    case "VIETTEL":
        //        BaseInfo.gI().TELCO_CODE = 1;
        //        break;
        //    case "VN VINAPHONE":
        //    case "VINAPHONE":
        //    case "VN VINAPHONE-VinaPhone":
        //    case "VN VINAPHONE-VINAPHONE":
        //        BaseInfo.gI().TELCO_CODE = 2;
        //        break;
        //    case "MOBIFONE":
        //    case "VN MOBIFONE":
        //    case "VN MOBIFONE-MobiFone":
        //    case "VN MOBIFONE-MOBIFONE":
        //        BaseInfo.gI().TELCO_CODE = 3;
        //        break;
        //    default:
        //        BaseInfo.gI().TELCO_CODE = 0;
        //        break;
        //}
        //Debug.Log(net + " ------=========== " + BaseInfo.gI().TELCO_CODE);
        if (BaseInfo.gI().TELCO_CODE != 0)
        {
            SendData.onSendSms9029(BaseInfo.gI().TELCO_CODE);
        }
    }

    private void SetBG(Image img) {
        LoadAssetBundle.LoadSprite(currentBgTap, Res.AS_UI, "bg_unpress");
        currentBgTap = img;
        LoadAssetBundle.LoadSprite(currentBgTap, Res.AS_UI, "bg_press");
    }

    private void SetObj(GameObject obj) {
        currentObj.SetActive(false);
        currentObj = obj;
        currentObj.SetActive(true);
    }


    //public void setValue() {
    //    //grid.repositionNow = true;
    //    if (BaseInfo.gI().isCharging == 3 || BaseInfo.gI().isCharging == 5)
    //    {
    //        is_thecao = false;
    //    }
    //    else
    //    {
    //        is_thecao = true;
    //    }

    //    if (BaseInfo.gI().isCharging == 0 || BaseInfo.gI().isCharging == 5)
    //    {
    //        is_sms = false;
    //    }
    //    else
    //    {
    //        is_sms = true;
    //    }

    //    if (BaseInfo.gI().isHidexuchip == 0)
    //    {
    //        is_doichip = false;
    //    }
    //    else
    //    {
    //        is_doichip = true;
    //    }
    //    if (BaseInfo.gI().isHidexuchip == 1)
    //    {
    //        is_doixu = false;
    //    }
    //    else
    //    {
    //        is_doixu = true;
    //    }
    //    if (BaseInfo.gI().isHidexuchip == 2)
    //    {
    //        is_doichip = false;
    //        is_doixu = false;
    //    }
    //    else if (BaseInfo.gI().isHidexuchip != 0 && BaseInfo.gI().isHidexuchip != 1)
    //    {
    //        is_doichip = true;
    //        is_doixu = true;
    //    }
    //    is_sms9029 = BaseInfo.gI().is9029;

    //    tg_thecao.gameObject.SetActive(is_thecao);
    //    tg_sms.gameObject.SetActive(is_sms);
    //    tg_sms9029.gameObject.SetActive(is_sms9029);
    //    tg_doichip.gameObject.SetActive(is_doichip);
    //    tg_doixu.gameObject.SetActive(is_doixu);
    //    Debug.Log("is_thecao " + is_thecao + " is_sms " + is_sms + " is_sms9029 " + is_sms9029 + " is_doichip " + is_doichip + " is_doixu " + is_doixu);
    //    // grid.Reposition();
    //}
    
    //public void onShow() {
    //    setValue();
    //    base.onShow();
    //}
}
