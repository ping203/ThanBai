using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelThongBaoMoiChoi : PanelGame {
	public Text label;
	public Button btnOK;
	public Button btnCancel;
	public delegate void CallBack();
	public CallBack onClickOK;
	// Use this for initialization
	void Start() {
		
	}
	
	// Update is called once per frame
	void Update() {
		
	}
	public void onShow(string mess) {
		DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
			label.text = mess;
			btnCancel.gameObject.SetActive(true);
			btnOK.gameObject.SetActive(false);
			onShow();
		});
		
	}
	
	public void onShow(string mess, CallBack clickOK) {
		DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
			label.text = mess;
			//btnCancel.gameObject.SetActive(true);
			btnOK.gameObject.SetActive(true);
			onClickOK = clickOK;
			onShow();
		});
		
	}
	public void onShowDCN(string mess, CallBack clickOK) {
		DoOnMainThread.ExecuteOnMainThread.Enqueue(() => {
			label.text = mess;
			btnCancel.gameObject.SetActive(false);
			btnOK.gameObject.SetActive(true);
			onClickOK = clickOK;
			onShow();
		});
		
	}

    public void onClickButtonOK () {
        SoundManager.instance.startClickButtonAudio ();
		onHide();
		onClickOK.Invoke();
	}

    public void onClickCancelAll () {
        SoundManager.instance.startClickButtonAudio ();
        BaseInfo.gI ().isNhanLoiMoiChoi = false;
		onHide ();
	}
}
