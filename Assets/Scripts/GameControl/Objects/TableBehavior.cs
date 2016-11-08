using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;

public class TableBehavior : MonoBehaviour {
    //UI
    [SerializeField]
    Text lb_tenban;
    [SerializeField]
    Text lb_muccuoc, lb_can;
    [SerializeField]
    Slider slide_tinhtrang;
    [SerializeField]
    Slider slide_tinhtrang_XocDia;
    [SerializeField]
    Text numPlayer;
    [SerializeField]
    Image spriteLock;
    [SerializeField]
    Image backgroundSprite;
    [SerializeField]
    Color[] color;
    TableItem tableItem;

    //public void Scroll_CellIndex(TableItem tableItem) {
    //    this.tableItem = tableItem;
    //    if (index % 2 == 0) {
    //        backgroundSprite.color = color[0];
    //    } else {
    //        backgroundSprite.color = color[1];
    //    }
    //    lb_tenban.text = "Bàn " + tableItem.id;
    //    //lb_muccuoc.text = BaseInfo.formatMoney(money) + " " + BaseInfo.formatMoney(needMoney);
    //    // if (typeTable == Res.ROOMFREE) {
    //    lb_muccuoc.text = BaseInfo.formatMoneyNormal(tableItem.money) + " " + Res.MONEY_FREE;
    //    lb_can.text = BaseInfo.formatMoneyNormal(tableItem.needMoney) + " " + Res.MONEY_FREE;
    //    // } else {
    //    //   lb_muccuoc.text = BaseInfo.formatMoneyDetailDot(money) + " " + Res.MONEY_VIP;
    //    //     lb_can.text = BaseInfo.formatMoneyDetailDot(needMoney) + " " + Res.MONEY_VIP;
    //    //}
    //    if (GameControl.instance.gameID == GameID.XOCDIA) {
    //        slide_tinhtrang.gameObject.SetActive(false);
    //        slide_tinhtrang_XocDia.gameObject.SetActive(true);
    //    } else {
    //        slide_tinhtrang.gameObject.SetActive(true);
    //        slide_tinhtrang_XocDia.gameObject.SetActive(false);
    //        slide_tinhtrang.GetComponent<RectTransform>().sizeDelta = new Vector2(20 * tableItem.maxUser, 25);
    //    }
    //    slide_tinhtrang.value = (float)tableItem.nUser / tableItem.maxUser;

    //    if (tableItem.Lock == 1) {
    //        spriteLock.gameObject.SetActive(true);
    //    } else {
    //        spriteLock.gameObject.SetActive(false);
    //    }
    //    numPlayer.text = tableItem.nUser + "/" + tableItem.maxUser;
    //}

    void ScrollCellIndex(int index) {
        tableItem = RoomViewScript.instance.listTable[index];
        setInfo(tableItem, index);
    }

    void setInfo(TableItem tableItem, int index) {
        if (index % 2 == 0) {
            backgroundSprite.color = color[0];
        } else {
            backgroundSprite.color = color[1];
        }
        lb_tenban.text = "Bàn " + tableItem.id;
        //lb_muccuoc.text = BaseInfo.formatMoney(money) + " " + BaseInfo.formatMoney(needMoney);
        tableItem.typeTable = BaseInfo.gI().typetableLogin;
        //if (tableItem.typeTable == Res.ROOMFREE) {
        //    lb_muccuoc.text = BaseInfo.formatMoneyNormal(tableItem.money) + " " + Res.MONEY_FREE;
        //    lb_can.text = BaseInfo.formatMoneyNormal(tableItem.needMoney) + " " + Res.MONEY_FREE;
        //} else {
            lb_muccuoc.text = BaseInfo.formatMoneyDetailDot(tableItem.money) + " " + Res.MONEY_VIP;
            lb_can.text = BaseInfo.formatMoneyDetailDot(tableItem.needMoney) + " " + Res.MONEY_VIP;
        //}
        if (BaseInfo.gI().gameID == GameID.XOCDIA) {
            slide_tinhtrang.gameObject.SetActive(false);
            slide_tinhtrang_XocDia.gameObject.SetActive(true);
            slide_tinhtrang_XocDia.value = (float)tableItem.nUser / tableItem.maxUser;
            numPlayer.text = tableItem.nUser + "/" + tableItem.maxUser;
        } else {
            slide_tinhtrang.gameObject.SetActive(true);
            slide_tinhtrang_XocDia.gameObject.SetActive(false);
            slide_tinhtrang.GetComponent<RectTransform>().sizeDelta = new Vector2(21 * tableItem.maxUser, 21);
            slide_tinhtrang.value = (float)tableItem.nUser / tableItem.maxUser;
        }

        bool isLock = tableItem.isLock == 1 ? true : false;
        spriteLock.gameObject.SetActive(isLock);
    }

    public void clickTable() {
        SoundManager.instance.startClickButtonAudio();
        long moneyTemp = 0;
        string money = "";
        //if (BaseInfo.gI().typetableLogin == Res.ROOMFREE) {
        //    moneyTemp = BaseInfo.gI().mainInfo.moneyChip;
        //    money = Res.MONEY_FREE;
        //} else {
            moneyTemp = BaseInfo.gI().mainInfo.moneyXu;
            money = Res.MONEY_VIP;
        //}
        if (moneyTemp < tableItem.needMoney) {
            PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("","Bạn cần có ít nhât "
                    + BaseInfo.formatMoney(tableItem.needMoney) + " " + money
                    + " để vào bàn! Bạn muốn nạp thêm " + money + "?", delegate {
                        LoadAssetBundle.LoadScene("sub_chuyen_xu", "subChuyenXu") ;
                    });
        } else {
            BaseInfo.gI().numberPlayer = tableItem.maxUser;
            //if (GameControl.instance.gameID == GameID.POKER
            //        || GameControl.instance.gameID == GameID.XITO
            //        || GameControl.instance.gameID == GameID.LIENG) {
            //    BaseInfo.gI().moneyNeedTable = tableItem.needMoney;
            //    GameControl.instance.panelRutTien.show((long)(tableItem.needMoney), tableItem.maxMoney, 0, tableItem.id, 0, 0, RoomControl.roomType);
            //} else {
            PanelWaiting.instance.ShowLoading();
            //SendData.onJoinTablePlay(id, "", -1);
            SendData.onJoinTablePlay(tableItem.id, "", -1);
            //}
        }

    }
}
