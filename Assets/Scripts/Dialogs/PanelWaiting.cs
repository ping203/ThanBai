﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PanelWaiting : MonoBehaviour {

    public static PanelWaiting instance;

    void Awake() {
        instance = this;
    }

    public void ShowLoading()
    {
        gameObject.SetActive(true);
    }
    public void HideLoading()
    {
        gameObject.SetActive(false);
    }
}
