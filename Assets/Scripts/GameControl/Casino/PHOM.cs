using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using AppConfig;

public class PHOM : HasMasterCasino {
    public Image sp_cardNoc;
    public Text lb_sobai;

    public Button btn_doiluat, btn_danhbai, btn_bocbai,
            btn_xepbai, btn_an, btn_ha_phom;
    public Text lb_danhbai, lb_bocbai, lb_xepbai, lb_anbai, lb_haphom;
    public bool isHaphom;
    public int totalCardNoc = 0;
    public ArrayCard[] cardDrop;

    bool isHaphomRoi = false;

    void Awake() {
        nUsers = 4;
        GameControl.instance.currentCasino = this;
    }

    //void Start() {
    //    GameControl.instance.StartGame();
    //    UnloadScene(Res.PHOM_NAME);
    //}

    public override void setMasterSecond(string master) {
        base.setMasterSecond(master);
        if (!isStart) {
            if (master.Equals(BaseInfo.gI().mainInfo.nick)) {
                btn_doiluat.gameObject.SetActive(true);
            } else {
                btn_doiluat.gameObject.SetActive(false);
            }
        }
    }
    public override void resetData() {
        base.resetData();
        setSoBaiNoc(0);
        isHaphomRoi = false;
        try {
            for (int i = 0; i < players.Length; i++) {
                players[i].cardPhom[0].removeAllCard();
                players[i].cardPhom[1].removeAllCard();
                players[i].cardPhom[2].removeAllCard();
                players[i].card_win[0] = -1;
                players[i].card_win[1] = -1;
                players[i].card_win[2] = -1;
                players[i].allCardPhom = null;
                players[i].setPlaying(false);
            }
            for (int i = 0; i < 4; i++) {
                cardDrop[i].removeAllCard();
            }
            //players[0].gameObject.transform.localPosition = new Vector3(0, -115, 0);
            BaseInfo.gI().isHaPhom = false;
            disableAllBtnTable();

        } catch (Exception e) {
            // TODO: handle exception

        }
    }

    public void demoPhom() {
        int[] card1 = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        int[] card3 = { 1, 2, 3, 4 };
        int[] card41 = { 1, 1, 1 };
        int[] card42 = { 11, 11, 11 };

        for (int i = 0; i < 4; i++) {
            players[i].cardHand.setArrCard(card1, false);
            //for(int j = 0; j < 2; j++) {
            players[i].cardPhom[0].setArrCard(card41, true);
            players[i].cardPhom[1].setArrCard(card42, true);
            //}
            cardDrop[i].setArrCard(card3, true);
        }
        setPositionPhom(true);
    }


    // button
    public void clickButtonDoiLuat() {
        SendData.onChangeRuleTbl();
    }
    public void clickButtonDanhbai() {
        if (players[0].cardHand.getArrCardChoose() != null) {
            if (players[0].cardHand.getArrCardChoose().Length > 0) {
                int cardDrop = players[0].cardHand.getArrCardChoose()[0];
                SendData.onFireCard(cardDrop);
            } else {
                PopupAndLoadingScript.instance.toast.showToast("Quý khách chưa chọn bài");
            }

        } else {
            if (PopupAndLoadingScript.instance.toast != null)
                PopupAndLoadingScript.instance.toast.showToast("Quý khách chưa chọn bài");
        }
    }
    public void clickButtonHaPhom() {
        int[] arr = players[0].cardHand.getArrCardChoose();
        if (arr != null) {
            nguoiDungHaPhom(arr);
            arr = null;
            isHaphomRoi = true;
        } else {
            SendData.onHaPhom(null);
        }
    }

    public void clickButtonDatCuoc() {

    }
    public void clickButtonBocBai() {
        SendData.onGetCardNoc();
        showButton(true, false, false, false, true);
        preCard = 52;
    }


    public void clickButtonAnBai() {
        SendData.onGetCardFromPlayer();
        btn_bocbai.gameObject.SetActive(false);
        btn_an.gameObject.SetActive(false);
        showButton(true, false, false, false, true);
    }
    public void clickButtonXepBai() {
        processXepBai();
    }

