using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using Facebook.Unity;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using AppConfig;

public class IMEI : MonoBehaviour {

    // Use this for initialization
    //public Text imei;
    public static IMEI instance;
    AndroidJavaClass unity;
    AndroidJavaObject currentActivity;
    void Awake()
    {
        instance = this;
    }

    void Start() {
#if UNITY_ANDROID && !UNITY_EDITOR
        unity = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = unity.GetStatic<AndroidJavaObject>("currentActivity");
        GetIMEI();
#endif
    }

    public void GetIMEI() {
#if UNITY_ANDROID && !UNITY_EDITOR
        var mainactivity = new AndroidJavaClass("com.nomercy.nguyenquochai.myapplication.MainActivity");
        mainactivity.CallStatic("GetIMEI", currentActivity, new IMEIImp());
#endif
    }

#if UNITY_ANDROID
    public class IMEIImp : AndroidJavaProxy {
        public IMEIImp() : base("com.nomercy.nguyenquochai.myapplication.IMEIEventHandler") {
        }

        public void OnIMEIResult(string imei) {
            //IMEI.instance.imei.text = imei;
            Debug.Log("IMEIIII " + imei);
            ClientConfig.HardWare.IMEI = imei;
        }
    }
#endif
}
