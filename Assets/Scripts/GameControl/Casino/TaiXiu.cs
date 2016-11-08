using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using DG.Tweening;
using System.Collections.Generic;

public class TaiXiu : BaseCasino {
    public Text lblTotalMoneyTai, lblTotalMoneyXiu, lblMoneyTai, lblMoneyXiu;//tong tien tai, xiu all player, va cua mh
    public Text lblPlayerTai, lblPlayerXiu;//so tien mh dat
    public Text lblPhien;
    public Text text_diem;
    public Text lblNumPlayerDatTai, lblNumPlayerDatXiu;//So ng choi dat

    public Button btn_dat_tai, btn_dat_xiu;
    public TimeCountDown timeCountDown_TaiXiu;
    public TimeCountDown timeCountDown_nho;

    public long tiendat = 0;
    public long tiendatTai = 0, tiendatXiu = 0;
    public long tongTienTai = 0, tongTienXiu = 0;
    public string strTiendat = "";

    public Sprite[] sp_xucxac;
    public Image[] img_xucxac;

    public GameObject group_xoc, group_ketqua;
    public Animator[] anim_xoc;

    public GameObject img_xoay_tai, img_xoay_xiu;
    public Transform img_text_tai, img_text_xiu;

    public PanelHistory panelHistory;
    public override void onJoinTableSuccess(string master) {
    }

    public override void setMasterSecond(string master) {
    }

    //public override void onjoinTaiXiu(Message message) {
    //    base.onjoinTaiXiu(message);
    //    try {
    //        resetData();
    //        ((TaiXiuPlayer)players[0]).CreateInfoPlayer();
    //        isPlaying = message.reader().ReadBoolean();
    //        int time = message.reader().ReadInt();
    //        btn_dat_tai.enabled = true;
    //        btn_dat_xiu.enabled = true;
    //        if (time > 0) {
    //            if (isPlaying) {
    //                timeCountDown_TaiXiu.setTime(time);
    //            } else {
    //                text_diem.gameObject.SetActive(false);
    //                timeCountDown_nho.setTime(time);
    //                btn_dat_tai.enabled = false;
    //                btn_dat_xiu.enabled = false;
    //            }
    //        } else {
    //            timeCountDown_TaiXiu.setTime(0);
    //            text_diem.gameObject.SetActive(false);
    //            timeCountDown_nho.setTime(0);
    //        }
    //        long tong_tien_tai = message.reader().ReadLong();
    //        long tong_tien_xiu = message.reader().ReadLong();
    //        long tien_tai = message.reader().ReadLong();
    //        long tien_xiu = message.reader().ReadLong();

    //        lblTotalMoneyTai.text = "" + BaseInfo.formatMoneyNormal(tong_tien_tai);
    //        lblTotalMoneyXiu.text = "" + BaseInfo.formatMoneyNormal(tong_tien_xiu);
    //        lblMoneyTai.text = "" + BaseInfo.formatMoneyNormal(tien_tai);
    //        lblMoneyXiu.text = "" + BaseInfo.formatMoneyNormal(tien_xiu);

    //        int size = message.reader().ReadInt();
    //        removeLichSu();
    //        int valueTX = 0;
    //        int phien_ss = 1;
    //        for (int i = 0; i < size; i++) {
    //            valueTX = message.reader().ReadByte();
    //            phien_ss = message.reader().ReadInt();
    //            genTXLichSu(valueTX, phien_ss);
    //        }
    //        int number_tai = message.reader().ReadInt();
    //        int number_xiu = message.reader().ReadInt();
    //        lblNumPlayerDatTai.text = number_tai + "";
    //        lblNumPlayerDatXiu.text = number_xiu + "";
    //        int phien = message.reader().ReadInt();
    //        lblPhien.text = "#" + phien;

    //        PanelWaiting.instance.HideLoading();
    //    } catch (Exception e) {
    //        Debug.LogException(e);
    //    }
    //}