    private void processXepBai() {
        if (!players[0].isUserXepbai) {
            sortCards(players[0].cardHand.getArrCardAll());
        }

        if (players[0].cardhand_xepbai != null) {

            if (players[0].id_xepbai < players[0].cardhand_xepbai.Length - 1) {
                players[0].id_xepbai++;
            } else {
                players[0].id_xepbai = 0;
            }
            ((PhomPlayer)players[0])
                    .processXepBai(players[0].cardhand_xepbai[players[0].id_xepbai]);

            players[0].isUserXepbai = true;
        }
    }

    private void sortCards(int[] cardhand) {
        players[0].cardhand_xepbai = RTL.sortPhom1(cardhand,
                RTL.getPhom3Minh(cardhand, players[0].getEatCard()),
                players[0].getEatCard());
        players[0].id_xepbai = players[0].cardhand_xepbai.Length - 1;
        players[0].isUserXepbai = true;
    }
    public override void disableAllBtnTable() {
        base.disableAllBtnTable();
        btn_an.gameObject.SetActive(false);
        btn_batdau.gameObject.SetActive(false);
        btn_doiluat.gameObject.SetActive(false);
        btn_bocbai.gameObject.SetActive(false);
        btn_danhbai.gameObject.SetActive(false);
        btn_ha_phom.gameObject.SetActive(false);
        btn_sansang.gameObject.SetActive(false);
        btn_xepbai.gameObject.SetActive(false);
        if (btn_datcuoc != null)
            btn_datcuoc.gameObject.SetActive(false);
    }
    private void showButton(bool xepbai, bool bocbai, bool anbai,
            bool habai, bool danhbai) {
        btn_xepbai.gameObject.SetActive(xepbai);
        btn_bocbai.gameObject.SetActive(bocbai);
        btn_an.gameObject.SetActive(anbai);
        btn_ha_phom.gameObject.SetActive(habai);
        btn_danhbai.gameObject.SetActive(danhbai);

        //if (xepbai) {
        //    setDisable(btn_xepbai, false);
        //}
        //else {
        //    setDisable(btn_xepbai, true);
        //}
        //if (bocbai) {
        //    setDisable(btn_bocbai, false);
        //}
        //else {
        //    setDisable(btn_bocbai, true);

        //}
        //if (anbai) {
        //    setDisable(btn_an, false);
        //}
        //else {
        //    setDisable(btn_an, true);
        //}
        //if (habai) {
        //    setDisable(btn_ha_phom, false);
        //}
        //else {
        //    setDisable(btn_ha_phom, true);
        //}
        //if (danhbai) {
        //    setDisable(btn_danhbai, false);
        //}
        //else {
        //    setDisable(btn_danhbai, true);
        //}


    }
    private void nguoiDungHaPhom(int[] arr) {
        int size = ((PhomPlayer)players[0]).getEatCard().Length;
        int[] eatArr = size > 0 ? new int[((PhomPlayer)players[0])
                .getEatCard().Length] : null;
        for (int i = 0; i < size; i++) {
            eatArr[i] = ((PhomPlayer)players[0]).getEatCard()[i];
        }
        int[][] phom = RTL.checkPhom(arr, eatArr);
        if (phom == null) {
            SendData.onHaPhom(null);
        } else {
            SendData.onHaPhom(phom);
            btn_ha_phom.gameObject.SetActive(false);
        }
        eatArr = null;
    }
    private void setSoBaiNoc(int sobai) {
        totalCardNoc = sobai;

        if (sobai <= 0) {
            sp_cardNoc.gameObject.SetActive(false);
            lb_sobai.text = "";
        } else {
            sp_cardNoc.gameObject.SetActive(true);
            lb_sobai.text = sobai + "";
        }
    }
    public override void startTableOk(int[] cardHand, Message msg, string[] nickPlay) {
        base.startTableOk(cardHand, msg, nickPlay);
        btn_doiluat.gameObject.SetActive(false);
        isHaphom = false;
        isHaphomRoi = false;
        setPositionPhom(false);
        try {
            for (int i = 0; i < players.Length; i++) {
                if (i != 0) {
                    players[i].cardHand.maxW = cardHand.Length * 20;
                }
                players[i].cardPhom[0].removeAllCard();
                players[i].cardPhom[1].removeAllCard();
                players[i].cardPhom[2].removeAllCard();
                players[i].card_win[0] = -1;
                players[i].card_win[1] = -1;
                players[i].card_win[2] = -1;
                players[i].allCardPhom = null;
                players[i].isUserXepbai = false;
            }
            players[0].cardhand_xepbai = null;
            for (int i = 0; i < 4; i++) {
                cardDrop[i].removeAllCard();

            }
            BaseInfo.gI().isHaPhom = false;
            //players[0].gameObject.transform.localPosition = new Vector3(xPlay, -115, 0);
            players[0].setCardHand(
                    RTL.sortPhom(cardHand,
                            RTL.getPhom3(cardHand, players[0].card_win)), true,
                    false, false);
            players[0].isUserXepbai = false;
            totalCardNoc = getTotalPlayerPlaying() * 4 - 1;
            setSoBaiNoc(totalCardNoc);

            for (int i = 1; i < players.Length; i++) {
                if (players[i].isPlaying()) {
                    addCardHandOtherPlayer(9, i);
                }
            }
            SoundManager.instance.startchiabaiAudio();
        } catch (Exception e) {
            // TODO: handle exception

        }
        setActiveBtn(btn_xepbai, false);
    }
    public override void onStartForView(string[] playingName) {
        base.onStartForView(playingName);
        if (players[0].getName().Equals(BaseInfo.gI().mainInfo.nick) && !BaseInfo.gI().isView) {
            lb_Btn_sansang.text = "Xin chờ...";
            btn_sansang.gameObject.SetActive(true);
        }

        if (players[0].isMaster()) {
            btn_batdau.gameObject.SetActive(false);
            if (btn_datcuoc != null)
                btn_datcuoc.gameObject.SetActive(false);
        }

        btn_xepbai.gameObject.SetActive(false);
        btn_an.gameObject.SetActive(false);
        btn_bocbai.gameObject.SetActive(false);
        btn_danhbai.gameObject.SetActive(false);
        btn_ha_phom.gameObject.SetActive(false);
        BaseInfo.gI().isHaPhom = false;
        totalCardNoc = getTotalPlayerPlaying() * 4 - 1;
        setSoBaiNoc(totalCardNoc);
        for (int i = 0; i < players.Length; i++) {
            if (players[i].isPlaying()) {
                addCardHandOtherPlayer(9, i);
            }
        }
        for (int i = 0; i < players.Length; i++) {
            players[i].cardPhom[0].removeAllCard();
            players[i].cardPhom[1].removeAllCard();
            players[i].cardPhom[2].removeAllCard();
            players[i].card_win[0] = -1;
            players[i].card_win[1] = -1;
            players[i].card_win[2] = -1;
            players[i].allCardPhom = null;
        }
        for (int i = 0; i < 4; i++) {
            cardDrop[i].removeAllCard();
        }
    }
    public override void onFireCard(string nick, string turnname, int[] card) {
        SoundManager.instance.startchiabaiAudio();
        //SoundManager.Get().startAudio(SoundManager.AUDIO_TYPE.CHIABAI);
        int c = card[0];
        nickFire = nick;
        preCard = card[0];
        finishTurn = false;
        int pos = getPlayer(nick);
        if (pos == 0) {
            players[0].isUserXepbai = false;
        }
        for (int i = 0; i < cardDrop[pos].getSize(); i++) {
            cardDrop[pos].getCardbyPos(i).setMo(true);
        }

        players[pos].addToCardFromCard(players[pos].cardHand, cardDrop[pos], c,
                false);
        if (cardDrop[pos].getSize() >= 1) {
            cardDrop[pos].getCardbyPos(cardDrop[pos].getSize() - 1).setMo(false);
        }
        setTurn(turnname, null);
    }
    public override void setTurn(string nick, Message message) {
        base.setTurn(nick, message);
        bool xepbai = true, bocbai = false, anbai = false, haphom = false, danhbai = false;
        anbai = false;
        bocbai = true;
        if (!isStart) {
            return;
        }

        if (nick.ToLower()
                .Equals(BaseInfo.gI().mainInfo.nick.ToLower())) {
            if (players[0].cardHand.getSize() < 10) {
                bocbai = true;
                danhbai = false;
                // muiTenBoc.setVisible(true);
            } else {
                bocbai = false;
                danhbai = true;
                // muiTenBoc.setVisible(false);
            }
            if (cardDrop[0].getSize() == 4) {
                // btn_ha_phom.setDisabled(true);
                BaseInfo.gI().isHaPhom = true;
                haphom = true;
                danhbai = false;
                anbai = false;
                bocbai = false;
                xepbai = true;
                isHaphom = true;
            } else {
                int[][] cardPhoms = RTL.getCardFromPlayer(
                        players[0].cardHand.getArrCardAll(),
                        players[0].getEatCard(), preCard);

                if (preCard != -1 && cardPhoms != null) {
                    // cho phep an card
                    int arrPhom = -1;
                    for (int i = 0; i < cardPhoms.Length; i++) {
                        for (int j = 0; j < cardPhoms[i].Length; j++) {
                            if (preCard == cardPhoms[i][j]) {
                                arrPhom = i;
                                break;
                            }
                        }
                        if (arrPhom != -1) {
                            break;
                        }
                    }

                    if (arrPhom != -1) {

                        for (int j = 0; j < cardPhoms[arrPhom].Length; j++) {
                            Card card = players[0].cardHand
                                    .getCardbyID(cardPhoms[arrPhom][j]);
                            if (card != null) {
                                card.setChoose(true);
                            }
                        }
                    }

                    for (int i = 0; i < 4; i++) {
                        if (CheckInArr(preCard, cardDrop[i].getArrCardAll())) {
                            break;
                        }
                    }
                    anbai = true;
                }
            }
        } else {
            bocbai = false;
            danhbai = false;
        }

        if (players[0].isPlaying() && !BaseInfo.gI().isView) {
            if (isHaphomRoi) {
                showButton(xepbai, bocbai, anbai, haphom, danhbai);
            } else
                showButton(xepbai, bocbai, anbai, false, danhbai);
        }
    }
    public override void onGetCardNoc(string nick, int card) {
        base.onGetCardNoc(nick, card);
        int idpl = getPlayer(nick);
        //SoundManager.Get().startAudio(SoundManager.AUDIO_TYPE.CHIABAI);
        if (idpl != -1) {
            if (players[idpl].getName().Equals(BaseInfo.gI().mainInfo.nick)) {
                players[idpl].addToCardHand(card, true);
                players[idpl].isUserXepbai = false;
            } else {
                players[idpl].addToCardHand(card, false);
            }
            totalCardNoc--;
            setSoBaiNoc(totalCardNoc);
        }
    }
    public override void onFireCardFail() {
        base.onFireCardFail();
        if (!BaseInfo.gI().isView) {
            setActiveBtn(btn_danhbai, true);
        }
    }
    public override void onFinishGame(Message message) {
        base.onFinishGame(message);
        btn_xepbai.gameObject.SetActive(false);
        btn_an.gameObject.SetActive(false);
        btn_bocbai.gameObject.SetActive(false);
        btn_danhbai.gameObject.SetActive(false);
        btn_ha_phom.gameObject.SetActive(false);

        for (int i = 0; i < 4; i++) {
            if (players[i].getName().Length > 0) {
                players[i].setInfo("" + players[i].diem);
            }
            cardDrop[i].removeAllCard();
        }
        prevPlayer = -1;
        preCard = -1;
        if (BaseInfo.gI().isView) {
            btn_sansang.gameObject.SetActive(false);
        }
    }
    public override void onEatCardSuccess(string from, string to, int card) {
        base.onEatCardSuccess(from, to, card);
        SoundManager.instance.startAnbaiAudio();
        int id1 = getPlayer(from), id2 = getPlayer(to);
        if (id1 == -1 || id2 == -1) {
            // activity.showToast("Không hợp lệ");
        } else {
            if (card != -1) {

                players[id1].onEatCard(card);
                if (id1 == 0) {
                    players[0].isUserXepbai = false;
                    players[0].addToCardHand(card, true);
                    int[] cardnew = RTL.sortPhom(players[0].cardHand
                            .getArrCardAll(), RTL.getPhom3(
                            players[0].cardHand.getArrCardAll(),
                            players[0].getEatCard()));
                    players[0].setCardHand(cardnew, players[0].getEatCard(), false, false, false);
                    //processXepBai();
                    if (!BaseInfo.gI().isView) {
                        setActiveBtn(btn_danhbai, true);
                    }
                } else {

                }
                try {
                    for (int i = 0; i < players.Length; i++) {
                        if (players[i].isPlaying() && players[i].pos == id2) {
                            cardDrop[i].removeCardByID(card);
                        }
                    }
                } catch (Exception e) {

                }
            }
        }
    }
    public override void onAttachCard(string from, string to, int[] arrayPhom, int[] cardsgui) {
        base.onAttachCard(from, to, arrayPhom, cardsgui);
        int pos = getPlayer(to);
        int posfrom = getPlayer(from);
        if (pos != -1) {
            players[pos].setCardPhom(arrayPhom, false);
            if (from.Equals(BaseInfo.gI().mainInfo.nick)) {

                for (int i = 0; i < cardsgui.Length; i++) {
                    players[posfrom].cardHand.removeCardByID(cardsgui[i]);
                }
            }
        }
    }
    public override void onDropPhomSuccess(string nick, int[] arrayPhom) {
        base.onDropPhomSuccess(nick, arrayPhom);
        SoundManager.instance.startHaphomAudio();
        int pos = getPlayer(nick);
        if (pos != -1) {
            int size;
            int[] temp;
            if (players[pos].getAllCardPhom() == null) {
                size = arrayPhom.Length;
                temp = new int[size];
                for (int i = 0; i < arrayPhom.Length; i++) {
                    temp[i] = arrayPhom[i];

                }
            } else {

                size = players[pos].getAllCardPhom().Length + arrayPhom.Length;
                temp = new int[size];
                for (int i = 0; i < players[pos].getAllCardPhom().Length; i++) {
                    temp[i] = players[pos].getAllCardPhom()[i];
                }
                for (int i = players[pos].getAllCardPhom().Length; i < arrayPhom.Length
                        + players[pos].getAllCardPhom().Length; i++) {
                    temp[i] = arrayPhom[i
                            - players[pos].getAllCardPhom().Length];

                }
            }
            players[pos].setCardPhom(temp, true);
            players[pos].cardHand.setAllMo(false);
            if (pos == 0) {
                for (int i = 0; i < arrayPhom.Length; i++) {
                    players[pos].cardHand.removeCardByID(arrayPhom[i]);
                }
            }
            try {
                for (int i = 0; i < players[pos].card_win.Length; i++) {
                    players[pos].card_win[i] = -1;
                }

            } catch (Exception e) {
                // TODO: handle exception
            }
            if (nick.Equals(BaseInfo.gI().mainInfo.nick)) {
                disableAllBtnTable();
                setPositionPhom(true);
            }
        }
    }
    public override void onBalanceCard(string from, string to, int card) {
        base.onBalanceCard(from, to, card);
        try {

            int id1 = getPlayer(from), id2 = getPlayer(to);
            if (id1 == -1 || id2 == -1) {

            } else {
                cardDrop[id1].removeCardByID(card);
                cardDrop[id2].addCard(card);
            }
        } catch (Exception e) {

        }
    }
    public override void InfoCardPlayerInTbl(Message message, string turnName, int time, sbyte numP) {
        base.InfoCardPlayerInTbl(message, turnName, time, numP);
        try {
            // turn name
            for (int i = 0; i < numP; i++) {
                int ids = getPlayer(message.reader().ReadUTF());

                players[ids].setPlaying(true);

                int[] card = new int[9];
                for (int j = 0; j < card.Length; j++) {
                    card[j] = 52;
                }
                players[ids].cardHand.setArrCard(card, false, true, false, ids);
                players[ids].cardHand.setSobai(0);

                int sizeWin = message.reader().ReadInt();
                for (int j = 0; j < sizeWin; j++) {
                    players[ids].onEatCard(message.reader().ReadInt());
                }
                int sizeRub = message.reader().ReadInt();
                for (int j = 0; j < sizeRub; j++) {
                    this.cardDrop[ids].addCard(message.reader().ReadInt());
                }

                int sizePhom = message.reader().ReadByte();
                if (sizePhom > 0) {
                    int[] phom = new int[sizePhom];
                    for (int j = 0; j < sizePhom; j++) {
                        phom[j] = message.reader().ReadByte();
                    }
                    players[ids].setCardPhom(phom, true);
                }
                setSoBaiNoc(0);
            }
            int numPlaying = 0;
            int numCR = 0;

            for (int i = 0; i < players.Length; i++) {
                if (players[i] != null && players[i].isPlaying()) {
                    numPlaying++;
                    numCR += this.cardDrop[i].getSize();
                }
            }
            setSoBaiNoc(numPlaying * 4 - (numCR));
            setTurn(turnName, time);
        } catch (Exception e) {
            // TODO: handle exception
        }
    }
    public override void onInfome(Message message) {
        try {
            Debug.LogError("InfoMe");
            bool isXepBai = false;
            bool isBocBai = false;
            bool isAnBai = false;
            bool isHaBai = false;
            bool isDanhBai = false;
            disableAllBtnTable();
            isStart = true;
            players[0].setPlaying(true);
            //players[0].gameObject.transform.localPosition = new Vector3(xPlay, -115, 0);

            int sizeCardHand = message.reader().ReadByte();
            int[] cardHand = new int[sizeCardHand];
            for (int i = 0; i < sizeCardHand; i++) {
                cardHand[i] = message.reader().ReadByte();
            }
            players[0].setCardHand(cardHand, false, false, false);

            // btn_xepbai.setDisabled(true);
            // btn_sansang.setDisabled(false);

            btn_xepbai.gameObject.SetActive(true);
            isXepBai = true;

            int sizeCardRub = message.reader().ReadByte();
            for (int j = 0; j < sizeCardRub; j++) {
                this.cardDrop[0].addCard(message.reader().ReadByte());
            }
            setSoBaiNoc(totalCardNoc + 4 - sizeCardRub);
            int sizeWin = message.reader().ReadByte();
            for (int j = 0; j < sizeWin; j++) {
                players[0].onEatCard(message.reader().ReadByte());
            }

            int sizePhom = message.reader().ReadByte();
            if (sizePhom > 0) {
                int[] phom = new int[sizePhom];
                for (int j = 0; j < sizePhom; j++) {
                    phom[j] = message.reader().ReadByte();
                }
                players[0].setCardPhom(phom, true);
            }

            String turnvName = message.reader().ReadUTF();
            int turnvTime = message.reader().ReadInt();
            setTurn(turnvName, turnvTime);
            if (turnName.Equals(BaseInfo.gI().mainInfo.nick)) {
                if (cardDrop[0].getSize() == 4) {
                    BaseInfo.gI().isHaPhom = true;
                    isHaBai = true;
                    // btn_ha_phom.setDisabled(true);
                } else {
                    if (players[0].cardHand.getSize() < 10) {
                        // setVisibleButtonBoc(true);
                        isBocBai = true;
                    } else {
                        // btn_danhbai.setDisabled(true);
                        isDanhBai = true;
                    }
                    int[][] cardPhoms = RTL.getCardFromPlayer(
                            players[0].cardHand.getArrCardAll(),
                            players[0].getEatCard(), preCard);
                    if (preCard != -1 && cardPhoms != null) {// cho
                        // phep
                        // an
                        // card
                        int arrPhom = -1;
                        for (int i = 0; i < cardPhoms.Length; i++) {
                            for (int j = 0; j < cardPhoms[i].Length; j++) {
                                if (preCard == cardPhoms[i][j]) {
                                    arrPhom = i;
                                    break;
                                }
                            }
                            if (arrPhom != -1) {
                                break;
                            }
                        }
                        for (int j = 0; j < cardPhoms[arrPhom].Length; j++) {
                            Card card = players[0].cardHand
                                    .getCardbyID(cardPhoms[arrPhom][j]);
                            if (card != null) {
                                card.setChoose(true);
                            }
                        }

                        isAnBai = true;
                        isBocBai = true;
                    } else {
                    }
                }
            } else {
                // disableAllBtnTable();
            }
            showButton(isXepBai, isBocBai, isAnBai, isHaBai, isDanhBai);
        } catch (Exception e) {
            // TODO: handle exception
        }
    }

