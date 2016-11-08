using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DG.Tweening;
using AppConfig;

public class RoomControl : StageControl {
    //public static int roomType = 2; //1:Thuong, 2:VIP
    //  public GameObject prefabsTable;
    public Transform parent;

    public Transform tf_effect;
    //public Button[] btn_click_game;

    //public Text displayName;
    //public Text lb_id;
    //public Text displayXu;
    //public Image imgAvata;
    //public RawImage rawAvata;

    //public Image game_name;
    //public Sprite[] gameNames;

    //public List<GameObject> listRoom = new List<GameObject>();

    //public Text text_noti;
    
    // Use this for initialization
    void Start() {
        //for (int i = 0; i < btn_click_game.Length; i++) {
        //    GameObject obj = btn_click_game[i].gameObject;
        //    btn_click_game[i].onClick.AddListener(delegate {
        //        onClickGame(obj);
        //    });
        //}
        
    }

    //const float posXDefault = 200.0f;
    //public void setNoti(string str)
    //{
    //    if (!str.Equals(""))
    //    {
    //        text_noti.text = str;

    //        float w = LayoutUtility.GetPreferredWidth(text_noti.rectTransform);
    //        text_noti.transform.localPosition = new Vector3(posXDefault, 0, 0);
    //        float posEnd = -posXDefault - w - 50;

    //        float time = (posXDefault - posEnd) / 50;
    //        text_noti.transform.DOKill();
    //        text_noti.transform.DOLocalMoveX(posEnd, time).SetLoops(-1).SetEase(Ease.Linear);
    //    }
    //}

    void OnEnable() {
        //gameControl.top.setGameName();
    }

    // Update is called once per frame
    void Update() {
        if (gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) {
            //gameControl.disableAllDialog();
            onBack();
        }
        //if (www != null) {
        //    if (www.isDone && !isOne) {
        //        rawAvata.texture = www.texture;
        //        isOne = true;
        //    }
        //}
    }
    public override void onBack() {
        
    }

    //public Image game_name;
    //public void setGameName() {
    //    //string name = "CHỌN GAME";
    //    int nameIndex = -1;
    //    switch (gameControl.gameID) {
    //        case GameID.PHOM:
    //            // name = "PHỎM";
    //            nameIndex = 0;
    //            break;
    //        case GameID.TLMN:
    //            //  name = "TIẾN LÊN MN";
    //            nameIndex = 1;
    //            break;
    //        case GameID.XITO:
    //            // name = "XÌ TỐ";
    //            nameIndex = 2;
    //            break;
    //        case GameID.MAUBINH:
    //            // name = "MẬU BINH";
    //            nameIndex = 3;
    //            break;
    //        case GameID.BACAY:
    //            // name = "BA CÂY";
    //            nameIndex = 4;
    //            break;
    //        case GameID.LIENG:
    //            // name = "LIÊNG";
    //            nameIndex = 5;
    //            break;
    //        case GameID.XAM:
    //            // name = "SÂM";
    //            nameIndex = 6;
    //            break;
    //        case GameID.XOCDIA:
    //            // name = "XÓC ĐĨA";
    //            nameIndex = 7;
    //            break;
    //        case GameID.POKER:
    //            // name = "POKER";
    //            nameIndex = 8;
    //            break;
    //        case GameID.TAIXIU:
    //            //name = "TÀI XỈU";
    //            nameIndex = 9;
    //            break;
    //        case GameID.TLMNsolo:
    //            // name = "TIẾN LÊN MN Solo";
    //            nameIndex = 10;
    //            break;
    //        case GameID.XENG:
    //            // name = "XÈNG";
    //            nameIndex = 11;
    //            break;
    //    }
    //    game_name.sprite = gameControl.gameNames[nameIndex];
    //}

    public void createScollPane() {
        // List<TableItem> listTable = gameControl.listTable;
        PopupAndLoadingScript.instance.ShowLoading();
        //if (parent.childCount > 0) {
        //loopVerticalScrollRect.RefillCells();
        //loopVerticalScrollRect.enabled = true;
        //for (int i = 0; i < listRoom.Count; i++) {
        //    Destroy(listRoom[i]);
        //}
        //listRoom.Clear();
        //for (int i = 0; i < listTable.Count; i++) {
        //    GameObject obj = Instantiate(prefabsTable) as GameObject;
        //    obj.transform.SetParent(parent);
        //    obj.transform.localScale = Vector3.one;
        //    obj.GetComponent<TableBehavior>().index = i;
        //    obj.GetComponent<TableBehavior>().ScrollCellIndex(listTable[i]);
        //    listRoom.Add(obj);
        //}
        PanelWaiting.instance.HideLoading();
    }

