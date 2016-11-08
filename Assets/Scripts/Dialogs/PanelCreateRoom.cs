using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class PanelCreateRoom : PanelGame
{
    //public Slider sliderMoney;
    public Text muccuocTxt;
    public Text inputPlayer;

    float rateVIP, rateFREE;
    private int indexMucCuoc;
    private long currentMucCuoc;
    public InputField passwordIp;
    private List<long> listMC = new List<long>();

    // Use this for initialization
    void Start()
    {
        //sliderMoney.onValueChanged.AddListener(onChangeMoney);
        indexMucCuoc = 0;
        listMC.AddRange(RoomViewScript.instance.listMucCuoc);
        currentMucCuoc = listMC[indexMucCuoc];
        muccuocTxt.text = currentMucCuoc.ToString();
    }

    public void NextMucCuoc()
    {
        indexMucCuoc++;
        if (indexMucCuoc > listMC.Count - 1)
            indexMucCuoc = listMC.Count - 1;
        currentMucCuoc = listMC[indexMucCuoc];
        muccuocTxt.text = currentMucCuoc.ToString();
    }

    public void PrevMucCuoc()
    {
        indexMucCuoc--;
        if (indexMucCuoc < 0)
            indexMucCuoc = 0;
        currentMucCuoc = listMC[indexMucCuoc];
        muccuocTxt.text = currentMucCuoc.ToString();
    }

    //public void onChangeMoney(float value) {
    //    if (BaseInfo.gI().typetableLogin == Res.ROOMVIP) {
    //        rateVIP = (float)1 / BaseInfo.gI().listBetMoneysVIP.Count;
    //        for (int j = 0; j < BaseInfo.gI().listBetMoneysVIP.Count; j++) {
    //            if (value <= j * rateVIP) {
    //                //inputMoney.text = BaseInfo.formatMoneyDetailDot(BaseInfo.gI().listBetMoneysVIP[j].listBet[0]);
    //                break;
    //            }
    //        }
    //    } else {
    //        rateFREE = (float)1 / BaseInfo.gI().listBetMoneysFREE.Count;
    //        for (int j = 0; j < BaseInfo.gI().listBetMoneysFREE.Count; j++) {
    //            if (value <= j * rateFREE) {
    //                //inputMoney.text = BaseInfo.formatMoneyDetailDot(BaseInfo.gI().listBetMoneysFREE[j].listBet[0]);
    //                break;
    //            }
    //        }
    //    }
    //}

    public void createTableGame()
    {
        try
        {
            SoundManager.instance.startClickButtonAudio();
            int gameid = BaseInfo.gI().gameID;
            //string strMoney = muccuocTxt.text;
            string strMaxPlayer = inputPlayer.text;
            if (strMaxPlayer == "")
            {
                PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Quý khách chưa điền đủ thông tin!");
                return;
            }
            if (/*!BaseInfo.gI().checkNumber(strMoney) || */!BaseInfo.gI().checkNumber(strMaxPlayer))
            {
                PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Nhập sai!");
                return;
            }
            //long money = long.Parse(strMoney);
            int maxplayer = int.Parse(strMaxPlayer);
            string password = passwordIp.text;
            bool check = false;
            string info = "";
            switch (gameid)
            {
                case GameID.TLMN:
                case GameID.PHOM:
                case GameID.XAM:
                case GameID.MAUBINH:
                    {
                        if (maxplayer > 4 || maxplayer < 2)
                        {
                            check = false;
                            info = "Số người phải lớn hơn 2 và nhỏ hơn 4";
                        }
                        else
                        {
                            check = true;
                        }
                        break;
                    }
                case GameID.POKER:
                case GameID.XITO:
                case GameID.LIENG:
                case GameID.BACAY:
                    {
                        if (maxplayer > 5 || maxplayer < 2)
                        {
                            check = false;
                            info = "Số người phải lớn hơn 2 và nhỏ hơn 5";
                        }
                        else
                        {
                            check = true;
                        }
                        break;
                    }
                case GameID.XOCDIA:
                    {
                        if (maxplayer != 9)
                        {
                            check = false;
                            info = "Số người phải bằng 9!";
                        }
                        else
                            check = true;
                    }
                    break;
                case GameID.TLMNsolo:
                    {
                        if (maxplayer != 2)
                        {
                            check = false;
                            info = "Số người phải bằng 2!";
                        }
                        else
                            check = true;
                    }
                    break;
            }
            if (check)
            {
                //if (BaseInfo.gI().typetableLogin == Res.ROOMVIP) {
                if (10 * currentMucCuoc > BaseInfo.gI().mainInfo.moneyXu)
                {
                    PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Không đủ tiền để tạo bàn!");
                }
                else
                {
                    //PopupAndLoadingScript.instance.ShowLoading();
                    SendData.onCreateTable(gameid, 2, currentMucCuoc, maxplayer, 0, password);
                    GetComponent<UIPopUp>().HideDialog();
                }
                //} else {
                //    if (10 * currentMucCuoc > BaseInfo.gI().mainInfo.moneyChip) {
                //        PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Không đủ tiền để tạo bàn!");
                //    } else {
                //        SendData.onCreateTable(gameid, 1, currentMucCuoc, maxplayer, 0, password);
                //    }
                //}
            }
            else
            {
                PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", info);
            }
        }
        catch (Exception e)
        {
            //sua
            //GameControl.instance.toast.showToast("Định dạng bàn không đúng!");
        }
    }

    public override void onShow()
    {
        //sliderMoney.value = 0;
        //onChangeMoney(0);
        base.onShow();
    }
}