    public override void onJoinTableSuccess(string master) {
        base.onJoinTableSuccess(master);
        if (BaseInfo.gI().mainInfo.nick.Equals(master)) {
            btn_doiluat.gameObject.SetActive(true);
        } else {
            btn_doiluat.gameObject.SetActive(false);
        }
    }
    public override void allCardFinish(string nick, int[] card) {
        card = RTL.sort(card);
        int poss = getPlayer(nick);
        if (players[poss].isPlaying()) {
            players[poss].diem = RTL.getScoreFinal(card);
            if (poss != 0) {
                players[poss].cardHand.maxW = card.Length * 20;
            }
            players[poss].setCardHandInFinishGame(RTL.sortPhom(card,
                                RTL.getPhom3(card, players[poss].getEatCard())));
        }

        for (int i = 0; i < 4; i++) {
            cardDrop[i].removeAllCard();
        }
    }

    private void setActiveBtn(Button button, bool isActive) {
        //UILabel lb = null;
        //if (button == btn_danhbai) {
        //    lb = lb_danhbai;
        //}
        //else if (button == btn_bocbai) {
        //    lb = lb_bocbai;
        //}
        //else if (button == btn_xepbai) {
        //    lb = lb_xepbai;
        //}
        //else if (button == btn_an) {
        //    lb = lb_anbai;
        //}
        //else if (button == btn_ha_phom) {
        //    lb = lb_haphom;
        //}

        button.gameObject.SetActive(isActive);
        //if (isDisable) {
        //    if (lb != null) {
        //        lb.color = Color.gray;
        //    }
        //    button.state = UIButtonColor.State.Disabled;
        //}
        //else {
        //    if (lb != null) {
        //        lb.color = Color.white;
        //    }
        //    button.state = UIButtonColor.State.Normal;
        //}

        //button.enabled = !isDisable;
    }

