using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelMessage : PanelGame {
	public Text textTu, textLuc, textNoiDung;

    public void setTextMail (string tu, string luc, string noidung) {
		textTu.gameObject.SetActive (true);
        textLuc.gameObject.SetActive(true);
        textTu.text = tu;
		textLuc.text = luc;
		textNoiDung.text = noidung;
	}

    public void setTextSK (string content) {
		textTu.gameObject.SetActive (false);
		textLuc.gameObject.SetActive (false);
        textNoiDung.text = content;
	}
}
