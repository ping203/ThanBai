using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleActive : MonoBehaviour {
    Toggle tg;

    public GameObject OBJ;
    public bool isStart;
    // Use this for initialization
    void Awake() {
        tg = GetComponent<Toggle>();
        if (tg != null)
            tg.onValueChanged.AddListener(change);
        if (OBJ != null)
            OBJ.SetActive(isStart);
    }
    void Update() {
    }

    void change(bool isCheck) {
        if (OBJ != null)
            OBJ.SetActive(isCheck);
    }
}
