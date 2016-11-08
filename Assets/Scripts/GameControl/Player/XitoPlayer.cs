using UnityEngine;
using System.Collections;
using DG.Tweening;

public class XitoPlayer : ABSUser {
    // Use this for initialization
    void Start() {
        for (int i = 0; i < cardHand.getSizeArrayCard(); i++) {
            XitoInput xitoIP = new XitoInput((Xito)casinoStage, cardHand,
                           cardHand.getCardbyPos(i));
            cardHand.getCardbyPos(i).setListenerClick(delegate {
                xitoIP.click();
            });
        }
    }

    public override void setMaster(bool isMaster) {
        base.setMaster(false);
    }

    GameObject objWinEffect;
    public override void setRank(int rank) {
        sp_typeCard.StopAllCoroutines();
        sp_typeCard.gameObject.transform.position = new Vector3(0, -25, 0);

        switch (rank) {
            case 0:
                if (pos == 0) {
                    SoundManager.instance.startLostAudio();
                }
                break;
            case 1:
                LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Win_Effect", (obj) => {
                    objWinEffect = obj;
                    objWinEffect.transform.SetParent(gameObject.transform);
                    objWinEffect.transform.localPosition = Vector3.zero;
                    //Debug.LogError("Load Win Effect 55");
                });
                if (pos == 0) {
                    SoundManager.instance.startWinAudio();
                }
                break;
            case 2:
            case 3:
            case 4:
                if (pos == 0) {
                    SoundManager.instance.startLostAudio();
                }
                break;
            case 5:
                LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Win_Effect", (obj) => {
                    objWinEffect = obj;
                    objWinEffect.transform.SetParent(gameObject.transform);
                    objWinEffect.transform.localPosition = Vector3.zero;
                    //Debug.LogError("Load Win Effect 55");
                });
                if (pos == 0) {
                    SoundManager.instance.startWinAudio();
                }
                break;
            default:
                break;
        }
        Invoke("setVisibleThang", 3f);
    }

    public new void setVisibleThang()
    {
        if (objWinEffect != null)
            Destroy(objWinEffect);
    }

    public override void addToCardHand(int card, bool p) {
        base.addToCardHand(card, p);
        cardHand.StopAllCoroutines();
        addToCard(cardHand, card, true, p, false, false);
    }

    public override void setLoaiBai(int type) {
        base.setLoaiBai(type);
        if (type < 0 || type > 8) {
            return;
        }
        //if (pos != 0) {
        sp_typeCard.StopAllCoroutines();
        sp_typeCard.gameObject.SetActive(true);
        LoadAssetBundle.LoadSprite(sp_typeCard, Res.AS_UI, Res.TypeCard_Name[type], () => {
            sp_typeCard.SetNativeSize();
        });
        //sp_typeCard.sprite = GameControl.instance.list_typecards[type];

            sp_typeCard.gameObject.transform.localPosition = new Vector3(0, -50, 0);
            sp_typeCard.gameObject.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            sp_typeCard.transform.DOLocalMoveY(-20, 0.5f);
            //TweenPosition.Begin (sp_typeCard.gameObject, 0.5f, new Vector3 (0, -20, 0));
            StartCoroutine(setVisible(sp_typeCard.gameObject, 2.5f));
        //} else {
        //if (pos == 0) {
        //    if (((Xito)casinoStage).listTypeCard != null)
        //        ((Xito)casinoStage).listTypeCard.setTg(type);
        //}
    }
}
