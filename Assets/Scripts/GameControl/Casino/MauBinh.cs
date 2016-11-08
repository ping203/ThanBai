using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using DG.Tweening;
using UnityEngine.EventSystems;

public class MauBinh : BaseCasino, IHasChanged {
    public static MauBinh instance;
    public Text lb_time_mb;
    public Image sp_chi1, sp_chi2, sp_chi3;
    public Image sp_lung;
    //public Sprite[] list_sp_chi;
    public Image sp_chi;
    public Button btn_xong, btn_xeplai;
    public GameObject go_chi;

    public TimerLieng timerWaiting;

    int turntimeMB;
    long timeReceiveTurnMB;
    //public ListTypeCard listTypeCard;
    Vector3 vt_chi_1;// = new Vector3(220, 135, 0);
    Vector3 vt_chi_2;// = new Vector3(220, 60, 0);
    Vector3 vt_chi_3;// = new Vector3(220, -15, 0);

    void Awake() {
        instance = this;
        GameControl.instance.currentCasino = instance;
    }

    void Start() {
        //UnloadScene(Res.MAUBINH_NAME);
        //GameControl.instance.StartGame();
        vt_chi_3 = players[0].cardMauBinh[2].transform.localPosition;
        vt_chi_2 = players[0].cardMauBinh[1].transform.localPosition;
        vt_chi_1 = players[0].cardMauBinh[0].transform.localPosition;
    }

    void OnEnable() {
        HasEndDrag();
        go_chi.SetActive(false);
    }

    public override void onTimeAuToStart(sbyte time) {
        if (players.Length > 1) {
            if (timerWaiting != null) {
                timerWaiting.setActive(time);
            }
        }
    }

    public override void onJoinTableSuccess(string master) {
        for (int i = 0; i < nUsers; i++) {
            if (!players[i].isSit()) {
                players[i].setInvite(true);
            } else {
                players[i].setInvite(false);
            }
        }
        masterID = "";
        if (toggleLock != null)
            toggleLock.gameObject.SetActive(false);

    }

    public void demoMB() {
        int[] card41 = { 1, 1, 1 };
        int[] card42 = { 11, 11, 11, 11, 11 };

        for (int i = 0; i < 4; i++) {
            players[i].cardMauBinh[0].setArrCard(card41, true);
            players[i].cardMauBinh[1].setArrCard(card42, true);
            players[i].cardMauBinh[2].setArrCard(card42, true);
        }
    }

    public override void setMasterSecond(string master) {
        for (int i = 0; i < nUsers; i++) {
            if (!players[i].isSit()) {
                players[i].setInvite(true);
            } else {
                players[i].setInvite(false);
            }
        }
        masterID = "";
        if (toggleLock != null) {
            toggleLock.gameObject.SetActive(false);
        }
    }

    public void clickButtonXepXong() {
        guiFinalMauBinh();
        btn_xong.gameObject.SetActive(false);
        btn_xeplai.gameObject.SetActive(true);
        go_chi.SetActive(false);
    }

    public void clickButtonXepLai() {
        isTouchMB = true;
        setTouchCardMB(true);
        btn_xong.gameObject.SetActive(true);
        btn_xeplai.gameObject.SetActive(false);
        players[0].sp_xepXong.gameObject.SetActive(false);
        reSetTypeCard0();

        go_chi.SetActive(true);
        action_card_up();
    }

