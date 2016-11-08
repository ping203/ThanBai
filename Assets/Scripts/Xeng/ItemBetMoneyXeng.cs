using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemBetMoneyXeng : MonoBehaviour {
    public Text text_money;
    public Text text_rate;
    public Button btn_click;

    [HideInInspector]
    public int id;
    [HideInInspector]
    public long money;
    [HideInInspector]
    public float rate;

    public void setMoney(long _money) {
        money += _money;
        text_money.text = money + "";
    }

    public void setInfo(int _id, long _money, float _rate) {
        id = _id;
        money = _money;
        rate = _rate;
        text_money.text = money + "";
        text_rate.text = "X" + rate;
    }
}