    //public override void onTimestartTaixiu(Message message) {
    //    base.onTimestartTaixiu(message);
    //    try {
    //        sbyte time = message.reader().ReadByte();
    //        text_diem.gameObject.SetActive(false);
    //        timeCountDown_nho.setTime(time);

    //        btn_dat_tai.enabled = false;
    //        btn_dat_xiu.enabled = false;
    //    } catch (Exception e) {
    //        Debug.LogException(e);
    //    }
    //}

    //public override void onAutostartTaixiu(Message message) {
    //    base.onAutostartTaixiu(message);
    //    try {
    //        resetData();
    //        sbyte time = message.reader().ReadByte();
    //        int phien = message.reader().ReadInt();

    //        timeCountDown_TaiXiu.setTime(time);
    //        text_diem.gameObject.SetActive(false);
    //        timeCountDown_nho.gameObject.SetActive(false);
    //        group_ketqua.SetActive(false);
    //        lblPhien.text = "#" + phien;

    //        btn_dat_tai.enabled = true;
    //        btn_dat_xiu.enabled = true;
    //    } catch (Exception e) {
    //        Debug.LogException(e);
    //    }
    //}

    //public override void onGameoverTaixiu(Message message) {
    //    base.onGameoverTaixiu(message);
    //    try {
    //        int size = message.reader().ReadInt();
    //        int kq1 = message.reader().ReadInt();
    //        int kq2 = message.reader().ReadInt();
    //        int kq3 = message.reader().ReadInt();
    //        int taihayxiu = message.reader().ReadInt();
    //        int phien_nb = message.reader().ReadInt();
    //        tongTienTai = message.reader().ReadLong();
    //        tongTienXiu = message.reader().ReadLong();
    //        lblTotalMoneyTai.text = formatMoney(tongTienTai);
    //        lblTotalMoneyXiu.text = formatMoney(tongTienXiu);
    //        xoc();
    //        text_diem.gameObject.SetActive(true);
    //        text_diem.text = "" + (kq1 + kq2 + kq3);

    //        StartCoroutine(show_ketqua(taihayxiu, kq1, kq2, kq3));

    //        lblPhien.text = "#" + phien_nb;
    //        genTXLichSu(taihayxiu, phien_nb);

    //        btn_dat_tai.enabled = false;
    //        btn_dat_xiu.enabled = false;
    //    } catch (Exception e) {
    //        Debug.LogException(e);
    //    }
    //}

