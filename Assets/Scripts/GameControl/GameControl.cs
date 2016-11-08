using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.NetworkInformation;
using UnityEngine.UI;
using DG.Tweening;
using emsandacom.Popup;
using AppConfig;

public class GameControl : MonoBehaviour {
    public void sendSMS(string port, string content) {
        //#if UNITY_WP8
        //        UnityPluginForWindowPhone.Class1.sendSMS(port, content);
        //#else
        string str = content;
        if (content.Contains("#")) {
            str = content.Replace("#", "%23");
        }
        Application.OpenURL("sms:" + port + @"?body=" + str);

        //#endif
    }
    public static GameControl instance;

    public BaseCasino currentCasino;
    public StageControl currenStage;

    public bool cancelAllInvite = false;
    void Awake() {
        instance = this;
        Res.list_cards = Resources.LoadAll<Sprite>("Cards/cardall");
    }

    void Start() {
        
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", ClientConfig.Language.GetText("exit_game"), () => { Application.Quit(); });
        }
    }

    public bool isInfo = true;
    public void setCasino(int gameID, int type) {
        isInfo = false;
        switch (gameID) {
            case GameID.TLMN:
                currentCasino = FindObjectOfType<TLMN>();
                break;
            case GameID.XAM:
                currentCasino = FindObjectOfType<Xam>();
                break;
            case GameID.LIENG:
                if (type == 0) { // 5
                    currentCasino = FindObjectOfType<Lieng>();
                } else { // 9
                    //currentCasino = (BaseCasino)currenStage;
                }
                break;
            case GameID.BACAY:
                if (type == 0) { // 5
                    currentCasino = FindObjectOfType<Bacay>();
                    ;
                } else { // 9
                    //currentCasino = (BaseCasino)currenStage;
                }
                break;
            case GameID.PHOM:
                currentCasino = FindObjectOfType<PHOM>();
                break;
            case GameID.POKER:
                if (type == 0) { // 5
                    currentCasino = FindObjectOfType<Poker>();
                    ;
                }
                //    else { // 9
                //        currentCasino = (BaseCasino)currenStage;
                //    }
                break;
            case GameID.XITO:
                currentCasino = FindObjectOfType<Xito>();
                ;
                break;

            case GameID.MAUBINH:
                currentCasino = FindObjectOfType<MauBinh>();
                ;
                break;
            case GameID.XOCDIA:
                currentCasino = FindObjectOfType<XocDia>();
                ;
                break;
            default:
                break;
        }
        initCardType(gameID);
    }

    private void initCardType(int gameID) {
        // TODO Auto-generated method stub
        switch (gameID) {
            case GameID.LIENG:
            case GameID.BACAY:
            case GameID.PHOM:
            case GameID.XITO:
                Card.setCardType(0);
                break;
            default:
                Card.setCardType(1);
                break;
        }
    }

    public void StartGame(string scene_name) {
        //StartCoroutine(WaitForSeconds(scene_name));
        currentCasino.UnloadScene(scene_name);
        if (!BaseInfo.gI().isView)
        {
            Message jtbSuccessMess = BaseInfo.gI().OnJoinTableSuccess;
            currentCasino.onJoinTableSuccess(jtbSuccessMess);
            Message infoCardPlayerMess = BaseInfo.gI().InfoCardPlayerInTbl;
            currentCasino.InfoCardPlayerInTbl(infoCardPlayerMess);
        }
        else
        {
            Message jtbSuccessMess = BaseInfo.gI().OnJoinTableSuccess;
            currentCasino.onJoinView(jtbSuccessMess);
        }
    }

    IEnumerator WaitForSeconds(string scene_name) {
        yield return new WaitForSeconds(.2f);
        //if (currentCasino == null)
        //{
        //    StartCoroutine(WaitForSeconds(scene_name));
        //}
        //else
        //{
            
        //}
    }

    public void SetSecondHandler() {
        //Debug.LogError("SetSecondHandler " + BaseInfo.gI().gameID);
        switch (BaseInfo.gI().gameID) {
            case GameID.PHOM:
                Card.setCardType(0);
                BaseInfo.gI().nameTale = "Phỏm";
                ProcessHandler.getInstance().setSecondHandler(PHandler.getInstance());
                break;
            case GameID.TLMN:
                Card.setCardType(1);
                BaseInfo.gI().nameTale = "TLMN";
                ProcessHandler.getInstance().setSecondHandler(TLMNHandler.getInstance());
                break;
            //case GameID.TLMNsolo:
            //    Card.setCardType(1);
            //    BaseInfo.gI().nameTale = "Tiến Lên Miền Nam Solo";
            //    ProcessHandler.setSecondHandler(TLMNHandler.getInstance());
            //    break;
            case GameID.XAM:
                Card.setCardType(1);
                BaseInfo.gI().nameTale = "Sâm";
                ProcessHandler.getInstance().setSecondHandler(TLMNHandler.getInstance());
                break;
            case GameID.BACAY:
                Card.setCardType(0);
                BaseInfo.gI().nameTale = "Ba Cây";
                ProcessHandler.getInstance().setSecondHandler(TLMNHandler.getInstance());
                break;
            case GameID.XITO:
                Card.setCardType(0);
                BaseInfo.gI().nameTale = "Xì Tố";
                ProcessHandler.getInstance().setSecondHandler(XiToHandler.getInstance());
                break;
            case GameID.LIENG:
                Card.setCardType(0);
                BaseInfo.gI().nameTale = "Liêng";
                ProcessHandler.getInstance().setSecondHandler(XiToHandler.getInstance());
                break;
            case GameID.POKER:
                Card.setCardType(1);
                BaseInfo.gI().nameTale = "Poker";
                ProcessHandler.getInstance().setSecondHandler(XiToHandler.getInstance());
                break;
            case GameID.MAUBINH:
                Card.setCardType(1);
                BaseInfo.gI().nameTale = "Mậu Binh";
                break;
            case GameID.XOCDIA:
                Card.setCardType(0);
                BaseInfo.gI().nameTale = "Xóc Đĩa";
                break;
            default:
                break;
        }
    }

    void OnApplicationQuit() {
        NetworkUtil.GI().cleanNetwork();
    }

    void OnApplicationPause(bool pauseStatus) {
        NetworkUtil.GI().resume(pauseStatus);
    }

    void resume() {
        //NetworkUtil.GI().resume();
    }
}
