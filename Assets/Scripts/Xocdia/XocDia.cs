using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using System.Collections;

public class XocDia : BaseCasino {
    public TimerXocdia m_timerWaiting;
    public ThongbaoXocdia m_thongbaoXocdia;
    public ThongbaoXocdia m_thongbaoXocdia1;
    public DiaComponent m_diaComponent;
    public WinXocdia m_winXocdia;
    public Text[] m_cacMucCuoc;

    //Cai xoc dia.
    public static Transform PlayerLamcai;
    public static bool SystemIsMaster;
    public Text m_btnDatGapDoiLabel;
    public Text m_btnDatLaiLabel;
    public Text m_btnHuyCuocLabel;
    public Text m_btnHuyChanLabel;
    public Text m_btnHuyLeLabel;
    public Text m_btnLamcaiLabel;

    //For create bai xoc dia.
    public Transform m_baixocdia;
    public List<Transform> m_pos;
    public GameObject xdRed;
    public GameObject xdWhite;

    public List<int> m_indexPos;
    public List<GameObject> m_baiXocdia;

    //For create bang lich su.
    public Transform m_bangLichsu;
    public GameObject m_lsDo;
    public GameObject m_lsTrang;
    public Transform m_posLichsu;
    public Text m_nChan;
    public Text m_nLe;

    private float m_deltaX = 19.0f;
    private float m_deltaY = -18f;
    private List<GameObject> m_lsCount;

    public static bool isDatTatTay = false;

    //Ten nguoi choi lam cai.
    //private string m_master;
    //So luong chip mau do.
    private int m_chipRed = -1;

    //Muc cuoc cua nguoi choi
    public List<GameObject> m_mucCuocAnim;
    //public List<GameObject> m_chips;

    private long m_mucCuoc;
    //private GameObject m_chipCuoc;

    //Cho phep dat cuoc cua.
    private bool m_isDatCuocCua = false;

    //Tong so tien cuoc cua cac cua.BaseInfo
    /// <summary>
    /// Tong so tien cuoc cua cua cac player
    /// 0-chan, 1-le, 2-4do, 3-4trang, 4 - 3do, 5-3trang 
    /// </summary>
    public Text[] m_totalMoneyCuaCuoc;
    /// <summary>
    /// Tong so tien cuoc cua cua cac player
    /// 0-chan, 1-le, 2-4do, 3-4trang, 4 - 3do, 5-3trang 
    /// </summary>
    private long[] m_totalMoney;

    /// <summary>
    /// Tong so tien cuoc cua cua player[0]
    /// 0-chan, 1-le, 2-4do, 3-4trang, 4 - 3do, 5-3trang 
    /// </summary>
    public Text[] m_totalMeMoneyCuaCuoc;
    /// <summary>
    /// Tong so tien cuoc cua cua player[0]
    /// 0-chan, 1-le, 2-4do, 3-4trang, 4 - 3do, 5-3trang 
    /// </summary>
    private long[] m_totalMeMoney;

    //Xu ly viec dat cuoc gap doi.
    private const int DOUBLE_BET_MAX = 4;
    private int m_double_bet_me = 0;

    //Button with color disable(0, 100, 100, 255).set color normal
    public Button m_btnDatGapDoi;
    public Button m_btnDatLai;
    public Button m_btnHuyCuoc;
    public Button m_btnLamCai;
    public Button m_btnHuyChan;
    public Button m_btnHuyLe;
    public Button m_btnHuyCai;
    private static Color COLOR_DISABLE_BTN = new Color(170.0f / 255.0f, 170.0f / 255.0f, 170.0f / 255.0f);
    private static Color COLOR_DISABLE_TXT_BTN = new Color(255.0f / 255.0f, 0.0f, 0.0f);

    //Kiem tra viec dat cuoc cua player0.
    private bool m_isBetMe = false;
    private bool m_isBetMeAgain = false;
    //Kiem tra xem van truoc co dat cuoc ko.
    private bool m_isBetPrevious = false;

    //Player dat cuoc cua le va chan.
    private List<ABSUser> m_playerCCC = new List<ABSUser>();
    private List<ABSUser> m_playerCCL = new List<ABSUser>();

    private bool m_me_master = false;

    private bool m_chipStopMove = false;

    void Awake() {
        GameControl.instance.currentCasino = this;
        ChipXocdia.onChipMove += OnChipMove;
        ChipXocdia.onChipMoveFinish += OnChipMoveFinish;
    }

    void Start() {
        //UnloadScene(Res.XOCDIA_NAME);
        //GameControl.instance.StartGame();
        //StartCoroutine(LoadMucCuoc());
    }

