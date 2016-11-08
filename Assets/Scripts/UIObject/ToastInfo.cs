using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToastInfo : MonoBehaviour {
    public Text txt_info, txt_info2;
    public float width {
        get { return img.rectTransform.rect.width; }
    }
    public float height {
        get { return img.rectTransform.rect.height; }
    }
    public Image img;

    public void setInfo(string str) {
        txt_info.text = str;
        txt_info2.text = str;
    }
}
