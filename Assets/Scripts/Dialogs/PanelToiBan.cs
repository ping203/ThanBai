using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class PanelToiBan : PanelGame {
    public InputField ip_soban;
    public InputField ip_pass;

    public void toiBan() {
        try {
            SoundManager.instance.startClickButtonAudio();
            string str = ip_soban.text;
            string pass = ip_pass.text;
            if (string.IsNullOrEmpty(str)) {
                PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Quý khách chưa nhập tên bàn.");
                return;
            }
            int tbid = int.Parse(str);
            SendData.onJoinTableForView(tbid, pass);
            GetComponent<UIPopUp>().HideDialog();
        } catch (Exception e) {
            //sua
            //GameControl.instance.toast.showToast("Định dạng bàn không đúng!");
            Debug.LogException(e);
        }
    }
}
