using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ChipChuBan : MonoBehaviour {
    private long soChip;
    public Image sp_chip0;
    public Text lb_sochip;
    public Image sp_chip1;
    public Image sp_chip2;

    public void setMoneyChip(long money) {
        soChip = money;
        //string name;
        //if (money > BaseInfo.gI().moneyTable * 20) {
        //    name = "icon_chip_1";
        //} else if (money > BaseInfo.gI().moneyTable * 10) {
        //    name = "icon_chip_2";
        //} else if (money > BaseInfo.gI().moneyTable * 5) {
        //    name = "icon_chip_3";
        //} else if (money > BaseInfo.gI().moneyTable * 1) {
        //    name = "icon_chip_4";
        //} else {
        //    name = "icon_chip_5";
        //}
        //LoadAssetBundle.LoadSprite(sp_chip0, Res.AS_UI, "allin");

        if (money <= 0) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
            lb_sochip.text = "" + BaseInfo.formatMoneyNormal(money);
        }
    }
    public void setSoChip(long soChip) {
        this.soChip = soChip;
    }
    public void setMoneyChipChu(long money) {
        soChip = money;
        //string name;
        //if (money > BaseInfo.gI().moneyTable * 20) {
        //    name = "icon_chip_1";
        //} else if (money > BaseInfo.gI().moneyTable * 10) {
        //    name = "icon_chip_2";
        //} else if (money > BaseInfo.gI().moneyTable * 5) {
        //    name = "icon_chip_3";
        //} else if (money > BaseInfo.gI().moneyTable * 1) {
        //    name = "icon_chip_4";
        //} else {
        //    name = "icon_chip_5";
        //}
        //LoadAssetBundle.LoadSprite(sp_chip0, Res.AS_UI, "allin");

        if (money <= 0) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
            lb_sochip.text = "" + BaseInfo.formatMoneyNormal(money);
            StartCoroutine(scaleTo(this.gameObject));
        }
    }

    IEnumerator scaleTo(GameObject obj) {
        obj.transform.DOScale(0.8f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        obj.transform.DOScale(1f, 0.3f);
    }
    //public void showChipPhu(int pos) {
    //    switch (pos) {
    //        case 1:
    //            sp_chip1.gameObject.SetActive(true);
    //            break;
    //        case 2:
    //            sp_chip2.gameObject.SetActive(true);
    //            break;
    //    }
    //}


    public long getMoneyChip() {
        return soChip;
    }
}
