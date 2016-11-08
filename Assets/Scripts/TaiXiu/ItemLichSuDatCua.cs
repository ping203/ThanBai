using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemLichSuDatCua : MonoBehaviour {
    public Text txt_tg, txt_ten, txt_hoan, txt_dat;

    public void setInfo(string tg, string ten, string hoan, string dat) {
        txt_tg.text = tg;
        txt_ten.text = (ten.Length <= 7 ? ten : ten.Substring(0, 7));
        txt_hoan.text = hoan;
        txt_dat.text = dat;
    }
}
