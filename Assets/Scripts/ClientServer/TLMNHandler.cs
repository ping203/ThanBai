using UnityEngine;
using System.Collections;
using System;

public class TLMNHandler : MessageHandler {
    private static IChatListener listenner;
    private static TLMNHandler instance;

    public TLMNHandler() {
    }
    public static TLMNHandler getInstance() {
        if (instance == null) {
            instance = new TLMNHandler();
        }
        return instance;
    }

    public static void setListenner(ListernerServer listener) {
        listenner = listener;
    }

    protected override void serviceMessage(Message message, int messageId) {
        try {

            string nick = "";
            switch (messageId) {
                case CMDClient.CMD_FIRE_CARD:
                    // card=SerializerHelper.readArrayInt(message);
                    if (message.reader().ReadInt() == -1) {
                        listenner.onFireCardFail();
                        Debug.Log("CMD_FIRE_CARD  TLMN handler");
                    }
                    else {
                        nick = message.reader().ReadUTF();
                        int size = message.reader().ReadInt();
                        sbyte[] cardfire = new sbyte[size];
                        for (int i = 0; i < size; i++) {
                            cardfire[i] = message.reader().ReadByte();
                        }
                        int[] data = new int[cardfire.Length];
                        for (int i = 0; i < data.Length; i++) {
                            data[i] = cardfire[i];
                        }
                        // listenner.onFireCard(nick,SerializerHelper.readArrayInt(message));
                        listenner.onFireCard(nick, message.reader().ReadUTF(), data);
                    }
                    break;
                case CMDClient.CMD_FINISH:
                    break;
                case CMDClient.CMD_PASS:// bo luot
                    listenner.onNickSkip(message.reader().ReadUTF(), message
                            .reader().ReadUTF());
                    break;
                case CMDClient.CMD_KILL_PIG:// nhan dc nick user bi chat heo
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