    IEnumerator LoadMucCuoc()
    {
        yield return new WaitForEndOfFrame();
        if (BaseInfo.gI().listMucCuocXocDia.Count > 0)
        {
            long muccuoc1 = BaseInfo.gI().listMucCuocXocDia[0];
            long muccuoc2 = BaseInfo.gI().listMucCuocXocDia[1];
            long muccuoc3 = BaseInfo.gI().listMucCuocXocDia[2];
            long muccuoc4 = BaseInfo.gI().listMucCuocXocDia[3];
            GameControl.instance.currentCasino.onXocdiaNhanCacMucCuoc(muccuoc1, muccuoc2, muccuoc3, muccuoc4);
        }
        if (BaseInfo.gI().listIdHistoryXocDia.Count > 0)
        {
            GameControl.instance.currentCasino.onXocDiaHistory(BaseInfo.gI().listIdHistoryXocDia);
        }
        if(BaseInfo.gI().onInfoMe != null)
            GameControl.instance.currentCasino.onInfome(BaseInfo.gI().onInfoMe);
        if (BaseInfo.gI().onXocDiaUpdateCua != null)
            GameControl.instance.currentCasino.onXocDiaUpdateCua(BaseInfo.gI().onXocDiaUpdateCua);
    }

    public void Enable() {
        ResetData();
    }

    private void CreateIndexPos() {
        m_indexPos = new List<int>();
        for (int i = 0; i < 9; i++) {
            m_indexPos.Add(i);
        }
    }

    private void ClearIndexPos() {
        if (m_indexPos.Count > 0) {
            m_indexPos.Clear();
        }
    }

    private void ClearBaixocdia() {
        if (m_baiXocdia != null && m_baiXocdia.Count > 0) {
            for (int i = 0; i < m_baiXocdia.Count; i++) {
                Destroy(m_baiXocdia[i]);
            }

            m_baiXocdia.Clear();
        }
    }

