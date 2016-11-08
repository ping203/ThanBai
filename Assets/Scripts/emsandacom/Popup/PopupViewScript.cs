using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AppConfig;
using DG.Tweening;

namespace emsandacom.Popup
{
    public class PopupViewScript : MonoBehaviour
    {
        public static PopupViewScript instance;
        //public Text tittle;
        public Text message_1;
        public Text message_2;
        public Text message_3;
        public delegate void CallBack();
        public CallBack okClickOneCallBack;
        public CallBack okClickTwoCallBack;
        public CallBack okClickThreeCallBack;
        public CallBack okCancelCallBack;
        public GameObject objOneButton;
        public GameObject objTwoButton;
        public GameObject objThreeButton;
        private GameObject currentObj;
        public GameObject bg_hide;

        void Awake()
        {
            instance = this;
        }

        public void ShowPopupOneButton(string title = "", string message = "", CallBack okCall = null)
        {
            message_1.text = message;
            okClickOneCallBack = okCall;
            bg_hide.SetActive(true);
            objOneButton.SetActive(true);
            objOneButton.transform.DOScale(1, 0.1f);
        }

        public void ShowPopupTwoButton(string title = "", string message = "", CallBack okCall = null)
        {
            message_2.text = message;
            okClickTwoCallBack = okCall;
            bg_hide.SetActive(true);
            objTwoButton.SetActive(true);
            objTwoButton.transform.DOScale(1, 0.1f);
        }

        public void ShowPopupThreeButton(string title = "", string message = "", CallBack okCall = null, CallBack okCancel = null)
        {
            message_3.text = message;
            okClickThreeCallBack = okCall;
            okCancelCallBack = okCancel;
            bg_hide.SetActive(true);
            objThreeButton.SetActive(true);
            objThreeButton.transform.DOScale(1, 0.1f);
        }

        public void OkOne()
        {
            if(okClickOneCallBack != null)
                okClickOneCallBack.Invoke();
            objOneButton.transform.DOScale(0, 0.1f).OnComplete(ScaleFinishOne);
        }
        public void OkTwo()
        {
            if (okClickTwoCallBack != null)
                okClickTwoCallBack.Invoke();
            CancelTwo();
        }
        public void OkThree()
        {
            if (okClickThreeCallBack != null)
                okClickThreeCallBack.Invoke();
            CancelThree();
        }

        private void ScaleFinishOne() {
            bg_hide.SetActive(false);
            objOneButton.SetActive(false);
        }

        private void ScaleFinishTwo()
        {
            bg_hide.SetActive(false);
            objTwoButton.SetActive(false);
        }

        private void ScaleFinishThree()
        {
            bg_hide.SetActive(false);
            objThreeButton.SetActive(false);
        }

        public void CancelTwo()
        {
            objTwoButton.transform.DOScale(0, 0.1f).OnComplete(ScaleFinishTwo);
        }

        public void CancelThree()
        {
            objThreeButton.transform.DOScale(0, 0.1f).OnComplete(ScaleFinishThree);
        }

        public void CancelInvite()
        {
            if (okCancelCallBack != null)
                okCancelCallBack.Invoke();
            CancelThree();
        }
    }
}
