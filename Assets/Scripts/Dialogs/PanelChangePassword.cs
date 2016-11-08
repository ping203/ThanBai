using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelChangePassword : PanelGame {
    public InputField ip_pass_current, ip_pass_new, ip_pass_again;

    public void changePass() {

        SoundManager.instance.startClickButtonAudio();
        string oldPass = ip_pass_current.text;
        string newPass1 = ip_pass_new.text;
        string newPass2 = ip_pass_again.text;
        if (oldPass == "" || newPass1 == "" || newPass2 == "") {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Bạn hãy nhập đủ thông tin.");
            return;
        }

        if (oldPass != BaseInfo.gI().pass) {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Mật khẩu cũ không đúng.");
            return;
        }

        if (newPass1 != newPass2) {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Mật khẩu không giống nhau.");
            return;
        }
        PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("","Bạn muốn gửi tin nhắn để đổi mật khẩu.", delegate {
            SendData.onGetPass(BaseInfo.gI().mainInfo.nick);
        });
        GetComponent<UIPopUp>().HideDialog();
    }
}
