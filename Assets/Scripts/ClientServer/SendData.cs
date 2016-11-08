using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
public class SendData
{
    public static bool getPass = false;
    public static bool isLogin = false;
    public static void onSendSmsSyntax()
    {
        Message msg = new Message(CMDClient.CMD_SMS);
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onAutoJoinTable()
    {
        try
        {
            Message msg = new Message(CMDClient.CMD_AUTOJOINTABLE);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }


    public static void onGetListInvite()
    {
        Message msg = new Message(CMDClient.CMD_LIST_INVITE);
        try
        {
            msg.writer().WriteShort((short)1);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        NetworkUtil.GI().sendMessage(msg);
    }


    public static void onJoinTableForView(int tbid, String pass)
    {
        //Debug.LogError("tbid: " + tbid);
        //Debug.LogError("pass: " + pass);
        Message msg = new Message(CMDClient.CMD_FOR_VIEW);
        try
        {
            msg.writer().WriteShort((short)tbid);
            msg.writer().WriteUTF(pass);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        NetworkUtil.GI().sendMessage(msg);
    }

    //public static void onGetPhoneCSKH() {
    //    try {
    //        Message msg = new Message(CMDClient.CMD_GETPHONECSKH);
    //        NetworkUtil.GI().sendMessage(msg);
    //    }
    //    catch (Exception ex) {
    //        Debug.LogException(ex);
    //    }
    //}

    public static Message onGetPhoneCSKH()
    {
        Message msg = new Message(CMDClient.CMD_GETPHONECSKH);
        try
        {
            msg.writer().WriteByte((byte)CMDClient.PROVIDER_ID);
            //            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
        }
        return msg;
    }

    public static void onGetPass(string nick)
    {
        Message msg = new Message(CMDClient.CMD_GET_PASS);
        try
        {
            msg.writer().WriteByte((sbyte)CMDClient.PROVIDER_ID);
            msg.writer().WriteUTF(nick);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        getPass = true;
        NetworkUtil.GI().sendMessage(msg);
    }

    public static Message onGetMessagePass(string nick)
    {
        Message msg = new Message(CMDClient.CMD_GET_PASS);
        try
        {
            msg.writer().WriteByte((sbyte)CMDClient.PROVIDER_ID);
            msg.writer().WriteUTF(nick);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        return msg;
    }



    public static void onViewInfoFriend(string nick)
    {
        Message msg = new Message(CMDClient.CMD_VIEW_INFO_FRIEND);
        try
        {
            msg.writer().WriteUTF(nick);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }


    //public static void onLogin(string username, string pass) {
    //    Message msg = new Message(CMDClient.CMD_LOGIN);
    //    try {
    //        msg.writer().WriteUTF(username);
    //        msg.writer().WriteUTF(pass);
    //        msg.writer().WriteUTF(Res.version);
    //    }
    //    catch (Exception ex) {
    //        Debug.LogException(ex);

    //    }
    //    NetworkUtil.GI().sendMessage(msg);

    //}

    //public static Message onGetMessageLogin(string username, string pass) {
    //    Message msg = new Message(CMDClient.CMD_LOGIN);
    //    try {
    //        msg.writer().WriteUTF(username);
    //        msg.writer().WriteUTF(pass);
    //        msg.writer().WriteUTF(Res.version);
    //    }
    //    catch (Exception ex) {
    //        Debug.LogException(ex);
    //    }
    //    return msg;
    //}

    public static void onSendSkipTurn()
    {
        Message msg = new Message(CMDClient.CMD_PASS);
        try
        {
            msg.writer().WriteShort((short)1);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);

    }


    public static void onJoinRoom(int rid)
    {
        Message msg = new Message(CMDClient.CMD_LIST_TABLE);
        try
        {
            msg.writer().WriteByte((sbyte)rid);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        //Debug.LogError("onJoinRoom");
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onJoinTable(int tbid, string pass,
            long folowMoney)
    {

        Message msg = new Message(CMDClient.CMD_JOIN_TABLE);
        try
        {
            msg.writer().WriteShort((short)tbid);
            msg.writer().WriteUTF(pass);
            msg.writer().WriteLong(folowMoney);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }


        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onOutTable()
    {
        Message msg = new Message(CMDClient.CMD_EXIT_TABLE);
        try
        {
            msg.writer().WriteShort(0);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        // SerializerHelper.WriteInt(Integer.parseInt(tbid));

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onOutView()
    {
        Message msg = new Message(CMDClient.CMD_EXIT_VIEW);
        NetworkUtil.GI().sendMessage(msg);
    }


    public static void onStartGame()
    {
        Message msg = new Message(CMDClient.CMD_START_GAME);
        try
        {
            msg.writer().WriteShort((short)1);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        // SerializerHelper.WriteInt(tbid);

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onSendArrayPhom(int currentTableID, int[][] array)
    {
        Message msg = new Message(CMDClient.CMD_HA_PHOM_TAY);
        try
        {
            msg.writer().WriteShort((short)currentTableID);
            msg.writer().WriteByte((byte)array.Length);
            for (int i = 0; i < array.Length; i++)
            {
                sbyte[] card = new sbyte[array[i].Length];
                msg.writer().WriteInt(array[i].Length);
                for (int j = 0; j < card.Length; j++)
                {
                    card[j] = (sbyte)array[i][j];
                    msg.writer().WriteByte(card[j]);
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onFireCard(int card)
    {
        Message msg = new Message(CMDClient.CMD_FIRE_CARD);
        try
        {
            msg.writer().WriteShort(1);
            msg.writer().WriteByte((byte)card);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onFireCardTL(int[] card)
    {
        Message msg = new Message(CMDClient.CMD_FIRE_CARD);
        try
        {
            msg.writer().WriteShort(1);
            // SerializerHelper.WriteInt(card);

            sbyte[] data = new sbyte[card.Length];
            msg.writer().WriteInt(data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (sbyte)card[i];
                msg.writer().WriteByte(card[i]);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        // SerializerHelper.WriteInt(Integer.parseInt(tbid));

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onGetCardNoc()
    {
        Message msg = new Message(CMDClient.CMD_GET_CARD);
        try
        {
            msg.writer().WriteShort(1);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        // SerializerHelper.WriteInt(tbid);

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onGetCardFromPlayer()
    {
        Message msg = new Message(CMDClient.CMD_EAT_CARD);
        try
        {
            msg.writer().WriteShort(1);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        // SerializerHelper.WriteInt(tbid);

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onLogOut()
    {
        Message msg = new Message(CMDClient.CMD_EXIT_GAME);
        try
        {
            msg.writer().WriteUTF(BaseInfo.gI().username);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onRegister(string username, string pass)
    {
        Message msg = new Message(CMDClient.CMD_REGISTER);
        try
        {
            msg.writer().WriteByte((byte)CMDClient.PROVIDER_ID);
            msg.writer().WriteUTF(username);
            msg.writer().WriteUTF(pass);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static Message onGetMessageRegister(string username, string pass)
    {
        Message msg = new Message(CMDClient.CMD_REGISTER);
        try
        {
            msg.writer().WriteByte((byte)CMDClient.PROVIDER_ID);
            msg.writer().WriteUTF(username);
            msg.writer().WriteUTF(pass);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        return msg;
    }

    public static void onUpdateProfile(string[] info)
    {
        Message msg = new Message(CMDClient.CMD_UPDATE_PROFILE);
        try
        {
            msg.writer().WriteInt(info.Length);
            for (int i = 0; i < info.Length; i++)
            {
                msg.writer().WriteUTF(info[i]);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onReady(int ready)
    {
        Message msg = new Message(CMDClient.CMD_READY);
        try
        {
            msg.writer().WriteShort(1);
            // SerializerHelper.WriteInt(ready);
            msg.writer().WriteByte((sbyte)ready);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        // SerializerHelper.WriteInt(Integer.parseInt(tbid));

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onHaPhom(int[][] array)
    {
        try
        {
            Message msg = new Message(CMDClient.CMD_DROP_PHOM);
            if (array == null)
            {
                msg.writer().WriteShort(0);
            }
            else
            {
                msg.writer().WriteShort(1);
                msg.writer().WriteByte((sbyte)array.Length);
                for (int i = 0; i < array.Length; i++)
                {
                    sbyte[] card = new sbyte[array[i].Length];
                    msg.writer().WriteInt(array[i].Length);
                    for (int j = 0; j < card.Length; j++)
                    {
                        card[j] = (sbyte)array[i][j];
                        msg.writer().WriteByte(card[j]);
                    }
                }
            }
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }



    public static void onGetProfile()
    {
        Message msg = new Message(CMDClient.CMD_PROFILE);
        try
        {
            msg.writer().WriteUTF(BaseInfo.gI().username);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onInviteFriend(string nick_accept)
    {
        Message msg = new Message(CMDClient.CMD_INVITE_FRIEND);
        try
        {
            msg.writer().WriteUTF(nick_accept);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onAcceptInviteFriend(sbyte gameid, short roomid,
            short tblid, long folowMoney)
    {
        Message msg = new Message(CMDClient.CMD_ANSWER_INVITE_FRIEND);
        try
        {
            msg.writer().WriteByte(gameid);
            msg.writer().WriteShort(roomid);
            msg.writer().WriteShort(tblid);
            msg.writer().WriteLong(folowMoney);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onKick(string nick)
    {
        Message msg = new Message(CMDClient.CMD_KICK);
        try
        {
            msg.writer().WriteUTF(nick);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onUpdateWaitting()
    {
        Message msg = new Message(CMDClient.CMD_UPDATE_WAITTING_ROOM);
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onUpdateRoom()
    {
        Message msg = new Message(CMDClient.CMD_UPDATE_ROOM);
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onViewTable(int id)
    {
        Message msg = new Message(CMDClient.CMD_VIEW);
        try
        {
            msg.writer().WriteByte((byte)id);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onSetTblPass(string pass)
    {
        Message msg = new Message(CMDClient.CMD_SET_PASSWORD);
        try
        {
            msg.writer().WriteShort(0);
            msg.writer().WriteUTF(pass);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onSendMsgChat(string stmsg)
    {
        Message msg = new Message(CMDClient.CMD_CHAT_MSG);
        try
        {
            msg.writer().WriteShort(1);
            msg.writer().WriteUTF(stmsg);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onSendMsgChat(string toNick, string msgSend)
    {
        Message msg = new Message(CMDClient.CMD_CHAT);
        try
        {
            msg.writer().WriteUTF(toNick);
            msg.writer().WriteUTF(msgSend);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);

    }

    public static void onSendPingPong()
    {
        Message msg = new Message(CMDClient.CMD_PING_PONG);
        try
        {
            msg.writer().WriteByte((byte)1);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onGetFriendList(string nick)
    {
        Message msg = new Message(CMDClient.CMD_FRIEND_LIST);
        try
        {
            msg.writer().WriteUTF(nick);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onGetInboxMessage()
    {
        Message msg = new Message(CMDClient.CMD_GET_INBOX_MESSAGE);
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onSendMessageToUser(string sender, string nick,
            string content)
    {
        Message msg = new Message(CMDClient.CMD_SEND_MESSAGE);
        try
        {
            msg.writer().WriteUTF(sender);
            msg.writer().WriteUTF(nick);
            msg.writer().WriteUTF(content);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onDelMessage(int id)
    {
        Message msg = new Message(CMDClient.CMD_DEL_MESSAGE);
        try
        {
            msg.writer().WriteInt(id);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onReadMessage(int id)
    {
        Message msg = new Message(CMDClient.CMD_READ_MESSAGE);
        try
        {
            msg.writer().WriteInt(id);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        // System.out.println("send get noi dung msg");
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onSendGameID(sbyte id)
    {
        Message msg = new Message(CMDClient.CMD_JOIN_GAME);
        try
        {
            msg.writer().WriteByte(id);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onJoinTablePlay(int tbid, String pass,
        long folowMoney)
    {
        Message msg = new Message(CMDClient.CMD_JOIN_TABLE_PLAY);
        try
        {
            msg.writer().WriteShort((short)tbid);
            // msg.writer().writeUTF(nick);
            msg.writer().WriteUTF(pass);
            msg.writer().WriteLong(folowMoney);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onSendMoneyCuoc(int tbID, long money)
    {
        Message msg = new Message(CMDClient.CMD_CUOC);
        try
        {
            msg.writer().WriteShort((short)tbID);
            msg.writer().WriteLong(money);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onAccepFollow()
    {
        Message msg = new Message(CMDClient.CMD_THEO);
        try
        {
            msg.writer().WriteShort(1);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onChangeRuleTbl()
    {
        Message msg = new Message(CMDClient.CMD_CHANGERULETBL);
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onSendSmsChangePass(string old, string newp,
            string renewp)
    {
        // string sms = SMSManager.SMS_CHANGE_PASS_SYNTAX + " " + Login.userName
        // + " " + old + " " + newp;
        // SMSManager.SendSMSToFriend(sms, new Action() {
        //
        // public void perform() {
        // MScreen.currentPage.showInfo("Gửi yêu cầu đổi mật khẩu thành công.");
        // }
        // }, SMSManager.SMS_CHANGE_PASS_NUMBER);
        // Message msg = new Message( CMDClient.CMD_CHANGPASS);
        // msg.writer().WriteUTF(old);
        // msg.writer().WriteUTF(newp);
        // msg.writer().WriteUTF(renewp);
        // NetworkUtil.GI().sendMessage(msg);
    }

    public static void onAddFriendChat(string nick)
    {
        Message msg = new Message(CMDClient.CMD_ADD_FRIEND_CHAT);
        try
        {
            msg.writer().WriteUTF(nick);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void sendKey()
    {
        NetworkUtil.GI().sendMessage(new Message((sbyte)-27));
    }

    public static void onCuocXT(int type, long money)
    {
        Message msg = new Message(CMDClient.CMD_CUOC);
        try
        {
            msg.writer().WriteInt(type);
            msg.writer().WriteLong(money);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    public static void onGetInfoTable()
    {
        try
        {
            Message msg = new Message(CMDClient.CMD_INFOPLAYER_TBL);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public static void onGetInfoGift()
    {
        try
        {
            Message msg = new Message(CMDClient.CMD_INFO_GIFT2);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public static void onSendGift(int id, long gia)
    {
        try
        {
            Message msg = new Message(CMDClient.CMD_SEND_GIFT2);
            msg.writer().WriteInt(id);
            msg.writer().WriteLong(gia);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public static void onSendGetMoney(long money)
    {
        try
        {
            Message msg = new Message(CMDClient.CMD_GET_MONEY);
            msg.writer().WriteLong(money);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public static void onFlipCard(byte index)
    {
        try
        {
            Message msg = new Message(CMDClient.CMD_FLIP_CARD);
            msg.writer().WriteByte(index);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public static void onUpdateVersionNew(byte providerID)
    {
        try
        {
            Message msg = new Message(CMDClient.CMD_UPDATE_VERSION_NEW);
            msg.writer().WriteByte(providerID);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public static Message onGetMessageUpdateVersionNew(sbyte providerID)
    {
        Message msg = null;
        try
        {
            msg = new Message(CMDClient.CMD_UPDATE_VERSION_NEW);
            msg.writer().WriteByte(providerID);
            // NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
        return msg;
    }

    public static void onIntroduceFriend(byte providerID)
    {
        try
        {
            Message msg = new Message(CMDClient.INTRODUCE_FRIEND);
            msg.writer().WriteByte(providerID);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public static Message onGetMessageIntroduceFriend(sbyte providerID)
    {
        Message msg = null;
        try
        {
            msg = new Message(CMDClient.INTRODUCE_FRIEND);
            msg.writer().WriteByte(providerID);
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
        return msg;
    }

    public static void onSendGCM(string regID)
    {
        try
        {
            Message msg = new Message(CMDClient.CMD_REGISTER_GCM);
            msg.writer().WriteUTF(regID);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public static void onFinalMauBinh(int[] card)
    {
        Message msg = new Message(CMDClient.CMD_FINAL_MAUBINH);
        try
        {
            // msg.writer().WriteShort(1);
            // SerializerHelper.WriteInt(card);
            sbyte[] data = new sbyte[card.Length];
            msg.writer().WriteInt(data.Length);
            for (int i = 0; i < data.Length; i++)
            {
                data[i] = (sbyte)card[i];
                msg.writer().WriteByte(data[i]);
            }
            // SerializerHelper.writeArrayInt(card);


        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        // SerializerHelper.WriteInt(Integer.parseInt(tbid));

        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onSendCuocBC(long money)
    {
        try
        {
            Message msg = new Message(CMDClient.CMD_CUOC);
            msg.writer().WriteLong(money);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public static void onFlip3Cay()
    {
        try
        {
            Message msg = new Message(CMDClient.CMD_FLIP_3CAY);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
            // TODO: handle exception
        }
    }

    public static void onBuyItem(int id, string nick_nem, string nick_bi_nem)
    {
        Message msg = new Message(CMDClient.CMD_BUY_ITEM);
        try
        {
            msg.writer().WriteInt(id);
            msg.writer().WriteUTF(nick_nem);
            msg.writer().WriteUTF(nick_bi_nem);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onCreateTable(int gameid, int roomid, long money, int maxplayer, int choinhanh, String password)
    {
        Message msg = new Message(CMDClient.CMD_CREATE_TABLE);
        try
        {
            BaseInfo.gI().numberPlayer = maxplayer;
            msg.writer().WriteInt(gameid);
            msg.writer().WriteInt(roomid);
            msg.writer().WriteLong(money);
            msg.writer().WriteInt(maxplayer);
            msg.writer().WriteInt(choinhanh);
            msg.writer().WriteUTF(password);
        }
        catch (Exception e)
        {
            // TODO Auto-generated catch block\

            Debug.LogException(e);
        }
        NetworkUtil.GI().sendMessage(msg);
    }
    public static void baoxam(int type)
    {
        try
        {
            Message message = new Message(CMDClient.CMD_BAOXAM);
            message.writer().WriteByte(type);
            NetworkUtil.GI().sendMessage(message);
        }
        catch (Exception e)
        {
        }
    }

    public static void onUpdateAvata(int idAvata)
    {
        Message msg = new Message(CMDClient.CMD_UPDATE_AVATA);
        try
        {
            msg.writer().WriteInt(idAvata);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onChangeName(string newName)
    {
        Message msg = new Message(CMDClient.CMD_CHANGE_NAME);
        try
        {
            msg.writer().WriteUTF(newName);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onXuToNick(long userID, long xu)
    {
        Message msg = new Message(CMDClient.CMD_XU_TO_NICK);
        try
        {
            msg.writer().WriteLong(userID);
            msg.writer().WriteLong(xu);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onXuToChip(long xu)
    {
        Message msg = new Message(CMDClient.CMD_XU_TO_CHIP);
        try
        {
            msg.writer().WriteLong(xu);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onChipToXu(long chip)
    {
        Message msg = new Message(CMDClient.CMD_CHIP_TO_XU);
        try
        {
            msg.writer().WriteLong(chip);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
        NetworkUtil.GI().sendMessage(msg);
    }

    public static void onChangeBetMoney(long money)
    {
        Message msg = new Message(CMDClient.CMD_CHANGE_BETMONEY);
        try
        {
            msg.writer().WriteLong(money);
            NetworkUtil.GI().sendMessage(msg);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public static void onUpdateProfile(string email, string sdt)
    {
        Message msg = new Message(CMDClient.CMD_UPDATE_PROFILE);
        try
        {
            msg.writer().WriteUTF(email);
            msg.writer().WriteUTF(sdt);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }

        NetworkUtil.GI().sendMessage(msg);
    }

    /**
	 * 
	 * @param telco
	 *            1-viettel, 2-vina, 3-mobi
	 */
    public static void onSendSms9029(int telco)
    {
        Message msg = new Message(CMDClient.CMD_SMS_9029);
        try
        {
            msg.writer().WriteInt(telco);
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
        NetworkUtil.GI().sendMessage(msg);
    }


    //Game xoc dia
    public static void onSendXocDiaDatCuoc(byte cua, long money)
    {
        try
        {
            Message message = new Message(CMDClient.CMD_XOCDIA_DATCUOC);
            message.writer().WriteByte(cua);
            message.writer().WriteLong(money);
            NetworkUtil.GI().sendMessage(message);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public static void onSendLamCai()
    {
        try
        {
            Message message = new Message(CMDClient.CMD_LAMCAI);
            NetworkUtil.GI().sendMessage(message);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public static void onSendDatLai()
    {
        try
        {
            Message message = new Message(CMDClient.CMD_DATLAI);
            NetworkUtil.GI().sendMessage(message);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public static void onSendDatGapDoi()
    {
        try
        {
            Message message = new Message(CMDClient.CMD_GAPDOI);
            NetworkUtil.GI().sendMessage(message);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public static void onSendHuyCuoc()
    {
        try
        {
            Message message = new Message(CMDClient.CMD_HUYCUOC);
            NetworkUtil.GI().sendMessage(message);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public static void onSendChucNangCai(byte type)
    {
        try
        {
            Message message = new Message(CMDClient.CMD_CHUCNANG_HUYCUA);
            message.writer().WriteByte(type);
            NetworkUtil.GI().sendMessage(message);
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }
    //Game Xoc dia
}
