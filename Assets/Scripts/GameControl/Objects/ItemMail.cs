using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using AppConfig;

public class ItemMail : MonoBehaviour {
    public int id;
    private string guiTu;
    private string guiLuc;
    private string noiDung;
    public Image icon;
    public Text title_txt;
    public GameObject objBgPress;

    public void UpdateItemMail(int id, string guitu, string guiluc, string noidung, sbyte isRead)
    {
        this.id = id;
        title_txt.text = guitu;
        guiTu = guitu;
        guiLuc = guiluc;
        noiDung = noidung;
        if (isRead == 0)
            LoadAssetBundle.LoadSprite(icon, Res.AS_UI, "icon_mail_1");
        else
            LoadAssetBundle.LoadSprite(icon, Res.AS_UI, "icon_mail_2");
    }

    public void ClickItemMail() {
        objBgPress.SetActive(true);
        LoadAssetBundle.LoadSprite(icon, Res.AS_UI, "icon_mail_2");
        PanelMail.instance.ClickItemMail(objBgPress, guiTu + " " + guiLuc, noiDung);
    }

    public void ClickDeleteMail() {
        PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("", ClientConfig.Language.GetText("mail_delete_confirm"), delegate {
            SendData.onDelMessage(id);
            Destroy(this);
        });
    }

  //  public void setIconItemSK(int id, string tilte, string noiDung) {
  //      this.id = id;
  //      this.content = noiDung;
  //      del.gameObject.SetActive(false);
  //      if (isRead == 0) {
  //          icon.sprite = icon_mail[0];
  //      } else {
  //          icon.sprite = icon_mail[1];
  //      }
  //      if (content.Length > 20) {
  //          lb_content.text = (content.Substring(0, 20) + "...");
  //      } else {
  //          lb_content.text = (content);
  //      }
  //  }

        //public  void delMess() {
        //      PopupAndLoadingScript.instance.popup.ShowPopupTwoButton("","Bạn muốn xóa tin nhắn này?", delegate {
        //          SendData.onDelMessage(id);
        //          Destroy(this);
        //      });
        //  }
    }
