using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Xam : HasMasterCasino {
    public Button btn_danhbai;
    public Button btn_boluot;
    public Button btn_baoxam;
    public Button btn_khongbaoxam;
    public TimerXam timer_baoxam;
    private Vector3 posBaoXam = new Vector3(0, 30, 0);

    // Use this for initialization
   void Awake() {
        nUsers = 4;
        GameControl.instance.currentCasino = this;
    }

    //void Start() {
    //    GameControl.instance.StartGame();
    //    UnloadScene(Res.SAM_NAME);
    //}

    public override void resetData() {
        base.resetData();

        tableArrCard1.removeAllCard();
        tableArrCard2.removeAllCard();

        tableArrCard = null;
        //players[0].gameObject.transform.localPosition = new Vector3(0, -115, 0);
        btn_danhbai.gameObject.SetActive(false);
        btn_boluot.gameObject.SetActive(false);

        btn_baoxam.gameObject.SetActive(false);
        timer_baoxam.setEnableClick(false);
        btn_khongbaoxam.gameObject.SetActive(false);
        timer_baoxam.setDeActive();
        timer_baoxam.gameObject.SetActive(false);
        timer_baoxam.gameObject.transform.localPosition = posBaoXam;
        timer_baoxam.gameObject.transform.localScale = new Vector3(1, 1, 1);

        for (int i = 0; i < players.Length; i++) {
            players[i].sp_baoSam.gameObject.SetActive(false);
        }
    }
    public override void onFinishGame(Message message) {
        base.onFinishGame(message);
        btn_danhbai.gameObject.SetActive(false);
        btn_boluot.gameObject.SetActive(false);
        btn_baoxam.gameObject.SetActive(false);
        timer_baoxam.setEnableClick(false);
        btn_khongbaoxam.gameObject.SetActive(false);
        timer_baoxam.setDeActive();
        timer_baoxam.gameObject.SetActive(false);

        for (int i = 0; i < players.Length; i++) {
            players[i].sp_baoSam.gameObject.SetActive(false);
        }
    }
    public override void startTableOk(int[] cardHand, Message msg, string[] nickPlay) {
        base.startTableOk(cardHand, msg, nickPlay);
        tableArrCard1.removeAllCard();
        tableArrCard2.removeAllCard();
        tableArrCard = null;

        players[0].setCardHand(cardHand, true, false, false);

        for (int i = 1; i < players.Length; i++) {
            if (players[i].isPlaying() && !players[i].st_name.Equals("")) {
                addCardHandOtherPlayer(10, i);
            }
            players[i].sp_baoSam.gameObject.SetActive(false);
        }
        SoundManager.instance.startchiabaiAudio ();
    }

    public override void onStartForView(string[] playingName) {
        base.onStartForView(playingName);
        if (players[0].getName().Equals(BaseInfo.gI().mainInfo.nick) && !BaseInfo.gI().isView) {
            lb_Btn_sansang.text = "Xin chờ...";
            btn_sansang.gameObject.SetActive(true);
        }
        tableArrCard1.removeAllCard();
        tableArrCard2.removeAllCard();
        tableArrCard = null;
        for (int i = 1; i < players.Length; i++) {
            if (players[i].isPlaying() && !players[i].st_name.Equals("")) {
                addCardHandOtherPlayer(10, i);
            }
        }
    }

    public override void setTurn(string nick, Message message) {
        base.setTurn(nick, message);
        try {
            if (nick.ToLower().Equals(
                    BaseInfo.gI().mainInfo.nick.ToLower())) {
                btn_danhbai.gameObject.SetActive(true);
                if (tableArrCard != null) {
                    if (tableArrCard.Length > 0) {
                        btn_boluot.gameObject.SetActive(true);
                    } else {
                        btn_boluot.gameObject.SetActive(false);
                    }
                } else {
                    btn_boluot.gameObject.SetActive(false);
                }
            } else {
                btn_danhbai.gameObject.SetActive(false);
                btn_boluot.gameObject.SetActive(false);
            }
            players[getPlayer(nick)].cardHand.setAllMo(false);
            turnName = nick;
            if (turnName.Equals(nickFire)) {
                finishTurn = true;
                tableArrCard1.setArrCard(new int[] { }, false, false, false, -1);
                tableArrCard2.setArrCard(new int[] { }, false, false, false, -1);
                tableArrCard = null;
                btn_boluot.gameObject.SetActive(false);
                for (int i = 0; i < players.Length; i++) {
                    players[i].cardHand.setAllMo(false);
                }
            }

            if (timer_baoxam.isActive()) {
                timer_baoxam.setDeActive();
                btn_baoxam.gameObject.SetActive(false);
                timer_baoxam.setEnableClick(false);
                btn_khongbaoxam.gameObject.SetActive(false);
                setKhongBaoXam();
            }
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }

    public void clickButtonDanh() {
        int[] card = players[0].cardHand.getArrCardChoose();
        if (card == null) {
            //sua
            //gameControl.toast.showToast("Chưa chọn bài!");
        } else {
            SendData.onFireCardTL(card);
            btn_danhbai.gameObject.SetActive(false);
            btn_boluot.gameObject.SetActive(false);
        }
    }
    public void clickButtonBo() {
        SendData.onSendSkipTurn();
        btn_boluot.gameObject.SetActive(false);
        btn_danhbai.gameObject.SetActive(false);
    }
    public override void onFireCardFail() {
        base.onFireCardFail();
        btn_boluot.gameObject.SetActive(true);
        btn_danhbai.gameObject.SetActive(true);
    }
    public override void onNickSkip(string nick, string turnname) {
        base.onNickSkip(nick, turnname);
        players[getPlayer(nick)].cardHand.setAllMo(true);
        setTurn(turnname, null);
    }

    public override void onFireCard(string nick, string turnname, int[] card) {
        base.onFireCard(nick, turnname, card);
        if (players[0].cardHand.getArrCardChoose() != null) {
            if (!TLMNChooseCard.compareCard(
                    players[0].cardHand.getArrCardChoose(), card)) {
                players[0].cardHand.reAddAllCard();
            }
        }
        players[getPlayer(nick)].sp_baoSam.gameObject.SetActive(false);
        if (players[getPlayer(nick)].cardHand.getSoBai() == 1) {
            players[getPlayer(nick)].baoSam();
        }
        // 3 con 2
        if (card.Length == 3) {
            if ((laydu(card[0]) == 2) && (laydu(card[1]) == 2) && (laydu(card[2]) == 2)) {
                SoundManager.instance.start_ThuaDiCung();
                return;
            }
            // sam co
            if ((laydu(card[0]) == laydu(card[1]) && laydu(card[1]) == laydu(card[2]))) {
                SoundManager.instance.start_DODI();
                return;
            }
        }

        // 2 con 2
        if (card.Length == 2) {
            if ((laydu(card[0]) == 2) && (laydu(card[1]) == 2)) {
                SoundManager.instance.start_MAYHABUOI();
            }
        }
        // 1 con 2
        if (card.Length == 1) {
            if ((laydu(card[0]) == 2)) {
                SoundManager.instance.start_HAINE();
            }
        }

        if (card.Length >= 2) {
            SoundManager.instance.start_random();
        }
    }

    private int laydu(int a) {
        return Card.aNumber[a] % 13 + 1;
    }
    public override void onFinishTurn() {
        base.onFinishTurn();
        finishTurn = true;
        tableArrCard1.setArrCard(new int[] { }, false, false, false, -1);
        tableArrCard2.setArrCard(new int[] { }, false, false, false, -1);
        tableArrCard = null;
        btn_boluot.gameObject.SetActive(false);
        for (int i = 0; i < players.Length; i++) {
            players[i].cardHand.setAllMo(false);
        }
    }
    int posbx;

    public override void InfoCardPlayerInTbl(Message message, string turnName, int time, sbyte numP) {
        base.InfoCardPlayerInTbl(message, turnName, time, numP);
        try {
            string[] playingName = new string[numP];
            for (int i = 0; i < numP; i++) {
                playingName[i] = message.reader().ReadUTF();
                players[getPlayer(playingName[i])].setPlaying(true);
                sbyte numCard = message.reader().ReadByte();
                int[] temp;
                temp = new int[numCard];
                for (int j = 0; j < temp.Length; j++) {
                    temp[j] = 52;
                }
                players[getPlayer(playingName[i])].cardHand.setArrCard(temp,
                        false, true, false, getPlayer(playingName[i]));
                players[getPlayer(playingName[i])].cardHand.setVisibleSobai(true);

                players[getPlayer(playingName[i])].cardHand.setSobai(numCard);
            }
            setTurn(turnName, time);
            string nickbaoxam = message.reader().ReadUTF();
            if (!nickbaoxam.Equals("")) {
                setBaoXamToPlayer(getPlayer(nickbaoxam), false);
            }
            sbyte timeBaoXam = message.reader().ReadByte();
            if (timeBaoXam > 0) {
                timer_baoxam.setActive(timeBaoXam);
                timer_baoxam.gameObject.SetActive(true);
                timer_baoxam.gameObject.transform.localPosition = posBaoXam;
                timer_baoxam.gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }

    private void setKhongBaoXam() {
        timer_baoxam.StopAllCoroutines();
        timer_baoxam.transform.DOScale(0, 0.2f).OnComplete(delegate {
            timer_baoxam.gameObject.SetActive(false);
        });
    }

    private void setBaoXamToPlayer(int pos, bool isEff) {
        posbx = pos;
        timer_baoxam.gameObject.SetActive(true);
        if (isEff) {
            //timer_baoxam.transform.DOScale(1f, 1f);
            timer_baoxam.transform.DOLocalMove(players[pos].transform.localPosition, 0.5f);
        } else {
            //timer_baoxam.transform.localScale = new Vector3(1f, 1f, 1f);
            timer_baoxam.transform.localPosition = players[pos].transform.localPosition;
        }
        //players[pos].baoSam();
    }

    public override void onInfome(Message message) {
        base.onInfome(message);
        try {
            isStart = true;
            players[0].setPlaying(true);
            if (posbx == 0 && !timer_baoxam.isActive()) {
                timer_baoxam.gameObject.transform.localPosition = players[0].gameObject.transform.localPosition;
            }

            disableAllBtnTable();
            int sizeCardHand = message.reader().ReadByte();
            int[] cardHand = new int[sizeCardHand];
            for (int i = 0; i < sizeCardHand; i++) {
                cardHand[i] = message.reader().ReadByte();
            }
            players[0].setCardHand(cardHand, false, false, false);
            int sizeCardFire = message.reader().ReadByte();
            if (sizeCardFire > 0) {
                int[] cardFire = new int[sizeCardFire];
                for (int i = 0; i < sizeCardFire; i++) {
                    cardFire[i] = message.reader().ReadByte();
                }
                tableArrCard = cardFire;
                tableArrCard2.setArrCard(cardFire, false);
            }
            string turnName = message.reader().ReadUTF();
            int turnTime = message.reader().ReadInt();
            setTurn(turnName, turnTime);

            if (turnName.ToLower().Equals(
                    BaseInfo.gI().mainInfo.nick.ToLower())) {
                btn_danhbai.gameObject.SetActive(true);
                if (tableArrCard != null) {
                    if (tableArrCard.Length > 0) {
                        btn_boluot.gameObject.SetActive(true);
                    } else {
                        btn_boluot.gameObject.SetActive(false);
                    }
                } else {
                    btn_boluot.gameObject.SetActive(false);
                }
            } else {
                btn_danhbai.gameObject.SetActive(false);
                btn_boluot.gameObject.SetActive(false);
            }
        } catch (Exception e) {
        }
    }

    public void clickButtonBaoXam() {
        SendData.baoxam(1);
        btn_baoxam.gameObject.SetActive(false);
        timer_baoxam.setEnableClick(false);
        btn_khongbaoxam.gameObject.SetActive(false);
        timer_baoxam.setDeActive();
    }

    public void clickButtonKhongBaoXam() {
        SendData.baoxam(0);
        btn_baoxam.gameObject.SetActive(false);
        timer_baoxam.setEnableClick(false);
        btn_khongbaoxam.gameObject.SetActive(false);
    }

    internal void hetGioBaoXam() {
        btn_baoxam.gameObject.SetActive(false);
        timer_baoxam.setEnableClick(false);
        btn_khongbaoxam.gameObject.SetActive(false);
        setKhongBaoXam();
    }
    public override void onHoiBaoXam(sbyte time) {
        base.onHoiBaoXam(time);
        if (players[0].isPlaying()) {
            btn_baoxam.gameObject.SetActive(true);
            btn_khongbaoxam.gameObject.SetActive(true);
            timer_baoxam.setEnableClick(true);
        }

        timer_baoxam.setActive(time);

        timer_baoxam.gameObject.SetActive(true);
        timer_baoxam.transform.localPosition = posBaoXam;
        timer_baoxam.transform.localScale = new Vector3(1, 1, 1);
    }
    public override void onNickBaoXam(string name) {
        base.onNickBaoXam(name);
        btn_baoxam.gameObject.SetActive(false);
        timer_baoxam.setEnableClick(false);
        btn_khongbaoxam.gameObject.SetActive(false);
        timer_baoxam.setDeActive();
        setBaoXamToPlayer(getPlayer(name), true);
        setTurn(name, null);
    }
}