    bool isSendCard;
    private void guiFinalMauBinh() {
        if (!players[0].cardMauBinh[0].getCardbyPos(2).gameObject.activeInHierarchy
                || players[0].cardMauBinh[0].getCardbyPos(2).getId() > 51) {
            return;
        }
        int[] cardFinal = new int[13];
        int[] chi1 = new int[5];
        int[] chi2 = new int[5];
        int[] chi3 = new int[3];

        chi1 = PokerCard.sortValue(players[0].cardMauBinh[2]
                .getArrayCardAllTrue());
        chi2 = PokerCard.sortValue(players[0].cardMauBinh[1]
                .getArrayCardAllTrue());
        chi3 = PokerCard.sortValue(players[0].cardMauBinh[0]
                .getArrayCardAllTrue());

        for (int i = 0; i < chi1.Length; i++) {
            cardFinal[i] = chi1[i];
        }

        for (int i = 5; i < chi2.Length + 5; i++) {
            cardFinal[i] = chi2[i - 5];
        }

        for (int i = 10; i < chi3.Length + 10; i++) {
            cardFinal[i] = chi3[i - 10];
        }

        isSendCard = true;
        SendData.onFinalMauBinh(cardFinal);
        btn_xong.gameObject.SetActive(false);
        btn_xeplai.gameObject.SetActive(true);
        action_card_down();

        isTouchMB = false;
        setTouchCardMB(false);
        card_show_mb.gameObject.SetActive(false);
    }

    const float height_down = 30;
    void action_card_down() {
        players[0].cardMauBinh[0].resetPostionCard(120);
        players[0].cardMauBinh[1].resetPostionCard(200);
        players[0].cardMauBinh[2].resetPostionCard(200);

        players[0].cardMauBinh[2].transform.localPosition = vt_chi_3;
        players[0].cardMauBinh[1].transform.localPosition = vt_chi_2;
        players[0].cardMauBinh[0].transform.localPosition = vt_chi_1;

        setPositionCardHand(players[0].cardMauBinh[0].transform, -height_down * 2);
        setPositionCardHand(players[0].cardMauBinh[1].transform, -height_down);
    }
    void action_card_up() {
        players[0].cardMauBinh[0].resetPostionCard(150);
        players[0].cardMauBinh[1].resetPostionCard(250);
        players[0].cardMauBinh[2].resetPostionCard(250);

        setPositionCardHand(players[0].cardMauBinh[0].transform, height_down * 2);
        setPositionCardHand(players[0].cardMauBinh[1].transform, height_down);
    }

    public override void resetData() {
        base.resetData();
        resetMB();
    }

    public override void startTableOk(int[] cardHand, Message msg, string[] nickPlay) {
        base.startTableOk(cardHand, msg, nickPlay);
        isSendCard = false;
        btn_xeplai.gameObject.SetActive(false);
        timerWaiting.gameObject.SetActive(false);
        resetMB();
        try {
            turntimeMB = msg.reader().ReadByte();
        } catch (Exception e) {
            // TODO: handle exception
        }
        turntime = 0;
        timeReceiveTurnMB = GetCurrentMilli();

        lb_time_mb.text = ("" + turntimeMB);
        lb_time_mb.gameObject.SetActive(false);
        if (players.Length > 1)
            lb_time_mb.gameObject.SetActive(true);

        int[] card = new int[13];
        for (int i = 0; i < card.Length; i++) {
            card[i] = 52;
        }

        for (int i = 1; i < players.Length; i++) {
            if (players[i].isPlaying() && !players[i].getName().Equals("")) {
                StartCoroutine(setCardHand(card, i, true));
            }
        }

        btn_xong.gameObject.SetActive(false);
        StartCoroutine(setCardHand(cardHand, 0, true));
        StartCoroutine(delayXepXong());

        isTouchMB = true;
        setTouchCardMB(true);
    }

    void setTouchCardMB(bool isTouch) {
        for (int i = 0; i < players[0].cardMauBinh.Length; i++) {
            players[0].cardMauBinh[i].setTouch(isTouch);
        }
    }

    IEnumerator delayXepXong() {
        yield return new WaitForSeconds(1.6f);
        btn_xong.gameObject.SetActive(true);
    }

    public void setPositionCardHand(Transform arr, float height) {
        Vector3 vt = arr.localPosition;
        arr.DOLocalMoveY(vt.y + height, 0.2f);
    }

