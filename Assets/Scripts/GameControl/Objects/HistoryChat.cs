using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class HistoryChat : MonoBehaviour {
    List<string> listChat = new List<string>();
    //public UIScrollView scrollView;
    public Text lb_chat;
	// Use this for initialization
	void Start () {
        lb_chat.text = "";
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void addChat(string name, string mess, bool isMe)
    {
        string txt = name + ": " + mess;
        listChat.Add(txt + "\n\n");

        lb_chat.text = "";
        if (listChat.Count >=20)
        {
            listChat.RemoveAt(0);
        }
        for (int i = 0; i < listChat.Count; i++ )
        {
            lb_chat.text = lb_chat.text + listChat[i];
        }
        //lb_chat.ResizeCollider();
       // scrollView.ResetPosition();
       // scrollView.SetDragAmount(1, 1, false);
    }
}
