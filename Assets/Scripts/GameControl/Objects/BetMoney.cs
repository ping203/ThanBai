using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BetMoney {
    public int typeMoney;
    public List<long> listBet = new List<long>();
    public long maxMoney;

    public BetMoney() {

    }

    public void setListBet(long betMoney) {
        listBet.Add(betMoney);
    }
}
