using UnityEngine;
using System.Collections;

interface IChatListener
{

    void onDisConnect();

    //void onConnectionFail();

    void onLoginSuccess(Message msg);

    void onLoginFail(int id, string info);
    void onBaoXam(Message message);
    //void onListRoom(Message message);
    void onPhoneCSKH(Message message);
    void onListTable(int totalTB, Message message);

    void onJoinGame(Message message);
    void onJoinTablePlaySuccess(Message message);

    void onUserJoinTable(int tbid, string nick, string displayname,
        string link_avatar, int idAvata, sbyte pos, long money,
        long folowmoney);

    void onUserExitTable(int idTb, string master, string nick);

    void onJoinRoomFail(string info);

    void onJoinTableSuccess(Message message);

    void onJoinTableFail(string info);
    void onJoinTablePlayFail(string info);
    void onProfile(Message msg);

    void onStartFail(string info);

    void onStartSuccess(Message message);

    void onStartForView(Message message);
    void onExitView(Message message);
    void onSetTurn(Message message);

    void onFireCard(string nick, string turnname, int[] card);

    void onFireCardFail();

    void onGetCardNocSuccess(string nick, int card);

    void onEatCardSuccess(string from, string to, int card);

    void onBalanceCard(string from, string to, int card);

    void onReady(Message message);

    void onAttachCard(string from, string to, int[] cards, int[] cardsgui);

    void onAllCardPlayerFinish(Message message);

    void onFinishGame(Message message);

    void onDropPhomSuccess(string nick, int[] arrayPhom);

    void onInvite(string nickInvite, string displayName, sbyte gameid, short roomid, short tblid, long money, long minMoney, long maxMoney);
    void onChangeBetMoney(Message message);
    //void onRegSuccess(Message msg);

    //void onRegFail(string info);

    void onKickOK();

    void onSysKick();

    void onChatMessage(string nick, string msg, bool outs);

    //void onUpdateVersion(string version, string link,
    //        string decription);

    //void onUpdateProfile(int code, string info);

    void onInboxMessage(Message message);

    void onDelMessage(Message message);

    void onReadMessage(Message message);

    //void onUnReadMessage(Message messge);

    void onGameID(Message messge);

    void onMessageServer(string info);

    //void onMsgChat(string from, string msg);

    void onDetailUser(Message message);

    void onInfoSMS(Message message);

    void onSetNewMaster(string nick);

    //// /////// xi to
    void onNickCuoc(long moneyInPot, long soTienTo, long moneyBoRa,
           string nick, Message message);

    void onNickTheo(long money, string nick, Message message);

    void onNickSkip(string nick, string turnName);

    void onNickSkip(string nick, Message msg);

    void onUpdateMoneyMessage(string readstring, int type, long readInt);

    //void onUpdateVersion(Message message);

    void onGetPass(Message message);

    void onListInvite(Message msg);

    void onInfoPockerTbale(Message message);

    void onAddCardTbl(Message message);

    void onChangeRuleTbl(sbyte readByte);

    void onUpdateMoneyTbl(Message message);

    void onUpdateRoom(int readShort, Message message);

    //void onGetPhoneCSKH(Message message);

    void onGetAlertLink(Message message);

    //void onGetBoxGift(Message message);

    void infoWinPlayer(Message message);

    void InfoCardPlayerInTbl(Message message);

    void InfoGift2(Message message);

    void onGetMoney();

    void onTimeAuToStart(Message message);

    void startFlip(Message message);

    void onCardFlip(Message message);

    //void onUpdateVersionNew(string link);

    //void onIntroduceFriend(string mess, string link);

    void onRankMauBinh(Message message);

    void onFinalMauBinh(string name);

    void onWinMauBinh(string name, sbyte typeCard);

    void onInfoMe(Message message);

    void onBeginRiseBacay(Message message);

    void onFlip3Cay(Message message);
    void onListEvent(Message message);
    void onCuoc3Cay(Message message);
    //void onInfoSMSAppStore(Message message);
    void onBuyItem(Message message);

    void onJoinView(Message message);

    void onSetMoneyTable(long money);
    void onUpdataAvata(Message message);

    void onChangeName(Message message);

    void onPopupNotify(Message message);

    void onCreateTable(Message message);

    void onListBetMoney(Message message);

    void onRateScratchCard(Message message);

    void onXuToNick(Message message);

    void onXuToChip(Message message);

    void onChipToXu(Message message);

    void onReceiveFreeMoney(Message message);

    void onUpdateProfile(int code, string info);

    void onSMS9029(Message message);

    void onPhomha(Message message);

    //Xocdia
    void onBeGinXocDia(Message message);
    void onBeginXocDiaTimeDatcuoc(Message message);
    void onXocDiaMobat(Message message);
    void onXocDiaDatcuoc(Message message);
    void onXocDiaCacMucCuoc(Message message);
    void onXocDiaDatlai(Message message);
    void onXocDiaDatGapdoi(Message message);
    void onXocDiaHuycuoc(Message message);
    void onXocDiaUpdateCua(Message message);
    void onXocDiaHistory(Message message);
    void onXocDiaChucNangHuycua(Message message);
    void onXocDiaBeginTimerDungCuoc(Message message);
    //Xocdia
    void onListProduct(Message message);
    void onTienVay(Message message);

}