using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelShowTool : PanelGame {
    public Image img_new;
    public GameObject sp_new;

    public void OnEnable() {
        //sp_new.SetActive(GameControl.instance.panelMail.isShowNew());
    }
    public void onClick(string action) {
        switch (action) {
            case "doithuong":
                PanelWaiting.instance.ShowLoading();
                //SendData.onGetInfoGift();
                break;
            case "napthe":
                //GameControl.instance.panelNapChuyenXu.onShow();
                break;
            case "mail":
                //GameControl.instance.panelMail.onShow();
                break;
            case "help":
                //GameControl.instance.panleHelp.onShow();
                break;
        }
        onHide();
    }
}
