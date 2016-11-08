using UnityEngine;
using System.Collections;
using AppConfig;
using UnityEngine.UI;

public class LoadText : MonoBehaviour {

    public string key;
	// Use this for initialization
	void Start () {
        GetKey();
	}

    void GetKey() {
        GetComponent<Text>().text = ClientConfig.Language.GetText(key);
    }
}
