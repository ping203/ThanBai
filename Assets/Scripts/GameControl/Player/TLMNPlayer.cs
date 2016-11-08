using UnityEngine;
using System.Collections;

public class TLMNPlayer : ABSUser {
    // Use this for initialization
    void Start() {
        for (int i = 0; i < cardHand.getSizeArrayCard(); i++) {
            TLMNInput tlmnIP = new TLMNInput(this, (TLMN)casinoStage, cardHand,
                           cardHand.getCardbyPos(i));
            cardHand.getCardbyPos(i).setListenerClick(delegate {
                tlmnIP.click();
            });
        }
        //base.onHide ();
    }

    public static int[] sort(int[] arr) {// mang cac so thu tu quan bai tu 0-51
        int[] turn = arr;
        int length = turn.Length;
        for (int i = 0; i < length - 1; i++) {
            int min = i;
            for (int j = i + 1; j < length; j++) {
                if (getCardInfo(turn[j])[1] < getCardInfo(turn[min])[1]) {
                    // swap
                    min = j;
                } else if (getCardInfo(turn[j])[0] < getCardInfo(turn[min])[0]
                    && (getCardInfo(turn[j])[1] == getCardInfo(turn[min])[1])) {
                    min = j;
                }
            }
            int temp = turn[i];
            turn[i] = turn[min];
            turn[min] = temp;
        }
        return turn;
    }

    public static int[] getCardInfo(int i) {
        int[] turn = { i / 13, i % 13 };// 0 - type 1 - cardID
        return turn;
    }

    public override void setCardHand(int[] card, bool isDearling, bool inone, bool isFlipCard) {
        base.setCardHand(sort(card), isDearling, inone, isFlipCard);
    }

    public override void setRank(int rank) {
        //base.setRank (rank);
        if (cardHand.getSize() == 13) {
            if (rank == 1) {
                //sp_thang.sprite = ani_thang[2];
                sp_thang.gameObject.SetActive(true);
                //sp_thang.sprite = ani_thang[idTR];
                LoadAssetBundle.LoadSprite(sp_thang, Res.AS_UI, Res.ani_thang[2], () => {
                    sp_thang.SetNativeSize();
                });
                if (pos == 0) {
                    SoundManager.instance.startWinAudio();
                }
            } else {
                //sp_thang.sprite = ani_thang[4];
                sp_thang.gameObject.SetActive(true);
                LoadAssetBundle.LoadSprite(sp_thang, Res.AS_UI, Res.ani_thang[4], () => {
                    sp_thang.SetNativeSize();
                });
                if (pos == 0) {
                    SoundManager.instance.startLostAudio();
                }
            }
        } else {
            base.setRank(rank);
        }
    }

}
