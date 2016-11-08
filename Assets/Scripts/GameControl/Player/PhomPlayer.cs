using UnityEngine;
using System.Collections;
using System;
public class PhomPlayer : ABSUser {

    public Transform middleTransform;
    // Use this for initialization
    void Start() {
        for (int i = 0; i < cardHand.getSizeArrayCard(); i++) {
            PhomInput tlmnIP = new PhomInput((PHOM)casinoStage, cardHand,
                           cardHand.getCardbyPos(i));
            cardHand.getCardbyPos(i).setListenerClick(delegate {
                tlmnIP.click();
            });
        }
    }

    internal void processXepBai(int[] cards) {

        Vector3[] posOld = new Vector3[cards.Length];
        Vector3[] posNew = new Vector3[cards.Length];
        for (int i = 0; i < cardHand.getSizeArrayCard(); i++) {
            cardHand.getCardbyPos(i).StopAllCoroutines();
            // StopAllCoroutines();
            //if (cardHand.getCardbyPos(i).GetComponent<TweenPosition>() != null)
            //{
            //   // cardHand.getCardbyPos(i).GetComponent<TweenPosition>().enabled = false;
            //}


        }

        for (int i = 0; i < cards.Length; i++) {
            if (cardHand.getCardbyID(cards[i]) != null) {
                posOld[i] = cardHand.getCardbyID(cards[i]).transform.localPosition;
            } else {
                posOld[i] = new Vector3(0, 0, 0);
            }

        }
        cardHand.setArrCard(cards, false);

        for (int i = 0; i < cards.Length; i++) {
            if (cardHand.getCardbyID(cards[i]) != null) {
                posNew[i] = cardHand.getCardbyID(cards[i]).transform.localPosition;
            } else {
                posNew[i] = new Vector3(0, 0, 0);
            }
        }

        for (int i = 0; i < cards.Length; i++) {
            Card card = cardHand.getCardbyID(cards[i]);
            if (card != null) {
                card.transform.localPosition = posOld[i];
                //if (card.GetComponent<TweenPosition>() != null)
                //{
                //   // card.GetComponent<TweenPosition>().enabled = true;
                //}

                StartCoroutine(card.moveTo(posNew[i], Res.speedCard, 0, false));
            }
        }
        cardHand.setCardMo(getEatCard());
    }

    public override void addToCardHand(int card, bool isTouch) {
        base.addToCardHand(card, isTouch);
        if (getName().Equals(BaseInfo.gI().mainInfo.nick)) {
            addToCard(cardHand, card, true, isTouch, true, false);
        } else {
            addToCard(cardHand, card, true, isTouch, false, false);
        }
    }
    public override void resetData() {
        base.resetData();
        //if (pos == 0)
        //{
        //    Vector3 posi = transform.localPosition;
        //    posi.x = 0;
        //    this.transform.localPosition = posi;
        //}

        for (int i = 0; i < 3; i++) {
            cardPhom[i].setAllMo(false);
            cardPhom[i].removeAllCard();
        }

        for (int i = 0; i < card_win.Length; i++) {
            card_win[i] = -1;
        }
        allCardPhom = null;
    }
    public override void onEatCard(int card) {
        base.onEatCard(card);
        for (int i = 0; i < card_win.Length; i++) {
            if (card_win[i] == -1) {
                card_win[i] = card;
                if (pos != 0) {
                    addToCard(cardPhom[i], card, true, false, false, false);
                } else {
                    addToCard(cardPhom[i], card, false, false, false, true);
                }
                return;
            }
        }
    }
    public override int[] getEatCard() {

        int[] temp;
        int k = 0;
        for (int i = 0; i < card_win.Length; i++) {
            if (card_win[i] != -1) {
                k++;
            }
        }
        temp = new int[k];
        k = 0;
        for (int i = 0; i < card_win.Length; i++) {
            if (card_win[i] != -1) {
                temp[k] = card_win[i];
                k++;
            }
        }
        return temp;
    }
    public override void setCardPhom(int[] arrayPhom, bool isHaPhom) {
        base.setCardPhom(arrayPhom, true);
        try {

            allCardPhom = arrayPhom;
            int[][] cardPhom = RTL.getPhom3(arrayPhom, null);
            for (int i = 0; i < cardPhom.Length; i++) {
                if (cardPhom[i].Length >= 6) {

                }
            }
            for (int i = 0; i < 3; i++) {
                this.cardPhom[i].removeAllCard();
            }
            for (int i = 0; i < cardPhom.Length; i++) {
                this.cardPhom[i].setArrCard(cardPhom[i], isHaPhom);
            }

        } catch (Exception e) {
            // TODO: handle exception

        }
    }
    public override int[] getAllCardPhom() {
        if (allCardPhom == null) {
            return new int[] { };
        } else {
            return allCardPhom;
        }
    }
    public override void addToCard(ArrayCard arrC, int idCard, bool isDearling, bool isTouch, bool isSort, bool isEatCard) {
        try {
            arrC.addCard(idCard);
        } catch (Exception e) {
        }
        if (isSort) {
            //int[] temp = RTL.sort(cardHand.getArrCardAll());
            //if (pos == 0)
            //{
            //    cardHand.setArrCard(temp);
            //}
            //else
            //{
            //}
        }
        Card card = arrC.getCardbyID(idCard);
        if (card == null) {
            card = arrC.getCardbyPos(arrC.getSize() - 1);
        }
        if (isEatCard) {
            card.GetComponent<RectTransform>().sizeDelta = new Vector3(40, 60);
        }
        if (isDearling) {
            Vector3 oldPos = card.gameObject.transform.localPosition;
            //if (pos == 0)
            //    card.GetComponent<RectTransform>().sizeDelta = new Vector3(80, 110);
            card.gameObject.transform.SetParent(middleTransform);
            //Debug.LogError("oldPos " + oldPos);
            card.gameObject.transform.localPosition = middleTransform.localPosition;
            card.gameObject.transform.SetParent(arrC.mainTransform);
            //Debug.LogError(" Card Position " + card.gameObject.transform.localPosition + " Middle " + middleTransform.localPosition);
            StartCoroutine(card.moveTo(oldPos, .2f, 0, false));

            SoundManager.instance.startchiabaiAudio();

            //arrC.reAddAllCard(getEatCard());
        }
    }
    public override void setInfo(string diem) {
        lb_name_sansang.text = diem;
    }
}
