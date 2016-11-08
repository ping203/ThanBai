using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelDatCuoc : PanelGame {
    public Slider sliderMoney;
    public Text inputMoney;
    private long money;
    float rateVIP, rateFREE;
    // Use this for initialization
    void Start() {
        sliderMoney.onValueChanged.AddListener(onChangeMoney);
    }

    public void onChangeMoney(float value) {
        if (BaseInfo.gI().typetableLogin == Res.ROOMVIP) {
            rateVIP = (float)1 / BaseInfo.gI().listBetMoneysVIP.Count;
            for (int j = 0; j < BaseInfo.gI().listBetMoneysVIP.Count; j++) {
                if (value <= j * rateVIP) {
                    inputMoney.text = BaseInfo.formatMoneyDetailDot(BaseInfo.gI().listBetMoneysVIP[j].listBet[0]);
                    money = BaseInfo.gI().listBetMoneysVIP[j].listBet[0];
                    break;
                }
            }
        } else {
            rateFREE = (float)1 / BaseInfo.gI().listBetMoneysFREE.Count;
            for (int j = 0; j < BaseInfo.gI().listBetMoneysFREE.Count; j++) {
                if (value <= j * rateFREE) {
                    inputMoney.text = BaseInfo.formatMoneyDetailDot(BaseInfo.gI().listBetMoneysFREE[j].listBet[0]);
                    money = BaseInfo.gI().listBetMoneysFREE[j].listBet[0];
                    break;
                }
            }
        }
    }

    public void clickOK() {
        SoundManager.instance.startClickButtonAudio();
        SendData.onChangeBetMoney(money);
        onHide();
    }
    //public void onShow() {
    //    sliderMoney.value = 0;
    //    onChangeMoney(0);
    //    base.onShow();
    //}
}