    IEnumerator setCardHand(int[] cardHand, int pos, bool isDearling) {
        players[pos].cardMauBinh[0].setArrCard(new int[] { }, false);
        players[pos].cardMauBinh[1].setArrCard(new int[] { }, false);
        players[pos].cardMauBinh[2].setArrCard(new int[] { }, false);

        int[] cardChi1 = new int[] { cardHand[0], cardHand[1], cardHand[2], cardHand[3], cardHand[4] };
        int[] cardChi2 = new int[] { cardHand[5], cardHand[6], cardHand[7], cardHand[8], cardHand[9] };
        int[] cardChi3 = new int[] { cardHand[10], cardHand[11], cardHand[12] };

        bool isFlip;

        if (isDearling) {
            isFlip = true;
        } else {
            isFlip = false;
        }
        float timeDelay = 0.5f;

        if (pos == 0 && players[pos].isPlaying()) {
            players[pos].cardMauBinh[2].setArrCard(cardChi1,
                    isDearling, false, isFlip, pos);
            setLoaiBai(1, PokerCard.getTypeOfCardsPoker(cardChi1));
        } else {
            players[pos].cardMauBinh[2].setArrCard(cardChi1,
                    isDearling, false, false, pos);
        }
        yield return new WaitForSeconds(timeDelay);

        if (pos == 0 && players[pos].isPlaying()) {
            players[pos].cardMauBinh[1].setArrCard(cardChi2,
                    isDearling, false, isFlip, pos);
            setLoaiBai(2, PokerCard.getTypeOfCardsPoker(cardChi2));
        } else {
            players[pos].cardMauBinh[1].setArrCard(cardChi2,
                    isDearling, false, false, pos);
        }

        yield return new WaitForSeconds(timeDelay);
        if (pos == 0 && players[pos].isPlaying()) {
            players[pos].cardMauBinh[0].setArrCard(cardChi3,
                    isDearling, false, isFlip, pos);
            setLoaiBai(3, PokerCard.getTypeOfCardsPoker(cardChi3));
            checkLung(cardChi1, cardChi2, cardChi3);
        } else {
            players[pos].cardMauBinh[0].setArrCard(cardChi3,
                    isDearling, false, false, pos);
        }

        if (!isDearling) {
            timeDelay = 0;
        }

        /* if(players[0].getName ().Equals (BaseInfo.gI ().mainInfo.nick)) {
              if(infome) {
                  btn_xong.setVisible (true);
              } else
                  btn_xong.setVisible (isDearling);
          } else {
              btn_xong.setVisible (false);
          }*
         }*/
    }

    public override void onStartForView(string[] playingName) {
        base.onStartForView(playingName);
        resetMB();

        turntime = 0;
        turntimeMB = 32;
        timeReceiveTurnMB = GetCurrentMilli();
        btn_xeplai.gameObject.SetActive(false);
        lb_time_mb.text = ("" + turntimeMB);
        lb_time_mb.gameObject.SetActive(true);

        int[] card = new int[13];
        for (int i = 0; i < card.Length; i++) {
            card[i] = 52;
        }
        for (int i = 0; i < players.Length; i++) {
            if (players[i].isPlaying() && !players[i].st_name.Equals("")) {
                StartCoroutine(setCardHand(card, i, true));
            }
        }
        go_chi.SetActive(false);
        isTouchMB = false;
        setTouchCardMB(false);
    }

    public void setLoaiBai(int chi, int typeCard) {
        switch (chi) {
            case 1:
                //sp_chi1.sprite = gameControl.list_typecards[typeCard];
                LoadAssetBundle.LoadSprite(sp_chi1, Res.AS_UI, Res.TypeCard_Name[typeCard]);
                break;
            case 2:
                //sp_chi2.sprite = gameControl.list_typecards[typeCard];
                LoadAssetBundle.LoadSprite(sp_chi2, Res.AS_UI, Res.TypeCard_Name[typeCard]);
                break;
            case 3:
                //sp_chi3.sprite = gameControl.list_typecards[typeCard];
                LoadAssetBundle.LoadSprite(sp_chi3, Res.AS_UI, Res.TypeCard_Name[typeCard]);
                break;
        }
        if (players[0].getName().Equals(BaseInfo.gI().mainInfo.nick))
            go_chi.gameObject.SetActive(true);
    }

