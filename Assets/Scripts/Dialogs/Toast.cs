using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class Toast : MonoBehaviour {
    public Text label;
    public void showToast(string mess) {
        gameObject.SetActive(true);
        //Debug.Log("===========================show toast");
        label.text = mess;
        StartCoroutine(showT());
    }

    IEnumerator showT() {
        //TweenAlpha.Begin(gameObject, 0f, 0);
        //TweenAlpha.Begin(gameObject, 0.5f, 1);
        //yield return new WaitForSeconds(3f);
        //TweenAlpha.Begin(gameObject, 0.5f, 0);
        yield return new WaitForSeconds(2.0f);
        gameObject.SetActive(false);
    }
}
