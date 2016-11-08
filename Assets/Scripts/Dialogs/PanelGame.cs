using UnityEngine;
using System.Collections;
using DG.Tweening;

public class PanelGame : MonoBehaviour {
    #region old
    public GameObject group;
    public bool isShow = false;

    public virtual void onShow() {
        show();
    }
    void show() {
        isShow = true;
        this.gameObject.SetActive(true);
    }
    public virtual void onHide() {
        hide();
        //GameControl.instance.sound.startClickButtonAudio();
    }
    void hide() {
        Invoke("disApear", 0f);
    }
    void disApear() {
        isShow = false;
        this.gameObject.SetActive(false);
    }
    #endregion

    #region new
    [SerializeField]
    GameObject objDiaglog;

    public virtual void onHideNew() {
        hideNew();
    }

    void hideNew() {
        objDiaglog.GetComponent<UIPopUp>().HideDialog();
    }
    #endregion

    /*
     #region new

    public void OnEnable() {
         onShowNew();
     }
     [SerializeField]
     Transform dialogPopup;

     public bool isShowNew { get; set; }
     public virtual void onShowNew() {
         if (dialogPopup != null) {
             gameObject.SetActive(true);
             dialogPopup.localScale = Vector3.zero;
             dialogPopup.DOScale(1, 0.1f);//.OnComplete(delegate {
                                          //   transform.localScale = Vector3.one;
                                          // });
             showNew();
         }
     }
     void showNew() {
         isShow = true;
     }
     public virtual void onHideNew() {
         hideNew();
     }
     void hideNew() {
         if (dialogPopup != null) {
             dialogPopup.DOScale(0, 0.1f).OnComplete(delegate {
                 gameObject.SetActive(false);
                 transform.localScale = Vector3.one;
             });
         }
     }
     #endregion
     */
}
