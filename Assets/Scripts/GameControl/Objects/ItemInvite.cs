using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ItemInvite : MonoBehaviour {
    public Text txt_name, txt_money;
    public string full_name {
        set; get; }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void setText(string name, long money) {
        txt_name.text = name;
        txt_money.text = BaseInfo.formatMoney(money);
    }
}
