using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelChuyenXu : PanelGame {

	public InputField ip_userId, ip_xu;
	//public Slider sliderSoXu;

	//public void onChangeValue(){
	//	ip_xu.text = (int)(BaseInfo.gI ().mainInfo.moneyXu * sliderSoXu.value) + "";
	//}

    public void onClickChuyenXu () {
        SoundManager.instance.startClickButtonAudio ();
		if (!BaseInfo.gI().checkNumber(ip_userId.text.Trim()) || !BaseInfo.gI().checkNumber(ip_xu.text.Trim())) {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton ("","Nhập sai!");
			return;

		}
		if (ip_userId.text.Trim ().Equals ("") || ip_xu.text.Trim ().Equals ("")) {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Vui lòng nhập đầy đủ thông tin!");
			return;
		}

		long userid = long.Parse (ip_userId.text.Trim());
		long xu = long.Parse (ip_xu.text.Trim());

		SendData.onXuToNick (userid, xu);
	}

    public void TuChoi () {
        SoundManager.instance.startClickButtonAudio ();
		ip_userId.text = "";
		ip_xu.text = "";
		//sliderSoXu.value = 0;
	}
}
