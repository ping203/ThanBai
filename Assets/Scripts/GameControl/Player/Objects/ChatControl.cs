using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ChatControl : MonoBehaviour {
    // [SerializeField]
    // GameObject txt_chat_prefab, icon_chat_prefab;
    [SerializeField]
    ScrollRect scroll;
    [SerializeField]
    Transform parent_chat;
    [SerializeField]
    InputField ip_chat;
    List<GameObject> list = new List<GameObject>();

    [SerializeField]
    Transform parentSmile;
    //[SerializeField]
    //GameObject btnSmile;

    [SerializeField]
    GameObject chatSmile;

    private string[] smileName = new string[28] { "a1", "a2", "a3", "a4", "a5",
        "a6", "a7", "a8", "a9", "a10", "a11", "a12", "a13", "a14", "a15",
        "a16", "a17", "a18", "a19", "a20", "a21", "a22", "a23", "a24",
        "a25", "a26", "a27", "a28"};
    private string[] smileys = new string[28] { ":(", ";)", ":D", ";;)", ">:D<", ":-/",
        ":x", ":-O", "X(", ":>", ":-S", "#:-S", ">:)", ":(|", ":))", ":|",
        "/:)", "=;", "8-|", ":-&", ":-$", "[-(", "(:|", "=P~", ":-?",
        "=D>", "@-)", ":-<" };

    Dictionary<string, string> emoticons = new Dictionary<string, string>();
    void Awake() {
        StartCoroutine(loadAvata());
    }

    IEnumerator loadAvata() {
        yield return new WaitForEndOfFrame();
        LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Item_Chat_Smile", (btn) => {
            SetImage(btn);
        });
    }

    void SetImage(GameObject btn) {
        for (int i = 0; i < Res.EMOTION_COUNT; i++) {
            GameObject obj = Instantiate(btn) as GameObject;
            obj.transform.SetParent(parentSmile);
            obj.transform.localScale = Vector3.one;
            obj.name = "" + i;
            LoadAssetBundle.LoadSprite(obj.GetComponent<Button>().image, Res.AS_UI, "a" + (i+1));
            obj.GetComponent<Button>().onClick.AddListener(delegate {
                sendSmile(obj);
            });
        }
    }

    ChatControl() {
        for (int i = 0; i < smileName.Length; i++) {
            emoticons.Add(smileys[i], smileName[i]);
        }
    }

    internal void setText(string nick, string content) {
        string temp;
        bool check = emoticons.TryGetValue(content, out temp);
        //GameObject obj;
        if (list.Count >= 10) {
            Destroy(list[0]);
            list.RemoveAt(0);
        }
        if (nick.Length > 12) {
            nick = nick.Substring(0, 12);
        }
        if (check) {
            //obj = Instantiate(icon_chat_prefab) as GameObject;
            LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Chat_Smile", (prefabAB) => {
                GameObject obj = Instantiate(prefabAB) as GameObject;
                obj.GetComponent<Text>().text = nick + ":";

                LoadAssetBundle.LoadSprite(obj.GetComponentInChildren<Image>(), Res.AS_UI, temp);
                obj.transform.SetParent(parent_chat);
                obj.transform.localScale = Vector3.one;
                list.Add(obj);
            });
        } else {
            LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Chat_Text", (prefabAB) => {
                GameObject obj = Instantiate(prefabAB) as GameObject;
                obj.GetComponent<Text>().text = nick + ": " + content;

                obj.transform.SetParent(parent_chat);
                obj.transform.localScale = Vector3.one;
                list.Add(obj);
            });

        }
        //scroll.verticalNormalizedPosition = 0;
        scroll.verticalScrollbar.value = 0;
        ip_chat.text = "";
        StartCoroutine(wait());
    }

    IEnumerator wait() {
        yield return new WaitForSeconds(0.1f);
        scroll.verticalScrollbar.value = 0;
    }

    public void sendText() {
        SoundManager.instance.startClickButtonAudio();
        string str = ip_chat.text;
        if (str.Equals(""))
            return;

        SendData.onSendMsgChat(str);
    }

    public void groupSmile() {
        chatSmile.SetActive(true);
    }

    public void sendSmile(GameObject index) {
        SoundManager.instance.startClickButtonAudio();
        int i = int.Parse(index.name);
        string text = Chat.smileys[i];
        SendData.onSendMsgChat(text);
        chatSmile.SetActive(false);
    }

    public void clearList() {
        foreach (GameObject obj in list) {
            Destroy(obj);
            list.Remove(obj);
        }
    }
}
