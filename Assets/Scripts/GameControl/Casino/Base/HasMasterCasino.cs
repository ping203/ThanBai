using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HasMasterCasino : BaseCasino {
    public Button btn_batdau;
    public Button btn_sansang;
    public Button btn_datcuoc;
    public Text lb_Btn_sansang;

    public override void onStartForView(string[] playingName) {
        base.onStartForView(playingName);
        if (BaseInfo.gI().isView) {
            disableAllBtnTable();
        }
    }
    public override void changBetMoney(long betMoney, string info) {
        base.changBetMoney(betMoney, info);
        if (!BaseInfo.gI().isView && !players[0].isMaster() && !BaseInfo.gI().regOuTable && !BaseInfo.gI().checkHettien()) {
            btn_sansang.gameObject.SetActive(true);
            if (BaseInfo.gI().isAutoReady) {
                clickReady();
            }
        }
    }
    public override void onJoinView(Message message) {
        base.onJoinView(message);
        disableAllBtnTable();
    }

    public override void startTableOk(int[] cardHand, Message msg, string[] nickPlay) {
        base.startTableOk(cardHand, msg, nickPlay);

        if (BaseInfo.gI().isView) {
            disableAllBtnTable();
        }
        if (isPlaying) {
            lb_Btn_sansang.text = Res.TXT_XINCHO;
        } else {
            lb_Btn_sansang.text = Res.TXT_SANSANG;
        }
    }
    public override void onJoinTableSuccess(string master) {
        #region vao xem
        if (BaseInfo.gI().isView) {
            btn_batdau.gameObject.SetActive(false);
            btn_sansang.gameObject.SetActive(false);
            if (btn_datcuoc != null)
                btn_datcuoc.gameObject.SetActive(false);
            if (toggleLock != null)
                toggleLock.gameObject.SetActive(false);
            return;
        }
        #endregion //het vao xem

        if (BaseInfo.gI().mainInfo.nick.Equals(master)) {//neu mh la chu ban
            btn_batdau.gameObject.SetActive(true);
            btn_sansang.gameObject.SetActive(false);
            if(btn_datcuoc != null)
                btn_datcuoc.gameObject.SetActive(true);
            if (toggleLock != null)
                toggleLock.gameObject.SetActive(true);
        } else {//ko la chu ban
            btn_batdau.gameObject.SetActive(false);
            if (btn_datcuoc != null)
                btn_datcuoc.gameObject.SetActive(false);
            if (toggleLock != null)
                toggleLock.gameObject.SetActive(false);
            if (!isPlaying) {//neu ko choi
                btn_sansang.gameObject.SetActive(true);
                if (BaseInfo.gI().isAutoReady) {
                    if (!BaseInfo.gI().regOuTable) {
                        clickReady();
                    }
                }
                //if (!BaseInfo.gI().isFirstJoinTable) {
                //    BaseInfo.gI().isFirstJoinTable = true;
                //    btn_sansang.gameObject.SetActive(false);
                //    if (!BaseInfo.gI().regOuTable)
                //        SendData.onReady(1);// san sang
                //}
            }
        }
        if (isPlaying) {
            lb_Btn_sansang.text = Res.TXT_XINCHO;
        } else {
            lb_Btn_sansang.text = Res.TXT_SANSANG;
        }
    }

    public override void setMasterSecond(string master) {
        if (BaseInfo.gI().isView) {
            btn_batdau.gameObject.SetActive(false);
            btn_sansang.gameObject.SetActive(false);
            if (btn_datcuoc != null)
                btn_datcuoc.gameObject.SetActive(false);
            toggleLock.gameObject.SetActive(false);
        }
        if (!isStart) {
            if (master.Equals(BaseInfo.gI().mainInfo.nick)) {
                btn_batdau.gameObject.SetActive(true);
                btn_sansang.gameObject.SetActive(false);
                if (btn_datcuoc != null)
                    btn_datcuoc.gameObject.SetActive(true);
                toggleLock.gameObject.SetActive(false);
            } else {
                btn_batdau.gameObject.SetActive(false);
                if (btn_datcuoc != null)
                    btn_datcuoc.gameObject.SetActive(false);
                //btn_sansang.gameObject.SetActive(false);
                btn_sansang.gameObject.SetActive(true);
                if (!players[0].isReady()) {
                    if (BaseInfo.gI().isAutoReady) {
                        if (!BaseInfo.gI().regOuTable) {
                            clickReady();
                        }
                    }
                }
            }
        }
        if (toggleLock != null)
            if (master.Equals(BaseInfo.gI().mainInfo.nick)) {
                toggleLock.gameObject.SetActive(true);
            } else {
                toggleLock.gameObject.SetActive(false);
            }

        for (int i = 0; i < nUsers; i++) {
            if (!players[i].isSit()) {
                players[i].setInvite(true);
            } else {
                players[i].setInvite(false);
            }
        }
    }
    public void clickReady() {
        SendData.onReady(1);// san sang
        btn_sansang.gameObject.SetActive(false);
    }

    public void clickButtonDatcuoc() {
        //  gameControl.panelDatCuoc.onShow();
        //LoadAssetBundle.LoadScene(Res.AS_SUBSCENES, Res.AS_SUBSCENES_DAT_CUOC, () => {
        //    PanelDatCuoc.instance.onShow();
        //});
    }
    public void onClickButtonStart() {
        if (getTotalPlayerReady() > 1) {
            if (getTotalPlayerReady() < getTotalPlayer()) {
                PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("Thông báo", "Còn người chưa sẳn sàng,\nBạn có muốn bắt đầu không?", delegate {
                    btn_batdau.gameObject.SetActive(false);
                    SendData.onStartGame();
                    for (int i = 0; i < nUsers; i++) {
                        players[i].setVisibleRank(false);
                    }
                });
            } else {
                SendData.onStartGame();
                btn_batdau.gameObject.SetActive(false);
                if (btn_datcuoc != null)
                    btn_datcuoc.gameObject.SetActive(false);
            }

        } else {
            PopupAndLoadingScript.instance.toast.showToast("Chưa đủ người chơi!");
        }

    }
    public override void disableAllBtnTable() {
        btn_batdau.gameObject.SetActive(false);
        btn_sansang.gameObject.SetActive(false);
        if(btn_datcuoc != null)
            btn_datcuoc.gameObject.SetActive(false);
    }
}