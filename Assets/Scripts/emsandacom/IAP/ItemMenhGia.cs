using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemMenhGia : MonoBehaviour {

    public Text menhgiaTxt;

    public void UpdateMenhGia(string menhgia) {
        menhgiaTxt.text = menhgia;
    }
}
