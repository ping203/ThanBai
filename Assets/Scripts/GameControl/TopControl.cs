using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TopControl : PanelGame {
    public GameControl gameControl;
    public Text displayName;
    public Text lb_id;
    public Text displayXu;
    public Text displayFree;
    public Image imgAvata;
    public RawImage rawAvata;
    
    void OnEnable() {
        //updateProfileUser();
    }

    // Update is called once per frame
    void Update() {
        //if (www != null) {
        //    if (www.isDone && !isOne) {
        //        rawAvata.texture = www.texture;
        //        isOne = true;
        //    }
        //}
        displayXu.text = BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyXu) + Res.MONEY_VIP_UPPERCASE;
        //displayFree.text = BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyChip) + Res.MONEY_FREE_UPPERCASE;
    }
    //public Image game_name;
    public void setGameName() {
        //string name = "CHỌN GAME";
        int nameIndex = -1;
        switch (BaseInfo.gI().gameID) {
            case GameID.PHOM:
                // name = "PHỎM";
                nameIndex = 0;
                break;
            case GameID.TLMN:
                //  name = "TIẾN LÊN MN";
                nameIndex = 1;
                break;
            case GameID.XITO:
                // name = "XÌ TỐ";
                nameIndex = 2;
                break;
            case GameID.MAUBINH:
                // name = "MẬU BINH";
                nameIndex = 3;
                break;
            case GameID.BACAY:
                // name = "BA CÂY";
                nameIndex = 4;
                break;
            case GameID.LIENG:
                // name = "LIÊNG";
                nameIndex = 5;
                break;
            case GameID.XAM:
                // name = "SÂM";
                nameIndex = 6;
                break;
            case GameID.XOCDIA:
                // name = "XÓC ĐĨA";
                nameIndex = 7;
                break;
            case GameID.POKER:
                // name = "POKER";
                nameIndex = 8;
                break;
            case GameID.TAIXIU:
                //name = "TÀI XỈU";
                nameIndex = 9;
                break;
            case GameID.TLMNsolo:
                // name = "TIẾN LÊN MN Solo";
                nameIndex = 10;
                break;
            case GameID.XENG:
                // name = "XÈNG";
                nameIndex = 11;
                break;
        }
        //game_name.sprite = gameControl.gameNames[nameIndex];
    }

    //WWW www;
    //bool isOne = false;

    public void clickHomThu() {
        SoundManager.instance.startClickButtonAudio();
        //gameControl.panelMail.onShow();
    }

    public void clickNapXu() {
        SoundManager.instance.startClickButtonAudio();
        //gameControl.panelNapChuyenXu.onShow();

    }

    public void clickDoiThuong() {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadPrefab("prefabs", "PanelWait", (panel) => { panel.GetComponent<UIPopUp>().ShowDialog(); });
        //SendData.onGetInfoGift();
        //}
    }

    public void clickHelp() {
        SoundManager.instance.startClickButtonAudio();
        //gameControl.panleHelp.onShow();
    }

    public void clickCreateRoom() {
        SoundManager.instance.startClickButtonAudio();
        //gameControl.panelCreateRoom.onShow();
    }

    public void clickToiBan() {
        SoundManager.instance.startClickButtonAudio();
        //gameControl.panelToiBan.onShow();
    }

    public void clickNoti() {
        SoundManager.instance.startClickButtonAudio();
        //gameControl.panelNotiDoiThuong.onShow();
    }

    public void clickPlayNow() {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadPrefab("prefabs", "PanelWait", (panel) => { panel.GetComponent<UIPopUp>().ShowDialog(); });
        SendData.onAutoJoinTable();
    }

    public void clickRanking() {
        SoundManager.instance.startClickButtonAudio();
        //gameControl.panelRank.onShow();
    }

    public void clickInviteFacebook() {
        SoundManager.instance.startClickButtonAudio();
#if UNITY_WEBGL
        Application.ExternalCall("myFacebookInvite");
#endif
    }
}
