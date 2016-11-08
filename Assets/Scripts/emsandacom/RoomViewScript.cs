using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using DG.Tweening;
using AppConfig;

public class RoomViewScript : MonoBehaviour
{
    public static RoomViewScript instance;
    public Text displayName;
    public Text lb_id;
    public Text displayXu;
    public Text displayFree;
    public Image imgAvata;
    public RawImage rawAvata;
    public List<TableItem> listTable = new List<TableItem>();
    public List<long> listMucCuoc = new List<long>();
    public GameObject objListRoom;
    public GameObject objTop;
    public GameObject objBot;
    public Image logoImg;
    MyScrollView myScrollView;
    public GameObject objScrollTable;
    public GameObject objListMucCuoc;
    public List<TableItem> listTableAfterFilted = new List<TableItem>();
    public List<GameObject> listObjTable = new List<GameObject>();
    public Text notiTxt;
    //public bool isTableBiggerThan0 = false;
    //public int indexIncrease = 0;
    //public int indexDecrease = 0;
    //public GameObject objFirsTopTable;
    //public GameObject objLastBotTable;
    //private float positionX = 482;
    public Image bg_change_scene;
    const float posXDefault = 200.0f;
    private int currentGameID;
    private GameObject objCurrentMC;
    string scene_ab;
    string scene_name;

