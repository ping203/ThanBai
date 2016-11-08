using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MenuControl : StageControl {
    //public Text lb_textnoti;
    public Text lb_name;
    //public Text lb_id;
    //public Text lb_chip;
    public Text lb_xu;
    // public Text lb_num_mail;
    //public GameObject buttonDoiThuong;
    public Image imgAvata;
    public RawImage rawAvata;

    WWW www;
    bool isOne = false;
    // Use this for initialization
    void Start() {
        //updateAvataName ();
    }
    void OnEnable() {
        updateAvataName();
    }
    // Update is called once per frame
    void Update() {
        //if (gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) {
        //    //gameControl.disableAllDialog();
        //    onBack();
        //}

        //lb_chip.text = (BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyChip) + Res.MONEY_FREE + " Free");
        //lb_xu.text = (BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyXu) + Res.MONEY_VIP + " Vip");
        //if (BaseInfo.gI().soTinNhan > 0) {
        //    lb_num_mail.text = BaseInfo.gI().soTinNhan + "";
        //} else {
        //    lb_num_mail.text = BaseInfo.gI().soTinNhan + "";
        //    if (lb_num_mail.transform.parent.gameObject.activeInHierarchy) {
        //        lb_num_mail.transform.parent.gameObject.SetActive(false);
        //    }
        //}

        if (www != null) {
            if (www.isDone && !isOne) {
                rawAvata.texture = www.texture;
                isOne = true;
            }
        }
    }

    void deActive() {
        gameObject.SetActive(false);
    }

    public void updateAvataName() {
        lb_name.text = (BaseInfo.gI().mainInfo.displayname);
        //lb_id.text = "ID:" + BaseInfo.gI().mainInfo.userid;
        int idAvata = BaseInfo.gI().mainInfo.idAvata;
        string link_avata = BaseInfo.gI().mainInfo.link_Avatar;
        int num_star = BaseInfo.gI().mainInfo.level_vip;

        lb_xu.text = "" + BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyXu) + Res.MONEY_VIP;

        www = null;
        if (link_avata != "") {
            www = new WWW(link_avata);
            isOne = false;
            imgAvata.gameObject.SetActive(false);
            rawAvata.gameObject.SetActive(true);
        } else if (idAvata > 0) {
            imgAvata.gameObject.SetActive(true);
            rawAvata.gameObject.SetActive(false);
            // spriteAvata.spriteName = idAvata + "";
            //imgAvata.sprite = Res.getAvataByID(idAvata);//Res.list_avata[idAvata + 1];
            LoadAssetBundle.LoadSprite(imgAvata, Res.AS_AVATA, "" + idAvata);
        }
    }

    public override void onBack() {
        SoundManager.instance.startClickButtonAudio();
        PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("","Bạn có muốn thoát?", delegate {
            NetworkUtil.GI().close();
            LoadAssetBundle.LoadScene("sub_login", "subLogin");
        });
    }

    public void onClickGame(string obj) {
        PanelWaiting.instance.ShowLoading();
        SoundManager.instance.startClickButtonAudio();
        switch (obj) {
            case "tlmn":
                BaseInfo.gI().gameID = GameID.TLMN;
                break;
            case "tlmnsl":
                BaseInfo.gI().gameID = GameID.TLMNsolo;
                break;
            case "phom":
                BaseInfo.gI().gameID = GameID.PHOM;
                break;
            case "xito":
                BaseInfo.gI().gameID = GameID.XITO;
                break;
            case "poker":
                BaseInfo.gI().gameID = GameID.POKER;
                break;
            case "bacay":
                BaseInfo.gI().gameID = GameID.BACAY;
                break;
            case "lieng":
                BaseInfo.gI().gameID = GameID.LIENG;
                break;
            case "maubinh":
                BaseInfo.gI().gameID = GameID.MAUBINH;
                break;
            case "xam":
                BaseInfo.gI().gameID = GameID.XAM;
                break;
            case "xocdia":
                BaseInfo.gI().gameID = GameID.XOCDIA;
                break;
            //case "xeng":
            //    gameControl.gameID = GameID.XENG;
            //    SendData.onJoinXengHoaQua();
            //    //gameControl.top.setGameName();
            //    return;
            //case "taixiu":
            //    gameControl.gameID = GameID.TAIXIU;
            //    //sua
            //    //gameControl.setCasino(GameID.TAIXIU, 0);
            //    SendData.onjoinTaiXiu((byte)BaseInfo.gI().typetableLogin);
            //    //gameControl.top.setGameName();
            //    //gameControl.toast.showToast("GAME ĐANG PHÁT TRIỂN!");
            //    return;
        }

        //gameControl.top.setGameName();
        SendData.onSendGameID((sbyte)BaseInfo.gI().gameID);
    }

    public void clickSetting() {
        SoundManager.instance.startClickButtonAudio();
        PanelWaiting.instance.ShowLoading();

        //LoadAssetBundle.LoadScene("sub_setting", "sub_setting");
    }

    public void clickHelp() {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene("sub_help", "subHelp");
    }

    public void clickNapXu() {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene("sub_napxu", "subNapXu");

    }

    public void clickDoiThuong() {
        SoundManager.instance.startClickButtonAudio();
        //		if (BaseInfo.gI()..Count > 0
        //		        && BaseInfo.gI().giftTheCao.Count > 0)
        //		{
        //		    gameControl.panelDoiThuong.onShow();
        //		    gameControl.panelWaiting.onHide();
        //		}
        //		else
        //		{
        //gameControl.panelDoiThuong.onShow();
        PanelWaiting.instance.ShowLoading();
        //SendData.onGetInfoGift();
        //}
    }

    public void clickAvatar() {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene("sub_info_player", "subInfoPlayer", () =>
        {
            PanelInfoPlayer.instance.InfoMe();
        });
        //gameControl.panelInfoPlayer.infoMe();
        //gameControl.panelInfoPlayer.onShow();
    }

    public void clickHomThu() {
        SoundManager.instance.startClickButtonAudio();
        SendData.onGetInboxMessage();
        PopupAndLoadingScript.instance.ShowLoading();
        LoadAssetBundle.LoadScene("sub_mail", "subMail");
    }

    public void clickDienDan() {
        SoundManager.instance.startClickButtonAudio();
        //gameControl.dialogNotification.onShow("Bạn có muốn chuyển đến diễn đàn?", delegate {
        //    Application.OpenURL(Res.linkForum);
        //});
    }

    public void clickLuatChoi() {
        SoundManager.instance.startClickButtonAudio();
        //gameControl.dialogLuatChoi.onShow();
    }

    public override void Appear() {
        base.Appear();
        if (BaseInfo.gI().isPurchase) {
            //   buttonDoiThuong.SetActive(false);
        } else {
            //   buttonDoiThuong.SetActive(true);
        }
    }

    public void clickNoti() {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene("sub_noti_doithuong", "subNotiDoiThuong");
    }
}

