using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelRutTien : PanelGame {
    public static PanelRutTien instance;
    public Slider slider;
    public Text lb_current;
    public Toggle checkbox;
    int idtable, idroom, idgame, type;
    int typeRoom;
    long tienmin = 0, tienmax, tienchon;
    //public Text lb_rut;
    public bool tuDongRutTien = false;
    public int soTienRut;

    void Awake() {
        instance = this;
    }

    // Use this for initialization
    void Start() {
        slider.onValueChanged.AddListener(onValueChange);
    }

    public void clickButtonOk() {
        SoundManager.instance.startClickButtonAudio();
        if (checkbox.isOn) {
            BaseInfo.gI().tuDongRutTien = true;
            BaseInfo.gI().soTienRut = tienchon;
        } else {
            BaseInfo.gI().tuDongRutTien = false;
        }

        switch (type) {
            case 0:
                //SendData.onJoinTable(idtable,
                //       "", tienchon);

                PanelWaiting.instance.ShowLoading();
                SendData.onJoinTable(idtable, "", tienchon);
               // Debug.Log("=========================Table ID: " + idtable);
                break;
            case 1:
                // SendData.onAcceptInviteFriend ((sbyte) idgame,
                //        (short) idroom, (short) idtable, tienchon);

                PanelWaiting.instance.ShowLoading();
                SendData.onAcceptInviteFriend((sbyte)idgame,
                        (short)idroom, (short)idtable, tienchon);
                break;
            case 2:
            case 3:
                SendData.onSendGetMoney(tienchon);
                break;
        }
        onHide();
    }

    public void show(long tienMin, long tienMax, int type,
             int idTable, int roomID, int gameID, int typeRoom) {
        long temp;
        string moneyName;
        //if (typeRoom == 1) {
        //    temp = BaseInfo.gI().mainInfo.moneyChip;
        //    moneyName = Res.MONEY_FREE;
        //} else {
            temp = BaseInfo.gI().mainInfo.moneyXu;
            moneyName = Res.MONEY_VIP;
        //}

        if (tienMin > temp) {
            BaseInfo.gI().currentMinMoney = tienMin;
            BaseInfo.gI().currentMaxMoney = tienMax;
            //SendData.onJoinTablePlay (idTable, "",
            //        temp);
        } else {
            BaseInfo.gI().currentMinMoney = tienMin;
            BaseInfo.gI().currentMaxMoney = tienMax;

            if (tienMax > temp) {
                tienMax = temp;
            }
            if (temp < tienMin) {
                tienMin = temp;
            } else {

            }
            tienmin = tienMin;
            tienmax = tienMax;
            tienchon = tienMin;
            lb_current.text = BaseInfo.formatMoneyDetailDot(tienchon);
            slider.value = 0;

            //lb_rut.text = BaseInfo.formatMoney(tienchon);
            this.type = type;
            idtable = idTable;
            idroom = roomID;
            idgame = gameID;
            onShow();
        }
    }

    public void onValueChange(float value) {
        tienchon = (int)(value * (tienmax));
        if (tienchon < tienmin) {
            tienchon = tienmin;
        }
        lb_current.text = BaseInfo.formatMoneyDetailDot(tienchon);
    }
}
