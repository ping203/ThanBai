using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ItemRanking : MonoBehaviour {
    public Image img_avata, icon_top;
    // public RawImage raw_avata;
    public Text lb_Name;
    public Text lb_money;
    public Text lb_stt;
    //public Sprite[] sp_top;
    public void SetData(int stt, int idAvatar, string name, long money) {
        icon_top.gameObject.SetActive(false);
        //switch (stt) {
        //    case 1:
        //        icon_top.sprite = sp_top[0];
        //        break;
        //    case 2:
        //        icon_top.sprite = sp_top[1];
        //        break;
        //    case 3:
        //        icon_top.sprite = sp_top[2];
        //        break;
        //    default:
        //        icon_top.gameObject.SetActive(false);
        //        break;
        //}
        if (stt < 4) {
            LoadAssetBundle.LoadSprite(icon_top, Res.AS_UI, "top" + stt, () => {
                icon_top.gameObject.SetActive(true);
                icon_top.SetNativeSize();
            });
        }

        lb_stt.text = stt.ToString();
        //img_avata.sprite = Res.getAvataByID(idAvatar);
        LoadAssetBundle.LoadSprite(img_avata, Res.AS_AVATA, "" + idAvatar);
        if (name.Length > 10) {
            name = name.Substring(0, 10) + "...";
        }

        lb_Name.text = name;
        lb_money.text = BaseInfo.formatMoney(money);
    }
}