    public void checkLung(int[] cardChi1, int[] cardChi2, int[] cardChi3) {
        if (PokerCard.isBiggerArray(cardChi2, cardChi1)
               || PokerCard.isBiggerArray(cardChi3, cardChi2)
               || PokerCard.isBiggerArray(cardChi3, cardChi1)) {
            setLung(true);
        } else {
            setLung(false);
        }
    }

    public void setLung(bool isLung) {
        sp_lung.gameObject.SetActive(isLung);
    }

    public override void disableAllBtnTable() {
        base.disableAllBtnTable();
        btn_xong.gameObject.SetActive(false);
        btn_xeplai.gameObject.SetActive(false);
    }

    public override void onFinishGame(Message message) {
        base.onFinishGame(message);

        SoundManager.instance.startFinishAudio();

        btn_xeplai.gameObject.SetActive(false);
        btn_xong.gameObject.SetActive(false);
        lb_time_mb.gameObject.SetActive(false);
        sp_chi.gameObject.SetActive(false);
        sp_lung.gameObject.SetActive(false);
        for (int j = 0; j < players.Length; j++) {
            //players[j].setXepXong (0);
            players[j].sp_xepXong.gameObject.SetActive(false);
            players[j].sp_action.gameObject.SetActive(false);
            reSetTypeCard(j);
        }

        for (int i = 0; i < players[0].cardMauBinh[0].getSize(); i++) {
            players[0].cardMauBinh[0].getCardbyPos(i).transform.localScale = new Vector3(1, 1, 1);
        }
        for (int i = 0; i < players[0].cardMauBinh[1].getSize(); i++) {
            players[0].cardMauBinh[1].getCardbyPos(i).transform.localScale = new Vector3(1, 1, 1);
        }
        for (int i = 0; i < players[0].cardMauBinh[2].getSize(); i++) {
            players[0].cardMauBinh[2].getCardbyPos(i).transform.localScale = new Vector3(1, 1, 1);
        }
        players[0].cardMauBinh[0].setAllMo(false);
        players[0].cardMauBinh[1].setAllMo(false);
        players[0].cardMauBinh[2].setAllMo(false);
        go_chi.gameObject.SetActive(false);
    }

    public override void setMaster(string nick) {
        //base.setMaster (nick);
        for (int i = 0; i < nUsers; i++) {
            if (!players[i].isSit()) {
                players[i].setInvite(true);
            } else {
                players[i].setInvite(false);
            }
        }
        masterID = "";
        if (toggleLock != null) {
            toggleLock.gameObject.SetActive(false);
        }
    }

    bool isFinishMauBinh;

    public override void onSapBaChi(string namePlayer, long moneyEarn) {
        base.onSapBaChi(namePlayer, moneyEarn);
        if (moneyEarn > 0) {
            players[getPlayer(namePlayer)].flyMoney(
                    "+" + BaseInfo.formatMoneyDetailDot(moneyEarn) + "");
        } else if (moneyEarn < 0) {
            players[getPlayer(namePlayer)].flyMoney(
                    "-" + BaseInfo.formatMoneyDetailDot(-moneyEarn) + "");
            players[getPlayer(namePlayer)].setSap3Chi(true);
        }
    }

    public override void onLung(string namePlayer, long moneyEarn) {
        base.onLung(namePlayer, moneyEarn);

        if (namePlayer.Equals(BaseInfo.gI().mainInfo.nick)) {
            SoundManager.instance.startBinhlungAudio();
        }
        int pos = getPlayer(namePlayer);
        players[pos].setLoaiBai(-1);
        players[pos].setMoneyMB(moneyEarn);

        players[pos].setLung(true);
        players[pos].setXepXong(false);
    }

