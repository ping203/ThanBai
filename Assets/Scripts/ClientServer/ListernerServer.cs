using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using AppConfig;

public class ListernerServer : IChatListener
{
    //GameControl gameControl;
    //public static ListernerServer instance;

    public void initConnect()
    {
        NetworkUtil.GI().registerHandler(ProcessHandler.getInstance());
        //Debug.LogError("NetworkUtil " + (NetworkUtil.GI() == null));
        //Debug.LogError("ProcessHandle " + (ProcessHandler.getInstance() == null));
        ProcessHandler.setListenner(this);
        PHandler.setListenner(this);
        TLMNHandler.setListenner(this);
        XiToHandler.setListenner(this);

    }

    public ListernerServer()
    {
        // TODO Auto-generated constructor stub
        //this.gameControl = gameControl;
        initConnect();
    }

    public void onDisConnect()
    {
        PopupAndLoadingScript.instance.HideLoading();
        PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", "Mất kết nối!", delegate
        {
            //gameControl.disableAllDialog();
            //gameControl.setStage(gameControl.login);
            LoadAssetBundle.LoadScene(Res.LOGIN_AB, Res.LOGIN_NAME);
            Debug.Log("Mất kết nối!");
            NetworkUtil.GI().close();
        });
    }

    public void onLoginSuccess(Message msg)
    {
        //Debug.LogError("Login Success");
        try
        {
            BaseInfo.gI().SMS_CHANGE_PASS_SYNTAX = msg.reader().ReadUTF();
            BaseInfo.gI().SMS_CHANGE_PASS_NUMBER = msg.reader().ReadUTF();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        SendData.onSendSmsSyntax();
        if (BaseInfo.gI().TELCO_CODE != 0)
        {
            SendData.onSendSms9029(BaseInfo.gI().TELCO_CODE);
        }

        PopupAndLoadingScript.instance.HideLoading();
        LoginControl.instance.LoadLobby();
        //gameControl.room.onClickGame(gameControl.room.btn_click_game[0].gameObject);
        //LobbyViewScript.instance.UpdateProfileUser();
        //gameControl.top.setGameName();
    }

    public void onLoginFail(int id, string info)
    {
        /*PanelWaiting.instance.HideLoading();
        gameControl.panelThongBao.onShow(info);*/
        string info2 = "";
        switch (id)
        {
            case 0:
                PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", "Tài khoản hoặc mật khẩu không đúng! Bạn có muốn Lấy lại mật khẩu không?", delegate
                {
                    LoginControl.instance.popupGetPass.SetActive(true);
                });
                break;
            case 2:
                LoadAssetBundle.LoadScene(Res.INPUT_AB, Res.INPUT_NAME, () =>
                {
                    PanelInput.instance.onShow("", delegate
                    {
                        string phoneNumber = PanelInput.instance.ip_enter.text;

                        bool kt = false;

                        if (phoneNumber.Equals(""))
                        {
                            info2 = "Nhập vào số điện thoại.";
                            kt = true;
                        }
                        else if (PanelInput.instance.checkSDT(phoneNumber) == -1)
                        {
                            info2 = "Sai định dạng số điện thoại!";
                            kt = true;
                        }
                        else if (PanelInput.instance.checkSDT(phoneNumber) == -3)
                        {
                            info2 = "Số điện thoại phải nhiều hơn 9 và ít hơn 12 ký tự.";
                            kt = true;
                        }
                        if (kt)
                        {
                            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", info2);
                            return;
                        }
                        string imei = "";
#if UNITY_ANDROID && !UNITY_EDITOR
                        imei = ClientConfig.HardWare.IMEI;
#elif UNITY_EDITOR
                        imei = SystemInfo.deviceUniqueIdentifier;
#endif
                        LoginControl.instance.login(4, BaseInfo.gI().username, BaseInfo.gI().pass, imei, "", 1, BaseInfo.gI().username, "", phoneNumber);
                        PanelInput.instance.GetComponent<UIPopUp>().HideDialog();
                    });
                });

                break;
            default:
                break;
        }
        PopupAndLoadingScript.instance.HideLoading();
    }

    public void onListRoom(Message message)
    {
        //return;
        try
        {
            int len = message.reader().ReadByte();
            //gameControl.phongFree.Clear();
            //gameControl.phongVip.Clear();
            //gameControl.phongQT.Clear();
            for (int i = 0; i < len; i++)
            {
                RoomInfo r = new RoomInfo();
                r.setName(message.reader().ReadUTF());
                r.setId(message.reader().ReadByte());
                r.setMoney(message.reader().ReadLong());
                r.setNeedMoney(message.reader().ReadLong());
                r.setnUser(message.reader().ReadShort());
                r.setLevel(message.reader().ReadByte());
            }

            //int idRoom = Res.ROOMFREE;
            //message.reader().ReadInt();
            BaseInfo.gI().typetableLogin = message.reader().ReadInt();
            // message.reader().ReadInt();
            SendData.onJoinRoom(/*BaseInfo.gI().mainInfo.nick, */BaseInfo.gI().typetableLogin);

        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void onListTable(int totalTB, Message message)
    {
        //Debug.LogError("onListTable");
        RoomViewScript.instance.listTable.Clear();
        RoomViewScript.instance.listMucCuoc.Clear();
        List<TableItem> temp = new List<TableItem>();
        for (int i = 0; i < totalTB; i++)
        {
            try
            {
                TableItem ctb = new TableItem();
                ctb.id = (message.reader().ReadShort());
                ctb.status = (message.reader().ReadByte());
                ctb.nUser = (message.reader().ReadByte());
                sbyte Lock = message.reader().ReadByte();
                ctb.isLock = Lock;
                ctb.money = message.reader().ReadLong();
                ctb.needMoney = message.reader().ReadLong();
                ctb.maxMoney = message.reader().ReadLong();
                ctb.maxUser = (message.reader().ReadByte());
                ctb.typeTable = message.reader().ReadInt();
                ctb.choinhanh = message.reader().ReadInt();
                //if (GameControl.instance.gameID == GameID.LIENG) {
                //    Debug.Log(" onListTable     tableItem.nUser" + ctb.nUser + "   tableItem.maxUser  " + ctb.maxUser);
                //}
                RoomViewScript.instance.listTable.Add(ctb);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        try
        {
            int totalTBC = message.reader().ReadUnsignedShort();
            for (int i = 0; i < totalTBC; i++)
            {
                TableItem ctb = new TableItem();
                ctb.id = (message.reader().ReadShort());
                ctb.status = (message.reader().ReadByte());
                ctb.nUser = (message.reader().ReadByte());
                sbyte Lock = message.reader().ReadByte();
                ctb.isLock = Lock;
                ctb.money = message.reader().ReadLong();
                ctb.needMoney = message.reader().ReadLong();
                ctb.maxMoney = message.reader().ReadLong();
                ctb.maxUser = (message.reader().ReadByte());
                ctb.typeTable = message.reader().ReadInt();
                ctb.choinhanh = message.reader().ReadInt();
                //if (GameControl.instance.gameID == GameID.LIENG) {
                //    Debug.Log(" onListTable     tableItem.nUser" + ctb.nUser + "   tableItem.maxUser  " + ctb.maxUser);
                //}
                //RoomViewScript.instance.listTable.Add(ctb);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        #region stuffs
        //BaseInfo.gI().sort_giam_dan_muccuoc = false;
        //gameControl.room.sortMucCuoc();

        //int dem5 = 0, dem9 = 0;
        //int MAX = 1;
        //if (gameControl.gameID == GameID.TLMN
        //        || gameControl.gameID == GameID.PHOM
        //        || gameControl.gameID == GameID.XITO
        //        || gameControl.gameID == GameID.MAUBINH
        //        || gameControl.gameID == GameID.XAM
        //        || gameControl.gameID == GameID.TLMNsolo)
        //{
        //    MAX = 2;
        //}
        //if (gameControl.listTable.Count > 0)
        //{
        //    long muccuoc;
        //    muccuoc = gameControl.listTable[0].money;
        //    for (int i = 0; i < gameControl.listTable.Count; i++)
        //    {
        //        try
        //        {
        //            if (gameControl.listTable[i].nUser != 0)
        //            {
        //                temp.Add(gameControl.listTable[i]);
        //                if (gameControl.listTable[i].money != muccuoc)
        //                {
        //                    dem5 = 0;
        //                    dem9 = 0;
        //                }
        //                muccuoc = gameControl.listTable[i].money;
        //                continue;
        //            }
        //            else
        //            {
        //                if (gameControl.listTable[i].money == muccuoc
        //                        && (gameControl.listTable[i].maxUser < 9))
        //                {
        //                    dem5++;
        //                    if (dem5 <= MAX)
        //                    {
        //                        temp.Add(gameControl.listTable[i]);
        //                        muccuoc = gameControl.listTable[i].money;
        //                        continue;
        //                    }

        //                }
        //                else
        //                {
        //                }
        //                if (gameControl.listTable[i].money == muccuoc
        //                        && (gameControl.listTable[i].maxUser == 9))
        //                {
        //                    dem9++;
        //                    if (dem9 <= MAX)
        //                    {
        //                        temp.Add(gameControl.listTable[i]);
        //                        muccuoc = gameControl.listTable[i].money;
        //                        continue;
        //                    }
        //                }
        //                else
        //                {
        //                }
        //            }
        //            if (gameControl.listTable[i].money != muccuoc)
        //            {
        //                dem5 = 0;
        //                dem9 = 0;
        //                if ((gameControl.listTable[i].maxUser < 9))
        //                {
        //                    dem5++;
        //                    if (dem5 <= MAX)
        //                    {
        //                        temp.Add(gameControl.listTable[i]);
        //                        muccuoc = gameControl.listTable[i].money;
        //                        continue;
        //                    }

        //                }
        //                else
        //                {
        //                }
        //                if ((gameControl.listTable[i].maxUser == 9))
        //                {
        //                    dem9++;
        //                    if (dem9 <= MAX)
        //                    {
        //                        temp.Add(gameControl.listTable[i]);
        //                        muccuoc = gameControl.listTable[i].money;
        //                        continue;
        //                    }
        //                }
        //                else
        //                {
        //                }

        //            }
        //            muccuoc = gameControl.listTable[i].money;
        //        }
        //        catch (Exception e)
        //        {
        //            continue;
        //        }
        //    }
        //}
        #endregion
        //RoomViewScript.instance.listTable.Clear();
        //RoomViewScript.instance.listTable.AddRange(temp);
        //Xep theo muc cuoc, tu thap den cao
        BaseInfo.gI().sort_giam_dan_muccuoc = true;
        RoomViewScript.instance.listTable.Sort(delegate (TableItem tb1, TableItem tb2)
        {
            return tb1.money.CompareTo(tb2.money);
        });
        //=========================================
        //gameControl.room.createScollPane(gameControl.listTable);
        //Debug.LogError("List Table Count " + RoomViewScript.instance.listTable.Count);
        if (RoomViewScript.instance.isClickMC)
            RoomViewScript.instance.InstantiateTables(RoomViewScript.instance.currentMC);
        else
        {
            if (RoomViewScript.instance.listTable.Count > 0)
                RoomViewScript.instance.InstantiateMucCuoc();
        }
        //PopupAndLoadingScript.instance.HideLoading();
        //gameControl.setStage(gameControl.room);
    }

    public void onJoinGame(Message message)
    {
        try
        {
            onGameID(message);
            onListRoom(message);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void onUserJoinTable(int tbid, string nick, string displayname,
        string link_avatar, int idAvata, sbyte pos, long money,
        long folowmoney)
    {
        PlayerInfo pl = new PlayerInfo();
        pl.name = nick;
        pl.displayname = displayname;
        pl.link_avatar = link_avatar;
        pl.idAvata = idAvata;
        pl.money = money;
        pl.pos = pl.posServer = pos;
        pl.folowMoney = folowmoney;
        //gameControl.currentCasino.setPlayerInfo(pl,
        //       gameControl.currentCasino.players[0].serverPos);

        if (!BaseInfo.gI().isView)
        {
            //sua

            try
            {
                GameControl.instance.currentCasino.setPlayerInfo(pl, GameControl.instance.currentCasino.players[0].serverPos);
            }
            catch (Exception)
            {

                throw;
            }
        }
        else
        {
            //sua
            try
            {
                GameControl.instance.currentCasino.setPlayerInfoView(pl, pl.posServer);
            }
            catch (Exception)
            {

                throw;
            }
        }
        SoundManager.instance.startVaobanAudio();
        //SoundManager.instance.PlayVibrate();
    }
    public void onExitView(Message message)
    {
        //gameControl.currentCasino.onExitView(message);
        //sua
        //BaseCasino.instance.resetData();
        //PopupAndLoadingScript.instance.HideLoading();
        //sua
        //if (gameControl.gameID == GameID.XENG || gameControl.gameID == GameID.TAIXIU)
        //{
        //    //gameControl.room.onClickGame(gameControl.room.btn_click_game[0].gameObject);
        //}
        //else
        //{
        //    int i = gameControl.room.getIndexBtnGame(gameControl.gameID);
        //    //gameControl.room.onClickGame(gameControl.room.btn_click_game[i].gameObject);
        //}
        ////gameControl.top.setGameName();
        BaseInfo.gI().isView = false;
        try
        {
            GameControl.instance.currentCasino.LoadRoom();
        }
        catch (Exception)
        {

            throw;
        }
        //gameControl.setStage(gameControl.backState);
    }
    public void onUserExitTable(int idTb, string master, string nick)
    {
        //BaseInfo.gI().isFirstJoinTable = false;
        if (nick.Equals(BaseInfo.gI().mainInfo.nick))
        {
            //try
            //{
            //sua
            //gameControl.disableAllDialog();
            //BaseCasino.instance.resetData();
            //gameControl.currentCasino.DisAppear();
            //int currentGameID = PlayerPrefs.GetInt("gameid", -1);
            //if (currentGameID == GameID.XENG || currentGameID == GameID.TAIXIU)
            //{
            //    //gameControl.room.onClickGame(gameControl.room.btn_click_game[0].gameObject);
            //}
            //else
            //{
            //    int i = gameControl.room.getIndexBtnGame(gameControl.gameID);
            //    //gameControl.room.onClickGame(gameControl.room.btn_click_game[i].gameObject);
            //}
            //gameControl.top.setGameName();
            //Debug.LogError("onUserExitTable LoadRoom");
            try
            {
                GameControl.instance.currentCasino.LoadRoom();
            }
            catch (Exception)
            {

                throw;
            }
            BaseInfo.gI().isView = false;
            //}
            //catch (Exception e)
            //{
            //    Debug.LogException(e);
            //}
        }
        else
        {
            try
            {
                //sua
                GameControl.instance.currentCasino.removePlayer(nick);
                GameControl.instance.currentCasino.masterID = master;
                GameControl.instance.currentCasino.setMaster(master);
            }
            catch (Exception e)
            {
                // TODO: handle exception
                Debug.LogException(e);
            }
        }
    }

    public void onJoinTablePlaySuccess(Message message)
    {
        BaseInfo.gI().isView = false;
        GameControl.instance.SetSecondHandler();
        BaseInfo.gI().OnJoinTableSuccess = message;
        if (RoomViewScript.instance != null)
            RoomViewScript.instance.LoadGame();
        else
        {
            string scene_ab = ClientConfig.Language.GetText(BaseInfo.gI().gameID + "_ab");
            string scene_name = ClientConfig.Language.GetText(BaseInfo.gI().gameID + "_name");
            LoadAssetBundle.LoadScene(scene_ab, scene_name, ()=> {
                GameControl.instance.StartGame(scene_name);
            });
        }
    }

    public void onJoinRoomFail(string info)
    {
        PopupAndLoadingScript.instance.HideLoading();
        PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", info);
    }

    public void onJoinTableSuccess(Message message)
    {
        //sua
        //if (BaseInfo.gI().numberPlayer == 9) {
        //    GameControl.instance.setCasino(BaseInfo.gI().gameID, 1);
        //} else {
        //    GameControl.instance.setCasino(BaseInfo.gI().gameID, 0);
        //}
        //if (GameControl.instance.currentCasino != null)
        //    GameControl.instance.currentCasino.onJoinTableSuccess(message);
    }

    public void onJoinTablePlayFail(string info)
    {
        PopupAndLoadingScript.instance.HideLoading();
        //sua
        PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", info + ". Bạn có muốn nạp thêm tiền không ?", delegate
        {
            //SendData.onJoinTableForView(BaseInfo.gI().idTable, "");
            LoadAssetBundle.LoadScene(Res.ADDCOIN_AB, Res.ADDCOIN_NAME);
        });
    }

    public void onJoinTableFail(string info)
    {
        PopupAndLoadingScript.instance.HideLoading();
        //sua
        PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", info + ". Bạn có muốn nạp thêm tiền không ?", delegate
        {
            LoadAssetBundle.LoadScene(Res.ADDCOIN_AB, Res.ADDCOIN_NAME);
        });
    }

    public void onProfile(Message msg)
    {
        try
        {
            BaseInfo.gI().mainInfo.nick = msg.reader().ReadUTF();
            BaseInfo.gI().mainInfo.userid = msg.reader().ReadLong();
            BaseInfo.gI().mainInfo.moneyXu = msg.reader().ReadLong();
            BaseInfo.gI().mainInfo.moneyChip = msg.reader().ReadLong();
            msg.reader().ReadUTF();
            BaseInfo.gI().mainInfo.displayname = msg.reader().ReadUTF();
            BaseInfo.gI().mainInfo.link_Avatar = msg.reader().ReadUTF();
            BaseInfo.gI().mainInfo.idAvata = msg.reader().ReadInt();
            //sbyte gender = msg.reader().ReadByte();
            //if (gender == 1) {
            //    BaseInfo.gI().mainInfo.gender = "Nam";
            //}
            //else {
            //    BaseInfo.gI().mainInfo.gender = "Nữ";
            //}
            BaseInfo.gI().mainInfo.exp = msg.reader().ReadLong();
            BaseInfo.gI().mainInfo.score_vip = msg.reader().ReadLong();
            BaseInfo.gI().mainInfo.total_money_charging = msg.reader().ReadLong();
            BaseInfo.gI().mainInfo.total_time_play = msg.reader().ReadLong();

            BaseInfo.gI().mainInfo.soLanThang = msg.reader().ReadUTF();
            BaseInfo.gI().mainInfo.soLanThua = msg.reader().ReadUTF();
            BaseInfo.gI().mainInfo.soTienMax = msg.reader().ReadLong();
            BaseInfo.gI().mainInfo.soChipMax = msg.reader().ReadLong();
            BaseInfo.gI().mainInfo.soGDThanhCong = msg.reader().ReadInt();
            //BaseInfo.gI().mainInfo.LanDangNhapCuoi = msg.reader().ReadUTF();
            //BaseInfo.gI().mainInfo.gender = msg.reader().ReadByte();
            //BaseInfo.gI().mainInfo.isVIP = msg.reader().ReadByte();
            string email_sdt = msg.reader().ReadUTF();
            //sbyte level = msg.reader().ReadByte();
            //long mf = msg.reader().ReadLong();
            //long mfmax = msg.reader().ReadLong();
            string[] s = Regex.Split(email_sdt, "\\*");
            BaseInfo.gI().mainInfo.email = s[0];
            if (s.Length > 1)
                BaseInfo.gI().mainInfo.phoneNumber = s[1];
            else
            {
                BaseInfo.gI().mainInfo.phoneNumber = "";
            }
            //BaseInfo.gI().mainInfo.phoneNumber = str_phone;
            //BaseInfo.gI().mainInfo.level_vip = level;
            //BaseInfo.gI().mainInfo.moneyFree = mf;
            //BaseInfo.gI().mainInfo.moneyFreeMax = mfmax;
            //Debug.Log(" BaseInfo.gI().mainInfo.fullname: " + BaseInfo.gI().mainInfo.fullname);
            //Debug.LogError ("BaseInfo.gI ().mainInfo.soLanThua: " + BaseInfo.gI ().mainInfo.soLanThua);
            //Debug.LogError ("BaseInfo.gI ().mainInfo.LanDangNhapCuoi: " + BaseInfo.gI ().mainInfo.LanDangNhapCuoi);

            if (BaseInfo.gI().mainInfo.exp < 10)
            {
                BaseInfo.gI().mainInfo.level_vip = 0;
            }
            else if (BaseInfo.gI().mainInfo.exp >= 30 && BaseInfo.gI().mainInfo.exp < 100)
            {
                BaseInfo.gI().mainInfo.level_vip = 1;
            }
            else if (BaseInfo.gI().mainInfo.exp >= 100 && BaseInfo.gI().mainInfo.exp < 300)
            {
                BaseInfo.gI().mainInfo.level_vip = 2;
            }
            else if (BaseInfo.gI().mainInfo.exp >= 300 && BaseInfo.gI().mainInfo.exp < 10000)
            {
                BaseInfo.gI().mainInfo.level_vip = 3;
            }
            else if (BaseInfo.gI().mainInfo.exp >= 10000 && BaseInfo.gI().mainInfo.exp < 100000)
            {
                BaseInfo.gI().mainInfo.level_vip = 4;
            }
            else if (BaseInfo.gI().mainInfo.exp > 100000)
            {
                BaseInfo.gI().mainInfo.level_vip = 5;
            }

            //switch (UnityPluginForWindowPhone.Class1.getDeviceNetworkInformation()) {
            //    case "VIETTEL":
            //        BaseInfo.gI().TELCO_CODE = 1;
            //        break;
            //    case "VINAPHONE":
            //        BaseInfo.gI().TELCO_CODE = 2;
            //        break;
            //    case "MOBIFONE":
            //        BaseInfo.gI().TELCO_CODE = 3;
            //        break;
            //    default:
            //        BaseInfo.gI().TELCO_CODE = 1;
            //        break;
            //}

            //gameControl.menu.updateAvataName();
            if (LobbyViewScript.instance != null)
                LobbyViewScript.instance.UpdateProfileUser();
            BaseInfo.gI().isNhanLoiMoiChoi = true;
        }
        catch (Exception e)
        {
            // TODO Auto-generated catch block
            Debug.LogException(e);
        }
    }

    public void onStartFail(string info)
    {
        PopupAndLoadingScript.instance.HideLoading();
        PopupAndLoadingScript.instance.popup.ShowPopupOneButton(info);
    }

    public void onStartSuccess(Message message)
    {
        try
        {
            int[] cardHand = new int[1];
            int size = message.reader().ReadInt();
            sbyte[] c = new sbyte[size];
            for (int i = 0; i < size; i++)
            {
                c[i] = message.reader().ReadByte();
            }
            cardHand = new int[c.Length];
            for (int i = 0; i < c.Length; i++)
            {
                cardHand[i] = c[i];
            }

            int size1 = message.reader().ReadByte();
            string[] playingName = new string[size1];
            for (int i = 0; i < size1; i++)
            {
                playingName[i] = message.reader().ReadUTF();
            }

            // dialog_waiting.dismiss();
            //sua
            try
            {
                GameControl.instance.currentCasino.startTableOk(cardHand, message, playingName);
                GameControl.instance.currentCasino.isStart = true;
                GameControl.instance.currentCasino.isPlaying = true;
            }
            catch (Exception)
            {

                throw;
            }

        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void onStartForView(Message message)
    {
        try
        {
            int size = message.reader().ReadByte();
            string[] playingName = new string[size];
            for (int i = 0; i < size; i++)
            {
                playingName[i] = message.reader().ReadUTF();
            }
            //sua
            if (GameControl.instance.currentCasino != null)
                GameControl.instance.currentCasino.onStartForView(playingName);
        }
        catch (Exception e)
        {
        }
    }

    public void onSetTurn(Message message)
    {
        try
        {
            string nick = message.reader().ReadUTF();
            //sua
            GameControl.instance.currentCasino.setTurn(nick, message);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onFireCard(string nick, string turnname, int[] card)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onFireCard(nick, turnname, card);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onFireCardFail()
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onFireCardFail();
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onGetCardNocSuccess(string nick, int card)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onGetCardNoc(nick, card);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onEatCardSuccess(string from, string to, int card)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onEatCardSuccess(from, to, card);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onBalanceCard(string from, string to, int card)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onBalanceCard(from, to, card);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onReady(Message message)
    {
        try
        {
            int tbid = message.reader().ReadShort();
            int totalReady = message.reader().ReadByte();
            for (int i = 0; i < totalReady; i++)
            {
                String nick = message.reader().ReadUTF();
                bool ready = message.reader().ReadBoolean();
                //sua
                int pl = GameControl.instance.currentCasino.getPlayer(nick);
                if (pl != -1)
                {
                    GameControl.instance.currentCasino.players[pl]
                            .setReady(ready);
                }
                if (nick.Equals(BaseInfo.gI().mainInfo.nick))
                {
                    if (GameControl.instance.currenStage is HasMasterCasino)
                    {
                        if (!ready)
                        {
                            ((HasMasterCasino)GameControl.instance.currenStage).btn_sansang.gameObject.SetActive(true);
                            ((HasMasterCasino)GameControl.instance.currenStage).lb_Btn_sansang.text = (Res.TXT_SANSANG);
                        }
                        else
                        {
                            ((HasMasterCasino)GameControl.instance.currenStage).btn_sansang.gameObject.SetActive(false);
                        }
                    }

                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void onAttachCard(string from, string to, int[] cards, int[] cardsgui)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onAttachCard(from, to, cards, cardsgui);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onAllCardPlayerFinish(Message message)
    {
        try
        {
            string nick = message.reader().ReadUTF();
            int size = message.reader().ReadInt();
            sbyte[] c = new sbyte[size];
            for (int i = 0; i < size; i++)
            {
                c[i] = message.reader().ReadByte();
            }
            int[] card = new int[c.Length];
            for (int i = 0; i < c.Length; i++)
            {
                card[i] = c[i];
            }
            //sua
            try
            {
                GameControl.instance.currentCasino.allCardFinish(nick, card);
            }
            catch (Exception)
            {

                throw;
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void onFinishGame(Message message)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onFinishGame(message);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onDropPhomSuccess(string nick, int[] arrayPhom)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onDropPhomSuccess(nick, arrayPhom);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onInvite(string nickInvite, string displayName, sbyte gameid, short roomid, short tblid, long money, long minMoney, long maxMoney)
    {
        string gameName = "";
        switch (gameid)
        {
            case GameID.PHOM:
                gameName = "Game Phỏm";
                break;
            case GameID.TLMN:
                gameName = "Game Tiến Lên Miền Nam";
                break;
            case GameID.XAM:
                gameName = "Game Xâm";
                break;
            case GameID.XITO:
                gameName = "Game Xito";
                break;
            case GameID.MAUBINH:
                gameName = "Game Mậu Binh";
                break;
            case GameID.BACAY:
                gameName = "Game Ba Cây";
                break;
            case GameID.LIENG:
                gameName = "Game Liêng";
                break;
            case 7:
                return;
            // break;
            case GameID.POKER:
                gameName = "Game Pocker";
                break;
            case GameID.XOCDIA:
                gameName = "Game Xóc Đĩa";
                break;
        }
        if (BaseInfo.gI().isNhanLoiMoiChoi)
        {// && !gameControl.dialogNapXu.isShow && !gameControl.dialogDoiThuong.isShow
            string m = "";
            if (roomid == 1)
            {
                m = Res.MONEY_FREE;
            }
            else
            {
                m = Res.MONEY_VIP;
            }
            PopupAndLoadingScript.instance.popup.ShowPopupThreeButton("", displayName.Equals("") ? nickInvite
                                          : displayName
                + " mời bạn đánh " + gameName + ", mức cược là  " + money + m
                + ", bạn có đồng ý không?", delegate
                {
                    BaseInfo.gI().moneyNeedTable = minMoney;
                    if (maxMoney == -1)
                    {
                        SendData.onAcceptInviteFriend(gameid, roomid, tblid, -1);
                        SendData.onJoinTablePlay(tblid, "", -1);
                        PopupAndLoadingScript.instance.ShowLoading();
                    }
                    else
                    {
                        int temp;
                        if (roomid == 2)
                        {
                            temp = 2;
                        }
                        else
                        {
                            temp = 1;
                        }
                        LoadAssetBundle.LoadScene(Res.RUTTIEN_AB, Res.RUTTIEN_NAME, () =>
                        {
                            PanelRutTien.instance.show(
                                 (int)(minMoney * 2.5f), maxMoney, 1, tblid,
                                 roomid, gameid, temp);
                        });
                    }
                    PopupAndLoadingScript.instance.HideLoading();
                }, delegate { BaseInfo.gI().isNhanLoiMoiChoi = false; });
        }
    }

    //public void onRegSuccess(Message msg)
    //{
    //    //gameControl.toast.showToast("Đăng ký thành công!");
    //    RegisterViewScript.instance.loginWhenRegSucces();
    //}

    //public void onRegFail(string info)
    //{
    //    PopupAndLoadingScript.instance.HideLoading();
    //    //gameControl.panelMessageSytem.onShow(info);
    //    Debug.LogError("Regis fail " + info);
    //    Debug.LogError("Info " + info);
    //    PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", info);
    //}

    public void onKickOK()
    {
        PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Bạn bị đuổi khỏi bàn chơi!");
    }

    public void onSysKick()
    {
        PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", "Tài khoản của bạn không đủ tiền để chơi tiếp./n Bạn có muốn nạp thêm tiền?", delegate
        {
            //sua
            //gameControl.panelNapChuyenXu.onShow();
        });
    }

    public void onChatMessage(string nick, string msg, bool outs)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onMsgChat(nick, msg);
        }
        catch (Exception)
        {

            throw;
        }
    }

    //public void onUpdateProfile(int code, string info)
    //{
    //    PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Cập nhật thông tin cá nhân thành công!");
    //}

    public void onUnReadMessage(Message messge)
    {
        try
        {
            int nMail = messge.reader().ReadInt();
            BaseInfo.gI().soTinNhan = nMail;
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public void onGameID(Message message)
    {
        //int gameId = 0;
        try
        {
            //sua
            BaseInfo.gI().gameID = message.reader().ReadByte();
            if (BaseInfo.gI().gameID == -99)
            {
                BaseInfo.gI().gameID = message.reader().ReadByte();
                int currentRoom = message.reader().ReadByte();
                String nameRoom = message.reader().ReadUTF();
                //    BaseInfo.gI().typetableLogin = message.reader().ReadByte();
                //}
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        //sua
        if (RoomViewScript.instance != null)
            RoomViewScript.instance.SetGameName(BaseInfo.gI().gameID);
    }

    public void onMessageServer(string info)
    {
        PopupAndLoadingScript.instance.HideLoading();
        PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", info);//, delegate {
                                                                          //if (info.Equals("Tài khoản hoặc mật khẩu không đúng") && !gameControl.login.gameObject.activeInHierarchy) {
                                                                          //    gameControl.setStage(gameControl.login);
                                                                          //}
                                                                          // });
    }

    //public void onMsgChat(string from, string msg)
    //{

    //}

    public void onSetMoneyTable(long money)
    {

    }

    public void onDetailUser(Message message)
    {
        Debug.LogError("onDetailUser");
        try
        {
            string nick = message.reader().ReadUTF();
            long userID = message.reader().ReadLong();
            long money = message.reader().ReadLong();
            long chip = message.reader().ReadLong();
            string name = message.reader().ReadUTF();
            string displayname = message.reader().ReadUTF();
            long exp = message.reader().ReadLong();
            long score_vip = message.reader().ReadLong();
            long total_money_charging = message.reader().ReadLong();
            long total_time_play = message.reader().ReadLong();

            string soLanThang = message.reader().ReadUTF();
            string soLanThua = message.reader().ReadUTF();
            long soTienMax = message.reader().ReadLong();
            long soChipMax = message.reader().ReadLong();
            int soGDThanhCong = message.reader().ReadInt();
            string LanDangNhapCuoi = message.reader().ReadUTF();
            string link_Avatar = message.reader().ReadUTF();
            int idAvata = message.reader().ReadInt();
            int level = 0;
            if (exp < 10)
            {
                level = 0;
            }
            else if (exp >= 30 && exp < 100)
            {
                level = 1;
            }
            else if (exp >= 100 && exp < 300)
            {
                level = 2;
            }
            else if (exp >= 300 && exp < 10000)
            {
                level = 3;
            }
            else if (exp >= 10000 && exp < 100000)
            {
                level = 4;
            }
            else
            {
                level = 5;
            }

            //if (!BaseInfo.gI().isInGame)
            //{
            //    LoadAssetBundle.LoadScene(Res.INFOPLAYER_AB, Res.INFOPLAYER_NAME, () =>
            //    {
            //        PanelInfoPlayer.instance.InfoProfile(displayname, userID, money, chip, soLanThang, soLanThua, link_Avatar, idAvata, "", "", level);
            //    });
            //    //gameControl.panelInfoPlayer.infoProfile(displayname, userID, money, moneyFree, soLanThang, soLanThua, link_Avatar, idAvata, "", "", 0);
            //}
            //else {
            //LoadAssetBundle.LoadScene(Res.INFO_INGAME_AB, Res.INFO_INGAME_NAME, () =>
            //{
            //NPCController.instance.UpdateInfo(displayname, userID, money, level, soLanThang, soLanThua, link_Avatar, idAvata);
            //});
            //}
            PopupAndLoadingScript.instance.HideLoading();
            //gameControl.panelInfoPlayer.onShow();
        }
        catch (Exception e)
        {
            // TODO: handle exceptione.
            Debug.LogException(e);
        }
    }

    public void onInfoSMS(Message message)
    {
        try
        {
            sbyte len = message.reader().ReadByte();
            BaseInfo.gI().isCharging = len;
            Debug.Log("=========== " + len);
            for (int i = 0; i < 2; i++)
            {
                string name = message.reader().ReadUTF();
                string syntax = message.reader().ReadUTF();
                short port = message.reader().ReadShort();

                int type = (port % 1000) / 100;
                if (type == 6)
                {
                    BaseInfo.gI().name10 = name;
                    BaseInfo.gI().syntax10 = syntax;
                    BaseInfo.gI().port10 = port + "";
                }
                else if (type == 7)
                {
                    BaseInfo.gI().name15 = name;
                    BaseInfo.gI().syntax15 = syntax;
                    BaseInfo.gI().port15 = port + "";
                }
                else
                {
                    if (i == 0)
                    {
                        BaseInfo.gI().name10 = name;
                        BaseInfo.gI().syntax10 = syntax;
                        BaseInfo.gI().port10 = port + "";
                    }
                    else
                    {
                        BaseInfo.gI().name15 = name;
                        BaseInfo.gI().syntax15 = syntax;
                        BaseInfo.gI().port15 = port + "";
                    }
                }
            }
        }
        catch (Exception e)
        {
            // TODO: handle exception
            Debug.LogException(e);
        }
    }

    public void onSetNewMaster(string nick)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.setMaster(nick);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onNickCuoc(long moneyInPot, long soTienTo, long moneyBoRa, string nick, Message message)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onNickCuoc(moneyInPot, soTienTo, moneyBoRa, nick, message);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onNickTheo(long money, string nick, Message message)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onHaveNickTheo(money, nick, message);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onNickSkip(string nick, string turnName)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onNickSkip(nick, turnName);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onNickSkip(string nick, Message msg)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onNickSkip(nick, msg);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onUpdateMoneyMessage(string readstring, int type, long readInt)
    {
        // Debug.Log ("== " + readstring + " Type: " + type + " Money: " + readInt);
        if (readstring.Equals(BaseInfo.gI().mainInfo.nick))
        {
            if (type == 0)
            {
                BaseInfo.gI().mainInfo.moneyXu = readInt;
            }
            else
            {
                BaseInfo.gI().mainInfo.moneyChip = readInt;
            }
            if (GameControl.instance.currentCasino != null)
            {
                try
                {
                    GameControl.instance.currentCasino.players[0].setMoney(readInt);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }

    //public void onUpdateVersion(Message message)
    //{
    //    try
    //    {
    //        NetworkUtil.GI().close();
    //        gameControl.setStage(gameControl.login);

    //        sbyte updateType = message.reader().ReadByte();
    //        string linkUpdate = message.reader().ReadUTF();
    //        string mess = message.reader().ReadUTF();
    //        gameControl.dialogWaiting.onHide();
    //        gameControl.dialogNotification.onShow(mess, delegate { Application.OpenURL(linkUpdate); });
    //    }
    //    catch (Exception e)
    //    {
    //        // TODO: handle exception
    //    }
    //}

    public void onInfoPockerTbale(Message message)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onInfo(message);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onAddCardTbl(Message message)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onAddCardTbl(message);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onChangeRuleTbl(sbyte readByte)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.setLuatChoi(readByte);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onUpdateMoneyTbl(Message message)
    {
        //sua
        try
        {
            if(GameControl.instance.currentCasino != null)
                GameControl.instance.currentCasino.onUpdateMoneyTbl(message);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onUpdateRoom(int readShort, Message message)
    {
        //Debug.LogError("onUpdateRoom");
        RoomViewScript.instance.listTable.Clear();
        List<TableItem> list = new List<TableItem>();

        List<TableItem> temp = new List<TableItem>();
        for (int i = 0; i < readShort; i++)
        {
            try
            {
                TableItem ctb = new TableItem();
                ctb.id = (message.reader().ReadShort());
                ctb.status = (message.reader().ReadByte());
                ctb.nUser = (message.reader().ReadByte());
                ctb.isLock = message.reader().ReadByte();
                //ctb.lock = lock;
                ctb.money = message.reader().ReadLong();
                ctb.needMoney = message.reader().ReadLong();
                ctb.maxMoney = message.reader().ReadLong();
                ctb.maxUser = (message.reader().ReadByte());
                ctb.typeTable = message.reader().ReadInt();
                ctb.choinhanh = message.reader().ReadInt();
                list.Add(ctb);

            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
        }

        try
        {
            int totalTBC = message.reader().ReadUnsignedShort();
            for (int i = 0; i < totalTBC; i++)
            {
                TableItem ctb = new TableItem();
                ctb.id = (message.reader().ReadShort());
                ctb.status = (message.reader().ReadByte());
                ctb.nUser = (message.reader().ReadByte());
                ctb.isLock = message.reader().ReadByte();
                //ctb.lock = lock;
                ctb.money = message.reader().ReadLong();
                ctb.needMoney = message.reader().ReadLong();
                ctb.maxMoney = message.reader().ReadLong();
                ctb.maxUser = (message.reader().ReadByte());
                ctb.typeTable = message.reader().ReadInt();
                ctb.choinhanh = message.reader().ReadInt();
                temp.Add(ctb);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        // Collections.sort(gameControl.listTable, sort_tang_dan_muccuoc);
        //BaseInfo.gI().sort_giam_dan_muccuoc = false;
        //gameControl.room.sortMucCuoc();

        int dem5 = 0, dem9 = 0;
        int MAX = 1;
        //sua
        //if (gameControl.gameID == GameID.TLMN || gameControl.gameID == GameID.PHOM
        //        || gameControl.gameID == GameID.XITO
        //        || gameControl.gameID == GameID.MAUBINH
        //        || gameControl.gameID == GameID.XAM
        //        || gameControl.gameID == GameID.TLMNsolo)
        //{
        MAX = 2;
        //}
        if (list.Count > 0)
        {
            long muccuoc;
            muccuoc = list[0].money;
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    if (list[i].nUser != 0)
                    {
                        temp.Add(list[i]);
                        if (list[i].money != muccuoc)
                        {
                            dem5 = 0;
                            dem9 = 0;
                        }
                        muccuoc = list[i].money;
                        continue;
                    }
                    else
                    {
                        if (list[i].money == muccuoc
                                && (list[i].maxUser < 9))
                        {
                            dem5++;
                            if (dem5 <= MAX)
                            {
                                temp.Add(list[i]);
                                muccuoc = list[i].money;
                                continue;
                            }

                        }
                        else
                        {
                        }
                        if (list[i].money == muccuoc
                                && (list[i].maxUser == 9))
                        {
                            dem9++;
                            if (dem9 <= MAX)
                            {
                                temp.Add(list[i]);
                                muccuoc = list[i].money;
                                continue;
                            }
                        }
                        else
                        {
                        }
                    }
                    if (list[i].money != muccuoc)
                    {
                        dem5 = 0;
                        dem9 = 0;
                        if ((list[i].maxUser < 9))
                        {
                            dem5++;
                            if (dem5 <= MAX)
                            {
                                temp.Add(list[i]);
                                muccuoc = list[i].money;
                                continue;
                            }

                        }
                        else
                        {
                        }
                        if ((list[i].maxUser == 9))
                        {
                            dem9++;
                            if (dem9 <= MAX)
                            {
                                temp.Add(list[i]);
                                muccuoc = list[i].money;
                                continue;
                            }
                        }
                        else
                        {
                        }

                    }
                    muccuoc = list[i].money;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }

        list.Clear();
        list.AddRange(temp);
        switch (BaseInfo.gI().type_sort)
        {
            case 1:// ban cuonc
                if (BaseInfo.gI().sort_giam_dan_bancuoc)
                {
                    list.Sort(delegate (TableItem tb1, TableItem tb2)
                    {
                        return tb1.id.CompareTo(tb2.id);
                    });
                }
                else
                {
                    list.Sort(delegate (TableItem tb1, TableItem tb2)
                    {
                        return tb2.id.CompareTo(tb1.id);
                    });
                }
                break;
            case 2:// muc cuoc
                if (BaseInfo.gI().sort_giam_dan_muccuoc)
                {
                    list.Sort(delegate (TableItem tb1, TableItem tb2)
                    {
                        return tb1.money.CompareTo(tb2.money);
                    });
                }
                else
                {
                    list.Sort(delegate (TableItem tb1, TableItem tb2)
                    {
                        return tb2.money.CompareTo(tb1.money);
                    });
                }
                break;
            case 3:// nguoi choi
                if (BaseInfo.gI().sort_giam_dan_nguoichoi)
                {
                    list.Sort(delegate (TableItem tb1, TableItem tb2)
                    {
                        return tb1.nUser.CompareTo(tb2.nUser);
                    });
                }
                else
                {
                    list.Sort(delegate (TableItem tb1, TableItem tb2)
                    {
                        return tb2.nUser.CompareTo(tb1.nUser);
                    });
                }
                break;
            default:
                break;
        }

        // gameControl.room.createScollPane(gameControl.listTable);
        //RoomViewScript.instance.InstantiateMucCuoc();
        //sua
        //gameControl.setStage(gameControl.room);
        PopupAndLoadingScript.instance.HideLoading();
    }

    public void onPhoneCSKH(Message message)
    {
        try
        {
            BaseInfo.gI().cskh = message.reader().ReadUTF();
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public void onGetAlertLink(Message message)
    {
        try
        {
            sbyte type = message.reader().ReadByte();
            string title = message.reader().ReadUTF();
            string link = message.reader().ReadUTF();
            //Debug.LogError("Title " + title);
            AlertMess al = new AlertMess();
            al.mess = title;
            al.link = link;
            BaseInfo.gI().msgAlert.Add(al);
            if (BaseInfo.gI().msgAlert[0].mess.Contains("\n"))
                BaseInfo.gI().msgAlert[0].mess = BaseInfo.gI().msgAlert[0].mess.Replace("\n", " ");
            //Debug.LogError("onGetAlertLink " + title);
            if (LobbyViewScript.instance != null)
            {
                if (!string.IsNullOrEmpty(title))
                    LobbyViewScript.instance.SetNoti(title);
                else
                    LobbyViewScript.instance.SetNoti(ClientConfig.Language.GetText("noti"));
            }
        }
        catch (IOException e)
        {
            // TODO Auto-generated catch block
            Debug.LogException(e);
        }
    }
    public void infoWinPlayer(Message message)
    {
        try
        {
            int len = message.reader().ReadByte();
            List<InfoWinTo> info2 = new List<InfoWinTo>();
            List<InfoWinTo> info = new List<InfoWinTo>();
            for (int i = 0; i < len; i++)
            {
                InfoWinTo inf = new InfoWinTo();
                inf.name = message.reader().ReadUTF();
                inf.rank = i + 1;
                inf.money = message.reader().ReadLong();
                inf.type = message.reader().ReadByte();
                inf.typeCard = message.reader().ReadByte();
                sbyte len2 = message.reader().ReadByte();
                inf.arrCard = new sbyte[len2];
                for (int j = 0; j < len2; j++)
                {
                    inf.arrCard[j] = message.reader().ReadByte();
                }
                info2.Add(inf);
                if (inf.money > 0)
                {
                    info.Add(inf);
                    int l = info.Count - 1;
                    if (info[l].arrCard.Length > 0)
                    {
                        if (l > 0)
                        {
                            int k = inf.arrCard.Length - 5;
                            bool isSame = true;
                            for (int j = k; j < inf.arrCard.Length; j++)
                            {
                                if (info[l].arrCard[j] % 13 != info
                                        [l - 1].arrCard[j] % 13)
                                {
                                    isSame = false;
                                    break;
                                }
                            }
                            if (isSame)
                            {
                                info[l].rank = info[l - 1].rank;
                            }
                        }
                    }
                }
            }
            GameControl.instance.currentCasino.onInfoWinPlayer(info, info2);
        }
        catch (IOException e)
        {
            Debug.LogException(e);
        }
    }

    public void InfoCardPlayerInTbl(Message message)
    {
        //sua
        BaseInfo.gI().InfoCardPlayerInTbl = message;
        //GameControl.instance.currentCasino.InfoCardPlayerInTbl(message);
    }

    public void onChangeBetMoney(Message message)
    {
        try
        {
            long betMoney = message.reader().ReadLong();
            String info = message.reader().ReadUTF();
            BaseInfo.gI().betMoney = betMoney;
            BaseInfo.gI().moneyTable = betMoney;

            if (betMoney > 0)
            {
                //sua
                try
                {
                    GameControl.instance.currentCasino.changBetMoney(betMoney, info);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", info);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
    public void onGetMoney()
    {
        if (((BaseToCasino)GameControl.instance.currentCasino) != null && ((BaseToCasino)GameControl.instance.currentCasino).btn_ruttien != null)
        {
            ((BaseToCasino)GameControl.instance.currentCasino).btn_ruttien
                    .gameObject.SetActive(true);
        }
        long moneyPlayer = 0;
        if (BaseInfo.gI().typetableLogin == Res.ROOMFREE)
        {
            moneyPlayer = BaseInfo.gI().mainInfo.moneyChip;
        }
        else
        {
            moneyPlayer = BaseInfo.gI().mainInfo.moneyXu;
        }
        if (BaseInfo.gI().tuDongRutTien)
        {
            // SendData.onSendGetMoney(-1);
            if (moneyPlayer < BaseInfo.gI().soTienRut)
            {
                SendData.onSendGetMoney(moneyPlayer);
            }
            else
            {
                SendData.onSendGetMoney(BaseInfo.gI().soTienRut);
            }
            if (((BaseToCasino)GameControl.instance.currentCasino).btn_ruttien != null)
            {
                ((BaseToCasino)GameControl.instance.currentCasino).btn_ruttien
                        .gameObject.SetActive(false);
            }
        }
        else
        {
            if (moneyPlayer < BaseInfo.gI().moneyNeedTable)
            {
                PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("",
                        "Không đủ tiền để rút, bạn có muốn nạp thêm?",
                    delegate
                    {
                        // show dialog nap tien
                    });
            }
            else
            {
                PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("",
                        "Không đủ tiền, bạn có muốn lấy thêm "
                                + " để tiếp tục chơi?", delegate
                                {
                                    //sua
                                    LoadAssetBundle.LoadScene(Res.RUTTIEN_AB, Res.RUTTIEN_NAME, () =>
                                    {
                                        PanelRutTien.instance.show(
                                                    BaseInfo.gI().currentMinMoney,
                                                    BaseInfo.gI().currentMaxMoney, 2, 0, 0,
                                                    0, BaseInfo.gI().typetableLogin);
                                    });
                                });
            }

        }

    }

    public void onTimeAuToStart(Message message)
    {
        try
        {
            //sua
            GameControl.instance.currentCasino.onTimeAuToStart(message.reader().ReadByte());
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public void startFlip(Message message)
    {
        try
        {
            //sua
            GameControl.instance.currentCasino.startFlip(message.reader().ReadByte());
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public void onCardFlip(Message message)
    {
        try
        {
            //sua

            GameControl.instance.currentCasino.onCardFlip(message.reader().ReadByte());
        }
        catch (Exception e)
        {
            // TODO Auto-generated catch block

        }
    }

    //public void onUpdateVersionNew(string link)
    //{
    //    gameControl.dialogWaiting.onHide();
    //    gameControl.dialogNotification.onShow("Cập nhập phiên bản?", delegate { Application.OpenURL(link); });
    //}

    //public void onIntroduceFriend(string mess, string link)
    //{
    //    gameControl.dialogWaiting.onHide();
    //    gameControl.dialogNotification.onShow("Nhập số điện thoại, bạn có đồng ý không?", delegate { GameControl.sendSMS(link, mess); });
    //}

    public void onRankMauBinh(Message message)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onRankMauBinh(message);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onFinalMauBinh(string name)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onFinalMauBinh(name);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onWinMauBinh(string name, sbyte typeCard)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onWinMauBinh(name, typeCard);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onInfoMe(Message message)
    {
        //sua
        Debug.LogError("Info Me Listenter");
        try
        {
            if(GameControl.instance.currentCasino != null)
                GameControl.instance.currentCasino.onInfome(message);
            else
                BaseInfo.gI().onInfoMe = message;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onBeginRiseBacay(Message message)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onBeginRiseBacay(message);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onFlip3Cay(Message message)
    {

    }

    public void onCuoc3Cay(Message message)
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onCuoc3Cay(message);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onBaoXam(Message message)
    {
        try
        {
            sbyte type = message.reader().ReadByte();
            if (type == 0)
            {
                sbyte time = message.reader().ReadByte();
                //sua
                try
                {
                    GameControl.instance.currentCasino.onHoiBaoXam(time);
                }
                catch (Exception)
                {

                    throw;
                }
            }
            else if (type == 1)
            {
                String name = message.reader().ReadUTF();
                //sua
                try
                {
                    GameControl.instance.currentCasino.onNickBaoXam(name);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public void onFinishTurn()
    {
        //sua
        try
        {
            GameControl.instance.currentCasino.onFinishTurn();
        }
        catch (Exception)
        {

            throw;
        }
    }

    //public void onNewEvent(Message message)
    //{
    //    try {
    //        sbyte size = message.reader().ReadByte();
    //        BaseInfo.gI().listEvent.Clear();
    //        for (int i = 0; i < size; i++) {
    //            VuaBaiEvent events = new VuaBaiEvent();
    //            events.title = message.reader().ReadUTF();
    //            events.content = message.reader().ReadUTF();

    //            events.link = message.reader().ReadUTF();
    //            events.dateOpen = message.reader().ReadUTF();
    //            events.dateEnd = message.reader().ReadUTF();
    //            BaseInfo.gI().listEvent.Add(events);
    //        }
    //        if (BaseInfo.gI().listEvent.Count > 0) {
    //            gameControl.dialogEvent.onShow();
    //        }

    //    } catch (Exception e) {
    //        // TODO: handle exception
    //    }
    //}

    //public void onInfoSMSAppStore(Message message)
    //{
    //    try
    //    {
    //        BaseInfo.gI().isPurchase = true;
    //        if (BaseInfo.gI().isPurchase)
    //        {
    //            gameControl.menu.buttonDoiThuong.SetActive(false);
    //        }
    //        else
    //        {
    //            gameControl.menu.buttonDoiThuong.SetActive(true);
    //        }
    //        //sbyte size = message.reader().ReadByte();
    //        //if (size == 0)
    //        //{
    //        //    BaseInfo.gI().isPurchase = false;
    //        //} else{
    //        //    BaseInfo.gI().isPurchase = true;
    //        //}
    //        //for (int i = 0; i < size; i++ )
    //        //{
    //        //    string key = message.reader().ReadUTF();
    //        //    string value = message.reader().ReadUTF();
    //        //}
    //    } catch (Exception ex){

    //    }
    //}
    public void onBuyItem(Message message)
    {
        try
        {
            int id = message.reader().ReadInt();
            string nick_nem = message.reader().ReadUTF();
            string nick_bi_nem = message.reader().ReadUTF();
            //sua
            try
            {
                GameControl.instance.currentCasino.actionNem(id, nick_nem, nick_bi_nem);
            }
            catch (Exception)
            {

                throw;
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void InfoGift2(Message message)
    {
        try
        {
            //sua
            BaseInfo.gI().listTheCao.Clear();
            BaseInfo.gI().listVatPham.Clear();
            int size = message.reader().ReadInt();
            for (int i = 0; i < size; i++)
            {
                int id = message.reader().ReadInt();
                int type = message.reader().ReadInt();
                // type 1: the cao
                // type 2: vat pham
                string name = message.reader().ReadUTF();
                long cost = message.reader().ReadLong();
                string telco = message.reader().ReadUTF();
                long price = message.reader().ReadLong();
                long balance = message.reader().ReadLong();
                string des = message.reader().ReadUTF();
                string links = message.reader().ReadUTF();
                if (type == 1)
                {
                    InfoTheCao thecao = new InfoTheCao();
                    thecao.id = id;
                    thecao.nameItem = name;
                    thecao.cost = cost;
                    thecao.telco = telco;
                    thecao.price = price;
                    thecao.balance = balance;
                    thecao.des = des;
                    thecao.links = links;
                    BaseInfo.gI().listTheCao.Add(thecao);
                }
                else if (type == 2)
                {
                    InfoVatPham vatpham = new InfoVatPham();
                    vatpham.id = id;
                    vatpham.nameItem = name;
                    vatpham.cost = cost;
                    vatpham.telco = telco;
                    vatpham.price = price;
                    vatpham.balance = balance;
                    vatpham.des = des;
                    vatpham.links = links;
                    BaseInfo.gI().listVatPham.Add(vatpham);
                }
            }
            PanelDoiThuong.instance.addGiftInfoTheCao(BaseInfo.gI().listTheCao);
        }
        catch (Exception e)
        {
            // TODO: handle exception
            Debug.LogException(e);
        }
        PopupAndLoadingScript.instance.HideLoading();
    }

    public void onListInvite(Message msg)
    {
        PopupAndLoadingScript.instance.HideLoading();
        try
        {
            short total = msg.reader().ReadShort();
            if (total <= 0)
            {
                PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Tất cả người chơi đều đang bận!");
            }
            else
            {
                //sua
                LoadAssetBundle.LoadScene(Res.MOICHOI_AB, Res.MOICHOI_NAME, () =>
                {
                    for (int i = 0; i < total; i++)
                    {
                        string name = msg.reader().ReadUTF();
                        string displayname = msg.reader().ReadUTF();
                        long money = msg.reader().ReadLong();
                        PanelMoiChoi.instance.addIcon(name, displayname, money);
                    }
                });

                //gameControl.panelMoiChoi.onShow();
            }
        }
        catch (Exception e)
        {
            // TODO: handle exception
            Debug.LogException(e);
        }

    }


    public void onJoinView(Message message)
    {
        //sua
        BaseInfo.gI().isView = true;
        BaseInfo.gI().OnJoinTableSuccess = message;
        RoomViewScript.instance.LoadGame();
    }

    public void onUpdataAvata(Message message)
    {
        try
        {
            sbyte type = message.reader().ReadByte();
            string info = message.reader().ReadUTF();

            PanelInfoPlayer.instance.InfoMe();
            //gameControl.panelInfoPlayer.infoMe();
            //gameControl.menu.updateAvataName();
            LobbyViewScript.instance.UpdateProfileUser();
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", info);

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onChangeName(Message message)
    {
        try
        {
            sbyte type = message.reader().ReadByte();
            string info = message.reader().ReadUTF();
            if (type == 1)
            {
                string name = message.reader().ReadUTF();

                BaseInfo.gI().mainInfo.displayname = name;
                PanelInfoPlayer.instance.InfoMe();
                //gameControl.panelInfoPlayer.infoMe();
                //gameControl.menu.updateAvataName();
                LobbyViewScript.instance.UpdateProfileUser();
            }
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", info, delegate { });
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onPopupNotify(Message message)
    {
        //LoadAssetBundle.LoadScene(Res.NOTIDOITHUONG_AB, Res.NOTIDOITHUONG_NAME, ()=> {
        try
        {
            //sua
            //if (PanelNotiDoiThuong.instance.tgParent.transform.childCount == 0)
            //{
            int size = message.reader().ReadInt();
            for (int i = 0; i < size; i++)
            {
                int id = message.reader().ReadInt();
                int type = message.reader().ReadInt();
                string title = message.reader().ReadUTF();
                string content = message.reader().ReadUTF();
                //Debug.Log("ID: " + id + " Type: " + type + " Title: " + title + " Content: " + content);

                //PanelNotiDoiThuong.instance.setActiveTab(title, content);
            }
            //}
            //gameControl.panelNotiDoiThuong.onShow();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        //});
    }

    public void onCreateTable(Message message)
    {
        try
        {
            sbyte type = message.reader().ReadByte();
            if (type == 1)
            {
                //PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", "Tạo bàn thành công", delegate { RoomViewScript.instance.LoadGame(message); });
                //sua
                //gameControl.panelCreateRoom.onHide();
            }
            else
            {
                //PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Tạo bàn thất bại");
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onListBetMoney(Message message)
    {
        try
        {
            BaseInfo.gI().listBetMoneysVIP.Clear();
            BaseInfo.gI().listBetMoneysFREE.Clear();

            int size = message.reader().ReadInt();
            for (int i = 0; i < size; i++)
            {
                BetMoney betM = new BetMoney();
                betM.typeMoney = message.reader().ReadInt();
                betM.maxMoney = message.reader().ReadLong();
                string listBet = message.reader().ReadUTF();
                //long bet = long.Parse(listBet);
                long bet = 10;
                if (listBet.Length > 0)
                {
                    betM.setListBet(bet);
                    if (betM.typeMoney == 0)
                    {
                        BaseInfo.gI().listBetMoneysVIP.Add(betM);
                    }
                    else if (betM.typeMoney == 1)
                    {
                        BaseInfo.gI().listBetMoneysFREE.Add(betM);
                    }
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onRateScratchCard(Message message)
    {
        if (BaseInfo.gI().list_tygia != null)
            BaseInfo.gI().list_tygia.Clear();
        try
        {
            int size = message.reader().ReadInt();
            for (int i = 0; i < size; i++)
            {
                int menhgia = message.reader().ReadInt();
                int xu = message.reader().ReadInt();
                TyGia tygia = new TyGia(menhgia, xu);
                BaseInfo.gI().list_tygia.Add(tygia);
            }
            BaseInfo.gI().sms10 = message.reader().ReadInt();
            BaseInfo.gI().sms15 = message.reader().ReadInt();
            BaseInfo.gI().tyle_xu_sang_chip = message.reader().ReadInt();
            BaseInfo.gI().tyle_chip_sang_xu = message.reader().ReadInt();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onXuToNick(Message message)
    {
        try
        {
            sbyte type = message.reader().ReadByte();
            if (type == 1)
            {
                long xu = message.reader().ReadLong();
                BaseInfo.gI().mainInfo.moneyXu += xu;
            }
            string info = message.reader().ReadUTF();
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", info);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Vui lòng nhập đúng UserID cần chuyển đến, xem UserID trong phần thông tin cá nhân!");
        }
    }

    public void onXuToChip(Message message)
    {
        try
        {
            sbyte type = message.reader().ReadByte();
            if (type == 1)
            {
                long xu = message.reader().ReadLong();
                long chip = message.reader().ReadLong();
                BaseInfo.gI().mainInfo.moneyXu -= xu;
                BaseInfo.gI().mainInfo.moneyChip += chip;
            }
            string info = message.reader().ReadUTF();
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", info);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onChipToXu(Message message)
    {
        try
        {
            sbyte type = message.reader().ReadByte();
            if (type == 1)
            {
                long xu = message.reader().ReadLong();
                long chip = message.reader().ReadLong();
                BaseInfo.gI().mainInfo.moneyXu += xu;
                BaseInfo.gI().mainInfo.moneyChip -= chip;
            }
            string info = message.reader().ReadUTF();
            PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", info, delegate { });
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onInboxMessage(Message message)
    {
        try
        {
            //int total = message.reader().ReadByte();
            //for (int i = 0; i < total; i++) {
            //    int id = message.reader().ReadInt();
            //    string guiTu = message.reader().ReadUTF();
            //    string guiLuc = message.reader().ReadUTF();
            //    string noiDung = message.reader().ReadUTF();
            //    sbyte isread = message.reader().ReadByte();
            //    Mail mail = new Mail();
            //    mail.id = id;
            //    mail.guiTu = guiTu;
            //    mail.guiLuc = guiLuc;
            //    mail.content = noiDung;
            //    mail.isRead = isread;
            //    if (!BaseInfo.gI().listMail.Contains(mail))
            //        BaseInfo.gI().listMail.Add(mail);
            //}
            PopupAndLoadingScript.instance.HideLoading();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onDelMessage(Message message)
    {

    }


    public void onListEvent(Message message)
    {
        try
        {
            int total = message.reader().ReadInt();

            //Debug.LogError("Total Su Kien " + total);
            for (int i = 0; i < total; i++)
            {
                Event sk = new Event();
                int id = message.reader().ReadInt();
                string title = message.reader().ReadUTF();
                string content = message.reader().ReadUTF();
                sk.id = id;
                sk.title = title;
                sk.content = content;
                if (!BaseInfo.gI().listEvent.Contains(sk))
                    BaseInfo.gI().listEvent.Add(sk);
            }
            PopupAndLoadingScript.instance.HideLoading();
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onReadMessage(Message message)
    {
        if (BaseInfo.gI().soTinNhan > 0)
            BaseInfo.gI().soTinNhan--;
    }

    public void onGetPass(Message message)
    {
        try
        {
            string sms = message.reader().ReadUTF();
            string ds = message.reader().ReadUTF();
#if UNITY_EDITOR
            PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", "Soạn tin theo cú pháp " + sms + " gửi đến " + ds);
#else
            PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("","Chương trình sẽ gửi tin nhắn để đổi mật khẩu (phí 1000đ), bạn có đồng ý không?",
                                                      delegate { GameControl.instance.sendSMS(ds, sms); });
#endif
            PopupAndLoadingScript.instance.HideLoading();

        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        //finally {
        //    SendData.getPass = false;
        //}
    }

    public void onReceiveFreeMoney(Message message)
    {
        //System.out.println("COng tien");
        //try
        //{
        //    sbyte x = message.reader().ReadByte();
        //    if (x == 1)
        //    {
        //        long tiencong = message.reader().ReadLong();
        //        BaseInfo.gI().mainInfo.moneyChip += tiencong;
        //        PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Bạn đã được cộng " + tiencong + " tiền free");
        //    }
        //    else
        //    {
        //        PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Bạn không đủ điều kiện nhận tiền free");
        //    }
        //}
        //catch (IOException e)
        //{
        //    //e.printStackTrace();
        //    Debug.LogException(e);
        //}
    }

    public void onLoginfirst(Message message)
    {
        sbyte type = message.reader().ReadByte();
        if (type == 0)
        {
            // tao nv
            //gameControl.panelDangKy.avata.spriteName = BaseInfo.gI().mainInfo.idAvata + "";
            //gameControl.panelDangKy.tg_sex.value = false;
            //if (BaseInfo.gI().mainInfo.gender == 1)
            //    gameControl.panelDangKy.tg_sex.value = true;
            //gameControl.panelDangKy.onShow();
        }
        else if (type == -1)
        {
            // khong thanh cong
            PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", "Không thành công", delegate { });
        }
        else if (type == 1)
        {
            // thanh cong
            //if (gameControl.panelDangKy.isShow) {
            //    gameControl.panelDangKy.onHide();
            //}
            BaseInfo.gI().mainInfo.idAvata = message.reader().ReadInt();
            BaseInfo.gI().mainInfo.displayname = message.reader().ReadUTF();
            //BaseInfo.gI().mainInfo.gender = message.reader().ReadByte();
            BaseInfo.gI().mainInfo.phoneNumber = message.reader().ReadUTF();
            //Debug.LogError("===============================: " + BaseInfo.gI().mainInfo.displayname);
            //gameControl.menu.updateProfileUser();
            //gameControl.room.updateProfileUser();
        }
    }

    public void onInfoNhiemvu(Message message)
    {
        try
        {
            //gameControl.panelNhiemVu.clearParent();
            sbyte size = message.reader().ReadByte();
            for (int i = 0; i < size; i++)
            {
                int id = message.reader().ReadByte();
                string des = message.reader().ReadUTF();
                int xuBonus = message.reader().ReadInt();
                string giftcode = message.reader().ReadUTF();
                bool check = message.reader().ReadBoolean();

                //gameControl.panelNhiemVu.addItem(id, des, xuBonus, giftcode, check);
            }

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onUpdateNhiemvu(Message message)
    {
        try
        {
            sbyte id = message.reader().ReadByte();
            string tt = message.reader().ReadUTF();
            bool check = message.reader().ReadBoolean();
            //gameControl.panelNhiemVu.updateItem(id, tt, check);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onTop(Message message)
    {
        try
        {
            //sbyte gameid = message.reader().ReadByte();
            //gameControl.main.clearGrid (gameid);
            //sua
            //gameControl.panelRank.clearList();
            //int size = message.reader().ReadByte();
            //for (int i = 0; i < size; i++)
            //{
            //    string displayname = message.reader().ReadUTF();
            //    int idAvata = message.reader().ReadInt();
            //    long money = message.reader().ReadLong();

            //    //RankingPlayer temp = new RankingPlayer();
            //    //temp.rank = i + 1;
            //    //temp.playerName = displayname;
            //    //temp.idAvata = idAvata;
            //    //temp.money = money;

            //    gameControl.panelRank.InstanceItem(i + 1, idAvata, displayname, money);
            //}

        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public void onUpdateProfile(int code, string info)
    {
        PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Cập nhật thông tin cá nhân thành công!");
    }

    public void onSMS9029(Message message)
    {
        //LoadAssetBundle.LoadScene(Res.CHUYENXU_AB, Res.CHUYENXU_NAME, () => {
        try
        {
            //if (PanelNapChuyenXu.instance.panelSMS.parent.transform.childCount == 0)
            //{
            sbyte size = message.reader().ReadByte();
            if (size == 0)
                BaseInfo.gI().is9029 = false;
            else
                BaseInfo.gI().is9029 = true;
            List<Item9029> list = new List<Item9029>();
            for (int i = 0; i < size; i++)
            {
                Item9029 it = new Item9029();
                string name = message.reader().ReadUTF();
                string syntax = message.reader().ReadUTF();
                short port = message.reader().ReadShort();
                long money = message.reader().ReadLong();

                it.Name = name;
                it.sys = syntax;
                it.port = port;
                it.money = money;

                list.Add(it);
            }

            list.Sort(delegate (Item9029 it1, Item9029 it2)
            {
                return it1.money.CompareTo(it2.money);
            });
            BaseInfo.gI().list9029.AddRange(list);
            //PanelNapChuyenXu.instance.panelSMS.addList9029(list);
            //}
        }
        catch (IOException e)
        {
            Debug.LogException(e);
            Debug.Log("9029=======================");
        }
        //});
    }

    public void onCardXepMB(Message message)
    {
        try
        {
            int size = message.reader().ReadInt();
            byte[] cards = new byte[size];
            int[] cardss = new int[size];
            message.reader().Read(cards, 0, size);
            for (int i = 0; i < size; i++)
            {
                cardss[i] = cards[i];
            }
            //gameControl.mainScreen.curentCasino.onCardXepMB (cardss);

        }
        catch (IOException e)
        {
            Debug.LogException(e);
        }
    }

    public void onPhomha(Message message)
    {
        //gameControl.mainScreen.curentCasino.onPhomha (message);
        //sua
        try
        {
            GameControl.instance.currentCasino.onPhomha(message);
        }
        catch (Exception)
        {

            throw;
        }
    }

    //Xoc dia
    public void onBeGinXocDia(Message message)
    {
        try
        {
            int time = message.reader().ReadByte();
            //sua
            try
            {
                GameControl.instance.currentCasino.onBeGinXocDia(time);
            }
            catch (Exception)
            {

                throw;
            }
        }
        catch (IOException e)
        {
            Debug.LogException(e);
        }
    }

    public void onBeginXocDiaTimeDatcuoc(Message message)
    {
        try
        {
            int time = message.reader().ReadByte();
            //sua
            try
            {
                GameControl.instance.currentCasino.onBeginXocDiaTimeDatcuoc(time);
            }
            catch (Exception)
            {

                throw;
            }
        }
        catch (IOException e)
        {
            Debug.LogException(e);
        }
    }

    public void onXocDiaMobat(Message message)
    {
        try
        {
            //Lay so luong quan do tu server gui ve.
            int numRed = message.reader().ReadByte();
            //sua
            try
            {
                GameControl.instance.currentCasino.onXocDiaMobat(numRed);
            }
            catch (Exception)
            {

                throw;
            }
        }
        catch (IOException e)
        {
            Debug.LogException(e);
        }
    }

    public void onXocDiaDatcuoc(Message message)
    {
        try
        {
            string nick = message.reader().ReadUTF();
            sbyte cua = message.reader().ReadByte();
            long money = message.reader().ReadLong();
            sbyte typeCHIP = message.reader().ReadByte();
            //sua

            try
            {
                GameControl.instance.currentCasino.onXocDiaDatcuoc(nick, cua, money, typeCHIP);
            }
            catch (Exception)
            {

                throw;
            }
        }
        catch (IOException e)
        {
            Debug.LogException(e);
        }
    }

    public void onXocDiaCacMucCuoc(Message message)
    {
        try
        {
            long muc1 = message.reader().ReadLong();
            long muc2 = message.reader().ReadLong();
            long muc3 = message.reader().ReadLong();
            long muc4 = message.reader().ReadLong();
            //sua
            
            try
            {
                if (GameControl.instance.currentCasino != null)
                    GameControl.instance.currentCasino.onXocdiaNhanCacMucCuoc(muc1, muc2, muc3, muc4);
                else {
                    BaseInfo.gI().listMucCuocXocDia.Add(muc1);
                    BaseInfo.gI().listMucCuocXocDia.Add(muc2);
                    BaseInfo.gI().listMucCuocXocDia.Add(muc3);
                    BaseInfo.gI().listMucCuocXocDia.Add(muc4);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        catch (IOException e)
        {
            Debug.LogException(e);
        }
    }

    public void onXocDiaDatlai(Message message)
    {
        try
        {
            string nick = message.reader().ReadUTF();
            sbyte socua = message.reader().ReadByte();
            //sua
            try
            {
                GameControl.instance.currentCasino.onXocDiaDatlai(nick, socua, message);
            }
            catch (Exception)
            {

                throw;
            }
        }
        catch (IOException e)
        {
            Debug.LogException(e);
        }
    }

    public void onXocDiaDatGapdoi(Message message)
    {
        try
        {
            string nick = message.reader().ReadUTF();
            sbyte socua = message.reader().ReadByte();
            //sua
            try
            {
                GameControl.instance.currentCasino.onXocDiaDatGapdoi(nick, socua, message);
            }
            catch (Exception)
            {

                throw;
            }
        }
        catch (IOException e)
        {
            Debug.LogException(e);
        }
    }

    public void onXocDiaHuycuoc(Message message)
    {
        try
        {
            string nick = message.reader().ReadUTF();
            long moneycua0 = message.reader().ReadLong();
            long moneycua1 = message.reader().ReadLong();
            long moneycua2 = message.reader().ReadLong();
            long moneycua3 = message.reader().ReadLong();
            long moneycua4 = message.reader().ReadLong();
            long moneycua5 = message.reader().ReadLong();
            //sua

            try
            {
                GameControl.instance.currentCasino.onXocDiaHuycuoc(nick, moneycua0, moneycua1, moneycua2, moneycua3, moneycua4, moneycua5);
            }
            catch (Exception)
            {

                throw;
            }
        }
        catch (IOException e)
        {
            Debug.LogException(e);
        }
    }

    public void onXocDiaUpdateCua(Message message)
    {
        //sua
        try
        {
            if(GameControl.instance.currentCasino != null)
                GameControl.instance.currentCasino.onXocDiaUpdateCua(message);
            else
                BaseInfo.gI().onXocDiaUpdateCua = message;
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onXocDiaHistory(Message message)
    {
        try
        {
            //Danh sach chua cac id history cua van choi xoc dia.
            //id = 1 : quan do
            //id = 0 : quan trang
            List<int> id = new List<int>();

            string a = message.reader().ReadUTF();
            string[] chuoi = Regex.Split(a, ",");
            if (chuoi.Length == 0)
            {

            }
            else
            {
                for (int i = 0; i < chuoi.Length; i++)
                {
                    if (chuoi[i] != "")
                    {
                        int idQuan = System.Convert.ToInt32(chuoi[i]);
                        id.Add(idQuan);
                    }
                }
                //sua
                
                try
                {
                    if (GameControl.instance.currentCasino != null)
                        GameControl.instance.currentCasino.onXocDiaHistory(id);
                    else {
                        BaseInfo.gI().listIdHistoryXocDia.AddRange(id);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }

        }
        catch (IOException e)
        {
            Debug.LogException(e);
        }
    }

    public void onXocDiaChucNangHuycua(Message message)
    {
        //sua

        try
        {
            GameControl.instance.currentCasino.onXocDiaChucNangHuycua(message);
        }
        catch (Exception)
        {

            throw;
        }
    }

    public void onXocDiaBeginTimerDungCuoc(Message message)
    {
        //sua

        try
        {
            GameControl.instance.currentCasino.onXocDiaBeginTimerDungCuoc(message);
        }
        catch (Exception)
        {

            throw;
        }
    }
    //Xoc dia

    public void onMoneyFree(long money)
    {
        //gameControl.panelGetMoneyDay.onShow(money);
    }

    public void onListProduct(Message message)
    {
        try
        {
            //int total = message.reader().ReadInt();
            //for (int i = 0; i < total; i++)
            //{
            //    string productId = message.reader().ReadUTF();
            //    int price = message.reader().ReadInt();
            //    int xu = message.reader().ReadInt();
            //}
        }
        catch (Exception ex) { }
    }

    public void onTienVay(Message message)
    {
        try
        {
            //bool isCheck = message.reader().ReadBoolean();
            //long moneyOwe = message.reader().ReadLong();
            //int total = message.reader().ReadInt();
            //for (int i = 0; i < total; i++)
            //{
            //    long bonus1 = message.reader().ReadLong();
            //    long bonus2 = message.reader().ReadLong();
            //    long bonus3 = message.reader().ReadLong();
            //}
        }
        catch (Exception ex) { }
    }

    /*
#region XENG
    public void onXeng_joinGame(Message message)
    {
        PopupAndLoadingScript.instance.HideLoading();
        //sua
        //gameControl.setCasino(GameID.XENG, 0);
    }

    public void onXeng_exitGame(Message message)
    {
        PopupAndLoadingScript.instance.HideLoading();
        //sua
        //gameControl.setStage(gameControl.room);
        ////gameControl.room.onClickGame(gameControl.room.btn_click_game[0].gameObject);
        //gameControl.top.setGameName();
    }

    public void onXeng_datCuoc(Message message)
    {
        SendData.onStartQuayXengHoaQua();
    }

    public void onXeng_quayXeng(Message message)
    {
    }

    public void onXeng_gameOver(Message message)
    {
        //sua
        //gameControl.xeng.recieveCode(message);
    }
#endregion

#region TAI XIU
    public void onjoinTaiXiu(Message message)
    {
        //sua
        //gameControl.currentCasino.onjoinTaiXiu(message);
    }

    public void onTimestartTaixiu(Message message)
    {
        //sua
        //gameControl.currentCasino.onTimestartTaixiu(message);
    }

    public void onAutostartTaixiu(Message message)
    {
        //sua
        //gameControl.currentCasino.onAutostartTaixiu(message);
    }

    public void onGameoverTaixiu(Message message)
    {
        //sua
        //gameControl.currentCasino.onGameoverTaixiu(message);
    }

    public void onCuocTaiXiu(Message message)
    {
        //sua
        //gameControl.currentCasino.onCuocTaiXiu(message);
    }

    public void oninfoTaiXiu(Message message)
    {
        //sua
        //gameControl.currentCasino.oninfoTaiXiu(message);
    }

    public void onupdatemoneyTaiXiu(Message message)
    {
        //sua
        //gameControl.currentCasino.onupdatemoneyTaiXiu(message);
    }

    public void oninfoLSTheoPhienTaiXiu(Message message)
    {
        //sua
        //gameControl.currentCasino.oninfoLSTheoPhienTaiXiu(message);
    }

    public void onExitTaiXiu(Message message)
    {
        PopupAndLoadingScript.instance.HideLoading();
        //sua
        //gameControl.setStage(gameControl.room);

        ////gameControl.room.onClickGame(gameControl.room.btn_click_game[0].gameObject);
        //gameControl.top.setGameName();
    }
#endregion

    */
    public void onUpVIP(Message message)
    {
        sbyte vip = message.reader().ReadByte();
        //sua
        //gameControl.panelUpVip.onShow(vip);
    }
}