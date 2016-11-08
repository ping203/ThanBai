using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PanelHistory : PanelGame {
    //UI
    public GameObject prefab_ls_dat_cuoc;
    public Transform parent_ls_dat_cuoc_tai, parent_ls_dat_cuoc_xiu;

    public Text txt_date, txt_score;
    public Text txt_phien;
    public Text txt_tong_hoan_tai, txt_tong_dat_tai;
    public Text txt_tong_hoan_xiu, txt_tong_dat_xiu;

    public Image[] XucXac;
    public Sprite[] sp_xucXac;

    public Transform img_text_tai, img_text_xiu;
    //UI
    //Chua danh sach item
    List<GameObject> list = new List<GameObject>();

    //Variable

    public void reset() {
        for (int i = 0; i < list.Count; i++) {
            Destroy(list[i]);
        }
        list.Clear();
    }

    public void setInfoTop(string date, int[] kq) {
        txt_date.text = date;
        txt_score.text = "Điểm " + kq[0] + kq[1] + kq[2];
        XucXac[0].sprite = sp_xucXac[kq[0] - 1];
        XucXac[1].sprite = sp_xucXac[kq[1] - 1];
        XucXac[2].sprite = sp_xucXac[kq[2] - 1];
    }

    public void setInfoBot(long tong_hoan_tai, long tong_dat_tai, long tong_hoan_xiu, long tong_dat_xiu) {
        txt_tong_hoan_tai.text = tong_hoan_tai + "";
        txt_tong_dat_tai.text = tong_dat_tai + "";
        txt_tong_hoan_xiu.text = tong_hoan_xiu + "";
        txt_tong_dat_xiu.text = tong_dat_xiu + "";
    }

    public void genItemLs(bool isTai, string date, string name, long tien_dat, long tien_hoan) {
        GameObject obj = Instantiate(prefab_ls_dat_cuoc) as GameObject;
        if (isTai) {
            obj.transform.SetParent(parent_ls_dat_cuoc_tai);
        } else {
            obj.transform.SetParent(parent_ls_dat_cuoc_xiu);
        }
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<ItemLichSuDatCua>().setInfo(date, name, tien_hoan + "", tien_dat + "");
        list.Add(obj);
    }
}
