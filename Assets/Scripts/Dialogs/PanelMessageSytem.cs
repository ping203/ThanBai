using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelMessageSytem : PanelGame {
    public Text txt_title, txt_content, txt_lb_ok, txt_lb_cancel;
    public Button btnOK;
    public Button btnCancel;
    public delegate void CallBack();
    public CallBack onClickOK;
    const float posXCenter = 0, posX = 80;

    public void onShow(string mess) {
        btnCancel.gameObject.SetActive(false);
        btnOK.gameObject.SetActive(true);
        DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
            txt_content.text = mess;
            base.onShow();
            setPosBtn(btnOK, posXCenter);
            txt_lb_ok.text = "Đồng ý";
            onClickOK = delegate { onHide(); };
        });

    }

    public void onShow(string mess, CallBack clickOK) {
        btnCancel.gameObject.SetActive(true);
        btnOK.gameObject.SetActive(true);
        DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
            txt_content.text = mess;
            onClickOK = clickOK;
            base.onShow();

            setPosBtn(btnOK, -posX);
            setPosBtn(btnCancel, posX);
            txt_lb_ok.text = "Đồng ý";
            txt_lb_cancel.text = "Hủy";
        });

    }
    public void onShowDCN(string mess, CallBack clickOK) {

        btnCancel.gameObject.SetActive(false);
        btnOK.gameObject.SetActive(true);
        DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
            txt_content.text = mess;
            onClickOK = clickOK;
            base.onShow();
            setPosBtn(btnOK, posXCenter);
            txt_lb_ok.text = "Đồng ý";
        });
    }

    public void onClickButtonOK() {
        SoundManager.instance.startClickButtonAudio();
        onHide();
        onClickOK.Invoke();
    }

    void setPosBtn(Button btn, float pos) {
        Vector3 vt = btn.transform.localPosition;
        vt.x = pos;
        btn.transform.localPosition = vt;
    }
}