    //WWW www;
    //bool isOne = false;
    //public void updateProfileUser() {
    //    string dis = BaseInfo.gI().mainInfo.displayname;
    //    if (dis.Length > 7) {
    //        dis = dis.Substring(0, 6) + "...";
    //    }
    //    displayName.text = dis;
    //    int idAvata = BaseInfo.gI().mainInfo.idAvata;
    //    string link_avata = BaseInfo.gI().mainInfo.link_Avatar;
    //    int num_star = BaseInfo.gI().mainInfo.level_vip;

    //    displayXu.text = BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyXu) + Res.MONEY_VIP;

    //    www = null;
    //    if (link_avata != "") {
    //        www = new WWW(link_avata);

    //        isOne = false;
    //        imgAvata.gameObject.SetActive(false);
    //        rawAvata.gameObject.SetActive(true);
    //    } else if (idAvata > 0) {
    //        imgAvata.gameObject.SetActive(true);
    //        rawAvata.gameObject.SetActive(false);
    //        imgAvata.sprite = Res.getAvataByID(idAvata);//Res.list_avata[idAvata + 1];
    //    }
    //}

    //public void clickAvatar() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    gameControl.panelInfoPlayer.infoMe();
    //    gameControl.panelInfoPlayer.onShow();
    //}

    public void clickButtonLamMoi() {
        SoundManager.instance.startClickButtonAudio();
        PanelWaiting.instance.ShowLoading();
        SendData.onUpdateRoom();
    }

    public void sortBanCuoc() {
        SoundManager.instance.startClickButtonAudio();
        BaseInfo.gI().sort_giam_dan_bancuoc = !BaseInfo.gI().sort_giam_dan_bancuoc;
        BaseInfo.gI().type_sort = 1;
        SendData.onUpdateRoom();
    }

    public void sortMucCuoc() {
        SoundManager.instance.startClickButtonAudio();
        BaseInfo.gI().sort_giam_dan_muccuoc = !BaseInfo.gI().sort_giam_dan_muccuoc;
        BaseInfo.gI().type_sort = 2;
        SendData.onUpdateRoom();
    }

    public void sortTrangThai() {
        SoundManager.instance.startClickButtonAudio();
        BaseInfo.gI().sort_giam_dan_nguoichoi = !BaseInfo.gI().sort_giam_dan_nguoichoi;
        BaseInfo.gI().type_sort = 3;
        SendData.onUpdateRoom();
    }

    public void clickButtonChoiNgay() {
        SoundManager.instance.startClickButtonAudio();
        PanelWaiting.instance.ShowLoading();
        SendData.onAutoJoinTable();
    }
    public void clickAnBanFull(bool isChecked) {
        PanelWaiting.instance.ShowLoading();
        BaseInfo.gI().isHideTabeFull = isChecked;
        SendData.onUpdateRoom();
    }
    [SerializeField]
    Button btn_vip, btn_free;
    [SerializeField]
    Sprite[] sp_btn;

    public void clickRoomVip() {
        SoundManager.instance.startClickButtonAudio();
        BaseInfo.gI().typetableLogin = Res.ROOMVIP;
        //SendData.onJoinRoom(Res.ROOMVIP);
        PanelWaiting.instance.ShowLoading();
        setStateButton();
    }

    public void clickRoopFree() {
        SoundManager.instance.startClickButtonAudio();
        BaseInfo.gI().typetableLogin = Res.ROOMFREE;
        //SendData.onJoinRoom(Res.ROOMFREE);
        PanelWaiting.instance.ShowLoading();
        setStateButton();
    }

    void setStateButton() {
        if (BaseInfo.gI().typetableLogin == Res.ROOMVIP) {
            btn_vip.image.sprite = sp_btn[0];
            btn_free.image.sprite = sp_btn[1];
        } else {
            btn_vip.image.sprite = sp_btn[1];
            btn_free.image.sprite = sp_btn[0];
        }
    }
    //public void clickSetting() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    gameControl.panelSetting.onShow();
    //}

