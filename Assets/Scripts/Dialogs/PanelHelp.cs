using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PanelHelp : PanelGame {
    public Toggle[] list_toggle;
    public GameObject[] list_panel;

    // Use this for initialization
    void Start() {
        for (int i = 0; i < list_toggle.Length; i++) {
            GameObject obj = list_panel[i];
            list_toggle[i].onValueChanged.AddListener(delegate {
                onChangetg(obj);
            });
        }
        list_toggle[0].isOn = true;
        onChangetg(list_panel[0]);
    }

    void onChangetg(GameObject obj) {
        for (int i = 0; i < list_panel.Length; i++) {
            list_panel[i].SetActive(false);
            if (obj == list_panel[i]) {
                list_panel[i].SetActive(true);
            }
        }
    }

    [SerializeField]
    ScrollRect scrollRect;
    [SerializeField]
    float pos = 0;//, posR;
    public void onClickScroll(bool isLeft) {
        if (isLeft) {
            pos -= 0.1f;
        } else {
            pos += 0.1f;
        }
        if (pos >= 1f) {
            pos = 1f;
        }
        if(pos <= 0) {
            pos = 0;
        }
        scrollRect.horizontalNormalizedPosition = pos;
    }
}