    public override void onRankMauBinh(Message message) {
        base.onRankMauBinh(message);
        try {
            sbyte chi = message.reader().ReadByte();
            if (chi != 4 && chi != 5) {
                sbyte size = message.reader().ReadByte();
                for (int i = 0; i < size; i++) {
                    string namePlayer = message.reader().ReadUTF();
                    sbyte typeCard = message.reader().ReadByte();
                    long moneyEarn = message.reader().ReadLong();
                    sbyte size2 = message.reader().ReadByte();
                    int[] cardChi = new int[size2];
                    for (int k = 0; k < size2; k++) {
                        cardChi[k] = message.reader().ReadByte();
                    }
                    onRankMauBinh(chi, namePlayer, typeCard, moneyEarn, cardChi);
                }
            } else if (chi == 4) {
                sbyte size = message.reader().ReadByte();
                for (int i = 0; i < size; i++) {
                    string namePlayer = message.reader().ReadUTF();
                    long moneyEarn = message.reader().ReadLong();
                    onSapBaChi(namePlayer, moneyEarn);
                }
            } else if (chi == 5) {
                sbyte size = message.reader().ReadByte();
                for (int i = 0; i < size; i++) {
                    string namePlayer = message.reader().ReadUTF();
                    long moneyEarn = message.reader().ReadLong();
                    onLung(namePlayer, moneyEarn);
                }
            }

        } catch (Exception e) {
            // TODO: handle exception
        }
    }