    public override void resetData() {
        try {
            lblPlayerTai.text = "0";
            lblPlayerXiu.text = "0";
            lblTotalMoneyTai.text = "0";
            lblTotalMoneyXiu.text = "0";
            lblMoneyTai.text = "0";
            lblMoneyXiu.text = "0";
            lblNumPlayerDatTai.text = "0";
            lblNumPlayerDatXiu.text = "0";

            tiendat = 0;
            strTiendat = "";
            tiendatTai = 0;
            tiendatXiu = 0;
            tongTienTai = 0;
            tongTienXiu = 0;

            timeCountDown_TaiXiu.setTime(0);
            text_diem.gameObject.SetActive(false);
            timeCountDown_nho.setTime(0);
            group_xoc.SetActive(false);
            group_ketqua.SetActive(false);
            // ketQuaTaiXiu.setVisible(false);
            // xocXucXac.finish();
            // tai.clearActions();
            // xiu.clearActions();
            // img_Xoay_TAI.setVisible(false);
            // img_Xoay_XIU.setVisible(false);
            img_xoay_tai.SetActive(false);
            img_xoay_xiu.SetActive(false);
            img_text_tai.localScale = Vector3.one;
            img_text_xiu.localScale = Vector3.one;
            // tai.getColor().a = 1f;
            // tai.setScale(1f);
            // xiu.getColor().a = 1f;
            // xiu.setScale(1f);
            group_mucCuoc.gameObject.SetActive(false);
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }

    //public override void onCuocTaiXiu(Message message) {
    //    base.onCuocTaiXiu(message);
    //    try {
    //        string nick = message.reader().ReadUTF();
    //        long money = message.reader().ReadLong();
    //        long my_money = message.reader().ReadLong();
    //        sbyte cua = message.reader().ReadByte();
    //        int number_people = message.reader().ReadInt();
    //        if (nick.Equals(BaseInfo.gI().mainInfo.nick)) {
    //            if (cua == 1) {
    //                tiendatTai = my_money;
    //                lblMoneyTai.text = formatMoney(tiendatTai);
    //                lblNumPlayerDatTai.text = number_people + "";
    //            } else {
    //                tiendatXiu = my_money;
    //                lblMoneyXiu.text = formatMoney(tiendatXiu);
    //                lblNumPlayerDatXiu.text = number_people + "";
    //            }
    //        }
    //        if (cua == 1) {
    //            tongTienTai = money;
    //            lblTotalMoneyTai.text = formatMoney(tongTienTai);
    //            lblNumPlayerDatTai.text = number_people + "";
    //        } else {
    //            tongTienXiu = money;
    //            lblTotalMoneyXiu.text = formatMoney(tongTienXiu);
    //            lblNumPlayerDatXiu.text = number_people + "";
    //        }
    //    } catch (Exception e) {
    //        Debug.LogException(e);
    //    }
    //}

    //public override void onupdatemoneyTaiXiu(Message message) {
    //    base.onupdatemoneyTaiXiu(message);
    //    try {
    //        long money = message.reader().ReadLong();
    //        BaseInfo.gI().mainInfo.moneyVip = money;

    //        players[0].setMoney(money);
    //        //addTweenFlyMoney(players[0]);
    //    } catch (Exception e) {
    //        Debug.LogException(e);
    //    }
    //}

    //public override void oninfoTaiXiu(Message message) {
    //    base.oninfoTaiXiu(message);
    //    try {
    //        int sizee = message.reader().ReadShort();
    //        InfoTaiXiu[] infoTaiXiu = new InfoTaiXiu[sizee];
    //        for (int i = 0; i < sizee; i++) {
    //            infoTaiXiu[i] = new InfoTaiXiu();
    //            infoTaiXiu[i].time = message.reader().ReadUTF();
    //            infoTaiXiu[i].cua_dat = message.reader().ReadByte();
    //            infoTaiXiu[i].ketqua = message.reader().ReadByte();
    //            infoTaiXiu[i].moneyCuoc = message.reader().ReadLong();
    //            infoTaiXiu[i].moneyTralai = message.reader().ReadLong();
    //            infoTaiXiu[i].moneyAn = message.reader().ReadLong();
    //        }
    //    } catch (Exception e) {
    //        Debug.LogException(e);
    //    }
    //}

    //public override void oninfoLSTheoPhienTaiXiu(Message message) {
    //    base.oninfoLSTheoPhienTaiXiu(message);
    //    try {
    //        int[] kq = new int[3];
    //        for (int i = 0; i < 3; i++) {
    //            kq[i] = message.reader().ReadByte();
    //        }
    //        if (kq[0] <= 0) {
    //            return;
    //        }
    //        string date = message.reader().ReadUTF();


    //        long tongDatTai = message.reader().ReadLong();
    //        long tongHoanTai = message.reader().ReadLong();
    //        long tongDatXiu = message.reader().ReadLong();
    //        long tongHoanXiu = message.reader().ReadLong();

    //        panelHistory.setInfoTop(date, kq);//set thong tin top panel ls
    //        panelHistory.setInfoBot(tongHoanTai, tongDatTai, tongHoanXiu, tongDatXiu);//set thong tin bot panel ls

    //        int sizee_Tai = message.reader().ReadInt();

    //        panelHistory.reset();
    //        for (int i = 0; i < sizee_Tai; i++) {
    //            string time = message.reader().ReadUTF();
    //            string name = message.reader().ReadUTF();
    //            long muccuoc = message.reader().ReadLong();
    //            long tralai = message.reader().ReadLong();
    //            panelHistory.genItemLs(true, time, name, muccuoc, tralai);
    //        }
    //        int sizee_Xiu = message.reader().ReadInt();
    //        for (int i = 0; i < sizee_Xiu; i++) {
    //            string time = message.reader().ReadUTF();
    //            string name = message.reader().ReadUTF();
    //            long muccuoc = message.reader().ReadLong();
    //            long tralai = message.reader().ReadLong();
    //            panelHistory.genItemLs(false, time, name, muccuoc, tralai);
    //        }

    //        panelHistory.onShow();
    //    } catch (Exception e) {
    //        Debug.LogException(e);
    //    }
    //}

    public static string formatMoney(long money) {
        if (money <= 0) {
            return "0";
        } else {
            return BaseInfo.formatMoneyNormal(money);
        }
    }

    IEnumerator show_ketqua(int isTai, int kq1, int kq2, int kq3) {
        yield return new WaitForSeconds(4);
        group_xoc.SetActive(false);
        group_ketqua.SetActive(true);
        img_xucxac[0].sprite = sp_xucxac[kq1 - 1];
        img_xucxac[1].sprite = sp_xucxac[kq2 - 1];
        img_xucxac[2].sprite = sp_xucxac[kq3 - 1];
        yield return new WaitForSeconds(1);
        actionThang(isTai);
    }

    void actionThang(int isTai) {
        if (isTai == 1) {
            img_xoay_tai.SetActive(true);
            img_xoay_xiu.SetActive(false);
            img_text_tai.transform.DOScale(1.2f, 0.2f).SetLoops(-1);
            if(tiendatTai > 0) {
                SoundManager.instance.start_taixiu_win();
            }
        } else {
            img_xoay_tai.SetActive(false);
            img_xoay_xiu.SetActive(true);
            img_text_xiu.transform.DOScale(1.2f, 0.2f).SetLoops(-1);
            if (tiendatXiu > 0) {
                SoundManager.instance.start_taixiu_win();
            }
        }
        StartCoroutine(actionThang_end());
    }
    IEnumerator actionThang_end() {
        yield return new WaitForSeconds(5);
        img_xoay_tai.SetActive(false);
        img_xoay_xiu.SetActive(false);
        img_text_tai.transform.localScale = Vector3.one;
        img_text_xiu.transform.localScale = Vector3.one;
        img_text_tai.transform.DOKill();
        img_text_xiu.transform.DOKill();
    }
    void xoc() {
        group_xoc.SetActive(true);
        group_mucCuoc.gameObject.SetActive(false);
        muc_cuoc = 0;
        str = "";
        for (int i = 0; i < anim_xoc.Length; i++) {
            anim_xoc[i].Play("action_xoc");
        }
        lblPlayerTai.text = "0";
        lblPlayerXiu.text = "0";
    }

    public void onClick_DatCuoc(int isTai) {
        SoundManager.instance.clickBtnAudio();
        if (isTai == 1) {
            isDatCuaTai = 1;
            lblPlayerTai.text = "";
        } else {
            isDatCuaTai = 2;
            lblPlayerXiu.text = "";
        }
        group_mucCuoc.gameObject.SetActive(true);
        muc_cuoc = 0;
        str = "";
    }

    //public override void onBack() {
    //    PanelWaiting.instance.ShowLoading();
    //    SoundManager.instance.clickBtnAudio();
    //    SendData.onExitTaiXiu();
    //}

    #region MucCuoc
    public GameObject group_mucCuoc;
    public GameObject group_chonNhanh, group_soKhac;
    int[] list_muccuoc = { 1000, 5000, 10000, 50000, 100000, 500000, 1000000, 10000000 };

    long muc_cuoc = 0;
    public void onClick_MucCuoc(int index) {//Ham xu ly khi chon nhanh
        SoundManager.instance.clickBtnAudio();
        muc_cuoc += list_muccuoc[index];
        if (isDatCuaTai == 1) {
            lblPlayerTai.text = formatMoney(muc_cuoc);
        } else {
            lblPlayerXiu.text = formatMoney(muc_cuoc);
        }
    }
    byte isDatCuaTai = 1;//1 la Tai, 2 la Xiu
    string str = "";
    public void onClick_Number(string action) {//Ham xu ly khi nhap so khac
        SoundManager.instance.clickBtnAudio();
        if (!action.Equals("-1")) {
            str += action;
        } else {
            int l = str.Length;
            if (l > 0)
                str = str.Substring(0, l - 1);
        }
        if (str != "")
            muc_cuoc = long.Parse(str);
        else
            muc_cuoc = 0;

        if (isDatCuaTai == 1) {
            lblPlayerTai.text = formatMoney(muc_cuoc);
        } else {
            lblPlayerXiu.text = formatMoney(muc_cuoc);
        }
    }

    //public void onClick_DongY() {
    //    SendData.onCuocTaiXiu(isDatCuaTai, muc_cuoc, (byte) BaseInfo.gI().typetableLogin);
    //    group_mucCuoc.SetActive(false);
    //    lblPlayerTai.text = "0";
    //    lblPlayerXiu.text = "0";
    //    SoundManager.instance.clickBtnAudio();
    //}

    public void onClick_Huy() {
        lblPlayerTai.text = "0";
        lblPlayerXiu.text = "0";
        group_mucCuoc.SetActive(false);
        SoundManager.instance.clickBtnAudio();
    }
    public Text text_isCN;
    bool isCn = false;
    public void onClick_CN_SK() {
        SoundManager.instance.clickBtnAudio();
        str = "";
        muc_cuoc = 0;
        if (isDatCuaTai == 1)
            lblPlayerTai.text = str;
        else
            lblPlayerXiu.text = str;
        if (isCn) {
            group_chonNhanh.SetActive(true);
            group_soKhac.SetActive(false);
            text_isCN.text = "SỐ KHÁC";
        } else {
            group_chonNhanh.SetActive(false);
            group_soKhac.SetActive(true);
            text_isCN.text = "CHỌN NHANH";
        }
        isCn = !isCn;
    }
    #endregion

    #region LICH SU
    List<GameObject> list_ls = new List<GameObject>();
    public GameObject ls_tx;
    public Transform his_parent;
    int index = 0;
    //Tao ra icon lich su va update
    void genTXLichSu(int value, int phien) {
        if (list_ls.Count < 18) {
            GameObject obj = Instantiate(ls_tx) as GameObject;
            obj.transform.SetParent(his_parent);
            obj.transform.localScale = Vector3.one;

            ItemInfoLichSuTaiXiu it = obj.GetComponent<ItemInfoLichSuTaiXiu>();//.setInfo(value, phien);
            it.setInfo(value, phien);
            it.img_yellow.gameObject.SetActive(false);
            list_ls.Add(obj);
            if (list_ls.Count == 18)
                it.img_yellow.gameObject.SetActive(true);
        } else {
            for (int i = 0; i < list_ls.Count; i++) {
                ItemInfoLichSuTaiXiu it = list_ls[i].GetComponent<ItemInfoLichSuTaiXiu>();
                if (i < list_ls.Count - 1) {
                    ItemInfoLichSuTaiXiu it2 = list_ls[i + 1].GetComponent<ItemInfoLichSuTaiXiu>();
                    it.setInfo(it2.isTai, it2.id_phien);
                    // it.img_yellow.gameObject.SetActive(false);
                } else {
                    it.setInfo(value, phien);
                    // it.img_yellow.gameObject.SetActive(true);
                }
            }
        }
    }
    //remove tat ca lich su
    void removeLichSu() {
        for (int i = 0; i < list_ls.Count; i++) {
            Destroy(list_ls[i]);
        }
        list_ls.Clear();
    }
    #endregion
}
