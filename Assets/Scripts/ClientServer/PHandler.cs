using UnityEngine;
using System.Collections;
using System;

public class PHandler : MessageHandler{
    private static IChatListener listenner;
    private static PHandler instance;

    public PHandler()
    {

    }
    public static PHandler getInstance()
    {
        if (instance == null)
            instance = new PHandler();
        return instance;
    }

    public static void setListenner(ListernerServer listener)
    {
        listenner = listener;
    }

    protected override void serviceMessage(Message message, int messageId)
    {
        	try {
			int card = -1;
			string from = "", to = "";
			switch (messageId) {
                case CMDClient.CMD_DROP_PHOM:
                    // card = SerializerHelper.readInt(message);
                    card = message.reader().ReadByte();
                    if (card == 0) {
                    }
                    else {
                        // listenner.onDropPhomSuccess(SerializerHelper
                        // .readUTF(message), SerializerHelper
                        // .readArrayInt(message));
                        string nn = message.reader().ReadUTF();
                        int size = message.reader().ReadInt();
                        sbyte[] arry = new sbyte[size];
                        for (int i = 0; i < size; i++) {
                            arry[i] = message.reader().ReadByte();
                        }

                        int[] cdp = new int[arry.Length];
                        for (int i = 0; i < arry.Length; i++) {
                            cdp[i] = arry[i];
                        }
                        listenner.onDropPhomSuccess(nn, cdp);
                    }
                    break;
                case CMDClient.CMD_EAT_CARD:
                    // card = SerializerHelper.readInt(message);
                    card = message.reader().ReadByte();
                    if (card == -1) {
                    }
                    else {
                        listenner.onEatCardSuccess(message.reader().ReadUTF(),
                                message.reader().ReadUTF(), card);
                    }
                    break;
                case CMDClient.CMD_BALANCE:
                    // card = SerializerHelper.readInt(message);
                    card = message.reader().ReadByte();
                    from = message.reader().ReadUTF();
                    to = message.reader().ReadUTF();
                    listenner.onBalanceCard(from, to, card);
                    break;
                case CMDClient.CMD_FIRE_CARD:
                    // card = SerializerHelper.readInt(message);
                    card = message.reader().ReadByte();
                    if (card == -1) {
                        listenner.onFireCardFail();
                        // MScreen.currentTable.centerSoftKey.caption =
                        // StaticText.fight();
                    }
                    else {
                        from = message.reader().ReadUTF();
                        listenner.onFireCard(from, message.reader().ReadUTF(),
                                new int[] { card });
                    }
                    break;
                case CMDClient.CMD_GET_CARD:
                    // card = SerializerHelper.readInt(message);
                    card = message.reader().ReadByte();
                    if (card == -1) {
                    }
                    else {
                        // System.out.println(card+" >>> card rut dc");
                        from = message.reader().ReadUTF();
                        listenner.onGetCardNocSuccess(from, card);
                    }
                    break;
                case CMDClient.CMD_GUI_CARD:
                    string fromplayer = message.reader().ReadUTF();
                    string toplayer = message.reader().ReadUTF();
                    int sizes = message.reader().ReadInt();
                    int[] phomgui = new int[sizes];
                    for (int i = 0; i < phomgui.Length; i++) {
                        phomgui[i] = message.reader().ReadByte();
                    }
                    int[] cardgui = new int[message.reader().ReadInt()];
                    for (int i = 0; i < cardgui.Length; i++) {
                        cardgui[i] = message.reader().ReadByte();
                    }
                    listenner.onAttachCard(fromplayer, toplayer, phomgui, cardgui);
                    break;
			default:
                    Debug.Log("Khong vao cau lenh naooooooooooooo ");
				break;
			}
		} catch (Exception ex) {
            Debug.LogException(ex);
		}
    }

    public override void onConnectionFail()
    {
        throw new System.NotImplementedException();
    }

    public override void onDisconnected()
    {
        throw new System.NotImplementedException();
    }

    public override void onConnectOk()
    {
        throw new System.NotImplementedException();
    }
}