    public override void onRankMauBinh(sbyte chi, string namePlayer, sbyte typeCard,
        long moneyEarn, int[] cardChi) {
        base.onRankMauBinh(chi, namePlayer, typeCard, moneyEarn, cardChi);

        btn_xong.gameObject.SetActive(false);
        btn_xeplai.gameObject.SetActive(false);
        lb_time_mb.gameObject.SetActive(false);

        for (int i = 0; i < players.Length; i++) {
            players[i].sp_xepXong.gameObject.SetActive(false);
        }
        go_chi.SetActive(false);
        if (!BaseInfo.gI().isView && players[0].isPlaying()) {
            sp_chi.transform.DOKill();
            sp_chi.transform.localScale = Vector3.one;
            if (!sp_chi.gameObject.activeInHierarchy)
                sp_chi.gameObject.SetActive(true);
            //sp_chi.sprite = list_sp_chi[chi - 1];
            LoadAssetBundle.LoadSprite(sp_chi, Res.AS_UI, "chi" + chi);
            sp_chi.transform.DOScale(1.2f, 0.2f).SetLoops(-1);
        }

        int iCard = 0;
        if (chi == 1) {
            iCard = 2;
        } else if (chi == 2) {
            iCard = 1;
        } else if (chi == 3) {
            iCard = 0;
        }
        int index = getPlayer(namePlayer);

        if (players[index].isPlaying()) {
            players[index].setXepXong(false);
        }
        if (index != 0)
            players[getPlayer(namePlayer)].cardMauBinh[iCard]
                    .setArrCard(cardChi, false);
        else if (!players[0].getName().Equals(BaseInfo.gI().mainInfo.nick)) {
            players[getPlayer(namePlayer)].cardMauBinh[iCard]
                    .setArrCard(cardChi, false);
        }
        
        switch (index) {
            case 0: {
                    if (!BaseInfo.gI().isView && players[0].isPlaying()) {//neu dang choi
                        SoundManager.instance.startSobaiAudio();
                        players[0].sp_typeCard.gameObject.SetActive(true);
                        LoadAssetBundle.LoadSprite(players[0].sp_typeCard, Res.AS_UI, Res.TypeCard_Name[typeCard], ()=> {
                            players[0].sp_typeCard.SetNativeSize();
                        });
                        players[0].sp_typeCard.transform.localScale = Vector3.zero;
                        Vector3 vt = players[0].sp_typeCard.transform.position;
                        players[0].sp_typeCard.transform.DOScale(1.4f, 0.2f);
                        StartCoroutine(setInvisible(players[0].sp_typeCard.gameObject));


                        for (int i = 0; i < players[0].cardMauBinh[0].getSize(); i++) {
                            players[0].cardMauBinh[0].getCardbyPos(i).cardMo.gameObject.SetActive(true);
                            players[0].cardMauBinh[0].getCardbyPos(i).transform.localScale = Vector3.one;
                        }
                        for (int i = 0; i < players[0].cardMauBinh[1].getSize(); i++) {
                            players[0].cardMauBinh[1].getCardbyPos(i).cardMo.gameObject.SetActive(true);
                            players[0].cardMauBinh[1].getCardbyPos(i).transform.localScale = Vector3.one;
                        }
                        for (int i = 0; i < players[0].cardMauBinh[2].getSize(); i++) {
                            players[0].cardMauBinh[2].getCardbyPos(i).cardMo.gameObject.SetActive(true);
                            players[0].cardMauBinh[2].getCardbyPos(i).transform.localScale = Vector3.one;
                        }
                        //players[0].sp_typeCard.sprite = gameControl.list_typecards[typeCard];
                        switch (chi) {
                            case 1: {
                                    for (int i = 0; i < players[0].cardMauBinh[2].getSize(); i++) {
                                        players[0].cardMauBinh[2].getCardbyPos(i).cardMo.gameObject.SetActive(false);
                                        players[0].cardMauBinh[2].getCardbyPos(i).transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                                    }

                                    vt.y = players[0].cardMauBinh[2].transform.position.y;
                                    break;
                                }
                            case 2: {
                                    for (int i = 0; i < players[0].cardMauBinh[1].getSize(); i++) {
                                        players[0].cardMauBinh[1].getCardbyPos(i).cardMo.gameObject.SetActive(false);
                                        players[0].cardMauBinh[1].getCardbyPos(i).transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                                    }
                                    vt.y = players[0].cardMauBinh[1].transform.position.y;
                                    break;
                                }
                            case 3: {
                                    for (int i = 0; i < players[0].cardMauBinh[0].getSize(); i++) {
                                        players[0].cardMauBinh[0].getCardbyPos(i).cardMo.gameObject.SetActive(false);
                                        players[0].cardMauBinh[0].getCardbyPos(i).transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                                    }

                                    vt.y = players[0].cardMauBinh[0].transform.position.y;
                                    break;
                                }
                        }
                        players[0].sp_typeCard.transform.position = vt;
                        players[0].setMoneyMB(moneyEarn);
                    } else { //Neu la xem
                        for (int i = 0; i < players[0].cardMauBinh[0].getSize(); i++) {
                            players[0].cardMauBinh[0].getCardbyPos(i).gameObject.SetActive(false);
                        }
                        for (int i = 0; i < players[0].cardMauBinh[1].getSize(); i++) {
                            players[0].cardMauBinh[1].getCardbyPos(i).gameObject.SetActive(false);
                        }
                        for (int i = 0; i < players[0].cardMauBinh[2].getSize(); i++) {
                            players[0].cardMauBinh[2].getCardbyPos(i).gameObject.SetActive(false);
                        }
                    }
                    break;
                }
            default: {
                    players[index].sp_typeCard.gameObject.SetActive(true);
                    players[index].sp_typeCard.transform.localScale = Vector3.zero;
                    //players[index].sp_typeCard.sprite = gameControl.list_typecards[typeCard];
                    LoadAssetBundle.LoadSprite(players[index].sp_typeCard, Res.AS_UI, Res.TypeCard_Name[typeCard], ()=> {
                        players[index].sp_typeCard.SetNativeSize();
                    });
                    players[index].sp_typeCard.transform.DOScale(1.4f, 0.2f).OnComplete(delegate {
                        StartCoroutine(setInvisible(players[index].sp_typeCard.gameObject));
                    });
                    switch (chi) {
                        case 1: {
                                for (int i = 0; i < players[index].cardMauBinh[2].getSize(); i++) {
                                    players[index].cardMauBinh[2].getCardbyPos(i).cardMo.gameObject.SetActive(false);
                                    players[index].cardMauBinh[2].getCardbyPos(i).setId(cardChi[i]);
                                }
                                players[index].sp_typeCard.transform.position = players[index].cardMauBinh[2].transform.position;
                                break;
                            }
                        case 2: {
                                for (int i = 0; i < players[index].cardMauBinh[1].getSize(); i++) {
                                    players[index].cardMauBinh[1].getCardbyPos(i).cardMo.gameObject.SetActive(false);
                                    players[index].cardMauBinh[1].getCardbyPos(i).setId(cardChi[i]);
                                }
                                players[index].sp_typeCard.transform.position = players[index].cardMauBinh[1].transform.position;
                                break;
                            }
                        case 3: {
                                for (int i = 0; i < players[index].cardMauBinh[0].getSize(); i++) {
                                    players[index].cardMauBinh[0].getCardbyPos(i).cardMo.gameObject.SetActive(false);
                                    players[index].cardMauBinh[0].getCardbyPos(i).setId(cardChi[i]);
                                }
                                players[index].sp_typeCard.transform.position = players[index].cardMauBinh[0].transform.position;
                                break;
                            }
                    }
                    players[index].setMoneyMB(moneyEarn);
                    break;
                }
        }
    }

