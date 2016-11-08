using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class NPCController : PanelGame {
    public static NPCController instance;
    [HideInInspector]
    public int id = 0;
    [HideInInspector]
    public string name_player = "";

    //public GameObject gourp_player;
    public GameObject btn_duoi;
    //public Text displayName;
    //public Text userIdTxt;
    //public Text xuTxt;
    //public Text levelTxt;
    //public Text winTxt;
    //public Text loseTxt;
    //public Image avatarImg;
    //public RawImage avatarRaw;
    //public GameObject objMain;
    //WWW www;

    [HideInInspector]
    public Toggle tg;

    void Awake() {
        instance = this;
    }

    //public void UpdateInfo(string displayname, long id, long xu, int level, string win, string lose, string linkavatar, int idavatar) {
        //displayName.text = displayname;
        //userIdTxt.text = id.ToString();
        //xuTxt.text = xu.ToString();
        //levelTxt.text = level.ToString();
        //winTxt.text = "Thắng: " + win + "(Ván)";
        //loseTxt.text = "Thắng: " + lose + "(Ván)";
        //if (linkavatar.Equals(""))
        //{
        //    avatarImg.gameObject.SetActive(true);
        //    avatarRaw.gameObject.SetActive(false);
        //    LoadAssetBundle.LoadSprite(avatarImg, Res.AS_AVATA, "" + idavatar);
        //}
        //else
        //{
        //    avatarImg.gameObject.SetActive(false);
        //    avatarRaw.gameObject.SetActive(true);
        //    StartCoroutine(GetAvatar(linkavatar));
        //}
    //}

    //private IEnumerator GetAvatar(string link) {
    //    www = new WWW(link);
    //    yield return www;
    //    avatarRaw.texture = www.texture;
    //}

    public void onClick(string action) {
        switch (action) {
            //case "info":
            //    info();
            //    break;
            case "Kick":
                kick();
                break;
            case "Bia":
                actionInGame(1);
                break;
            case "Bua":
                actionInGame(2);
                break;
            case "CaChua":
                actionInGame(3);
                break;
            case "Chan":
                actionInGame(4);
                break;
            case "Dep":
                actionInGame(5);
                break;
        }
        GetComponent<UIPopUp>().HideDialog();
    }
    public void setShowKick(bool isShow) {
        btn_duoi.SetActive(isShow);
    }

    private void info() {
        SendData.onViewInfoFriend(name_player);
        tg.isOn = false;
    }
    private void kick() {
        SendData.onKick(name_player);
        tg.isOn = false;
    }

    private void actionInGame(int index) {
        SendData.onBuyItem(index, BaseInfo.gI().mainInfo.nick, name_player);
        tg.isOn = false;
    }

    //private void actionNem(int id) {
    //    //sua
    //    ABSUser player1 = GameControl.instance.currentCasino.players[0];

    //    float distance = Vector2.Distance(player1.transform.position, tg.transform.position);
    //    float time = distance / 200;
    //    // LoadAssetBundle.LoadPrefab("prefabs")
    //    GameObject obj = Instantiate(GameControl.instance.gameObj_Actions_InGame[id - 1]) as GameObject;

    //    obj.transform.parent = player1.transform.parent;
    //    obj.transform.localPosition = player1.transform.localPosition;
    //    obj.transform.localScale = new Vector3(1, 1, 1);

    //    obj.transform.DOLocalMove(tg.transform.localPosition, time).OnComplete(delegate {
    //        finish(obj);
    //    });
    //}
    void finish(GameObject ob) {
        ob.GetComponent<Animator>().SetTrigger("action");
        Destroy(ob, 3f);
    }

    public void Close() {
        //objMain.transform.DOLocalMoveX(1000, 0.4f);
    }
}
