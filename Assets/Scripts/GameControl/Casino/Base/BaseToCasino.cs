using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;
public class BaseToCasino : BaseCasino {
    protected int first;
    public Button btn_bo, btn_xembai, btn_theo,
            btn_To;
    public Button btn_ruttien;
    protected bool is_bo, is_xem, is_theo, is_to;
    public Text txt_moneyCuoc;
    public Text lb_bo, lb_xem, lb_theo, lb_to;
    protected long moneyCuoc = 0;
    long minMoney, maxMoney;
    bool isDongY;
    public bool isAutoStart;
    protected bool started;

    public GameObject sliderTo;
    public Slider slider;
    public Text currentMoney;
    public void Awake() {
        is_bo = false;
        is_xem = false;
        is_theo = false;
        is_to = false;
        slider.onValueChanged.AddListener(OnSliderChange);
        //base.Awake();
    }

    protected void setMoneyCuoc(long moneyCuoc) {
        try {
            long moneycuoc1 = getMaxChips() - players[0].getMoneyChip();
            if (moneycuoc1 == 0) {
                txt_moneyCuoc.gameObject.SetActive(false);
                is_theo = false;
            } else {
                if (moneycuoc1 > players[0].getFolowMoney()) {
                    lb_theo.text = Res.TXT_CALL
                            + " "
                            + BaseInfo.formatMoneyDetailDot(players[0]
                                    .getFolowMoney());
                } else {
                    lb_theo.text = (Res.TXT_CALL + " "
                            + BaseInfo.formatMoneyDetailDot(moneycuoc1));
                }

                is_xem = false;
                setDisable(btn_xembai, true);

                if (this.moneyCuoc != moneycuoc1) {
                    is_theo = false;
                }
            }
            if (players[0].getFolowMoney() == 0) {
                enableAllButton(false);
                lb_theo.text = (Res.TXT_CALL);
            }
            this.moneyCuoc = moneycuoc1;
        } catch (Exception e) {
        }
    }

    protected long getMaxChips() {
        long max = 0;
        for (int i = 0; i < players.Length; i++) {
            if (players[i].getMoneyChip() > max) {
                max = players[i].getMoneyChip();
            }
        }
        return max;
    }

