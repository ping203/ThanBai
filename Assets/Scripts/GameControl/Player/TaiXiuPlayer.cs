using UnityEngine;
using System.Collections;

public class TaiXiuPlayer : ABSUser {
   /* WWW www_1;
    bool isOneAvata_1 = false;
    new void Update() {
        if (www_1 != null) {
            if (www_1.isDone && !isOneAvata_1) {
                raw_avatar.texture = www_1.texture;
                isOneAvata_1 = true;
            }
        }
    }*/

    public void CreateInfoPlayer() {
        setFollowMoney(BaseInfo.gI().mainInfo.moneyXu);
        setName(name);
        setDisplayeName(BaseInfo.gI().mainInfo.displayname);
        lb_money.text = BaseInfo.formatMoneyNormal(getFolowMoney());
        lb_money.gameObject.SetActive(true);
        if (lb_money_result2 != null)
            lb_money_result2.SetActive(false);

      /*  www_1 = null;
        isOneAvata_1 = false;*/
        if (BaseInfo.gI().mainInfo.link_Avatar != "") {
            // www_1 = new WWW(BaseInfo.gI().mainInfo.link_Avatar);
            //img_avatar.gameObject.SetActive(false);
            //raw_avatar.gameObject.SetActive(true);
            StartCoroutine(getAvata(BaseInfo.gI().mainInfo.link_Avatar));
        } else {
            img_avatar.gameObject.SetActive(true);
            raw_avatar.gameObject.SetActive(false);
            //img_avatar.sprite = Res.getAvataByID(BaseInfo.gI().mainInfo.idAvata);
            LoadAssetBundle.LoadSprite(img_avatar, Res.AS_AVATA, "" + BaseInfo.gI().mainInfo.idAvata);
        }
    }

    IEnumerator getAvata(string link) {
        WWW www = new WWW(link);
        yield return www;
        img_avatar.gameObject.SetActive(false);
        raw_avatar.gameObject.SetActive(true);
        raw_avatar.texture = www.texture;
        www.Dispose();
        www = null;
    }
}
