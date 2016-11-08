using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelDoiXuChip : PanelGame {
    public InputField ip_xu_doi;
    public Text ip_chip_nhan, tyle_xu_chip;

    // Use this for initialization
    void OnEnable() {
        if (tyle_xu_chip != null)
            tyle_xu_chip.text = "(Tỷ lệ: 1 " + Res.MONEY_VIP_UPPERCASE + " = " + BaseInfo.gI().tyle_xu_sang_chip + " " + Res.MONEY_FREE_UPPERCASE + ")";
        Huy();
    }
    public void xuToChip() {
        string str = ip_xu_doi.text.Trim();
        if (str.Equals("")) {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Bạn hãy nhập đủ thông tin.");
            return;
        }

        if (!BaseInfo.gI().checkNumber(str)) {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Nhập sai!");
            Huy();
            return;
        }
        long xu = long.Parse(str);
        if (xu > BaseInfo.gI().mainInfo.moneyXu) {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Số " + Res.MONEY_VIP + " chuyển phải <= số " + Res.MONEY_VIP + " hiện tại!");
            Huy();
            return;
        }

        SendData.onXuToChip(long.Parse(ip_xu_doi.text.Trim()));
        Huy();
    }

    public void onChangeValueInputDoiXu() {
        string str = ip_xu_doi.text.Trim();
        if (!str.Equals("")) {
            int xu = int.Parse(str);
            if (xu > BaseInfo.gI().mainInfo.moneyXu) {
                PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Số " + Res.MONEY_VIP + " chuyển phải <= số " + Res.MONEY_VIP + " hiện tại!");
                Huy();
                return;
            }
            int chip = xu * BaseInfo.gI().tyle_xu_sang_chip;
            ip_chip_nhan.text = Res.MONEY_VIP_UPPERCASE + " = " + BaseInfo.formatMoneyDetailDot(chip);
        } else {
            ip_chip_nhan.text = Res.MONEY_VIP_UPPERCASE + " = 0";
        }
    }

    public void Huy() {
        SoundManager.instance.startClickButtonAudio();
        ip_xu_doi.text = "";
        ip_chip_nhan.text = "";
    }
}
