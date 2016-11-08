using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class PanelChat : PanelGame {
    public static PanelChat instance;
    public GameObject tblSmile;
    public GameObject btnSmile;

    public GameObject tblText;
    public GameObject btnText;

    public InputField textChat;

    public static string[] textChats = { "Bạn ơi, đánh nhanh lên được không?", "Bắt đầu đi.",
        "Sẵn sàng đi", "Cho tớ chơi với, tớ hứa sẽ chơi ngoan!",
        "Thấy tớ đánh siêu chưa?", " Các cậu sợ tớ chưa? Heehe",
        "Tăng tiền cược lên bạn nhé?",
        "Thắng ván này tớ mời cậu đi xxx luôn.",
        "Cậu khóa bàn lại để chiến tay bo đi!", "Chết mày nè!", "Ảo vl...",
        "Huhu, sao đen đủi vậy...:(", "Chơi nhỏ chán quá!",
        "Mày hả bưởi...:D", "Tất tay đi nào!", "Đánh hay ghê!",
        "Mạng lag quá, bạn thông cảm nhé!", "Cho đánh với nào!"};
    Vector3 vtHide = new Vector3(0, -520, 0);
    // Use this for initialization
    void Awake() {
        instance = this;
        for (int i = 0; i < Res.EMOTION_COUNT; i++) {
            GameObject btn = Instantiate(btnSmile) as GameObject;
            btn.transform.SetParent(tblSmile.transform);
            btn.transform.localScale = Vector3.one;
            //btn.GetComponent<Button>().image.sprite = Res.getSmileByName("a" + (i + 1));
            LoadAssetBundle.LoadSprite(btn.GetComponent<Button>().image, Res.AS_UI, "a" + (i + 1));
            btn.name = "" + i;
            btn.GetComponent<Button>().onClick.AddListener(delegate {
                ClickSmile(btn);
            });
        }

        for (int i = 0; i < textChats.Length; i++) {
            GameObject btnT = Instantiate(btnText) as GameObject;
            btnT.transform.SetParent(tblText.transform);
            btnT.transform.localScale = Vector3.one;
            btnT.transform.FindChild("Text").GetComponent<Text>().text = textChats[i];
            btnT.name = "" + i;
            btnT.GetComponent<Button>().onClick.AddListener(delegate {
                ClickText(btnT);
            });
        }
    }

    public void sendChatQuick() {
        SoundManager.instance.startClickButtonAudio();
        string text = textChat.text;
        if (!text.Equals("")) {
            SendData.onSendMsgChat(text);
            textChat.text = "";
            onHide();
        }
    }

    void ClickSmile(GameObject index) {
        SoundManager.instance.startClickButtonAudio();

        int i = int.Parse(index.name);

        string text = Chat.smileys[i];
        SendData.onSendMsgChat(text);
        onHide();
    }

    void ClickText(GameObject index) {
        SoundManager.instance.startClickButtonAudio();
        SendData.onSendMsgChat(textChats[int.Parse(index.name)]);
        onHide();
    }

    public override void onShow() {
        group.transform.DOLocalMoveY(vtHide.y + 400, 0.6f);
        base.onShow();
    }

    public override void onHide() {
        group.transform.DOLocalMoveY(vtHide.y, 0.2f).OnComplete(delegate {
            base.onHide();
        });
    }
}
