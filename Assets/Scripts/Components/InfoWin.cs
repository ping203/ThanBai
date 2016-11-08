using UnityEngine;
using System.Collections;

public class InfoWin {
    public string name;
    public long money;
    public bool isMe;
    public string stt;

    public InfoWin(string stt, string name, long money, bool isMe) {
        // TODO Auto-generated constructor stub
        this.isMe = isMe;
        this.money = money;
        this.name = name;
        this.stt = stt;

    }
}
