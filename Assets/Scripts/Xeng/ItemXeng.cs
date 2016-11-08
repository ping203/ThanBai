using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemXeng : MonoBehaviour {
    [HideInInspector]
    public int id;// 1-8 cua thuong, 9 - lucky, 10-17 cua lon

    //UI
    public Image img_shadow, img_dot, img_effect;

    public void setEffect(bool isSet) {
        img_shadow.gameObject.SetActive(!isSet);
        img_dot.gameObject.SetActive(isSet);
    }

    public void setFinish() {
        img_effect.gameObject.SetActive(true);
        setEffect(true);
    }

    public void reset() {
        img_shadow.gameObject.SetActive(true);
        img_dot.gameObject.SetActive(false);
        img_effect.gameObject.SetActive(false);
    }
}
