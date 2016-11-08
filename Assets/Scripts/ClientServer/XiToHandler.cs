using UnityEngine;
using System.Collections;
using System;

public class XiToHandler : MessageHandler {
    public static XiToHandler getInstance() {
        if (instance == null) {
            instance = new XiToHandler();
        }
        return instance;
    }

    private static XiToHandler instance = null;
    private static IChatListener listenner;
    public static void setListenner(ListernerServer listener) {
        listenner = listener;
    }

    protected override void serviceMessage(Message message, int messageId) {
        try {
            String nick = "";
            int card = -1;
            switch (messageId) {
                case CMDClient.CMD_CUOC:// tra ve so tien nick tố
                    // card=SerializerHelper.readArrayInt(message);
                    long moneyInPot = message.reader().ReadLong();
                    long soTienTo = message.reader().ReadLong();
                    long moneyBora = message.reader().ReadLong();
                    nick = message.reader().ReadUTF();
                    listenner.onNickCuoc(moneyInPot, soTienTo, moneyBora, nick,
                            message);
                    break;
                case CMDClient.CMD_FINISH:
                    break;
                case CMDClient.CMD_PASS:// bo luot
                    listenner.onNickSkip(message.reader().ReadUTF(), message);
                    break;
                case CMDClient.CMD_THEO:// nhan dc nick user bi chat heo
                    listenner.onNickTheo(message.reader().ReadLong(), message
                            .reader().ReadUTF(), message);
                    break;
                case CMDClient.CMD_GET_CARD:
                    card = message.reader().ReadByte();
                    if (card == -1) {
                    }
                    else {
                        String nick1 = message.reader().ReadUTF();
                        listenner.onGetCardNocSuccess(nick1, card);
                    }
                    break;
                case CMDClient.CMD_INFOPOCKERTABLE:
                    listenner.onInfoPockerTbale(message);
                    break;
                case CMDClient.CMD_ADDCARDTABLE_POCKER:
                    listenner.onAddCardTbl(message);
                    break;
            }
        }
        catch (Exception ex) {
        }
    }

    public override void onConnectionFail() {
        throw new System.NotImplementedException();
    }

    public override void onDisconnected() {
        throw new System.NotImplementedException();
    }

    public override void onConnectOk() {
        throw new System.NotImplementedException();
    }
}