    public override void setMaster(string nick) {
        base.setMaster(nick);
        if (nick != "") {
            SystemIsMaster = false;
            PlayerLamcai = players[getPlayer(nick)].gameObject.transform;

            //Player-self lam cai.
            if (BaseInfo.gI().mainInfo.nick.Equals(nick)) {
                m_me_master = true;
                m_btnLamcaiLabel.text = "Hủy cái";
                SetVisibleButtonTable(false, false, false, true, true, true);
            } else {
                m_me_master = false;
                m_btnLamcaiLabel.text = "Làm cái";
                SetVisibleButtonTable(true, true, true, false, false, false);
            }

        } else {
            m_me_master = false;
            SystemIsMaster = true;
            PlayerLamcai = null;

            m_btnLamcaiLabel.text = "Làm cái";
            SetVisibleButtonTable(true, true, true, true, false, false);
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

        //Remove animation.
        m_winXocdia.RemoveWinXocdia();
        for (int i = 0; i < players.Length; i++) {
            players[i].GetComponent<XocdiaPlayer>().SetPlayerLose();
            players[i].GetComponent<XocdiaPlayer>().RemoveAllChip();
        }

        //Chua dat cuoc
        m_isBetMe = false;
        m_isBetMeAgain = false;
        m_me_master = false;

        //Set lam cai.
        m_btnLamcaiLabel.text = "Làm cái";
        if (master.Equals("")) {
            //He thong lam cai.
            SystemIsMaster = true;
            SetVisibleButtonTable(true, true, true, true, false, false);
        } else {
            SetVisibleButtonTable(true, true, true, false, false, false);

            for (int i = 0; i < players.Length; i++) {
                if (players[i].getName().Equals(master)) {
                    players[i].setMaster(true);
                    SystemIsMaster = false;
                    PlayerLamcai = players[i].gameObject.transform;
                } else {
                    players[i].setMaster(false);
                }
            }
        }

        //Khong cho dat cuoc
        m_isDatCuocCua = false;

        //Set default animation muc cuoc.
        for (int i = 0; i < m_mucCuocAnim.Count; i++) {
            if (i == 0)
                m_mucCuocAnim[i].SetActive(true);
            else {
                m_mucCuocAnim[i].SetActive(false);
            }
        }

        isDatTatTay = false;
    }

    //public override void onBack() {
    //    base.onBack();
    //    //ResetData();
    //}

    private void ResetData() {

        //Reset all data.
        PlayerLamcai = null;
        SystemIsMaster = true;
        ClearIndexPos();
        ClearBaixocdia();

        //Reset all time.
        m_timerWaiting.ResetAllTimer();
        m_winXocdia.RemoveWinXocdia();
        for (int i = 0; i < players.Length; i++) {
            players[i].GetComponent<XocdiaPlayer>().SetPlayerLose();
            players[i].GetComponent<XocdiaPlayer>().RemoveAllChip();
        }
        if (m_thongbaoXocdia != null) {
            m_thongbaoXocdia.SetAnimationThongbao_Idle();
        }
        if (m_thongbaoXocdia1 != null) {
            m_thongbaoXocdia1.EndFadeIn();
        }
        if (m_diaComponent != null) {
            m_diaComponent.SetAnimationXocdiaIdle();
        }

        //--------------------
        //So tien cuoc thong ke
        if (m_totalMoney == null) {
            m_totalMoney = new long[6];
        } else {
            for (int i = 0; i < m_totalMoney.Length; i++) {
                m_totalMoney[i] = 0;
            }
        }

        if (m_totalMeMoney == null) {
            m_totalMeMoney = new long[6];
        } else {
            for (int i = 0; i < m_totalMeMoney.Length; i++) {
                m_totalMeMoney[i] = 0;
            }
        }

        for (int i = 0; i < m_totalMoneyCuaCuoc.Length; i++) {
            m_totalMoneyCuaCuoc[i].text = "0";
        }

        for (int i = 0; i < m_totalMeMoneyCuaCuoc.Length; i++) {
            m_totalMeMoneyCuaCuoc[i].text = "0";
        }
        //So tien cuoc thong ke.
        //--------------------

        m_isBetMe = false;
        m_isBetMeAgain = false;
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
        if (toggleLock != null)
            toggleLock.gameObject.SetActive(false);
    }

    public override void onTimeAuToStart(sbyte time) {

        SetEnableButtonTable(false, false, false, true, false, false);

        //--------------------
        //So tien cuoc thong ke
        if (m_totalMoney == null) {
            m_totalMoney = new long[6];
        } else {
            for (int i = 0; i < m_totalMoney.Length; i++) {
                m_totalMoney[i] = 0;
            }
        }

        if (m_totalMeMoney == null) {
            m_totalMeMoney = new long[6];
        } else {
            for (int i = 0; i < m_totalMeMoney.Length; i++) {
                m_totalMeMoney[i] = 0;
            }
        }
        for (int i = 0; i < m_totalMoneyCuaCuoc.Length; i++) {
            m_totalMoneyCuaCuoc[i].text = "0";
        }

        for (int i = 0; i < m_totalMeMoneyCuaCuoc.Length; i++) {
            m_totalMeMoneyCuaCuoc[i].text = "0";
        }

        //Chua dat cuoc.
        m_isBetMe = false;
        m_isBetMeAgain = false;

        //Khong cho dat cuoc
        m_isDatCuocCua = false;

        //Clear bai xoc dia cu.
        ClearBaixocdia();

        //Set timer.
        if (m_thongbaoXocdia != null) {
            m_thongbaoXocdia.SetLbThongbao("Chờ bắt đầu ván mới");
            m_thongbaoXocdia.SetAnimationThongbao_Xuong();
        }

        if (m_diaComponent != null) {
            m_diaComponent.SetAnimationUpbat();
        }

        if (m_timerWaiting != null) {
            m_timerWaiting.setTimeAutoStart(time);
        }
        //Set timer.
    }

    public void OnChipMove(string playerName) {
        if (BaseInfo.gI().mainInfo.nick.Equals(playerName))
            m_chipStopMove = false;
    }

    public void OnChipMoveFinish(string playerName) {
        if (BaseInfo.gI().mainInfo.nick.Equals(playerName))
            m_chipStopMove = true;
    }

    public override void onBeGinXocDia(int time) {
        //Play sound.
        SoundManager.instance.startXocdiaAudio();
        //Play sound.

        SetEnableButtonTable(false, false, false, false, false, false);

        if (m_timerWaiting != null) {
            m_timerWaiting.setTimeAutoStart(0);
            m_timerWaiting.hideTimeWaiting();
            m_thongbaoXocdia.SetAnimationThongbao_Xuong();
            m_thongbaoXocdia.SetLbThongbao("Nhà cái bắt đầu xóc");

            m_timerWaiting.setTimeBeginXocdia(time);
        }
        if (m_diaComponent != null) {
            m_diaComponent.SetAnimationXocdia();
        }
        m_winXocdia.RemoveWinXocdia();
    }

    public override void onBeginXocDiaTimeDatcuoc(int time) {
        if (m_diaComponent != null) {
            m_diaComponent.SetAnimationXocdiaIdle();
        }

        if (m_me_master == true) {
            SetEnableButtonTable(false, false, false, false, false, false);
        } else {
            SetEnableButtonTable(true, true, true, false, false, false);
        }

        //Cho phep dat cuoc
        m_isDatCuocCua = true;

        if (time > 0) {
            if (m_timerWaiting != null) {
                m_timerWaiting.setTimeBeginXocdia(0);
                m_timerWaiting.hideTimeWaiting();
                m_timerWaiting.setTimeBeginDatcuoc(time);
            }
            if (m_thongbaoXocdia != null) {
                m_thongbaoXocdia.SetLbThongbao("Đặt cược");
                m_thongbaoXocdia.SetAnimationThongbao_Xuong();
            }
        }
    }

    public override void onXocDiaMobat(int numRed) {

        SetEnableButtonTable(false, false, false, false, false, false);

        //Khong cho dat cuoc
        m_isDatCuocCua = false;

        //Tao list index position
        CreateIndexPos();

        if (m_timerWaiting != null) {
            m_timerWaiting.setTimeBeginDatcuoc(0);
            m_timerWaiting.setTimeBeginDungcuoc(0);
            m_timerWaiting.hideTimeWaiting();
        }

        if (m_thongbaoXocdia != null) {
            m_thongbaoXocdia.SetLbThongbao("Mở bát");
            m_thongbaoXocdia.SetAnimationThongbao_Xuong();
        }

        if (m_diaComponent != null) {
            m_diaComponent.SetAnimationMobat();
        }

        m_baiXocdia = new List<GameObject>();
        int numWhite = 4 - numRed;
        for (int i = 0; i < numRed; i++) {
            //Random position
            int rd = UnityEngine.Random.Range(0, m_indexPos.Count);
            int index = m_indexPos[rd];

            GameObject go = Instantiate(xdRed) as GameObject;
            go.transform.SetParent(m_baixocdia, true);
            go.transform.position = m_pos[index].position;
            go.transform.localScale = Vector3.one;

            m_indexPos.Remove(index);
            m_baiXocdia.Add(go);
        }

        for (int i = 0; i < numWhite; i++) {
            //Random position
            int rd = UnityEngine.Random.Range(0, m_indexPos.Count);
            int index = m_indexPos[rd];

            GameObject go = Instantiate(xdWhite) as GameObject;
            go.transform.SetParent(m_baixocdia, true);
            go.transform.position = m_pos[index].position;
            go.transform.localScale = Vector3.one;

            m_indexPos.Remove(index);
            m_baiXocdia.Add(go);
        }

        m_chipRed = numRed;

        //Clear index
        ClearIndexPos();

        //Reset data.
        m_double_bet_me = 0;
    }
    long[] mucCuoc = new long[4];
    public override void onXocdiaNhanCacMucCuoc(long muc1, long muc2, long muc3, long muc4) {
        if (m_cacMucCuoc == null)
            return;

        mucCuoc[0] = muc1;
        mucCuoc[1] = muc2;
        mucCuoc[2] = muc3;
        mucCuoc[3] = muc4;

        m_cacMucCuoc[0].text = formatMoney(muc1);
        m_cacMucCuoc[1].text = formatMoney(muc2);
        m_cacMucCuoc[2].text = formatMoney(muc3);
        m_cacMucCuoc[3].text = formatMoney(muc4);

        //Set default muc cuoc.
        m_mucCuoc = muc1;
        //Set default animation muc cuoc.
        for (int i = 0; i < m_mucCuocAnim.Count; i++) {
            if (i == 0)
                m_mucCuocAnim[i].SetActive(true);
            else {
                m_mucCuocAnim[i].SetActive(false);
            }
        }

        isDatTatTay = false;
    }

    public override void onXocDiaHistory(List<int> aIDs) {
        if (m_lsCount == null) {
            m_lsCount = new List<GameObject>();
        } else {
            for (int i = 0; i < m_lsCount.Count; i++) {
                Destroy(m_lsCount[i]);
            }
            m_lsCount.Clear();
        }

        if (aIDs == null) {
            return;
        }
        int nChan = 0;
        int nLe = 0;
        for (int i = 0; i < aIDs.Count; i++) {
            GameObject go;
            if (aIDs[i] == 0) {
                nChan++;
                go = Instantiate(m_lsTrang) as GameObject;
            } else {
                nLe++;
                go = Instantiate(m_lsDo) as GameObject;
            }

            go.transform.SetParent(m_posLichsu, true);
            go.transform.localScale = Vector3.one;
            //Save total go lich su.
            //We will remove go lich su after function is called again.
            m_lsCount.Add(go);
        }

        if (m_nChan != null) {
            m_nChan.text = nChan.ToString();
        }

        if (m_nLe != null) {
            m_nLe.text = nLe.ToString();
        }
    }

    public void OnClickChangeMucCuoc(string lb_id) {
        int id = int.Parse(lb_id);
        //Play sound.
        SoundManager.instance.clickBtnAudio();
        //Play sound.

        //long mc = 0;
        //Debug.LogError("Muccuoc: " + muccuoc);
        for (int i = 0; i < m_mucCuocAnim.Count; i++) {
            m_mucCuocAnim[i].SetActive(false);
        }

        isDatTatTay = false;
        if (id == 4) {
            isDatTatTay = true;
            m_mucCuoc = BaseInfo.gI().mainInfo.moneyXu;
        } else
            m_mucCuoc = mucCuoc[id];
        m_mucCuocAnim[id].SetActive(true);
    }

    public void OnClickDatCuocCua(string lbId) {
        XocdiaPlayer xocdiaPlayer = players[0].GetComponent<XocdiaPlayer>();
        if (isDatTatTay) {
            m_mucCuoc = BaseInfo.gI().mainInfo.moneyXu;
        }
        xocdiaPlayer.DatCuoc(lbId, m_mucCuoc, m_isDatCuocCua);
    }

    public override void onXocDiaDatcuoc(string nick, sbyte cua, long money, int typeCHIP) {
        //Play sound.
        SoundManager.instance.MoneyAudio();
        //Play sound.
        m_isBetMe = true;

        if (isDatTatTay == true) {
            typeCHIP = 4;
        }

        //XocdiaPlayer xocdiaPlayer = null;
        for (int i = 0; i < players.Length; i++) {
            if (players[i].getName().Equals(nick)) {
                //if (xocdiaPlayer == null) {
                //    xocdiaPlayer = players[i].GetComponent<XocdiaPlayer>();
                //}
                players[i].GetComponent<XocdiaPlayer>().ActionChipDatcuoc(cua, typeCHIP);
            }
        }
        if (m_totalMoney == null) {
            m_totalMoney = new long[6];
        }
        m_totalMoney[cua] += money;
        m_totalMoneyCuaCuoc[cua].text = BaseInfo.formatMoney(m_totalMoney[cua]);
        if (BaseInfo.gI().mainInfo.nick.Equals(nick)) {
            m_totalMeMoney[cua] += money;
            m_totalMeMoneyCuaCuoc[cua].text = BaseInfo.formatMoney(m_totalMeMoney[cua]);
        }

        //Cache player bet the cua le
        //  m_playerCCL.Add(players[getPlayer(nick)]);
        switch (cua) {
            case 0:
                //Cache player bet the cua chan
                m_playerCCC.Add(players[getPlayer(nick)]);
                break;
            case 1:
                //Cache player bet the cua le
                m_playerCCL.Add(players[getPlayer(nick)]);
                break;
        }
    }

    public void OnClickBtnDatGapDoi() {
        //Play sound.
        SoundManager.instance.clickBtnAudio();
        //Play sound.

        if (m_me_master == true) {
            return;
        }

        if (m_double_bet_me < DOUBLE_BET_MAX && m_isBetMe == true) {
            SendData.onSendDatGapDoi();
        }
    }

    public void OnClickDatLai() {
        //Play sound.
        SoundManager.instance.clickBtnAudio();
        //Play sound.

        //if (m_isBetPrevious && !m_isBetMeAgain) {
        SendData.onSendDatLai();
        //}

    }

    public void OnClickHuyCuoc() {
        if (m_chipStopMove == false) {
            return;
        }

        //Play sound.
        SoundManager.instance.clickBtnAudio();
        //Play sound.

        if (m_isBetMe == true) {
            SendData.onSendHuyCuoc();
        }
    }

    public void OnClickLamCai() {
        //Play sound.
        SoundManager.instance.clickBtnAudio();
        //Play sound.
        //if( m_me_master == false) {
        SendData.onSendLamCai();
        //}
    }

    public void OnClickHuyChan() {
        //Play sound.
        SoundManager.instance.clickBtnAudio();
        //Play sound.

        SendData.onSendChucNangCai((byte)2);
        if (m_me_master)
            SetEnableButtonTable(false, false, false, false, false, false);
    }

    public void OnClickHuyLe() {
        //Play sound.
        SoundManager.instance.clickBtnAudio();
        //Play sound.

        SendData.onSendChucNangCai((byte)1);
        if (m_me_master)
            SetEnableButtonTable(false, false, false, false, false, false);
    }

    public override void onXocDiaDatGapdoi(string nick, sbyte socua, Message message) {
        //Play sound.
        SoundManager.instance.MoneyAudio();
        //Play sound.

        if (BaseInfo.gI().mainInfo.nick.Equals(nick)) {
            m_double_bet_me++;
        }

        if (m_double_bet_me >= DOUBLE_BET_MAX) {
            //Disable btnDatX2
            m_btnDatGapDoi.enabled = false;
            m_btnDatGapDoi.image.color = new Color(0, 100.0f / 255.0f, 100.0f / 255.0f);
        } else {
            //Enable btnDatx2
            m_btnDatGapDoi.enabled = true;
            m_btnDatGapDoi.image.color = new Color(1.0f, 1.0f, 1.0f);
        }

        XocdiaPlayer xocdiaPlayer = null;
        for (int i = 0; i < players.Length; i++) {
            if (players[i].getName().Equals(nick)) {
                xocdiaPlayer = players[i].gameObject.GetComponent<XocdiaPlayer>();
            }
        }

        for (int i = 0; i < socua; i++) {
            sbyte cua = message.reader().ReadByte();
            xocdiaPlayer.ActionChipDatGapDoi(cua);
            //xocdiaPlayer.CalculateMoneyDoubleBet (cua);
        }
    }

    public override void onXocDiaDatlai(string nick, sbyte socua, Message message) {
        //Play sound.
        SoundManager.instance.MoneyAudio();
        //Play sound.

        m_isBetMe = true;
        m_isBetMeAgain = true;

        for (int i = 0; i < socua; i++) {
            sbyte cua = message.reader().ReadByte();
            sbyte a = message.reader().ReadByte();
            if (a == 1) {
                sbyte soloaichip = message.reader().ReadByte();
                for (int j = 0; j < soloaichip; j++) {
                    sbyte loaichip = message.reader().ReadByte();
                    int sochip = message.reader().ReadInt();
                    for (int k = 0; k < sochip; k++) {
                        players[getPlayer(nick)].GetComponent<XocdiaPlayer>().ActionChipDatcuoc(cua, loaichip);
                    }
                }
            }
        }
        //end
    }

    public override void onXocDiaHuycuoc(string nick, long moneycua0, long moneycua1, long moneycua2,
    long moneycua3, long moneycua4, long moneycua5) {
        m_isBetMe = false;
        m_isBetMeAgain = false;

        players[getPlayer(nick)].GetComponent<XocdiaPlayer>().ActionTraTienCuoc(moneycua0, moneycua1, moneycua2, moneycua3,
           moneycua4, moneycua5);
    }

    public override void onXocDiaUpdateCua(Message message) {
        if (m_totalMoney == null) {
            m_totalMoney = new long[6];
        } else {
            for (int i = 0; i < m_totalMoney.Length; i++) {
                m_totalMoney[i] = 0;
            }
        }

        if (m_totalMeMoney == null) {
            m_totalMeMoney = new long[6];
        } else {
            for (int i = 0; i < m_totalMeMoney.Length; i++) {
                m_totalMeMoney[i] = 0;
            }
        }

        try {
            string nick = message.reader().ReadUTF();
            for (int i = 0; i < 6; i++) {
                m_totalMoney[i] = message.reader().ReadLong();
                m_totalMeMoney[i] = message.reader().ReadLong();
                m_totalMoneyCuaCuoc[i].text = BaseInfo.formatMoney(m_totalMoney[i]);
                if (BaseInfo.gI().mainInfo.nick.Equals(nick)) {
                    m_totalMeMoneyCuaCuoc[i].text = BaseInfo.formatMoney(m_totalMeMoney[i]);
                }
            }
        } catch (System.IO.IOException e) {
            Debug.LogException(e);
        }
    }

    public void SetVisibleButtonTable(bool btn1, bool btn2, bool btn3, bool btn4, bool btn5, bool btn6) {
        if (btn1 == true) {
            m_btnDatGapDoi.gameObject.SetActive(true);
        } else {
            m_btnDatGapDoi.gameObject.SetActive(false);
        }

        if (btn2 == true) {
            m_btnDatLai.gameObject.SetActive(true);
        } else {
            m_btnDatLai.gameObject.SetActive(false);
        }

        if (btn3 == true) {
            m_btnHuyCuoc.gameObject.SetActive(true);
        } else {
            m_btnHuyCuoc.gameObject.SetActive(false);
        }

        if (btn4 == true) {
            m_btnLamCai.gameObject.SetActive(true);
        } else {
            m_btnLamCai.gameObject.SetActive(false);
        }

        if (btn5 == true) {
            m_btnHuyChan.gameObject.SetActive(true);
        } else {
            m_btnHuyChan.gameObject.SetActive(false);
        }

        if (btn6 == true) {
            m_btnHuyLe.gameObject.SetActive(true);
        } else {
            m_btnHuyLe.gameObject.SetActive(false);
        }
    }

    public void SetEnableButtonTable(bool btn1, bool btn2, bool btn3, bool btn4, bool btn5, bool btn6) {
        if (btn1 == true) {
            m_btnDatGapDoi.enabled = true;
            m_btnDatGapDoi.image.color = Color.white;
            m_btnDatGapDoiLabel.color = Color.white;
        } else {
            m_btnDatGapDoi.enabled = false;
            m_btnDatGapDoi.image.color = COLOR_DISABLE_BTN;
            m_btnDatGapDoiLabel.color = COLOR_DISABLE_TXT_BTN;
        }

        if (btn2 == true) {
            m_btnDatLai.enabled = true;
            m_btnDatLai.image.color = Color.white;
            m_btnDatLaiLabel.color = Color.white;
        } else {
            m_btnDatLai.enabled = false;
            m_btnDatLai.image.color = COLOR_DISABLE_BTN;
            m_btnDatLaiLabel.color = COLOR_DISABLE_TXT_BTN;
        }

        if (btn3 == true) {
            m_btnHuyCuoc.enabled = true;
            m_btnHuyCuoc.image.color = Color.white;
            m_btnHuyCuocLabel.color = Color.white;
        } else {
            m_btnHuyCuoc.enabled = false;
            m_btnHuyCuoc.image.color = COLOR_DISABLE_BTN;
            m_btnHuyCuocLabel.color = COLOR_DISABLE_TXT_BTN;
        }

        if (btn4 == true) {
            m_btnLamCai.enabled = true;
            m_btnLamCai.image.color = Color.white;
            m_btnLamcaiLabel.color = Color.white;
        } else {
            m_btnLamCai.enabled = false;
            m_btnLamCai.image.color = COLOR_DISABLE_BTN;
            m_btnLamcaiLabel.color = COLOR_DISABLE_TXT_BTN;
        }

        if (btn5 == true) {
            m_btnHuyChan.enabled = true;
            m_btnHuyChan.image.color = Color.white;
            m_btnHuyChanLabel.color = Color.white;
        } else {
            m_btnHuyChan.enabled = false;
            m_btnHuyChan.image.color = COLOR_DISABLE_BTN;
            m_btnHuyChanLabel.color = COLOR_DISABLE_TXT_BTN;
        }

        if (btn6 == true) {
            m_btnHuyLe.enabled = true;
            m_btnHuyLe.image.color = Color.white;
            m_btnHuyLeLabel.color = Color.white;
        } else {
            m_btnHuyLe.enabled = false;
            m_btnHuyLe.image.color = COLOR_DISABLE_BTN;
            m_btnHuyLeLabel.color = COLOR_DISABLE_TXT_BTN;
        }
    }

    public override void onInfome(Message message) {
        ClearIndexPos();
        ClearBaixocdia();

        //Reset all time.
        m_timerWaiting.ResetAllTimer();
        m_winXocdia.RemoveWinXocdia();
        for (int i = 0; i < players.Length; i++) {
            players[i].GetComponent<XocdiaPlayer>().SetPlayerLose();
            players[i].GetComponent<XocdiaPlayer>().RemoveAllChip();
        }
        if (m_thongbaoXocdia != null) {
            m_thongbaoXocdia.SetAnimationThongbao_Idle();
        }
        if (m_thongbaoXocdia1 != null) {
            m_thongbaoXocdia1.EndFadeIn();
        }
        if (m_diaComponent != null) {
            m_diaComponent.SetAnimationXocdiaIdle();
        }

        //--------------------
        //So tien cuoc thong ke
        if (m_totalMoney == null) {
            m_totalMoney = new long[6];
        } else {
            for (int i = 0; i < m_totalMoney.Length; i++) {
                m_totalMoney[i] = 0;
            }
        }

        if (m_totalMeMoney == null) {
            m_totalMeMoney = new long[6];
        } else {
            for (int i = 0; i < m_totalMeMoney.Length; i++) {
                m_totalMeMoney[i] = 0;
            }
        }

        for (int i = 0; i < m_totalMoneyCuaCuoc.Length; i++) {
            m_totalMoneyCuaCuoc[i].text = "0";
        }

        for (int i = 0; i < m_totalMeMoneyCuaCuoc.Length; i++) {
            m_totalMeMoneyCuaCuoc[i].text = "0";
        }
        isDatTatTay = false;
        //So tien cuoc thong ke.
        //--------------------

        // m_isBetMe = false;
        // m_isBetMeAgain = false;
        Debug.LogError("OnInfoMe");
        try {
            sbyte status = message.reader().ReadByte();
            int time = message.reader().ReadInt();

            switch (status) {
                case 1:
                    //Ko cho dat cuoc
                    m_isDatCuocCua = false;
                    SetEnableButtonTable(false, false, false, false, false, false);

                    if (m_thongbaoXocdia != null) {
                        m_thongbaoXocdia.SetAnimationThongbao_Xuong();
                        m_thongbaoXocdia.SetLbThongbao("Nhà cái bắt đầu xóc");
                    }
                    if (m_timerWaiting != null) {
                        m_timerWaiting.setTimeAutoStart(0);
                        m_timerWaiting.hideTimeWaiting();
                        m_timerWaiting.setTimeBeginXocdia(time);
                    }
                    if (m_diaComponent != null) {
                        m_diaComponent.SetAnimationXocdia();
                    }
                    break;
                case 2:
                    //Cho phep dat cuoc
                    m_isDatCuocCua = true;
                    SetEnableButtonTable(true, true, true, false, true, true);

                    if (m_thongbaoXocdia != null) {
                        m_thongbaoXocdia.SetLbThongbao("Đặt cược");
                        m_thongbaoXocdia.SetAnimationThongbao_Xuong();
                    }
                    if (m_timerWaiting != null) {
                        m_timerWaiting.setTimeBeginXocdia(0);
                        m_timerWaiting.hideTimeWaiting();
                        m_timerWaiting.setTimeBeginDatcuoc(time);
                    }
                    break;
                case 3:
                    //Ko cho dat cuoc
                    m_isDatCuocCua = false;
                    SetEnableButtonTable(false, false, false, false, false, false);

                    if (m_thongbaoXocdia != null) {
                        m_thongbaoXocdia.SetLbThongbao("Nhà cái ngừng nhận cược");
                        m_thongbaoXocdia.SetAnimationThongbao_Xuong();
                    }
                    if (m_timerWaiting != null) {
                        m_timerWaiting.setTimeBeginDatcuoc(0);
                        m_timerWaiting.hideTimeWaiting();
                        m_timerWaiting.setTimeBeginDungcuoc(time);
                    }
                    break;
            }
        } catch (System.IO.IOException e) {
            Debug.LogException(e);
        }
    }

    public override void onFinishGame(Message message) {
        //Set win
        if (m_winXocdia != null) {
            m_winXocdia.SetWinXocdia(m_chipRed, players);
        }

        //Clear player data
        if (m_playerCCC != null) {
            m_playerCCC.Clear();
        }

        if (m_playerCCL != null) {
            m_playerCCL.Clear();
        }

        //Kiem tra xem van truoc co dat cuoc ko?
        if (m_isBetMe == true) {
            m_isBetPrevious = true;
        } else {
            m_isBetPrevious = false;
        }

        try {
            int cua1 = message.reader().ReadByte();
            int cua2 = message.reader().ReadByte();

            int size = message.reader().ReadByte();
            for (int i = 0; i < size; i++) {
                string name = message.reader().ReadUTF();
                long moneyEarn = message.reader().ReadLong();
                if (moneyEarn > 0) {
                    players[getPlayer(name)].GetComponent<XocdiaPlayer>().SetPlayerWin();
                    //Play sound.
                    SoundManager.instance.startNhatAudio();
                    //Play sound.
                } else {
                    if (moneyEarn < 0) {
                        //Play sound.
                        SoundManager.instance.startLostAudio();
                        //Play sound.
                        players[getPlayer(name)].flyMoney(moneyEarn + "");
                    }

                    players[getPlayer(name)].GetComponent<XocdiaPlayer>().SetPlayerLose();
                }
            }
            //m_mucCuoc = 0;
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }

    public override void onXocDiaChucNangHuycua(Message message) {
        try {
            sbyte type = message.reader().ReadByte();
            switch (type) {
                case 1://Huy cua le.
                    if (m_thongbaoXocdia1 != null) {
                        m_thongbaoXocdia1.SetLbThongbao("Nhà cái hủy của lẻ");
                        m_thongbaoXocdia1.ShowThongbao1();
                    }

                    //Action tra tien player da cuoc
                    if (m_playerCCL != null && m_playerCCL.Count > 0) {
                        for (int i = 0; i < m_playerCCL.Count; i++) {
                            m_playerCCL[i].GetComponent<XocdiaPlayer>().ActionTraTienCuoc(-1, 1, -1, -1, -1, -1);
                        }
                    }

                    //Reset tien.
                    m_totalMoney[1] = 0;
                    m_totalMoneyCuaCuoc[1].text = BaseInfo.formatMoney(m_totalMoney[1]);
                    m_totalMeMoney[1] = 0;
                    m_totalMeMoneyCuaCuoc[1].text = BaseInfo.formatMoney(m_totalMeMoney[1]);

                    break;
                case 2://Huy cua chan.
                    if (m_thongbaoXocdia1 != null) {
                        m_thongbaoXocdia1.SetLbThongbao("Nhà cái hủy của chẵn");
                        m_thongbaoXocdia1.ShowThongbao1();
                    }

                    //Action tra tien player da cuoc
                    if (m_playerCCC != null && m_playerCCC.Count > 0) {
                        for (int i = 0; i < m_playerCCC.Count; i++) {
                            m_playerCCC[i].GetComponent<XocdiaPlayer>().ActionTraTienCuoc(1, -1, -1, -1, -1, -1);
                        }
                    }

                    //Reset tien.
                    m_totalMoney[0] = 0;
                    m_totalMoneyCuaCuoc[0].text = BaseInfo.formatMoney(m_totalMoney[0]);
                    m_totalMeMoney[0] = 0;
                    m_totalMeMoneyCuaCuoc[0].text = BaseInfo.formatMoney(m_totalMeMoney[0]);

                    break;
            }
        } catch (System.IO.IOException e) {
            Debug.LogException(e);
        }
    }

    public override void onXocDiaBeginTimerDungCuoc(Message message) {
        SetEnableButtonTable(false, false, false, false, true, true);
        try {
            int time = message.reader().ReadByte();
            if (time > 1) {
                if (m_thongbaoXocdia != null) {
                    m_thongbaoXocdia.SetLbThongbao("Nhà cái ngừng nhận cược");
                    m_thongbaoXocdia.SetAnimationThongbao_Xuong();
                }
                if (m_timerWaiting != null) {
                    m_timerWaiting.setTimeBeginDatcuoc(0);
                    m_timerWaiting.hideTimeWaiting();
                    m_timerWaiting.setTimeBeginDungcuoc(time);
                }
            }
        } catch (System.IO.IOException e) {
            Debug.LogException(e);
        }
    }

    public static string formatMoney(long money) {
        return BaseInfo.formatMoney(money);
    }
}