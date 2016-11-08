using UnityEngine;
using System.Collections;
using System.Globalization;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelDoiThuong : PanelGame {
    public static PanelDoiThuong instance;
    public GameObject tblContaintGiftTheCao;
    public GameObject tblContaintGiftVatPham;
    private Image currentTapImg;
    public Image thecaoImg;
    public Image vatphamImg;
    public GameObject objTheCao;
    public GameObject objVatPham;
    private GameObject currentObj;
    public bool isLoaded = false;

    void Awake() {
        instance = this;
    }

    void Start() {
        currentTapImg = thecaoImg;
        currentObj = objTheCao;
    }

    public void ClickTheCao() {
        SetBG(thecaoImg);
        SetObj(objTheCao);
        if (tblContaintGiftTheCao.transform.childCount == 0)
        {
            addGiftInfoTheCao(BaseInfo.gI().listTheCao);
        }
    }

    public void ClickVatPham() {
        SetBG(vatphamImg);
        SetObj(objVatPham);
        if (tblContaintGiftVatPham.transform.childCount == 0) {
            addGiftInfoVatPham(BaseInfo.gI().listVatPham);
        }
    }

    private void SetBG(Image img) {
        LoadAssetBundle.LoadSprite(currentTapImg, Res.AS_UI, "bg_unpress");
        currentTapImg = img;
        LoadAssetBundle.LoadSprite(currentTapImg, Res.AS_UI, "bg_press");
    }

    private void SetObj(GameObject obj) {
        currentObj.SetActive(false);
        currentObj = obj;
        currentObj.SetActive(true);
    }

    public void addGiftInfoTheCao(List<InfoTheCao> list) {
        if (tblContaintGiftTheCao.transform.childCount == 0)
        {
            List<InfoTheCao> listTheCao = new List<InfoTheCao>();
            listTheCao.AddRange(list);
            if (listTheCao.Count > 0)
            {
                LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Button_Gift", (prefabAB) =>
                {
                    //GameObject thecao = prefabAB;
                    //thecao.transform.SetParent(tblContaintGiftTheCao.transform);
                    //thecao.transform.localScale = Vector3.one;
                    //thecao.GetComponent<InfoGift>().setInfoGift(listTheCao[0].id, listTheCao[0].nameItem, listTheCao[0].links, listTheCao[0].price, listTheCao[0].balance);
                    for (int i = 0; i < listTheCao.Count; i++)
                    {
                        GameObject obj = Instantiate(prefabAB);
                        obj.transform.SetParent(tblContaintGiftTheCao.transform);
                        obj.transform.localScale = Vector3.one;
                        obj.GetComponent<InfoGift>().setInfoGift(listTheCao[i].id, listTheCao[i].nameItem, listTheCao[i].links, listTheCao[i].price, listTheCao[i].balance);
                    }
                    Destroy(prefabAB);
                });
            }
        }
    }

    public void addGiftInfoVatPham(List<InfoVatPham> list)
    {
        if (tblContaintGiftVatPham.transform.childCount == 0)
        {
            List<InfoVatPham> listVatPham = new List<InfoVatPham>();
            listVatPham.AddRange(list);

            if (listVatPham.Count > 0)
            {
                LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Button_Gift", (prefabAB) =>
                {
                    GameObject vatpham = prefabAB;
                    vatpham.transform.SetParent(tblContaintGiftVatPham.transform);
                    vatpham.transform.localScale = Vector3.one;
                    vatpham.GetComponent<InfoGift>().setInfoGift(listVatPham[0].id, listVatPham[0].nameItem, listVatPham[0].links, listVatPham[0].price, listVatPham[0].balance);
                    for (int i = 1; i < listVatPham.Count; i++)
                    {
                        GameObject obj = Instantiate(vatpham);
                        obj.transform.SetParent(tblContaintGiftVatPham.transform);
                        obj.transform.localScale = Vector3.one;
                        obj.GetComponent<InfoGift>().setInfoGift(listVatPham[i].id, listVatPham[i].nameItem, listVatPham[i].links, listVatPham[i].price, listVatPham[i].balance);
                    }
                });
            }
        }
    }

    public void sendGift(int id, long priceGift, string name, long balance) {
        SoundManager.instance.startClickButtonAudio();
        if (BaseInfo.gI().mainInfo.moneyXu <= balance) {
            long money = balance + priceGift;
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Quý khách cần phải có ít nhất "
            + BaseInfo.formatMoneyDetailDot(money) + " " + Res.MONEY_VIP + " để đổi lấy phần quà này!");
            return;
        }
        PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("","Quý khách muốn đổi " + BaseInfo.formatMoneyNormal(priceGift) + " lấy " + name, delegate {
            SendData.onSendGift(id, priceGift);
        });
    }
}
