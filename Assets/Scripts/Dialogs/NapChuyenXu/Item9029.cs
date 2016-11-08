using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Item9029 : MonoBehaviour {
    public Text lb_vnd, lb_xu;
    public string Name;
    public string sys;
    // vnd, xu;
    public short port;
    public long money;

    public void setText(string name, string sys, short port, long money) {
        Name = name;
        this.sys = sys;
        this.port = port;
        this.money = money;

        lb_vnd.text = BaseInfo.formatMoneyDetailDot(long.Parse(name)) + " VND";
        lb_xu.text = " =   " + BaseInfo.formatMoneyDetailDot(money) + " " + Res.MONEY_VIP_UPPERCASE;
    }

    public void ClickItem9029() {
        PanelSMS.instance.onClick9029(Name, money, sys, port);
    }
}
