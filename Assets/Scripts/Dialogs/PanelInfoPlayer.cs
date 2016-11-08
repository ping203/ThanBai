using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelInfoPlayer : PanelGame {

    public static PanelInfoPlayer instance;
    public Text txt_id;
    public Text txt_name;
    public Text txt_xu;
    public Text txt_chip;
    public Text winTxt;
    public Text lostTxt;
    public Text levelTxt;
    // public Text chip;
    public Image Img_Avata;
    public RawImage Raw_Avata;
    //public PanelChangePassword panelChangePassword;
    //public PanelChangeName panelChangeName;
    //public PanelChangeAvata panelChangeAvata;
    public GameObject changePass, changeName, changeAvata, updateInfo;
    public InputField ip_email, ip_phone;

    //public Text[] label;

    //public Image[] stars;

    void Awake() {
        instance = this;
    }

     WWW www;
     bool isOne = false;
     // Update is called once per frame
     void Update() {
         if (www != null) {
             if (www.isDone && !isOne) {
                 Raw_Avata.texture = www.texture;
                 isOne = true;
             }
         }
     }

    bool isLoginFB;
    void OnEnable() {
        //sua
        //isLoginFB = LobbyViewScript.instance.login.isLoginFB;
    }

    public void ClickChangePass() {
        SoundManager.instance.startClickButtonAudio();
        if (isLoginFB) {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Quý khách không thể đổi mật khẩu.");
        } else {
            //GameControl.instance.panelChangePassword.onShow();
            //onHide();
            LoadAssetBundle.LoadScene(Res.CHANGE_PASS_AB, Res.CHANGE_PASS_NAME);
            CloseInfoPlayer();
        }
    }

    public void ClickChangeName() {
        SoundManager.instance.startClickButtonAudio();
        if (isLoginFB) {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Quý khách không thể đổi tên.");
        } else {
            //PopupAndLoadingScript.instance.popup.ShowPopupOneButton("",BaseInfo.gI().mainInfo.displayname);
            //onHide();
            LoadAssetBundle.LoadScene(Res.CHANGE_NAME_AB, Res.CHANGE_NAME_NAME);
            CloseInfoPlayer();
        }
    }

    public void ClickChangeAvata() {
        SoundManager.instance.startClickButtonAudio();
        if (isLoginFB) {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Quý khách không thể đổi tên.");
        } else {
            //GameControl.instance.panelChangeAvata.onShow();
            LoadAssetBundle.LoadScene(Res.CHANGE_AVATAR_AB, Res.CHANGE_AVATAR_NAME);
            //onHide();
            CloseInfoPlayer();
        }
    }

    public void ClickHopThu() {
        LoadAssetBundle.LoadScene(Res.CHATADMIN_AB, Res.CHATADMIN_NAME);
        CloseInfoPlayer();
    }

    //public void ClickUpdateInfo() {
    //    string phone = ip_phone.text;
    //    string email = ip_email.text;
    //    string info = "";
    //    if (phone.Equals("") || email.Equals("")) {
    //        info = "Bạn chưa nhập email hoặc số điện thoại.";
    //    } else if (!BaseInfo.gI().checkNumber(phone) || phone.Length < 10 || phone.Length > 11) {
    //        info = "Định dạng số điện thoại không đúng.";
    //    } else if (!BaseInfo.gI().checkMail(email)) {
    //        info = "Định dạng email không đúng.";
    //    }
    //    if (!info.Equals("")) {
    //        PopupAndLoadingScript.instance.popup.ShowPopupOneButton("",info);
    //    } else {
    //        SendData.onUpdateProfile(email, phone);
    //    }
    //}

    //public void onClickEditMail() {
    //    string email = ip_email.text;
    //    if (email.Equals(""))
    //        return;

    //    SendData.onUpdateProfile(email, BaseInfo.gI().mainInfo.phoneNumber);
    //    BaseInfo.gI().mainInfo.email = email;
    //}

    //public void onClickEditPhone() {
    //    string phone = ip_phone.text;
    //    if (phone.Equals(""))
    //        return;
    //    if (phone.Length == 10 || phone.Length == 11
    //                        || phone.Length == 12) {
    //        SendData.onUpdateProfile(BaseInfo.gI().mainInfo.email, phone);
    //        BaseInfo.gI().mainInfo.phoneNumber = phone;
    //    } else {
    //        GameControl.instance.panelMessageSytem.onShow("Số điện thoại không đúng!", delegate {
    //        });
    //    }
    //}

    public void InfoMe() {
        string name = "";
        if (!string.IsNullOrEmpty(BaseInfo.gI().mainInfo.displayname))
            name = BaseInfo.gI().mainInfo.displayname.ToUpper();
        else
            name = BaseInfo.gI().mainInfo.nick.ToUpper();
        long uid = BaseInfo.gI().mainInfo.userid;
        long xuMe = BaseInfo.gI().mainInfo.moneyXu;
        long chipMe = BaseInfo.gI().mainInfo.moneyChip;
        string slt = BaseInfo.gI().mainInfo.soLanThang;
        string slth = BaseInfo.gI().mainInfo.soLanThua;
        int idAva = BaseInfo.gI().mainInfo.idAvata;
        if (idAva <= 1) {
            idAva = 1;
        } else if (idAva >= 60) {
            idAva = 60;
        }
        //idAva++;
        string link_ava = BaseInfo.gI().mainInfo.link_Avatar;
        string email = BaseInfo.gI().mainInfo.email;
        string phone = BaseInfo.gI().mainInfo.phoneNumber;
        int num_star = BaseInfo.gI().mainInfo.level_vip;
        InfoProfile(name, uid, xuMe, chipMe, slt, slth, link_ava, idAva, email, phone, num_star);
    }

    public void UpdateAvata() {
        int id = BaseInfo.gI().mainInfo.idAvata;
        if(id<= 1) {
            id = 1;
        }else if(id >= 60) {
            id = 60;
        }
        //Img_Avata.sprite = Res.getAvataByID(id);
        LoadAssetBundle.LoadSprite(Img_Avata, Res.AS_AVATA, "" + id);
    }

    public void InfoProfile(string nameinfo, long userid, long xuinfo, long chipinfo,
        string slthang, string slthua, string link_avata, int idAvata,
        string email, string phone, int num_star) {
        // Ẩn các thông tin ko phải của mh
        changePass.SetActive(false);
        changeName.SetActive(false);
        changeAvata.SetActive(false);
        // updateInfo.SetActive(false);
        // ip_email.readOnly = true;
        // ip_phone.readOnly = true;

        if (GameControl.instance.isInfo) {
            // bool isMe = nameinfo.Equals(BaseInfo.gI().mainInfo.displayname);
            //Neu của mh thì hiện lên
            changePass.SetActive(true);
            changeName.SetActive(true);
            changeAvata.SetActive(true);
            // updateInfo.SetActive(isMe);
            // if (isMe) {
            //  ip_email.readOnly = false;
            //  ip_phone.readOnly = false;
            // }
        }

        txt_name.text = nameinfo;
        txt_id.text = userid.ToString();
        txt_xu.text = BaseInfo.formatMoneyDetailDot(xuinfo) + " " + Res.MONEY_VIP_UPPERCASE;
        //txt_chip.text = BaseInfo.formatMoneyDetailDot(chipinfo) + " " + Res.MONEY_FREE_UPPERCASE;
        //ip_email.text = email;
        //ip_phone.text = phone;

        //for (int i = 0; i < 5; i++) {
        //    //stars[i].spriteName = "Sao_toi_to";
        //    //if (i < num_star) {
        //    //    stars[i].spriteName = "Sao_sang_to";
        //    //}
        //}
        levelTxt.text = num_star.ToString();

        if (slthang.Length != 0 && slthua.Length != 0) {
            string[] st = slthang.Split(',');
            int slth = 0;
            int slthu = 0;
            for (int i = 0; i < st.Length; i++) {
                string[] kq = st[i].Split('-');
                //label[i].text = kq[1];
                slth += int.Parse(kq[1]);
            }

            string[] st1 = slthua.Split(',');
            for (int i = 0; i < st1.Length; i++) {
                string[] kq = st1[i].Split('-');
                //label[i].text += "/" + kq[1];
                slthu += int.Parse(kq[1]);
            }

            winTxt.text = "Thắng: " + slth;
            lostTxt.text = "Thua: " + slthu;
        }
        //www = null;
        if (link_avata.Equals("")) {
            Img_Avata.gameObject.SetActive(true);
            Raw_Avata.gameObject.SetActive(false);
            //Img_Avata.sprite = Res.getAvataByID(idAvata);
            LoadAssetBundle.LoadSprite(Img_Avata, Res.AS_AVATA, "" + idAvata);
        } else {
            Img_Avata.gameObject.SetActive(false);
            Raw_Avata.gameObject.SetActive(true);
            www = new WWW(link_avata);
            isOne = false;
            // StartCoroutine(getAvata(link_avata));
        }
    }

    public void CloseInfoPlayer() {
        GetComponent<UIPopUp>().HideDialog();
    }
/*
    IEnumerator getAvata(string link) {
        WWW www = new WWW(link);
        yield return www;
        Img_Avata.gameObject.SetActive(false);
        Raw_Avata.gameObject.SetActive(true);
        Raw_Avata.texture = www.texture;
        www.Dispose();
        www = null;
    }*/
}
