using UnityEngine;
using System.Collections;

public class CloseThongBao : MonoBehaviour {

    public void CloseMessage() {
        GetComponent<UIPopUp>().HideDialog();
    }
}
