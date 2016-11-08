using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

/*
public class Xeng : BaseCasino {

   const int LOOP_COUNT = 5;
   const float TIME_RUN = 0.05f;

   public static long bet_money = 0;
   long[] list_bet_money = { 50000, 10000, 5000, 1000, 500, 200, 100, 50 };
   int[] id_xeng = { 7, 5, 11, 1, 8, 18, 6, 4, 14, 9, 8, 17, 7, 5, 12, 2, 8, 16, 6, 13, 3, 9, 8, 15 };
   int[] item_rate = { 100, 40, 30, 20, 20, 15, 10, 5 };
   int code_sever = 0;
   public ItemXeng[] list_item_xeng;// chua vong xoay
   public ItemBetMoneyXeng[] list_item_bet_money;//chua danh sach dat cuoc
   public Toggle[] tg_bet_money;//chua danh sach muc cuoc

   float time;
   int loop;
   int index;
   int randomIndex;
   bool isSpin = false;
   float time_count = 0;

   public Text text_TongTien, text_ThangCuoc;
   public Button btn_spin, btn_reset;
   long money_total;

   new void Awake() {
       for (int i = 0; i < list_item_xeng.Length; i++) {
           list_item_xeng[i].id = id_xeng[i];
       }
       for (int i = 0; i < list_item_bet_money.Length; i++) {
           ItemBetMoneyXeng it = list_item_bet_money[i];
           it.setInfo(i + 1, 0, item_rate[i]);
           it.btn_click.onClick.AddListener(delegate {
               onClickItemBetMoney(it);
           });
       }
   }

   void OnEnable() {
       setMoneyInGame();
       resetGame();
   }

   void FixedUpdate() {
       if (isSpin) {
           time_count += Time.deltaTime;
           if (time_count >= time) {
               time_count = 0;
               spin();
           }
       }
   }

   public void onClick_Xeng(string action) {
       SoundManager.instance.clickBtnAudio();
       switch (action) {
           case "spin":
               btn_spin.enabled = false;
               onClick_spin();
               break;
           case "reset":
               resetGame();
               break;
       }
   }

   void onClick_spin() {
       SendData.onDatCuocXengHoaQua(list_item_bet_money);
       BaseInfo.gI().mainInfo.moneyVip = money_total;
   }

   void resetGame() {
       resetSpin();
       setMoneyInGame();
       text_ThangCuoc.text = "0";
       btn_spin.enabled = false;
       btn_reset.enabled = true;
       for (int i = 0; i < list_item_bet_money.Length; i++) {
           ItemBetMoneyXeng it = list_item_bet_money[i];
           it.money = 0;
           it.setMoney(0);
       }

       enableList(false);
       bet_money = 0;
   }

   void enableList(bool isEn) {
       for (int i = 0; i < tg_bet_money.Length; i++) {
           tg_bet_money[i].isOn = isEn;
       }
   }
   //Nhận code từ server và quay
   int[] codeFromServer = new int[3];
   long moneyWin;
   public void recieveCode(Message message) {
       try {
           resetSpin();
           int cod1 = message.reader().ReadInt();
           int cod2 = message.reader().ReadInt();
           int cod3 = message.reader().ReadInt();
           codeFromServer[0] = cod1;
           codeFromServer[1] = cod2;
           codeFromServer[2] = cod3;

           moneyWin = message.reader().ReadLong();
           enableList(false);
           randomIndex = getPosResult(codeFromServer[0]);
           isSpin = true;

           btn_reset.enabled = false;
       } catch (Exception e) {
           Debug.LogException(e);
       }
   }

   public void onClickBet(int index) {
       SoundManager.instance.MoneyAudio();
       btn_reset.enabled = true;
       bet_money = list_bet_money[index];
       for (int i = 0; i < list_item_bet_money.Length; i++) {
           ItemBetMoneyXeng it = list_item_bet_money[i];
           it.btn_click.enabled = true;
       }
   }

   void spin() {
       index++;
       if (index > list_item_xeng.Length - 1) {
           list_item_xeng[index - 1].setEffect(false);
           index = -1;
           loop--;
           if (loop == 3 || loop == 2 || loop == 1) {
               time = TIME_RUN * (8 - loop * 2);
           }
       }
       if (index > 0)
           list_item_xeng[index - 1].setEffect(false);
       else {
           index = 0;
       }
       list_item_xeng[index].setEffect(true);
       //Ket thuc
       if (loop == 1) {
           if (index == randomIndex) {
               isSpin = false;
               text_ThangCuoc.text = moneyWin + "";
               BaseInfo.gI().mainInfo.moneyVip += moneyWin;
               setMoneyInGame();
               if (codeFromServer[0] != 9) {
                   list_item_xeng[index].setFinish();
               } else {
                   int r1 = getPosResult(codeFromServer[1]);
                   int r2 = getPosResult(codeFromServer[2]);
                   list_item_xeng[r1].setFinish();
                   list_item_xeng[r2].setFinish();
               }
               if (moneyWin <= 0) {
                   SoundManager.instance.start_xeng_lose();
               } else {
                   SoundManager.instance.start_xeng_win();
               }
               //for (int i = 0; i < list_item_bet_money.Length; i++) {
               //    ItemBetMoneyXeng it = list_item_bet_money[i];
               //    it.money = 0;
               //    it.setMoney(0);
               //}
               bet_money = 0;
           }
       }
   }

   //lay danh sach vi tri cac xeng
   List<int> getListPosByID(int id) {
       List<int> list = new List<int>();
       for (int i = 0; i < list_item_xeng.Length; i++) {
           if (id == list_item_xeng[i].id) {
               list.Add(i);
           }
       }
       return list;
   }
   int getPosResult(int code_sever) {
       List<int> l = getListPosByID(code_sever);
       return l[UnityEngine.Random.Range(0, l.Count)];
   }

   void resetSpin() {
       time = TIME_RUN;
       loop = LOOP_COUNT;
       index = -1;
       for (int i = 0; i < list_item_xeng.Length; i++) {
           list_item_xeng[i].reset();
       }
       isSpin = false;
       time_count = 0;
       moneyWin = 0;
       text_ThangCuoc.text = moneyWin + "";
       bet_money = 0;
   }

   void setMoneyInGame() {
       SoundManager.instance.clickBtnAudio();
       money_total = BaseInfo.gI().mainInfo.moneyVip;
       text_TongTien.text = "" + money_total;
   }

   void onClickItemBetMoney(ItemBetMoneyXeng obj) {
       SoundManager.instance.clickBtnAudio();
       if (bet_money == 0)
           return;
       obj.setMoney(bet_money);
       money_total -= bet_money;
       text_TongTien.text = "" + money_total;
       btn_spin.enabled = true;
       btn_reset.enabled = true;
   }

   public override void onJoinTableSuccess(string master) {
   }

   public override void setMasterSecond(string master) {
   }

   //public override void onBack() {
   //    PanelWaiting.instance.ShowLoading();
   //    SendData.onExitXengHoaQua();
   //}
}
   */
