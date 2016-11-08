using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using AppConfig;
using DG.Tweening;

public class PanelMail : PanelGame {
    public static PanelMail instance;
    public GameObject objItemMailPref;
    public GameObject objItemSuKienPref;
    public GameObject objListMail;
    public GameObject objListSuKien;
    public Text title_txt;
    public Text content_txt;
    public Text nocontent_txt;
    private Image currentBgTap;
    public Image tnTapImg;
    public Image skTapImg;
    public Image adminTapImg;
    public GameObject currentItemMailBgPress;
    public GameObject currentItemSuKienBgPress;
    public GameObject objTinNhan;
    public GameObject objSuKien;
    public GameObject objChatAdmin;
    private GameObject currentObj;
    //public Transform tblContaintSuKien;
    //public Transform tblContaintTinNhan;
    //public GameObject itemMail;

    //public PanelMessage panelMessage;

    //List<GameObject> listMail = new List<GameObject>();
    //List<GameObject> listEvent = new List<GameObject>();

    //public Toggle tg_mail, tg_event;

    void Awake() {
        instance = this;
    }

    void Start() {
        currentBgTap = tnTapImg;
        currentObj = objTinNhan;
        InstanItemMail();
        InstanSuKien();
    }

    public void InstanItemMail() {
        int length = BaseInfo.gI().listMail.Count;
        List<Mail> list = new List<Mail>();
        list.AddRange(BaseInfo.gI().listMail);
        if (length > 0)
        {
            for (int i = 0; i < length; i++)
            {
                GameObject mail = Instantiate(objItemMailPref);
                mail.transform.SetParent(objListMail.transform);
                mail.transform.localScale = Vector3.zero;
                mail.GetComponent<ItemMail>().UpdateItemMail(list[i].id, list[i].guiTu, list[i].guiLuc, list[i].content, list[i].isRead);
                if (i == 0)
                {
                    currentItemMailBgPress = mail.transform.GetChild(4).gameObject;
                    currentItemMailBgPress.SetActive(true);
                }
                StartCoroutine(ScaleItemMail(mail, .3f));
            }
            title_txt.gameObject.SetActive(true);
            content_txt.gameObject.SetActive(true);
            title_txt.text = list[0].guiTu + " " + list[0].guiLuc;
            content_txt.text = list[0].content;
        }
        else {
            nocontent_txt.gameObject.SetActive(true);
        }
    }

    private IEnumerator ScaleItemMail(GameObject obj, float time) {
        yield return new WaitForSeconds(time);
        obj.transform.DOScale(1, .2f);
    }

    public void InstanSuKien()
    {
        int length = BaseInfo.gI().listEvent.Count;
        //Debug.LogError("Lenght Su Kien " + length);
        if (length > 0)
        {
            List<Event> list = new List<Event>();
            list.AddRange(BaseInfo.gI().listEvent);
            for (int i = 0; i < length; i++)
            {
                GameObject sukien = Instantiate(objItemSuKienPref);
                sukien.transform.SetParent(objListSuKien.transform);
                sukien.transform.localScale = Vector3.one;
                sukien.GetComponent<ItemSuKien>().UpdateItemSuKien(list[i].id, list[i].title, list[i].content);
                if (i == 0)
                {
                    PlayerPrefs.SetInt(BaseInfo.gI().mainInfo.userid + list[i].id + "", 1);
                    PlayerPrefs.Save();
                    currentItemSuKienBgPress = sukien.transform.GetChild(3).gameObject;
                    currentItemSuKienBgPress.SetActive(true);
                }
            }
            //title_txt.gameObject.SetActive(true);
            //content_txt.gameObject.SetActive(true);
            //title_txt.text = "THÔNG BÁO";
            //content_txt.text = list[0].content;
            //objSuKien.SetActive(false);
        }
    }

    public void ClickMail() {
        SoundManager.instance.startClickButtonAudio();
        SetBG(tnTapImg);
        SetObj(objTinNhan);
        title_txt.gameObject.SetActive(true);
        content_txt.gameObject.SetActive(true);
        if (objListMail.transform.childCount > 0)
        {

            title_txt.text = BaseInfo.gI().listMail[0].guiTu + " " + BaseInfo.gI().listMail[0].guiLuc;
            content_txt.text = BaseInfo.gI().listMail[0].content;
            nocontent_txt.gameObject.SetActive(false);
        }
        else
        {
            title_txt.text = "";
            content_txt.text = "";
            nocontent_txt.gameObject.SetActive(true);
            nocontent_txt.text = ClientConfig.Language.GetText("mail_no_content");
        }
        
    }

