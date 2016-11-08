using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using AppConfig;
using Facebook.Unity;

public class LobbyViewScript : MonoBehaviour
{
    public static LobbyViewScript instance;
    public Text text_noti;
    const float posXDefault = 200.0f;
    public Text displayName;
    public Text displayXu;
    WWW www;
    public Image imgAvata;
    public RawImage rawAvata;
    public GameObject objTop;
    public GameObject objBot;
    public Image bg_change_scene;
    public Transform objListGame;
    private List<GameObject> listGame = new List<GameObject>();
    public Text idTxt;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        UnloadScene(Res.MAIN_NAME);

        StartCoroutine(UpDateInfo());
        string title = "";
        //Debug.LogError("LINK " + BaseInfo.gI().msgAlert[0].mess);
        if (BaseInfo.gI().msgAlert.Count > 0 && !string.IsNullOrEmpty(BaseInfo.gI().msgAlert[0].mess))
            title = BaseInfo.gI().msgAlert[0].mess;
        else
            title = ClientConfig.Language.GetText("noti");
        SetNoti(title);
        StartCoroutine(LoadListGame(1f));
    }

    private void UnloadScene(string name) {
        List<string> listScene = new List<string>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (!SceneManager.GetSceneAt(i).name.Equals(name))
            {
                if (!listScene.Contains(SceneManager.GetSceneAt(i).name))
                    listScene.Add(SceneManager.GetSceneAt(i).name);
            }
        }

        for (int i = 0; i < listScene.Count; i++)
        {
            SceneManager.UnloadScene(listScene[i]);
        }
    }

    private IEnumerator LoadListGame(float time)
    {
        yield return new WaitForSeconds(time);
        LoadAssetBundle.LoadPrefab("prefabs", "ItemGame", (obj) =>
        {
            for (int i = 0; i < 9; i++)
            {
                GameObject objGame = Instantiate(obj);
                objGame.transform.SetParent(objListGame);
                objGame.GetComponent<ItemGame>().UpdateIcon(Res.idGame[i], Res.spriteName[i], i);
                objGame.transform.localScale = Vector3.zero;
                if (!listGame.Contains(objGame))
                    listGame.Add(objGame);

                if (i == 8)
                {
                    StartCoroutine(ScaleListGame());
                }
            }
        });
    }

    private IEnumerator ScaleListGame() {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < listGame.Count; i++)
        {
            yield return new WaitForSeconds(.1f);
            listGame[i].transform.DOScale(1, 0.2f);
        }
    }

    public void ClickIconGame(int id, int index) {
        objTop.transform.DOLocalMoveY(600, 1f);
        //Debug.LogError("ID " + id);
        StartCoroutine(LoadRoom(id, index));
        objBot.transform.DOLocalMoveY(-600, 1f);
    }

    private IEnumerator LoadRoom(int id, int index) {
        if (index < 5)
        {
            for (int i = 0; i < 5; i++)
            {
                if (listGame[i] != listGame[index])
                {
                    yield return new WaitForSeconds(.1f);
                    listGame[i].transform.DOScale(0, 0.2f);
                }
                if (i == 4)
                    LoadAssetBundle.LoadScene(Res.ROOM_AB, Res.ROOM_NAME);
            }
        }
        else
        {
            for (int i = listGame.Count - 1; i >= 4; i--)
            {
                if (listGame[i] != listGame[index])
                {
                    yield return new WaitForSeconds(.1f);
                    listGame[i].transform.DOScale(0, 0.2f);
                }
                if (i == 4)
                    LoadAssetBundle.LoadScene(Res.ROOM_AB, Res.ROOM_NAME);
            }
        }
        PlayerPrefs.SetInt("gameid", id);
        PlayerPrefs.Save();
        //SendData.onSendGameID((sbyte)id);
    }

    public void SetNoti(string str)
    {
        if (!str.Equals(""))
        {
            text_noti.text = str.Trim();

            float w = LayoutUtility.GetPreferredWidth(text_noti.rectTransform);
            text_noti.transform.localPosition = new Vector3(posXDefault, 0, 0);
            float posEnd = -posXDefault - w - 50;

            float time = (posXDefault - posEnd) / 50;
            text_noti.transform.DOKill();
            text_noti.transform.DOLocalMoveX(posEnd, time).SetLoops(-1).SetEase(Ease.Linear);
            //PlayerPrefs.SetString("noti", str);
            //PlayerPrefs.Save();
            RoomViewScript.str_noti = str.Trim();
        }
    }

    private IEnumerator UpDateInfo()
    {
        yield return new WaitForEndOfFrame();
        UpdateProfileUser();
    }

    bool isOne = false;
    public void UpdateProfileUser()
    {
        string dis = "";
        if (!string.IsNullOrEmpty(BaseInfo.gI().mainInfo.displayname))
            dis = BaseInfo.gI().mainInfo.displayname;
        else
            dis = BaseInfo.gI().mainInfo.nick;
        if (dis.Length > 10)
        {
            dis = dis.Substring(0, 10) + "...";
        }
        displayName.text = dis.ToUpper();
        int idAvata = BaseInfo.gI().mainInfo.idAvata;
        if (idAvata <= 1) {
            idAvata = 1;
        } else if (idAvata >= 60) {
            idAvata = 60;
        }
        //idAvata++;
        string link_avata = BaseInfo.gI().mainInfo.link_Avatar;
        int num_star = BaseInfo.gI().mainInfo.level_vip;
        idTxt.text = BaseInfo.gI().mainInfo.userid.ToString();
        displayXu.text = BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyXu) + Res.MONEY_VIP;

        www = null;
        if (link_avata != "")
        {
            StartCoroutine(GetAvata(link_avata));
        }
        else if (idAvata > 0)
        {
            imgAvata.gameObject.SetActive(true);
            rawAvata.gameObject.SetActive(false);
            LoadAssetBundle.LoadSprite(imgAvata, Res.AS_AVATA, idAvata.ToString());//Res.list_avata[idAvata + 1];
        }
    }

    private IEnumerator GetAvata(string link)
    {
        WWW www = new WWW(link);
        yield return www;
        imgAvata.gameObject.SetActive(false);
        rawAvata.gameObject.SetActive(true);
        rawAvata.texture = www.texture;
        www.Dispose();
        www = null;
    }

    public void ClickSetting()
    {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene(Res.SETTING_AB, Res.SETTING_NAME);
    }

    public void ClickAvatar()
    {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene(Res.INFOPLAYER_AB, Res.INFOPLAYER_NAME, () =>
        {
            PanelInfoPlayer.instance.InfoMe();
        });
    }

    public void ClickChatAdmin() {
        LoadAssetBundle.LoadScene(Res.CHATADMIN_AB, Res.CHATADMIN_NAME);
    }

    public void ClickNapTien() {
        LoadAssetBundle.LoadScene(Res.ADDCOIN_AB, Res.ADDCOIN_NAME);
    }

    public void ClickLuatChoi() {
        LoadAssetBundle.LoadScene(Res.HELP_AB, Res.HELP_NAME);
    }

    public void ClickFanpage() {

    }

    public void ClickDoiThuong() {
        LoadAssetBundle.LoadScene(Res.DOITHUONG_AB, Res.DOITHUONG_NAME, ()=> { SendData.onGetInfoGift(); });
    }

    public void BackToLogin()
    {
        SoundManager.instance.startClickButtonAudio();
        PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", ClientConfig.Language.GetText("lobby_quit"), () =>
        {
            //FB.LogOut();
            NetworkUtil.GI().close();
            bg_change_scene.gameObject.SetActive(true);
            LoadAssetBundle.LoadScene(Res.LOGIN_AB, Res.LOGIN_NAME);
        });
        
        //StartCoroutine(WaitToFade(1f));
    }

    //private IEnumerator WaitToFade(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    bg_change_scene.DOFade(0, 1).OnComplete(FadeBgChangeSceneFinish);
    //}

    //private void FadeBgChangeSceneFinish()
    //{
    //    bg_change_scene.gameObject.SetActive(false);
    //}
}