    public static string str_noti;
    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        bg_change_scene.gameObject.SetActive(false);
        //string noti = PlayerPrefs.GetString("noti", ClientConfig.Language.GetText("noti"));
        SetNoti(str_noti);
        currentGameID = PlayerPrefs.GetInt("gameid", -1);
        UnloadScene(Res.ROOM_NAME);
        //Debug.LogError("Game Id " + gameId);
        if (currentGameID == -1)
        {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", ClientConfig.Language.GetText("room_gameid_khong_ton_tai"), () => { BackToLobby(); });
            return;
        }
        scene_ab = ClientConfig.Language.GetText(currentGameID + "_ab");
        scene_name = ClientConfig.Language.GetText(currentGameID + "_name");
        SendData.onSendGameID((sbyte)currentGameID);
        myScrollView = objScrollTable.GetComponent<MyScrollView>();
        StartCoroutine(UpDateInfo());
        StartCoroutine(SetGameName(currentGameID));
        //StartCoroutine(LoadTables());
    }



    private void UnloadScene(string name)
    {
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

    private IEnumerator UpDateInfo()
    {
        yield return new WaitForEndOfFrame();
        UpdateProfileUser();
    }

    public void UpdateProfileUser()
    {
        string dis = BaseInfo.gI().mainInfo.displayname;
        if (dis.Length > 10)
        {
            dis = dis.Substring(0, 10) + "...";
        }
        displayName.text = dis.ToUpper();
        int idAvata = BaseInfo.gI().mainInfo.idAvata;
        if (idAvata <= 1)
        {
            idAvata = 1;
        }
        else if (idAvata >= 60)
        {
            idAvata = 60;
        }
        //idAvata++;
        string link_avata = BaseInfo.gI().mainInfo.link_Avatar;
        int num_star = BaseInfo.gI().mainInfo.level_vip;

        displayXu.text = BaseInfo.formatMoneyNormal(BaseInfo.gI().mainInfo.moneyXu) + Res.MONEY_VIP_UPPERCASE;
        lb_id.text = BaseInfo.gI().mainInfo.userid.ToString();
        // www = null;
        if (link_avata != "")
        {
            //www = new WWW(link_avata);
            //isOne = false;
            //imgAvata.gameObject.SetActive(false);
            //rawAvata.gameObject.SetActive(true);
            StartCoroutine(GetAvata(link_avata));
        }
        else if (idAvata > 0)
        {
            imgAvata.gameObject.SetActive(true);
            rawAvata.gameObject.SetActive(false);
            // imgAvata.sprite = Res.getAvataByID(idAvata);//Res.list_avata[idAvata + 1];
            LoadAssetBundle.LoadSprite(imgAvata, Res.AS_AVATA, "" + idAvata);
        }
    }

    IEnumerator GetAvata(string link)
    {
        WWW www = new WWW(link);
        yield return www;
        imgAvata.gameObject.SetActive(false);
        rawAvata.gameObject.SetActive(true);
        rawAvata.texture = www.texture;
        www.Dispose();
        www = null;
    }

    private IEnumerator LoadTables()
    {
        for (int i = 0; i < objListRoom.transform.childCount; i++)
        {
            objListRoom.transform.GetChild(i).transform.DOScale(1, 0.4f);
            yield return new WaitForSeconds(.1f);
        }
    }

    List<TableItem> LocBan(long mucCuoc)
    {
        List<TableItem> list = new List<TableItem>();
        for (int i = 0; i < listTable.Count; i++)
        {
            if (listTable[i].money == mucCuoc)
            {
                list.Add(listTable[i]);
            }
        }
        return list;
    }

    List<long> LocMucCuoc(List<TableItem> tables)
    {
        List<long> list = new List<long>();
        long m = tables[0].money;
        if (m > 0)
            list.Add(m);
        for (int i = 1; i < tables.Count; i++)
        {
            if (m != tables[i].money && tables[i].money > 0)
            {
                m = tables[i].money;
                if (!list.Contains(m))
                    list.Add(m);
            }
        }
        return list;
    }

    public void InstantiateMucCuoc()
    {
        listMucCuoc.Clear();
        listMucCuoc.AddRange(LocMucCuoc(listTable));
        listMucCuoc.Sort();
        //Debug.LogError("listMucCuoc count " + listMucCuoc.Count);
        LoadAssetBundle.LoadPrefab("prefabs", "ItemMucCuoc", (obj) =>
        {
            for (int i = 0; i < listMucCuoc.Count; i++)
            {
                GameObject objMC = Instantiate(obj);
                objMC.transform.SetParent(objListMucCuoc.transform);
                objMC.transform.localPosition = Vector3.zero;
                objMC.transform.localScale = Vector3.one;
                //Debug.LogError("Muc Cuoc " + listMucCuoc[i]);
                if (i == 0)
                {
                    objMC.transform.localScale = new Vector3(1.4f, 1.4f, 0);
                    objCurrentMC = objMC;
                    //objCurrentMC.GetComponent<Text>().color =  new Color32(255, 189, 47, 255);
                }
                //if (i > 0 && i < listMucCuoc.Count - 1)
                objMC.GetComponent<ItemMucCuoc>().UpdateMucCuoc(listMucCuoc[i]);

                //Debug.LogError("Muc Cuoc " + i + " " + listMucCuoc[i]);
            }
        });
    }

    public bool isClickMC = false;
    public long currentMC;
    public void ClickMucCuoc(GameObject obj, long muccuoc)
    {
        objCurrentMC.transform.DOScale(1, 0.1f);
        objCurrentMC.GetComponent<Text>().color = new Color32(255, 255, 255, 255);
        SoundManager.instance.startClickButtonAudio();
        if (objListRoom.transform.childCount > 0)
            myScrollView.ClearCells();
        PopupAndLoadingScript.instance.ShowLoading();
        //Debug.LogError("Muc Cuoc " + muccuoc);
        isClickMC = true;
        currentMC = muccuoc;
        objCurrentMC = obj;
        countScale = 0;
        SendData.onJoinRoom(BaseInfo.gI().typetableLogin);
    }

    public void MoveTopFinish()
    {
        InstantiateTables(listMucCuoc[0]);
    }

    public void InstantiateTables(long muccuoc)
    {
        if (objListRoom.transform.childCount > 0)
        {
            myScrollView.ClearCells();
        }
        GameObject objTable = GameObject.Find("ItemTable4(Clone)");
        Destroy(objTable);
        listTableAfterFilted = LocBan(muccuoc);
        //isTableBiggerThan0 = listTableAfterFilted.Count > 0;
        //Debug.LogError("listTableAfterFilted " + listTableAfterFilted.Count);
        if (currentGameID == GameID.PHOM || currentGameID == GameID.TLMN || currentGameID == GameID.MAUBINH || currentGameID == GameID.XAM)
        {
            LoadAssetBundle.LoadPrefab("prefabs", "ItemTable4", (obj) =>
            {
                myScrollView.OnStartFillItem(obj, listTableAfterFilted.Count);
                myScrollView.UpdateInfo = UpdateTable;
                PopupAndLoadingScript.instance.HideLoading();
            });
        }
        else if (currentGameID == GameID.XOCDIA)
        {
            LoadAssetBundle.LoadPrefab("prefabs", "ItemTable9", (obj) =>
            {
                myScrollView.OnStartFillItem(obj, listTableAfterFilted.Count);
                myScrollView.UpdateInfo = UpdateTable;
                PopupAndLoadingScript.instance.HideLoading();
            });
        }
        else
        {
            LoadAssetBundle.LoadPrefab("prefabs", "ItemTable5", (obj) =>
            {
                myScrollView.OnStartFillItem(obj, listTableAfterFilted.Count);
                myScrollView.UpdateInfo = UpdateTable;
                PopupAndLoadingScript.instance.HideLoading();
            });
        }
        LoadSceneGame(scene_ab, scene_name);
    }

    private void LoadSceneGame(string scene_ab, string scene_name)
    {
        LoadAssetBundle.LoadScene(scene_ab, scene_name, () =>
        {
            for (int i = 0; i < SceneManager.GetSceneByName(scene_name).rootCount; i++)
            {
                if (SceneManager.GetSceneByName(scene_name).GetRootGameObjects()[i].name.Equals("Canvas"))
                {
                    SceneManager.GetSceneByName(scene_name).GetRootGameObjects()[i].transform.GetChild(0).gameObject.SetActive(false);
                    break;
                }
            }
        });
    }

    int countScale = 0;
    private void UpdateTable(GameObject obj, int index)
    {
        listObjTable.Add(obj);
        //Debug.LogError("Current Game ID " + currentGameID);
        if (currentGameID == GameID.PHOM || currentGameID == GameID.TLMN || currentGameID == GameID.MAUBINH || currentGameID == GameID.XAM)
        {
            ItemTable4 itemTable = obj.GetComponent<ItemTable4>();
            itemTable.UpdateTable(listTableAfterFilted[index].id, listTableAfterFilted[index].money, listTableAfterFilted[index].needMoney, listTableAfterFilted[index].nUser, listTableAfterFilted[index].maxUser);
        }
        else if (currentGameID == GameID.XOCDIA)
        {
            ItemTable9 itemTable = obj.GetComponent<ItemTable9>();
            itemTable.UpdateTable(listTableAfterFilted[index].id, listTableAfterFilted[index].money, listTableAfterFilted[index].needMoney, listTableAfterFilted[index].nUser, listTableAfterFilted[index].maxUser);
        }
        else
        {
            ItemTable5 itemTable = obj.GetComponent<ItemTable5>();
            itemTable.UpdateTable(listTableAfterFilted[index].id, listTableAfterFilted[index].money, listTableAfterFilted[index].needMoney, listTableAfterFilted[index].nUser, listTableAfterFilted[index].maxUser);
        }

        if (countScale == 0)
            StartCoroutine(ScaleTable(1.2f, obj));
        else
        {
            StartCoroutine(ScaleTable(1f, obj));
        }
    }

    private IEnumerator ScaleTable(float value, GameObject obj)
    {
        countScale++;
        yield return new WaitForSeconds(.6f);
        if (obj != null)
            obj.transform.DOScale(value, 0.4f);
    }

    public void BackToLobby()
    {
        SoundManager.instance.startClickButtonAudio();
        objTop.transform.DOLocalMoveY(600, 0.4f);
        //StartCoroutine(LoadLobby());
        objBot.transform.DOLocalMoveY(-600, 0.4f).OnComplete(LoadLobby);
    }

    private void LoadLobby()
    {
        //bg_change_scene.gameObject.SetActive(true);
        LoadAssetBundle.LoadScene(Res.MAIN_AB, Res.MAIN_NAME);
        //for (int i = 0; i < objListRoom.transform.childCount; i++)
        //{
        //    objListRoom.transform.GetChild(i).transform.DOScale(0, 0.4f);
        //    yield return new WaitForSeconds(.1f);
        //    if (i == objListRoom.transform.childCount - 1)
        //    {
        //        LoadAssetBundle.LoadScene(Res.MAIN_AB, Res.MAIN_NAME);
        //    }
        //}
    }

    public void ClickSetting()
    {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene(Res.SETTING_AB, Res.SETTING_NAME);
    }

    public IEnumerator SetGameName(int gameId)
    {
        yield return new WaitForEndOfFrame();
        //Debug.LogError("Game ID " + gameId);
        LoadAssetBundle.LoadSprite(logoImg, Res.AS_UI, Res.logoName[gameId],()=> {
            logoImg.SetNativeSize();
        });
    }

    public void SetNoti(string str)
    {
        if (!str.Equals(""))
        {
            notiTxt.text = str;

            float w = LayoutUtility.GetPreferredWidth(notiTxt.rectTransform);
            notiTxt.transform.localPosition = new Vector3(posXDefault, 0, 0);
            float posEnd = -posXDefault - w - 50;

            float time = (posXDefault - posEnd) / 50;
            notiTxt.transform.DOKill();
            notiTxt.transform.DOLocalMoveX(posEnd, time).SetLoops(-1).SetEase(Ease.Linear);
        }
    }

    public void ClickTable(int id, long blind, long needMoney, int maxuser, GameObject obj)
    {
        long moneyTemp = BaseInfo.gI().mainInfo.moneyXu;
        if (moneyTemp < needMoney)
        {
            PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", "Quý khách cần có ít nhât "
                    + BaseInfo.formatMoney(needMoney) + " " + " Xu"
                    + " để vào bàn! Quý khách muốn nạp thêm " + " Xu" + "?", delegate
                    {
                        LoadAssetBundle.LoadScene(Res.ADDCOIN_AB, Res.ADDCOIN_NAME);
                    });
        }
        else
        {
            BaseInfo.gI().numberPlayer = maxuser;
            obj.transform.DOScale(0, 0.2f);
            SendData.onJoinTablePlay(id, "", -1);
        }
    }

    public void ClickCreateTable()
    {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene(Res.CREATETABLE_AB, Res.CREATETABLE_NAME);
    }

    public void ClickToiBan()
    {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene(Res.TOIBAN_AB, Res.TOIBAN_NAME);
    }

    public void ClickChoiNgay()
    {
        SoundManager.instance.startClickButtonAudio();
        SendData.onAutoJoinTable();
    }

    public void LoadGame()
    {
        bg_change_scene.gameObject.SetActive(true);
        for (int i = 0; i < SceneManager.GetSceneByName(scene_name).rootCount; i++)
        {
            if (SceneManager.GetSceneByName(scene_name).GetRootGameObjects()[i].name.Equals("Canvas"))
            {
                SceneManager.GetSceneByName(scene_name).GetRootGameObjects()[i].transform.GetChild(0).transform.localScale = Vector3.one;
                SceneManager.GetSceneByName(scene_name).GetRootGameObjects()[i].transform.GetChild(0).gameObject.SetActive(true);
                break;
            }
        }
        GameControl.instance.StartGame(scene_name);
        //SceneManager.UnloadScene(Res.ROOM_NAME);
    }

    public void ClickAvatar()
    {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene(Res.INFOPLAYER_AB, Res.INFOPLAYER_NAME, () =>
        {
            PanelInfoPlayer.instance.InfoMe();
        });
    }
}