    public void ClickSuKien() {
        SoundManager.instance.startClickButtonAudio();
        SetBG(skTapImg);
        SetObj(objSuKien);
        title_txt.gameObject.SetActive(true);
        content_txt.gameObject.SetActive(true);
        if (objListSuKien.transform.childCount > 0) {
            title_txt.text = "THÔNG BÁO";
            content_txt.text = BaseInfo.gI().listEvent[0].content;
            nocontent_txt.gameObject.SetActive(false);
        }
        else
        {
            title_txt.text = "";
            content_txt.text = "";
            nocontent_txt.gameObject.SetActive(true);
            nocontent_txt.text = ClientConfig.Language.GetText("sukien_no_content");
        }
    }

    public void ClickChatAdmin() {
        SoundManager.instance.startClickButtonAudio();
        SetBG(adminTapImg);
        SetObj(objChatAdmin);
        title_txt.gameObject.SetActive(false);
        content_txt.gameObject.SetActive(false);
        nocontent_txt.gameObject.SetActive(false);
    }

    public void ClickItemMail(GameObject obj, string title, string content) {
        SoundManager.instance.startClickButtonAudio();
        currentItemMailBgPress.SetActive(false);
        currentItemMailBgPress = obj;
        title_txt.text = title;
        content_txt.text = content;
    }

    public void ClickItemSuKien(GameObject obj, string title, string content)
    {
        SoundManager.instance.startClickButtonAudio();
        currentItemSuKienBgPress.SetActive(false);
        currentItemSuKienBgPress = obj;
        title_txt.text = title;
        content_txt.text = content;
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

    //void OnEnable() {
    //    if (tg_mail.isOn && listMail.Count <= 0) {
    //        panelMessage.setTextMail("", "", "Không có thư nào!");
    //    }
    //    if (tg_event.isOn && listEvent.Count <= 0) {
    //        panelMessage.setTextSK("Không có sự kiện nào!");
    //    }
    //}

    //public void addIconTinNhan(int id, string guiTu, string guiLuc, string noiDung, sbyte isRead) {
    //    GameObject btnT = Instantiate(itemMail) as GameObject;
    //    itemMail.transform.SetParent(tblContaintTinNhan);
    //    btnT.transform.localScale = Vector3.one;
    //    btnT.GetComponent<ItemMail>().setIconItemTN(id, guiTu, guiLuc, noiDung, isRead);
    //    btnT.GetComponent<Button>().onClick.AddListener(delegate {
    //        ClickDocTN(btnT);
    //    });
    //    listMail.Add(btnT);
    //}

    //public void addIconSuKien(int id, string title, string content) {
    //    GameObject btnT = Instantiate(itemMail) as GameObject;
    //    itemMail.transform.SetParent(tblContaintSuKien);
    //    btnT.transform.localScale = Vector3.one;
    //    btnT.GetComponent<ItemMail>().setIconItemSK(id, title, content);
    //    btnT.GetComponent<Button>().onClick.AddListener(delegate {
    //        ClickDocSK(btnT);
    //    });

    //    listEvent.Add(btnT);
    //}

    //public void ClearListMail() {
    //    for (int i = 0; i < listMail.Count; i++) {
    //        Destroy(listMail[i]);
    //    }
    //    listMail.Clear();
    //}
    //public void ClearListEvent() {
    //    for (int i = 0; i < listEvent.Count; i++) {
    //        Destroy(listEvent[i]);
    //    }
    //    listEvent.Clear();
    //}

    //public void ClickDocTN(GameObject obj) {
    //    for (int i = 0; i < listMail.Count; i++) {
    //        listMail[i].GetComponent<ItemMail>().setCheck(false);
    //    }
    //    ItemMail it = obj.GetComponent<ItemMail>();
    //    it.setCheck(true);
    //    int id = it.id;
    //    string guiluc = it.guiLuc;
    //    string guitu = it.guiTu;
    //    string nd = it.content;
    //    it.setRead();
    //    panelMessage.setTextMail(guitu, guiluc, nd);
    //    SendData.onReadMessage(id);
    //}

    //public void ClickDocSK(GameObject obj) {
    //    for (int i = 0; i < listMail.Count; i++) {
    //        listEvent[i].GetComponent<ItemMail>().setCheck(false);
    //    }
    //    ItemMail it = obj.GetComponent<ItemMail>();
    //    it.setCheck(true);
    //    int id = it.id;
    //    string nd = it.content;

    //    panelMessage.setTextSK(nd);
    //    SendData.onReadMessage(id);
    //}

    //public void onValuaChange(int i) {
    //    if (i == 1) {
    //        if (listMail.Count <= 0) {
    //            panelMessage.setTextMail("", "", "Không có thư nào!");
    //        }
    //    } else {
    //        if (listEvent.Count <= 0) {
    //            panelMessage.setTextSK("Không có sự kiện nào!");
    //        }
    //    }
    //}

    //public bool isShowNew() {
    //    if (listMail.Count > 0 || listEvent.Count > 0)
    //        return true;
    //    return false;
    //}
}
