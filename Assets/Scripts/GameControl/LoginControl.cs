using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using DG.Tweening;
using AppConfig;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class LoginControl : StageControl
{
    //public Text lb_version;
    public static LoginControl instance;
    public InputField input_username;
    public InputField input_passsword;
    //NetworkUtil net;
    public GameObject objLogin;
    public Image bgChangeScene;
    public InputField userInput;
    public GameObject popupGetPass;

    //void Update() {
    //    if (gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) {
    //        onBack();
    //    }
    //}

    void Awake()
    {

        instance = this;
        //net = GameObject.FindObjectOfType<NetworkUtil>();
        input_username.text = PlayerPrefs.GetString("username");
        input_passsword.text = PlayerPrefs.GetString("password");
        OnSubmit();
        if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }
    }

    void Start()
    {
        new ListernerServer();
        NetworkUtil.GI().connect(SendData.onGetPhoneCSKH());
        UnloadScene(Res.LOGIN_NAME);
        //bgChangeScene.gameObject.SetActive(true);
        //StartCoroutine(WaitToFade(1f));
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

    //private IEnumerator WaitToFade(float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    bgChangeScene.DOFade(0, 1).OnComplete(FadeBgChangeSceneFinish);
    //}

    public void FadeBgChangeSceneFinish()
    {
        bgChangeScene.gameObject.SetActive(false);
    }

    void OnEnable()
    {
        isLoginFB = false;
    }

    private bool checkNetWork()
    {
        return true;
    }

    #region Facebook
    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            // Signal an app activation App Event
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            // Pause the game - we will need to hide
            Time.timeScale = 0;
        }
        else
        {
            // Resume the game - we're getting focus again
            Time.timeScale = 1;
        }
    }
    List<string> perms = new List<string>() { "public_profile", "email", "user_friends" };
    void login_facebook()
    {
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        //txt_debug.text = result.ToString();
        if (FB.IsLoggedIn)
        {
            // AccessToken class will have session details
            var aToken = AccessToken.CurrentAccessToken;
            // Print current access token's User ID
            //Debug.Log(aToken.UserId); 
            //txt_debug.text += " " + aToken.ToString();
            // Print current access token's granted permissions
            //foreach (string perm in aToken.Permissions) {
            //    Debug.Log(perm);
            //}
            //string imei = GlobalPublic.gI().IMEI;
            //SendDataToSever.onLogin("", "", 1, imei, aToken.ToString());
            //}
            login(1, "sgc", "sgc", /*"357224070304436"*/ClientConfig.HardWare.IMEI, "", 1, "", aToken.TokenString, "");
            //Debug.LogError("Login : " + ClientConfig.HardWare.IMEI + " Token " + aToken.TokenString);
        }
        else
        {
            //txt_debug.text += (" User cancelled login");
        }
    }
    #endregion

    /**
 * 
 * @param username
 * @param pass
 * @param type
 *            : 1-facebook 2-choingay 3-gmail 4-login normal
 * @param imei
 * @param link_avatar
 * @param tudangky
 *            : 1 la tu dang ky, 0
 * @param displayName
 * @param accessToken
 * @param regPhone
 */

    public void MoveFormLogin()
    {
        objLogin.transform.DOLocalMoveX(249f, 0.4f);
    }

    public void login(sbyte type, string username, string pass,
                               string imei, string link_avatar, sbyte tudangky, string displayName,
                               string accessToken, string regPhone)
    {
        BaseInfo.gI().isPurchase = false;
        if (checkNetWork())
        {
            //if (NetworkUtil.GI().isConnected()) {
            //    NetworkUtil.GI().close();
            //}
            //if(net != null) {
            //    StartCoroutine(net.Start());
            //}
            Message msg = new Message(CMDClient.CMD_LOGIN_NEW);
            try
            {
                msg.writer().WriteByte(type);
                msg.writer().WriteUTF(username);
                msg.writer().WriteUTF(pass);
                msg.writer().WriteUTF(Res.version);
                msg.writer().WriteByte(CMDClient.PROVIDER_ID);
                msg.writer().WriteUTF(imei);
                msg.writer().WriteUTF(link_avatar);
                msg.writer().WriteByte(tudangky);
                msg.writer().WriteUTF(displayName);
                msg.writer().WriteUTF(accessToken);
                msg.writer().WriteUTF(regPhone);
            }
            catch (Exception ex)
            {
                Debug.LogException(ex);
            }
            //SendData.isLogin = true;
            NetworkUtil.GI().sendMessage(msg);
            BaseInfo.gI().username = username;
            BaseInfo.gI().pass = pass;
        }
        else
        {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Vui lòng kiểm tra kết nối mạng!");
        }
    }

    public void LoadLobby()
    {
        bgChangeScene.gameObject.SetActive(true);
        LoadAssetBundle.LoadScene(Res.MAIN_AB, Res.MAIN_NAME);
    }

    //public void onClick(string action) {
    //    switch (action) {
    //        case "login":
    //            doLogin();
    //            break;
    //        case "reg":
    //            onReg();
    //            break;
    //        case "playnow":
    //            PanelWaiting.instance.ShowLoading();
    //            clickLoginPlayNow();
    //            break;
    //        case "playfb":
    //            clickOnFacebook();
    //            break;
    //        case "getpass":
    //            clickQuenMK();
    //            break;
    //        case "setting":
    //            clickSetting();
    //            break;
    //        case "help":
    //            clickHelp();
    //            break;
    //    }
    //}

    public void onReg()
    {
        SoundManager.instance.startClickButtonAudio();
        LoadAssetBundle.LoadScene(Res.REGISTER_AB, Res.REGISTER_NAME);
    }

    public void loginWhenRegSucces()
    {
        input_username.text = BaseInfo.gI().username;
        input_passsword.text = BaseInfo.gI().pass;
    }

    //[SerializeField]
    //Toggle tg_dn, tg_dk;

    //IEnumerator delay(sbyte type, string username, string pass,
    //                         string imei, string link_avatar, sbyte tudangky, string displayName,
    //                         string accessToken, string regPhone) {
    //if (net != null) {
    //    if (!net.connected) {
    //        StartCoroutine(net.Start());
    //        while (!net.connected) {
    //            yield return new WaitForSeconds(0.1f);
    //            if (net.connected) {
    //                login(type, username, pass, imei, link_avatar, tudangky, displayName, accessToken, regPhone);
    //            }
    //        }
    //    } else {
    //        login(type, username, pass, imei, link_avatar, tudangky, displayName, accessToken, regPhone);
    //    }
    //}
    //}
    public void doLogin()
    {
        SoundManager.instance.startClickButtonAudio();
        string username = input_username.text;
        string password = input_passsword.text;
        if (username.Equals("") || password.Equals(""))
        {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Bạn chưa nhập thông tin!");
            return;
        }

        PopupAndLoadingScript.instance.ShowLoading();
        //string imei = SystemInfo.deviceUniqueIdentifier;
        string imei = "";
#if UNITY_ANDROID && !UNITY_EDITOR
        imei = ClientConfig.HardWare.IMEI;
#elif UNITY_EDITOR
        imei = SystemInfo.deviceUniqueIdentifier;
#endif
        //Debug.LogError("IMEI " + imei);
        //StartCoroutine(delay(4, username, password, imei, "", 0, username, "", ""));
        login(4, username, password, imei, "", 0, username, "", "");
        PlayerPrefs.SetString("username", username);
        PlayerPrefs.SetString("password", password);
        PlayerPrefs.Save();
    }

    public void clickLoginPlayNow()
    {
        SoundManager.instance.startClickButtonAudio();
        PopupAndLoadingScript.instance.ShowLoading();
        string imei = "";
#if UNITY_ANDROID && !UNITY_EDITOR
        imei = ClientConfig.HardWare.IMEI;
#elif UNITY_EDITOR
        imei = SystemInfo.deviceUniqueIdentifier;
#endif

        //Debug.LogError("IMEI " + imei);
        login(2, imei, imei, imei, "", 1, "", "", "");
    }

    public void clickOnFacebook()
    {
        SoundManager.instance.startClickButtonAudio();
        login_facebook();
        //GameControl.instance.sound.startClickButtonAudio();
#if UNITY_WEBGL
        Application.ExternalCall("myFacebookLogin");
#endif
        // Debug.Log("clickOnFacebook");
    }
    public bool isLoginFB { set; get; }
    public bool ScenceManager { get; private set; }

    //public void sendloginFB(string accessToken) {
    //    // login(1, "sgc", "sgc", GameControl.IMEI, "", 1, "", accessToken, "");
    //    PopupAndLoadingScript.instance.ShowLoading();
    //    StartCoroutine(delay(1, "sgc", "sgc", ClientConfig.HardWare.IMEI, "", 1, "", accessToken, ""));
    //    isLoginFB = true;
    //}

    public void ClickGetPass()
    {
        SoundManager.instance.startClickButtonAudio();
        string userName = userInput.text;
        if (!string.IsNullOrEmpty(userName))
        {
            SendData.onGetPass(userName);
            popupGetPass.GetComponent<UIPopUp>().HideDialog();
        }
        else
        {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", ClientConfig.Language.GetText("register_usergetpass"));
        }
    }

    //public void clickHelp() {
    //    //GameControl.instance.sound.startClickButtonAudio();
    //    //gameControl.panleHelp.onShow();
    //}

    public void clickCSKH()
    {
        PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", "Gọi điện đến tổng đài chăm sóc khách hàng "
            + BaseInfo.gI().cskh + "?", delegate
            {
                Application.OpenURL("tel://" + BaseInfo.gI().cskh);
            });
    }

    private int checkName(string username)
    {
        if (username.Length > 10 || username.Length < 4)
            return -3;

        for (int i = 0; i < username.Length; i++)
        {
            char c = username[i];
            if ((('0' > c) || (c > '9')) && (('A' > c) || (c > 'Z'))
                && (('a' > c) || (c > 'z')))
            {
                return -1;
            }
        }
        bool isTrung = true;
        for (int i = 0; i < username.Length - 1; i++)
        {
            char c1 = username[i];
            char c2 = username[i + 1];
            if (c1 != c2)
            {
                isTrung = false;
                break;
            }
        }
        if (isTrung)
        {
            return -4;
        }
        bool isLT = false;
        if (username[0] == '0' || username[0] == '1')
        {
            isLT = true;
            for (int i = 0; i < username.Length - 1; i++)
            {
                char c1 = username[i];
                char c2 = username[i + 1];
                if (('0' <= c1) && (c1 <= '9'))
                {

                }
                else
                {
                    isLT = false;
                    break;
                }
                if (('0' <= c2) && (c2 <= '9'))
                {

                }
                else
                {
                    isLT = false;
                    break;
                }
                if (int.Parse(c1 + "") != int.Parse(c2 + "") - 1)
                {
                    isLT = false;
                    break;
                }
            }
        }
        if (isLT)
        {
            return -4;
        }
        return 1;
    }

    void clickCapNhat()
    {
        if (checkNetWork())
        {
            PopupAndLoadingScript.instance.ShowLoading();
            NetworkUtil
                    .GI()
                    .sendMessage(
                            SendData.onGetMessageUpdateVersionNew(CMDClient.PROVIDER_ID));
        }
        else
        {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Vui lòng kiểm tra kết nối mạng!");
        }

    }

    void clickGioiThieuBanChoi()
    {
        if (checkNetWork())
        {
            PopupAndLoadingScript.instance.ShowLoading();
            NetworkUtil
                    .GI()
                    .sendMessage(
                            SendData.onGetMessageIntroduceFriend(CMDClient.PROVIDER_ID));
        }
        else
        {
            PopupAndLoadingScript.instance.popup.ShowPopupOneButton("", "Vui lòng kiểm tra kết nối mạng!");
        }


    }

    public void clickQuenMK()
    {
        //gameControl.dialogQuenMK.onShow(); 
        popupGetPass.SetActive(true);
    }

    void OnSubmit()
    {
        //this.loginGroup.transform.localPosition = new Vector3(0, 0, 0);
    }

    void OnShowKeyBoard()
    {
        //TweenPosition.Begin(this.loginGroup, 0.25f, new Vector3(0, 160, 0));
    }

    public override void Appear()
    {
        base.Appear();
        OnSubmit();
    }

    public void ExtiGame()
    {
        PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", ClientConfig.Language.GetText("exit_game"), () => { Application.Quit(); });
    }

    //void OnApplicationQuit() {
    //    Debug.LogError("OnApplicationQuit");
    //    NetworkUtil.GI().cleanNetwork();
    //}

}
