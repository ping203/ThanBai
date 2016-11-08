using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelChangeName : PanelGame {
    public Text oldName;
    public InputField ip_newname;

    void Start() {
        oldName.text = BaseInfo.gI().mainInfo.displayname;
    }

    public void changeName () {
        SoundManager.instance.startClickButtonAudio();
        string tenmoi = ip_newname.text;
		if (tenmoi != "") {
			if(tenmoi.Length >= 4 && tenmoi.Length <= 20){
				SendData.onChangeName (tenmoi);
                GetComponent<UIPopUp>().HideDialog();
			}else{
                PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Tên phải nhiều hơn 4 và ít hơn 20 kí tự.");
			}
		} else {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("","Nhập với tên mới.");
		}
	}
}
