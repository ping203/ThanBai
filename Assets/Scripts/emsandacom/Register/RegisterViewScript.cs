using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AppConfig;

//namespace emsandacom.Register
//{
public class RegisterViewScript : MonoBehaviour
{
    public static RegisterViewScript instance;
    public InputField inputUser;
    public InputField inputPass;
    public InputField inputConfirmPass;
    public Text thongbaoTxt;
    NetworkUtil net;
    //public GameObject objLoading;

    void Awake()
    {
        instance = this;
    }

    // Use this for initialization
    void Start()
    {
        //net = GameObject.FindObjectOfType<NetworkUtil>();
    }

    public void Register()
    {
        string message = "";
        string imei = ClientConfig.HardWare.IMEI;
        if (string.IsNullOrEmpty(inputUser.text) || inputUser.text.Length < 6)
        {
            message = ClientConfig.Language.GetText("register_tenkhonghople");
            ShowThongBao(message);
            return;
        }
        else if (string.IsNullOrEmpty(inputPass.text) || inputPass.text.Length < 6)
        {
            message = ClientConfig.Language.GetText("register_matkhauyeu");
            ShowThongBao(message);
            return;
        }
        else if (inputConfirmPass.Equals(inputPass.text))
        {
            message = ClientConfig.Language.GetText("register_matkhau_khonggiongnhau");
            ShowThongBao(message);
            return;
        }

        string user = inputUser.text;
        string pass = inputPass.text;
        BaseInfo.gI().username = user;
        BaseInfo.gI().pass = pass;
        PopupAndLoadingScript.instance.ShowLoading();
        //StartCoroutine(delayReg(user, pass, imei));
        //SendData.onRegister(user, pass, imei);
    }

    //private IEnumerator delayReg(string username, string pass, string imei)
    //{
    //    if (net != null)
    //    {
    //        if (!net.connected)
    //        {
    //            StartCoroutine(net.Start());
    //            while (!net.connected)
    //            {
    //                yield return new WaitForSeconds(0.1f);
    //                if (net.connected)
    //                {
    //                    SendData.onRegister(username, pass, imei);
    //                }
    //            }
    //        }
    //        else
    //        {
    //            SendData.onRegister(username, pass, imei);
    //        }
    //    }
    //}

    private void ShowThongBao(string message)
    {
        thongbaoTxt.text = message;
        thongbaoTxt.gameObject.SetActive(true);
    }

    public void loginWhenRegSucces()
    {
        PopupAndLoadingScript.instance.HideLoading();
        Debug.LogError("Regis success");
        GetComponent<UIPopUp>().HideDialog();
        inputUser.text = "";
        inputPass.text = "";
        inputConfirmPass.text = "";
        LoginControl.instance.loginWhenRegSucces();
    }

    public void CloseRegister()
    {
        GetComponent<UIPopUp>().HideDialog();
    }
}
//}
