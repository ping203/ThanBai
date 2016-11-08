using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class ItemMucCuoc : MonoBehaviour {
    public Text muccuocTxt;
    private long money;

    public void UpdateMucCuoc(long muccuoc) {
        //if (muccuoc > 0) {
        muccuocTxt.text = BaseInfo.formatMoneyNormal2(muccuoc);
        money = muccuoc;
        //}
    }

    public void ClickMucCuoc() {
        RoomViewScript.instance.ClickMucCuoc(gameObject, money);
        gameObject.transform.DOScale(1.4f, 0.1f);
        //muccuocTxt.color = new Color32(255, 189, 47, 255);
    }
}

