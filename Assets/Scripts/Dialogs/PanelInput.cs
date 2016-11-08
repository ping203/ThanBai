using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelInput : PanelGame {
    public static PanelInput instance;
    public Text lb_title;
    public InputField ip_enter;
    public delegate void CallBack();
    public CallBack onClickOK;

    void Awake() {
        instance = this;
    }

    public void onShow(string title, CallBack clickOK) {
        DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
            if(!string.IsNullOrEmpty(title))
                lb_title.text = title;
            onClickOK = clickOK;
            base.onShow();
        });
    }

    public void onShow_GetPass() {
        DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
            lb_title.text = "LẤY LẠI MẬT KHẨu";
            onClickOK = delegate {
                string nick = ip_enter.text;
                if (!nick.Equals("")) {
                    SendData.onGetPass(nick);
                    Debug.Log(nick);
                    base.onHide();
                } else {
                    PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Tài khoản không đúng!");
                }
            };
            base.onShow();
        });
    }

    public void onClickButtonOK() {
        SoundManager.instance.startClickButtonAudio();
        onClickOK.Invoke();
    }

    public int checkSDT(string sdt) {
        if (sdt.Length > 11 || sdt.Length < 10)
            return -3;

        for (int i = 0; i < sdt.Length; i++) {
            char c = sdt[i];
            if (('0' > c) || (c > '9')) {
                return -1;
            }
        }

        return 1;
    }
}
