using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class Chat : MonoBehaviour {
    public Text lblContent_left, lblContent_right;
    public Image spriteSmile;
    public GameObject chat_text_left, chat_text_right;
    private string[] smileName = new string[28] { "a1", "a2", "a3", "a4", "a5",
        "a6", "a7", "a8", "a9", "a10", "a11", "a12", "a13", "a14", "a15",
        "a16", "a17", "a18", "a19", "a20", "a21", "a22", "a23", "a24",
        "a25", "a26", "a27", "a28"};
    public static string[] smileys = new string[28] { ":(", ";)", ":D", ";;)", ">:D<", ":-/",
        ":x", ":-O", "X(", ":>", ":-S", "#:-S", ">:)", ":(|", ":))", ":|",
        "/:)", "=;", "8-|", ":-&", ":-$", "[-(", "(:|", "=P~", ":-?",
        "=D>", "@-)", ":-<" };

    Dictionary<string, string> emoticons = new Dictionary<string, string>();

    public Align align = Align.Left;
    Chat() {
        for (int i = 0; i < smileName.Length; i++) {
            emoticons.Add(smileys[i], smileName[i]);
        }
    }
    internal void setText(string content) {
        string temp;
        bool check = emoticons.TryGetValue(content, out temp);

        //transform.DOScale(1, 0.2f);
        if (check) {
            chat_text_left.SetActive(false);
            chat_text_right.SetActive(false);
            spriteSmile.gameObject.SetActive(true);
            // spriteSmile.sprite = Res.getSmileByName(temp);
            LoadAssetBundle.LoadSprite(spriteSmile, Res.AS_UI, temp);
            spriteSmile.transform.DOKill();
            spriteSmile.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            spriteSmile.transform.DOScale(1, 0.2f).SetLoops(-1);
        } else {
            //transform.DOKill();
            spriteSmile.gameObject.SetActive(false);
            chat_text_left.SetActive(false);
            chat_text_right.SetActive(false);
            string str = content;
            //if (content.Length > 25) {
            //    str = (content.Substring(0, 25) + "...");
            //}
            switch (align) {
                case Align.Left:
                    chat_text_left.SetActive(true);
                    lblContent_left.text = str;
                    break;
                case Align.Right:
                    chat_text_right.SetActive(true);
                    lblContent_right.text = str;
                    break;
            }
        }

        gameObject.SetActive(true);
        StopCoroutine(setInvisible());
        StartCoroutine(setInvisible());
    }

    IEnumerator setInvisible() {
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
    }
}
