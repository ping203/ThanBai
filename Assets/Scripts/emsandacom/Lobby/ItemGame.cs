using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class ItemGame : MonoBehaviour {

    public Image iconImg;
    private int gameId;
    private int index;

    public void UpdateIcon(int id, string image, int i) {
        LoadAssetBundle.LoadSprite(iconImg, "ui", image);
        gameId = id;
        index = i;
    }

    public void ClickIconGame() {
        //Debug.LogError("Index " + index);
        LobbyViewScript.instance.ClickIconGame(gameId, index);
        gameObject.transform.DOScale(0, 0.2f);
    }
}