    //public void clickHomThu() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    gameControl.panelMail.onShow();
    //}

    //public void clickNapXu() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    gameControl.panelNapChuyenXu.onShow();

    //}

    //public void clickDoiThuong() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    gameControl.panelWaiting.onShow();
    //    SendData.onGetInfoGift();
    //    //}
    //}

    public void clickHelp() {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene("sub_help", "subHelp");
    }

    public void clickCreateRoom() {
        SoundManager.instance.startClickButtonAudio();
        //gameControl.panelCreateRoom.onShow();
    }

    public void clickToiBan() {
        SoundManager.instance.startClickButtonAudio();
        //gameControl.panelToiBan.onShow();
    }

    //public void clickNoti() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    gameControl.panelNotiDoiThuong.onShow();
    //}
    public void clickPlayNow() {
        SoundManager.instance.startClickButtonAudio();
        PanelWaiting.instance.ShowLoading();
        SendData.onAutoJoinTable();
    }

    //public void clickRanking() {
    //    GameControl.instance.sound.startClickButtonAudio();
    //    // gameControl.panelRanking.onShow();
    //}
    public void onClickGame(GameObject game) {
        //for (int i = 0; i < btn_click_game.Length; i++) {
        //    btn_click_game[i].transform.DOKill();
        //}
        onClickGame(game.name);
        //tf_effect.localPosition = game.transform.localPosition;

        //vtPosCenter = mainTransform.InverseTransformPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        //tf_effect.position = game.transform.position;
        tf_effect.transform.SetParent(game.transform);
        tf_effect.localPosition = Vector3.zero;
        game.transform.DOScale(1.05f, 0.6f).SetLoops(-1);
    }
    public void onClickGame(string obj) {
        PanelWaiting.instance.ShowLoading();
        SoundManager.instance.startClickButtonAudio();
        switch (obj) {
            case "Button_Phom":
                BaseInfo.gI().gameID = GameID.PHOM;
                break;
            case "Button_TLMN":
                BaseInfo.gI().gameID = GameID.TLMN;
                break;
            case "Button_XiTo":
                BaseInfo.gI().gameID = GameID.XITO;
                break;
            case "Button_MB":
                BaseInfo.gI().gameID = GameID.MAUBINH;
                break;
            case "Button_Poker":
                BaseInfo.gI().gameID = GameID.POKER;
                break;
            case "Button_Sam":
                BaseInfo.gI().gameID = GameID.XAM;
                break;
            case "Button_3Cay":
                BaseInfo.gI().gameID = GameID.BACAY;
                break;
            case "Button_Lieng":
                BaseInfo.gI().gameID = GameID.LIENG;
                break;
            case "Button_XocDia":
                BaseInfo.gI().gameID = GameID.XOCDIA;
                break;
            case "Button_TaiXiu":
                BaseInfo.gI().gameID = GameID.TAIXIU;
                //sua
                //gameControl.setCasino(GameID.TAIXIU, BaseInfo.gI().typetableLogin);
                //SendData.onjoinTaiXiu((byte)BaseInfo.gI().typetableLogin);
                //gameControl.top.setGameName();
                return;
            case "Button_TLMNSl":
                BaseInfo.gI().gameID = GameID.TLMNsolo;
                break;
            case "Button_Xeng":
                BaseInfo.gI().gameID = GameID.XENG;
                //SendData.onJoinXengHoaQua();
                //gameControl.top.setGameName();
                return;
        }

        //gameControl.top.setGameName();
        SendData.onSendGameID((sbyte)BaseInfo.gI().gameID);
    }

    public int getIndexBtnGame(int gameID) {
        int index = 0;
        switch (BaseInfo.gI().gameID) {
            case GameID.PHOM:
                index = 1;
                break;
            case GameID.TLMN:
                index = 2;
                break;
            case GameID.TLMNsolo:
                index = 8;
                break;
            case GameID.XAM:
                index = 0;
                break;
            case GameID.BACAY:
                index = 6;
                break;
            case GameID.XITO:
                index = 9;
                break;
            case GameID.LIENG:
                index = 5;
                break;
            case GameID.POKER:
                index = 7;
                break;
            case GameID.MAUBINH:
                index = 3;
                break;
            case GameID.XOCDIA:
                index = 4;
                break;
        }

        return index;
    }
}