    void setPositionPhom(bool isHa) {
        //Vector3 vt = players[0].cardPhom[0].transform.localPosition;
        //Vector3 vt2 = players[0].cardPhom[1].transform.localPosition;
        //Vector3 vt3 = players[0].cardPhom[2].transform.localPosition;

        //if (isHa == false) {
        //    vt.x = -25;
        //    vt2.x = 0;
        //    vt3.x = 25;
        //} else {
        //    vt.x = -25;
        //    vt2.x = -25 + players[0].cardPhom[0].maxW + 20;
        //    vt3.x = 0 + players[0].cardPhom[0].maxW + players[0].cardPhom[1].maxW + 40;
        //}

        //players[0].cardPhom[0].transform.localPosition = vt;
        //players[0].cardPhom[1].transform.localPosition = vt2;
        //players[0].cardPhom[2].transform.localPosition = vt3;
    }

    private List<int> phomha = new List<int>();
    public override void onPhomha(Message message) {
        phomha.Clear();
        try {
            int len = message.reader().ReadInt();
            //if (len > 0) {
            btn_ha_phom.gameObject.SetActive(true);
            //} else {
            //    btn_ha_phom.gameObject.SetActive(false);
            //}
            for (int i = 0; i < len; i++) {
                int len2 = message.reader().ReadInt();
                for (int j = 0; j < len2; j++) {
                    int x = message.reader().ReadInt();
                    phomha.Add(x);
                }
            }
        } catch (Exception e) {
            Debug.LogException(e);
        }

        if (phomha.Count > 0) {
            for (int i = 0; i < phomha.Count; i++) {
                Card cardNhac = players[0].cardHand.getCardbyID(phomha[i]);
                if (cardNhac != null) {
                    cardNhac.setChoose(true);
                }

            }
        }
    }

    //public void BackToRoom()
    //{
    //    PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", ClientConfig.Language.GetText("ingame_exit"), delegate
    //    {
    //        PopupAndLoadingScript.instance.ShowLoading();
    //        if (BaseInfo.gI().isView)
    //        {
    //            SendData.onOutView();
    //        }
    //        else
    //        {
    //            SendData.onOutTable();
    //        }
    //        //BaseInfo.gI().regOuTable = true;
    //    });
    //}
}