    public override void startTableOk(int[] cardHand, Message msg, string[] nickPlay) {
        base.startTableOk(cardHand, msg, nickPlay);
        for (int i = 0; i < players.Length; i++) {
            players[i].setMoneyChip(0);
            players[i].setSoChip(0);
            players[i].resetPositionSp_TypeCard();
            players[i].chipBay.onMoveto(0, 1);
        }
        sliderTo.SetActive(false);
        isAutoStart = false;
        setMoneyCuoc(0);
        //if (moneyInPot != null) {
        //    for (int i = 0; i < moneyInPot.Length; i++) {
        //        moneyInPot[i].resetData();
        //    }
        //}
        //if (moneyTemp != null)
        //    moneyTemp.resetData();
        showAllButton(true, false, false);
        BaseInfo.gI().moneyMinTo = BaseInfo.gI().betMoney * 2;
        first = 0;
        chip_tong.setMoneyChip(0);
    }
    public override void onStartForView(string[] playingName) {
        base.onStartForView(playingName);
        hideAllButton();
    }
    public override void onJoinView(Message message) {
        // TODO Auto-generated method stub
        base.onJoinView(message);
        hideAllButton();
    }
    public override void onJoinTableSuccess(string master) {
        if (BaseInfo.gI().isView) {
            disableAllBtnTable();
        }
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
        //if (chip_tong != null)
        //    chip_tong.gameObject.SetActive(false);
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

    private void setDisable(Button button, bool isDisable) {
        Text lb = null;
        if (button == btn_theo) {
            lb = lb_theo;
        } else if (button == btn_To) {
            lb = lb_to;
        } else if (button == btn_xembai) {
            lb = lb_xem;
        } else if (button == btn_bo) {
            lb = lb_bo;
        }

        if (isDisable) {
            if (lb != null) {
                lb.color = Color.gray;
            }
        } else {
            if (lb != null) {
                lb.color = Color.white;
            }
        }
        if (button != null)
            button.enabled = !isDisable;
    }

    public override void setTurn(string nick, Message message) {
        base.setTurn(nick, message);
        if (!nick.Equals(BaseInfo.gI().mainInfo.nick)) {
            hideThanhTo();
        }
    }

    private void hideThanhTo() {
        sliderTo.gameObject.SetActive(false);
    }

    public override void onNickSkip(String nick, Message msg) {
        int poss = getPlayer(nick);
        players[poss].setAction(Res.AC_UPBO);
        players[poss].setTurn(false);
        players[poss].setRank(4);
        players[poss].cardHand.setAllMo(true);
        players[poss].setPlaying(false);
        if (poss == 0) {
            disableAllBtnTable();
        }
        try {
            setTurn(msg.reader().ReadUTF(), msg);
        } catch (Exception e) {

        }
        players[poss].chip.gameObject.SetActive(false);
        players[poss].chipBay.onMoveto(players[poss].getMoneyChip(), 1);
    }

    public override void onFinishGame(Message message) {
        try {
            isPlaying = false;
            prevPlayer = -1;
            preCard = -1;
            isStart = false;
            int total = message.reader().ReadByte();
            BaseInfo.gI().infoWin.Clear();
            for (int i = 0; i < total; i++) {
                string nick = message.reader().ReadUTF();
                int rank = message.reader().ReadByte();
                long money = message.reader().ReadLong();

                if (getPlayer(nick) == 0) {
                    BaseInfo.gI().infoWin.Add(new InfoWin(i + 1 + ". ", nick,
                            money, true));
                } else {
                    BaseInfo.gI().infoWin.Add(new InfoWin(i + 1 + ". ", nick,
                            money, false));
                }
                nickFire = "";
                for (int j = 0; j < nUsers; j++) {
                    if (players[j].isPlaying()
                            && players[j].getName().Equals(nick)) {
                        players[j].setRank(rank);
                        players[j].setReady(false);
                        players[j].setSoChip(0);
                        players[j].setMoneyChip(0);
                        break;
                    }
                }
            }
            disableAllBtnTable();
            onJoinTableSuccess(masterID);
            for (int j = 0; j < nUsers; j++) {
                if (players[j].isPlaying()) {
                    players[j].setPlaying(false);
                }
                players[j].setTurn(false);
            }
        } catch (Exception ex) {
            Debug.LogException(ex);
        }
        tongMoney = 0;
        chip_tong.setMoneyChipChu(tongMoney);
    }

    public override void disableAllBtnTable() {
        base.disableAllBtnTable();
        showAllButton(true, false, false);
    }
    void resetButton() {
        is_bo = false;
        //is_check_fold = false;
        is_xem = false;
        is_theo = false;
        is_to = false;

        //cb_fold.value = (false);
        //cb_fold.value = (false);
        //cb_check_fold.value = (false);
        //cb_check.value = (false);
        //cb_call.value = (false);
        //cb_callany.value = (false);
    }
    public override void resetData() {
        base.resetData();
        hideThanhTo();
    }

    private void hideAllButton() {
        btn_theo.gameObject.SetActive(false);
        btn_To.gameObject.SetActive(false);
        btn_xembai.gameObject.SetActive(false);
        btn_bo.gameObject.SetActive(false);
        btn_ruttien.gameObject.SetActive(false);
        btn_To.transform.parent.gameObject.SetActive(false);
    }
    protected void enableAllButton(bool isEnable) {
        setDisable(btn_theo, !isEnable);
        setDisable(btn_xembai, !isEnable);
        setDisable(btn_To, !isEnable);
        setDisable(btn_bo, !isEnable);
    }
    protected void showAllButton(bool isWait, bool isCheck,
            bool isVisible) {
        if (BaseInfo.gI().isView) {
            hideAllButton();
            return;
        }
        enableAllButton(true);
        isDongY = false;
        if (isWait && !BaseInfo.gI().isView) {
            // TODO Auto-generated method stub
            lb_bo.text = (Res.TXT_FOLD);
            //lb_check_fold.text = (Res.TXT_CHECK_FOLD );
            lb_xem.text = (Res.TXT_CHECK);
            lb_theo.text = (Res.TXT_CALL);
            //lb_callany.text = (Res.TXT_CALL_ANY );
            setDisable(btn_theo, true);
            setDisable(btn_To, true);
            setDisable(btn_xembai, true);
            setDisable(btn_bo, true);
        } else {
            // TODO Auto-generated method stub
            lb_bo.text = (Res.TXT_FOLD);
            lb_xem.text = (Res.TXT_CHECK);
            lb_theo.text = (Res.TXT_CALL);
            lb_to.text = (Res.TXT_RAISE);
            //setDisable(btn_call, false);
            setDisable(btn_bo, false);

        }

        if (!isCheck) {
            is_bo = false;
            is_xem = false;
            is_theo = false;
            is_to = false;
        }

        if (isVisible) {
            btn_bo.gameObject.SetActive(true);
            btn_xembai.gameObject.SetActive(true);
            btn_theo.gameObject.SetActive(true);
            btn_To.gameObject.SetActive(true);
            btn_ruttien.gameObject.SetActive(false);
        } else {
            btn_bo.gameObject.SetActive(false);
            btn_xembai.gameObject.SetActive(false);
            btn_theo.gameObject.SetActive(false);
            btn_To.gameObject.SetActive(false);
            btn_ruttien.gameObject.SetActive(true);
        }
    }
    protected long getMaxMoney(long money) {
        minMoney = getmoneyTong();
        if (minMoney > BaseInfo.gI().moneyMinTo) {
            minMoney = BaseInfo.gI().moneyMinTo;
        }
        if (minMoney < 0) {
            minMoney = 0;
        }

        if (money < minMoney) {
            // return;
            money = minMoney;
        }
        if (money % 10 != 0) {
            money = money - money % 10 + 10;
        }

        if (money > getmoneyTong()) {
            money = getmoneyTong();
        }
        long m = money + getMaxChips() - players[0].getMoneyChip();
        return m;
    }
    protected long getmoneyTong() {
        long money = players[0].getFolowMoney() - getMaxChips()
                + players[0].getMoneyChip();
        long max = 0;
        for (int i = 1; i < players.Length; i++) {
            if (players[i].isPlaying()) {
                if (players[i].getFolowMoney() - getMaxChips()
                        + players[i].getMoneyChip() >= max) {
                    max = players[i].getFolowMoney() - getMaxChips()
                            + players[i].getMoneyChip();
                }
            }
        }
        if (max < money) {
            money = max;
        }
        if (money < 0) {
            money = 0;
        }

        return money;
    }

    protected virtual void infoWinPlayer(InfoWinTo infoWin, List<InfoWinTo> info2) {
        for (int i = 0; i < players.Length; i++) {
            try {
                players[i].sp_typeCard.gameObject.SetActive(false);
                players[i].cardHand.setAllMo(true);
                players[i].sp_xoay.gameObject.SetActive(false);
                if (players[i].cardHand3Cay != null)
                    players[i].cardHand3Cay.setAllMo(true);
            } catch (Exception e) {
                Debug.LogException(e);
            }
            players[i].cardHand.reAddAllCard();
        }

        int poss = getPlayer(infoWin.name);
        if (players[poss].isSit()) {
            players[poss].sp_xoay.gameObject.SetActive(true);
        }

        players[poss].cardHand.setAllMo(false);
        try {
            if (players[poss].cardHand3Cay != null)
                players[poss].cardHand3Cay.setAllMo(false);
        } catch (Exception e) {
            Debug.LogException(e);
        }
        for (int i = 0; i < BaseInfo.gI().infoWinTo.Count; i++) {
            if (infoWin == BaseInfo.gI().infoWinTo[i]) {
                BaseInfo.gI().infoWinTo[i].rank = -1;
                break;
            }
        }
    }

    public override void onInfoWinPlayer(List<InfoWinTo> infoWin, List<InfoWinTo> info2) {
        BaseInfo.gI().infoWinTo = infoWin;
        if (infoWin.Count <= 0) {
            return;
        }
        for (int i = 0; i < infoWin.Count; i++) {
            StartCoroutine(actionInfoWin(infoWin, info2));
        }
    }
    IEnumerator actionInfoWin(List<InfoWinTo> infoWin, List<InfoWinTo> info2) {
        for (int i = 0; i < infoWin.Count; i++) {
            yield return new WaitForSeconds(1.5f);
            infoWinPlayer(infoWin[i], info2);
        }
    }
    protected void baseSetturn(long moneyCuoc) {
        setMoneyCuoc(moneyCuoc);
        if (moneyCuoc <= 0) {
            if (is_xem || is_to || is_theo) {
                SendData.onAccepFollow();
                showAllButton(true, false, true);
            }
            if (is_bo) {
                SendData.onSendSkipTurn();
                showAllButton(true, false, true);
            } else {
                showAllButton(false, false, true);
                setDisable(btn_theo, true);
            }
        } else if (moneyCuoc < players[0].getFolowMoney()) {
            if (is_bo) {
                SendData.onSendSkipTurn();
                showAllButton(true, false, true);
            } else if (is_to || is_theo) {
                SendData.onAccepFollow();
                showAllButton(true, false, true);
            } else {
                showAllButton(false, false, true);
                setDisable(btn_xembai, true);
                lb_theo.text = (Res.TXT_CALL + " "
                        + BaseInfo.formatMoneyDetailDot(moneyCuoc));
            }
        } else {
            if (is_bo) {
                SendData.onSendSkipTurn();
                showAllButton(true, false, true);
            } else if (is_to || is_theo) {
                SendData.onAccepFollow();
                showAllButton(true, false, true);
            } else {
                showAllButton(false, false, true);
                setDisable(btn_xembai, true);
                setDisable(btn_To, true);
                lb_theo.text = (Res.TXT_ALLIN);
            }
        }
        if (getmoneyTong() == 0) {
            setDisable(btn_To, true);
        }
    }

    public override void onNickCuoc(long moneyInPot, long soTienTo, long moneyBoRa, string nick, Message message) {
        base.onNickCuoc(moneyInPot, soTienTo, moneyBoRa, nick, message);
        if (BaseInfo.gI().moneyTable * 2 >= soTienTo) {
            BaseInfo.gI().moneyMinTo = BaseInfo.gI().moneyTable * 2;
        } else {
            BaseInfo.gI().moneyMinTo = soTienTo;
        }
        //Debug.LogError("Users Count " + nUsers);
        for (int i = 0; i < players.Length; i++) {
            if (players[i].isPlaying()) {
                if (players[i].getName().Equals(nick)) {
                    first++;
                    players[i].setAction(Res.AC_TO);
                    players[i].chipBay.onMoveto(moneyBoRa
                            + players[i].getMoneyChip(), 1);
                    players[i].setMoneyChip(moneyBoRa
                            + players[i].getMoneyChip());
                    SoundManager.instance.startToAudio();
                    tongMoney += soTienTo;
                    chip_tong.setMoneyChipChu(tongMoney);
                    break;
                }
            }
        }
        //Debug.LogError("Het vong for ");
        try {
            string nickTurn = message.reader().ReadUTF();
            setTurn(nickTurn, message);
            //Debug.LogError("Nick turn" + nickTurn);
        } catch (Exception e) {
            Debug.LogException(e);
        }

    }

    public override void onHaveNickTheo(long money, string nick, Message message) {
        if (money == 0) {
            players[getPlayer(nick)].setAction(Res.AC_XEMBAI);
        } else {
            players[getPlayer(nick)].setAction(Res.AC_THEO);

            players[getPlayer(nick)].setMoneyChip(money
               + players[getPlayer(nick)].getMoneyChip());
            players[getPlayer(nick)].chipBay.onMoveto(money
               + players[getPlayer(nick)].getMoneyChip(), 1);
            tongMoney += money;
            chip_tong.setMoneyChipChu(tongMoney);
        }

        try {
            setTurn(message.reader().ReadUTF(), message);
        } catch (Exception e) {
            // TODO: handle exception
            Debug.LogException(e);
        }
    }
    public void clickButtonBo() {
        //if (cb_fold.gameObject.activeInHierarchy) {
        //    if (is_fold) {
        //        resetButton();
        //        is_fold = false;
        //        cb_fold.value = false;
        //    }
        //    else {
        //        resetButton();
        //        is_fold = true;
        //        cb_fold.value = (true);
        //    }
        //}
        //else {
        SendData.onSendSkipTurn();
        showAllButton(true, false, false);
        isDongY = false;
        players[0].setTurn(false);
        //}
    }
    public void clickButtonCheckFold() {
        //if (cb_check_fold.gameObject.activeInHierarchy) {
        //    if (is_check_fold) {
        //        resetButton();
        //        cb_check_fold.value = (false);
        //    }
        //    else {
        //        resetButton();
        //        is_check_fold = true;
        //        cb_check_fold.value = (true);
        //    }
        //}
        //else {
        SendData.onAccepFollow();
        showAllButton(true, false, true);
        isDongY = false;
        players[0].setTurn(false);
        //}
    }
    public void clickButtonXem() {
        //if (cb_check.gameObject.activeInHierarchy) {
        //    if (is_check) {
        //        resetButton();
        //        cb_check.value = (false);
        //    }
        //    else {
        //        resetButton();
        //        is_check_fold = true;
        //        cb_check.value = (true);
        //    }
        //}
        //else {
        SendData.onAccepFollow();
        showAllButton(true, false, true);
        isDongY = false;
        players[0].setTurn(false);
        //}
    }
    public void clickButtonTheo() {
        //if (cb_call.gameObject.activeInHierarchy) {
        //    if (is_call) {
        //        resetButton();
        //        is_call = false;
        //        cb_call.value = (false);
        //    }
        //    else {
        //        resetButton();
        //        is_call = true;
        //        cb_call.value = (true);
        //    }
        //}
        //else {
        SendData.onAccepFollow();
        showAllButton(true, false, true);
        isDongY = false;
        players[0].setTurn(false);
        //}
    }
    public void clickButtonTo() {
        //if (isDongY) {
        //players[0].setTurn(false);
        //SendData.onCuocXT(-99, BaseInfo.gI().moneyto);
        //showAllButton(true, false, true);
        //hideThanhTo();
        //setDisable(btn_To, false);
        //Debug.Log("To cha no!");
        //} else {
        minMoney = getmoneyTong();
        if (minMoney > BaseInfo.gI().moneyMinTo) {
            minMoney = BaseInfo.gI().moneyMinTo;
        }
        if (minMoney < 0) {
            minMoney = 0;
        }
        if (minMoney > getmoneyTong()) {
            minMoney = getmoneyTong();
        }
        setMoneyTruot(minMoney);
        showThanhTo(
                minMoney + getMaxChips()
                        - players[0].getMoneyChip(),
                getmoneyTong() + getMaxChips()
                        - players[0].getMoneyChip());
        //lb_callany.text = (Res.TXT_DONGY );
        setDisable(btn_To, true);
        //}
        //isDongY = !isDongY;
    }

    public void clickButtonOkTo()
    {
        players[0].setTurn(false);
        SendData.onCuocXT(-99, BaseInfo.gI().moneyto);
        showAllButton(true, false, true);
        hideThanhTo();
        setDisable(btn_To, false);
    }
    public void clickButtonCancelTo()
    {
        hideThanhTo();
        setDisable(btn_To, false);
        isDongY = false;
    }

    public void clickButtnRutTien() {
        long temp = 0;
        //if (BaseInfo.gI().typetableLogin == Res.ROOMFREE) {
        //    temp = BaseInfo.gI().mainInfo.moneyChip;
        //} else {
        temp = BaseInfo.gI().mainInfo.moneyXu;
        //}
        if (temp < BaseInfo.gI().moneyNeedTable) {
            PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", "Không đủ tiền để rút, bạn có muốn nạp thêm? ", delegate {

            });
        } else {
            if (players[0].getFolowMoney() < BaseInfo.gI().currentMinMoney) {
                LoadAssetBundle.LoadScene(Res.RUTTIEN_AB, Res.RUTTIEN_NAME, () => {
                    PanelRutTien.instance.show(BaseInfo.gI().currentMinMoney,
                        BaseInfo.gI().currentMaxMoney, 2, 0, 0, 0, 2);
                });
            } else {
                LoadAssetBundle.LoadScene(Res.RUTTIEN_AB, Res.RUTTIEN_NAME, () => {
                    PanelRutTien.instance.show(BaseInfo.gI().currentMinMoney,
                      BaseInfo.gI().currentMaxMoney, 3, 0, 0, 0, 2);
                });
            }
        }
    }
    private void showThanhTo(float min, float maxMoney) {
        slider.value = 0;
        sliderTo.gameObject.SetActive(true);
        currentMoney.gameObject.SetActive(true);

        currentMoney.text = BaseInfo.formatMoneyNormal((int)min);
        minMoney = getmoneyTong();

        if (minMoney > BaseInfo.gI().moneyMinTo) {
            minMoney = BaseInfo.gI().moneyMinTo;
        }
        this.maxMoney = (long)maxMoney;
        setMoneyTruot(minMoney);
    }
    long money;
    private void setMoneyTruot(long money) {
        minMoney = getmoneyTong();
        if (minMoney > BaseInfo.gI().moneyMinTo) {
            minMoney = BaseInfo.gI().moneyMinTo;
        }
        if (minMoney < 0) {
            minMoney = 0;
        }

        if (money < minMoney) {
            // return;
            money = minMoney;
        }
        if (money % 10 != 0) {
            money = money - money % 10 + 10;
        }

        if (money > getmoneyTong()) {
            money = getmoneyTong();
        }
        this.money = money;

        BaseInfo.gI().moneyto = money;
        currentMoney.text = (BaseInfo.formatMoneyNormal((int)(BaseInfo
                        .gI().moneyto + getMaxChips() - players[0]
                        .getMoneyChip())));

    }
    public void OnSliderChange(float value) {
        setMoneyTruot((int)(value * maxMoney));
    }
    public override void onTimeAuToStart(sbyte p) {
        base.onTimeAuToStart(p);
        isAutoStart = true;
        showAllButton(true, false, false);
    }
}