    IEnumerator setInvisible(GameObject obj) {
        yield return new WaitForSeconds(2.0f);
        obj.transform.DOScale(0, 0.2f);
        yield return new WaitForSeconds(0.3f);
        obj.SetActive(false);
    }

    public override void onFinalMauBinh(string name) {
        base.onFinalMauBinh(name);

        int index = getPlayer(name);
        if (players[index].isPlaying()) {
            players[index].setXepXong(true);
        }
    }

    public override void allCardFinish(string nick, int[] card) {
        //base.allCardFinish (nick, card);
        int index = getPlayer(nick);
        if (index != 0) {
            if (card.Length > 0) {
                StartCoroutine(setCardHand(card, index, false));
            }
        }
        if (index == 0 && !BaseInfo.gI().mainInfo.nick.Equals(nick)) {
            StartCoroutine(setCardHand(card, index, false));
        }
        go_chi.SetActive(false);
    }

    void reSetTypeCard0() {

    }

    void reSetTypeCard(int pos) {


    }

    public override void onWinMauBinh(string name, sbyte typeCard) {
        // TODO Auto-generated method stub
        base.onWinMauBinh(name, typeCard);
        disableAllBtnTable();
        btn_xong.StopAllCoroutines();
        players[getPlayer(name)].setThangTrang(typeCard);

    }

    private void resetMB() {
        for (int i = 0; i < players.Length; i++) {
            players[i].resetData();
        }

        go_chi.SetActive(false);
        sp_lung.gameObject.SetActive(false);

        players[0].cardMauBinh[2].transform.localPosition = vt_chi_3;
        players[0].cardMauBinh[1].transform.localPosition = vt_chi_2;
        players[0].cardMauBinh[0].transform.localPosition = vt_chi_1;

        players[0].cardMauBinh[0].resetPostionCard(210);
        players[0].cardMauBinh[1].resetPostionCard(350);
        players[0].cardMauBinh[2].resetPostionCard(350);
    }

    public override void InfoCardPlayerInTbl(Message message, string turnName, int time, sbyte numP) {
        base.InfoCardPlayerInTbl(message, turnName, time, numP);
        int[] card = new int[13];
        for (int i = 0; i < card.Length; i++) {
            card[i] = 52;
        }
        try {
            for (int i = 0; i < numP; i++) {
                string name = message.reader().ReadUTF();
                bool isDangXep = message.reader().ReadBoolean();

                players[getPlayer(name)].setPlaying(true);
                if (getPlayer(name) == 2) {
                }
                StartCoroutine(setCardHand(card, getPlayer(name), false));

                if (isDangXep) {
                    players[0].cardMauBinh[2].transform.localPosition = vt_chi_3;
                    players[0].cardMauBinh[1].transform.localPosition = vt_chi_2;
                    players[0].cardMauBinh[0].transform.localPosition = vt_chi_1;
                } else {
                    action_card_down();
                }
            }
            if (time > 0) {
                turntimeMB = time;
                turntime = 0;
                timeReceiveTurnMB = GetCurrentMilli();
                lb_time_mb.text = ("" + turntimeMB);
                lb_time_mb.gameObject.SetActive(true);
            }
        } catch (Exception e) {
            // TODO: handle exception
        }

    }

