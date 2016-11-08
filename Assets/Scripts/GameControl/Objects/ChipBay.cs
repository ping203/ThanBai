using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ChipBay : MonoBehaviour {
    public static ChipBay instance;
    private long soChip;
    public Image chip1;
    public Image chip2;
    public Image chip3;
    public Transform posPlayer;
    public Transform posChipTong;

    Vector3 vtPosDefaut;
    Vector3 vtPosChipTong;

    void Awake() {
        instance = this;
    }

    void Start() {
        vtPosDefaut = posPlayer.localPosition;
        //vtPosChipTong = transform.InverseTransformPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0) + new Vector3(0, 60, 0));
        vtPosChipTong = posChipTong.localPosition;
    }

    public void setSoChip(long soChip) {
        this.soChip = soChip;
    }

    //public void InstantiateChip(long money) {
    //    //string act = action;
    //    //if (act.Equals("togapdoi"))
    //    LoadAssetBundle.LoadPrefab("chip", "to14", (obj) => {
    //        obj.transform.SetParent(posPlayer);
    //        obj.transform.localScale = Vector3.one;
    //        obj.transform.DOMove(vtPosChipTong, 0.2f);
    //        to_txt.gameObject.SetActive(true);
    //        to_txt.text = "" + BaseInfo.formatMoneyDetailDot(money);
    //    });
    //    //else if (act.Equals("to14")) {
    //    //    LoadAssetBundle.LoadPrefab("chip", "icon_chip_", (obj) =>
    //    //    {
    //    //        obj.transform.SetParent(posPlayer);
    //    //        obj.transform.localScale = Vector3.one;
    //    //        obj.transform.DOMove(posChipTong.position, 0.2f);
    //    //    });
    //    //}
    //}

    public void onMoveto(long money, int type) {
        soChip = money;
        if (money <= 0) {
            gameObject.SetActive(false);
        } else {
            gameObject.SetActive(true);
            string name;
            if (money > BaseInfo.gI().moneyTable * 20) {
                name = "icon_chip_1";
            } else if (money > BaseInfo.gI().moneyTable * 10) {
                name = "icon_chip_2";
            } else if (money > BaseInfo.gI().moneyTable * 5) {
                name = "icon_chip_3";
            } else if (money > BaseInfo.gI().moneyTable * 1) {
                name = "icon_chip_4";
            } else {
                name = "icon_chip_5";
            }
            LoadAssetBundle.LoadSprite(chip1, Res.AS_CHIP, name);
            LoadAssetBundle.LoadSprite(chip2, Res.AS_CHIP, name);
            LoadAssetBundle.LoadSprite(chip3, Res.AS_CHIP, name);
            Debug.Log(money + "     onMoveto    " + gameObject.activeInHierarchy);
            if (type == 1) {
                StartCoroutine(Moveto());
            } else {
                StartCoroutine(Moveback());
            }
        }
    }
    public long getMoneyChip() {
        return soChip;
    }


    IEnumerator Moveback() {
        chip1.transform.localPosition = vtPosChipTong;
        chip2.transform.localPosition = vtPosChipTong;
        chip3.transform.localPosition = vtPosChipTong;
        chip1.transform.DOLocalMove(posPlayer.localPosition, 0.6f);
        yield return new WaitForSeconds(0.1f);
        chip2.transform.DOLocalMove(posPlayer.localPosition, 0.6f);
        yield return new WaitForSeconds(0.1f);
        chip3.transform.DOLocalMove(posPlayer.localPosition, 0.6f);
        yield return new WaitForSeconds(0.65f);
        gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        StartCoroutine(MoveFromTo(vtPosChipTong, vtPosDefaut));

    }
    IEnumerator Moveto() {
        chip1.transform.localPosition = vtPosDefaut;
        chip2.transform.localPosition = vtPosDefaut;
        chip3.transform.localPosition = vtPosDefaut;
        chip1.transform.DOLocalMove(vtPosChipTong, 0.6f);
        yield return new WaitForSeconds(0.1f);
        chip2.transform.DOLocalMove(vtPosChipTong, 0.6f);
        yield return new WaitForSeconds(0.1f);
        chip3.transform.DOLocalMove(vtPosChipTong, 0.6f);
        yield return new WaitForSeconds(0.65f);
        gameObject.SetActive(false);
        yield return new WaitForEndOfFrame();
        StartCoroutine(MoveFromTo(vtPosDefaut, vtPosChipTong));
    }

    IEnumerator MoveFromTo(Vector3 vt_from, Vector3 vt_to) {
        chip1.transform.localPosition = vt_from;
        chip2.transform.localPosition = vt_from;
        chip3.transform.localPosition = vt_from;
        chip1.transform.DOLocalMove(vt_to, 0.6f);
        yield return new WaitForSeconds(0.1f);
        chip2.transform.DOLocalMove(vt_to, 0.6f);
        yield return new WaitForSeconds(0.1f);
        chip3.transform.DOLocalMove(vt_to, 0.6f);
        yield return new WaitForSeconds(0.65f);
        gameObject.SetActive(false);
    }
}
