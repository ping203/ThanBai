using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class Lieng : BaseToCasino {
    public TimerLieng timerWaiting;
    // public UISprite girl;

    new void Awake() {
        nUsers = 5;
        GameControl.instance.currentCasino = this;

    }

    //void Start() {
    //    UnloadScene(Res.LIENG_NAME);
    //    StartCoroutine(StartGame());
    //}

    //IEnumerator StartGame() {
    //    yield return new WaitForEndOfFrame();
    //    GameControl.instance.StartGame();
    //}

    public override void onTimeAuToStart(sbyte time)
    {
        if (timerWaiting != null)
        {
            timerWaiting.setActive(time);
        }
    }

    public override void resetData()
    {
        base.resetData();
        players[0].cardHand3Cay.removeAllCard();
        players[0].cardHand.removeAllCard();
    }

    public override void setTurn(string nick, Message message)
    {
        base.setTurn(nick, message);
        try
        {
            long moneyCuoc = message.reader().ReadLong();
            if (nick.Equals(BaseInfo.gI().mainInfo.nick))
            {
                if (players[0].getFolowMoney() == 0)
                {
                    SendData.onAccepFollow();
                }
                else
                {
                    baseSetturn(moneyCuoc);
                }

            }
            else
            {
                if (players[0].isPlaying())
                {
                    showAllButton(true, true, true);
                }
                else
                {
                    showAllButton(true, false, false);
                }
                setMoneyCuoc(moneyCuoc);
            }
        }
        catch (Exception e)
        {
            // TODO: handle exception
            Debug.LogException(e);
        }
    }

    public void onPaintDiemOtherPlayer()
    {

        for (int i = 1; i < players.Length; i++)
        {
            if (players[i].getName().Length > 0)
            {
                if (players[i].cardHand.getArrCardObj()[0].getId() != 52)
                {
                    players[i].setInfo(BaseInfo.tinhDiem(players[i].cardHand.getArrCardAll()));
                }
            }
        }
    }
    public override void calculDiem()
    {
        if (players[0].cardHand3Cay.getArrCardObj()[0].getId() != 52)
        {
            players[0].setInfo(BaseInfo.tinhDiem(players[0].cardHand3Cay.getArrCardAll()));
        }
    }
    public override void onFinishGame(Message message)
    {
        base.onFinishGame(message);
        if (players[0].getName().Equals(BaseInfo.gI().mainInfo.nick))
        {
            players[0].setInfo(BaseInfo.tinhDiem(players[0].cardHand3Cay.getArrCardAll()));
        }
        else
        {
            players[0].setInfo(BaseInfo.tinhDiem(players[0].cardHand.getArrCardAll()));
        }
        onPaintDiemOtherPlayer();
        //players[0].cardHand.removeAllCard();
    }

    public override void startTableOk(int[] cardHand, Message msg, string[] nickPlay)
    {
        base.startTableOk(cardHand, msg, nickPlay);
        //players[0].setCardHand(new int[] { 52, 52, 52 }, true, false, false);
        players[0].setCardHand(cardHand, true, false, false);
        players[0].cardHand3Cay.setArrCard(cardHand, true, false, false, 0);
        players[0].setInfo(BaseInfo.tinhDiem(cardHand));
        players[0].setMoneyChip(BaseInfo.gI().moneyTable);
        for (int i = 1; i < players.Length; i++)
        {
            if (players[i].isPlaying())
            {
                players[i].setCardHand(new int[] { 52, 52, 52 }, true, false,
                        false);
                players[i].setMoneyChip(BaseInfo.gI().moneyTable);
            }

        }
        turntime = 0;
    }
    public override void onStartForView(string[] playingName)
    {
        base.onStartForView(playingName);
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].isPlaying())
            {
                players[i].setCardHand(new int[] { 52, 52, 52 }, true, false,
                        false);
            }
        }
        turntime = 0;
    }

    public override void InfoCardPlayerInTbl(Message message, string turnName, int time, sbyte numP)
    {
        base.InfoCardPlayerInTbl(message, turnName, time, numP);
        try
        {
            string[] playingName = new string[numP];
            for (int i = 0; i < numP; i++)
            {
                playingName[i] = message.reader().ReadUTF();
                sbyte isSkip = message.reader().ReadByte(); // = 0 Skip.
                long chips = message.reader().ReadLong();
                players[getPlayer(playingName[i])].setMoneyChip(chips);
                players[getPlayer(playingName[i])].setPlaying(true);
                players[getPlayer(playingName[i])].cardHand.setArrCard(
                        new int[] { 52, 52, 52 }, false, false, false, getPlayer(playingName[i]));
                if (isSkip == 0)
                {
                    players[getPlayer(playingName[i])].cardHand.setAllMo(true);
                }
            }
            SoundManager.instance.startchiabaiAudio();
            setTurn(turnName, time);
        }
        catch (Exception e)
        {
            // TODO: handle exception
            Debug.LogException(e);
        }
    }

    public override void onInfome(Message message)
    {
        base.onInfome(message);
        try
        {
            isStart = true;
            players[0].setPlaying(true);
            int sizeCardHand = message.reader().ReadByte();
            int[] cardHand = new int[sizeCardHand];
            for (int j = 0; j < sizeCardHand; j++)
            {
                cardHand[j] = message.reader().ReadByte();
            }
            players[0].cardHand.setArrCard(cardHand, false);

            bool upBai = message.reader().ReadBoolean();
            if (upBai)
            {
                players[0].cardHand.setAllMo(true);
            }
            String turnvName = message.reader().ReadUTF();
            int turnvTime = message.reader().ReadInt();
            long money = message.reader().ReadLong();
            long moneyC = message.reader().ReadLong();
            long mIP = message.reader().ReadLong();
            players[0].setMoneyChip(moneyC);
            setTurn(turnvName, turnvTime);
            if (turnvName.Equals(BaseInfo.gI().mainInfo.nick))
            {
                baseSetturn(money);
            }
            else
            {
                if (players[0].isPlaying())
                {
                    showAllButton(true, true, true);
                }
                else
                {
                    showAllButton(true, false, false);
                }
                setMoneyCuoc(money);
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }
    }

    public GameObject help;

    public void showHelp()
    {
        help.SetActive(true);
    }
}