    public override void onInfome(Message message) {
        base.onInfome(message);
        try {
            isStart = true;
            players[0].setPlaying(true);
            //players[0].gameObject.transform.localPosition = new Vector3(xPlay, -115, 0);

            bool isDangXep = message.reader().ReadBoolean();
            sbyte len = message.reader().ReadByte();
            sbyte[] c = new sbyte[len];
            for (int i = 0; i < len; i++) {
                c[i] = message.reader().ReadByte();
            }

            int[] cardHand;
            cardHand = new int[c.Length];
            for (int i = 0; i < c.Length; i++) {
                cardHand[i] = c[i];
            }
            for (int i = 0; i < players[0].cardMauBinh[0].getSize(); i++) {
                players[0].cardMauBinh[0].getCardbyPos(i).transform.localScale = new Vector3(1, 1, 1);
            }
            for (int i = 0; i < players[0].cardMauBinh[1].getSize(); i++) {
                players[0].cardMauBinh[1].getCardbyPos(i).transform.localScale = new Vector3(1, 1, 1);
            }
            for (int i = 0; i < players[0].cardMauBinh[2].getSize(); i++) {
                players[0].cardMauBinh[2].getCardbyPos(i).transform.localScale = new Vector3(1, 1, 1);
            }
            reSetTypeCard0();


            if (isDangXep) {
                StartCoroutine(setCardHand(cardHand, 0, false));
                btn_xong.gameObject.SetActive(true);
                btn_xeplai.gameObject.SetActive(false);
            } else {
                StartCoroutine(setCardHand(cardHand, 0, false));
                btn_xong.gameObject.SetActive(false);
                btn_xeplai.gameObject.SetActive(true);
            }

        } catch (Exception e) {
            // TODO: handle exception
        }
    }
    void Update() {
        if (lb_time_mb.gameObject.activeInHierarchy) {
            long time = turntimeMB
                    - (GetCurrentMilli() - timeReceiveTurnMB) / 1000;
            if ((int)time <= 1 && !isSendCard) {
                isSendCard = true;
                guiFinalMauBinh();
            }
            lb_time_mb.text = ("" + time + "");
            if (time <= 0) {
                lb_time_mb.gameObject.SetActive(false);
            }
        }
    }
    public void HasDrop() {
        if (isTouchMB) {
            int[] c2 = players[0].cardMauBinh[2].getArrayCardAllTrue();
            int[] c1 = players[0].cardMauBinh[1].getArrayCardAllTrue();
            int[] c0 = players[0].cardMauBinh[0].getArrayCardAllTrue();
            int typecard1 = PokerCard.getTypeOfCardsPoker(c2);
            int typecard2 = PokerCard.getTypeOfCardsPoker(c1);
            int typecard3 = PokerCard.getTypeOfCardsPoker(c0);

            setLoaiBai(1, typecard1);
            setLoaiBai(2, typecard2);
            setLoaiBai(3, typecard3);

            checkLung(c2, c1, c0);
            card_show_mb.gameObject.SetActive(false);
        }
    }
    [SerializeField]
    Card card_show_mb;
    public bool isTouchMB = true;
    public void HasBeginDrag(int id) {
        if (isTouchMB) {
            card_show_mb.gameObject.SetActive(true);
            card_show_mb.setId(id);
        }
    }
    public void HasDrag() {
        if (isTouchMB) {
            card_show_mb.transform.position = Input.mousePosition;
        }
    }
    public void HasEndDrag() {
        if (isTouchMB) {
            card_show_mb.gameObject.SetActive(false);
            card_show_mb.setId(52);
        }
    }
}

namespace UnityEngine.EventSystems {
    public interface IHasChanged : IEventSystemHandler {
        void HasDrop();
        void HasBeginDrag(int id);
        void HasDrag();
        void HasEndDrag();
    }
}
