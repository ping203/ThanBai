using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemSuKien : MonoBehaviour {

    private int id;
    private string title;
    private string content;
    public Text titleTxt;
    public GameObject objBgPress;
    public Image iconRead;

    public void UpdateItemSuKien(int id, string title, string content) {
        this.id = id;
        this.title = title;
        this.content = content;
        titleTxt.text = title;
        int isRead = PlayerPrefs.GetInt(BaseInfo.gI().mainInfo.userid + id + "", 0);
        if (isRead == 0)
            LoadAssetBundle.LoadSprite(iconRead, Res.AS_UI, "icon_mail_1");
        else
            LoadAssetBundle.LoadSprite(iconRead, Res.AS_UI, "icon_mail_2");
    }

    public void ClickItemSuKien() {
        objBgPress.SetActive(true);
        LoadAssetBundle.LoadSprite(iconRead, Res.AS_UI, "icon_mail_2");
        PlayerPrefs.SetInt(BaseInfo.gI().mainInfo.userid + id + "", 1);
        PlayerPrefs.Save();
        PanelMail.instance.ClickItemSuKien(objBgPress, title, content);
    }

}
