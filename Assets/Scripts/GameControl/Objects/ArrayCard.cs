using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
public enum Align_Array {
    Horizontal,//Doc
    Vertival//Ngang
};

public enum Align_Anchor {
    Left,
    Right,
    Center,
    Top,
    Bot
};
public class ArrayCard : MonoBehaviour {
    public Align_Array align_Array = Align_Array.Horizontal;
    public Align_Anchor align_Anchor = Align_Anchor.Left;
    public GameObject prefabCard;
    public int type; // 0: center, 1: left, 2: right
    public int maxW = 100, maxH = 100;
    private List<Card> arrCard;
    private List<int> arrIntCard;
    private float wCard, hCard;
    private float disCard;
    public bool isSmall = false;
    public bool inone;
    public const float sizeCardSmall = 2.5f / 3f;
    public bool isTouch = false;
    public int maxCard = 13;
    public Transform mainTransform;
    public Transform middleTransform;
    //public Transform chohienbai;
    //public Transform firecardTrans;
    //public Transform cardLoc;
    //public GameObject cardImg;

    public Text lb_SoBai;
    private int soBai;
    public void setVisibleSobai(bool isVisible) {
        if (lb_SoBai != null) {
            lb_SoBai.gameObject.SetActive(isVisible);
        }
    }

    Vector3 vtPosCenter;
    void Awake() {
        init(type, maxW, maxH, isSmall, maxCard, inone, isTouch);

        vtPosCenter = mainTransform.InverseTransformPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        // Debug.LogError(" mainTransform " + mainTransform.transform.localPosition);
        //Debug.LogError(" mainTransform " + mainTransform.InverseTransformPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0)));
        // Debug.LogError("vtPosCenter " + vtPosCenter);
        //vtPosCenter = cardLoc.transform.localPosition;
    }
    public void init(int type, int maxW, int maxH, bool isSmall, int maxCard,
            bool inone, bool isTouch) {
        this.type = type;
        this.maxW = maxW;
        this.maxH = maxH;
        this.isSmall = isSmall;
        this.maxCard = maxCard;
        if (isSmall) {
            wCard = Card.W_CARD * sizeCardSmall;
            hCard = Card.H_CARD * sizeCardSmall;
        } else {
            wCard = Card.W_CARD;
            hCard = Card.H_CARD;
        }
        this.inone = inone;
        arrCard = new List<Card>();
        for (int i = 0; i < maxCard; i++) {
            GameObject obj = (GameObject)Instantiate(prefabCard);
            obj.transform.SetParent(mainTransform);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            if (gameObject.transform.parent.name.Equals("Player0") && !BaseInfo.gI().isView) {
                obj.GetComponent<RectTransform>().sizeDelta = new Vector3(80, 110);
            }
            Card card = obj.GetComponent<Card>();
            card.setId(52);
            card.gameObject.SetActive(false);
            if (isSmall) {
                card.transform.localScale = new Vector3(sizeCardSmall, sizeCardSmall, sizeCardSmall);
            }
            card.setTouchable(isTouch);
            arrCard.Add(card);
            card.transform.SetSiblingIndex(i);
        }
        setVisibleSobai(false);
        arrIntCard = new List<int>();
        isAllMo = false;
        reSet();
    }
    public void setTypeCard(int type, int maxW, bool isSmall) {
        this.type = type;
        this.maxW = maxW;
        //this.maxW = GameControl.WIDTH - GameControl.WPLAYER - 12;
        this.isSmall = isSmall;
        if (isSmall) {
            wCard = Card.W_CARD * sizeCardSmall;
            hCard = Card.H_CARD * sizeCardSmall;
        } else {
            wCard = Card.W_CARD;
            hCard = Card.H_CARD;
        }
    }
    public void addCard(int id) {
        arrIntCard.Add(id);
        reAddAllCard();
    }

    public void addCard(int id, int[] cardMo) {
        arrIntCard.Add(id);
        reAddAllCard(cardMo);
    }

    public void setCardMo(int[] cards) {
        if (cards == null || cards.Length <= 0) {
            return;
        } else {
            setAllMo(false);
            for (int i = 0; i < cards.Length; i++) {
                if (getCardbyID(cards[i]) != null) {
                    getCardbyID(cards[i]).setMo(true);
                }
            }
        }
    }
    //Mau binh
    public void resetPostionCard(int newMaxW) {
        this.maxW = newMaxW;
        try {
            if (maxW >= (getSize() * wCard)) {
                disCard = wCard;
            } else {
                disCard = maxW / getSize();
            }
            if (getSize() % 2 == 0) {
                for (int i = 0; i < getSize(); i++) {
                    arrCard[i].transform.localPosition = new Vector3(
                            -((int)getSize() / 2 - 0.5f)
                                    * disCard + i * disCard, 0, 0);

                    arrCard[i].transform.SetSiblingIndex(i);
                }
            } else {
                for (int i = 0; i < getSize(); i++) {
                    arrCard[i].transform.localPosition = new Vector3(
                        -((int)getSize() / 2) * disCard
                                + i * disCard, 0, 0);

                    arrCard[i].transform.SetSiblingIndex(i);
                }
            }
        } catch (Exception e) {
            // TODO: handle exception
            Debug.LogException(e);
        }
    }

    public void reAddAllCard() {
        try {
            for (int i = 0; i < maxCard; i++) {
                if (isSmall) {
                    arrCard[i].transform.localScale = new Vector3(sizeCardSmall, sizeCardSmall, sizeCardSmall);
                } else {
                    arrCard[i].transform.localScale = new Vector3(1, 1, 1);
                }
            }
            if (isHaPhom) {
                for (int i = 0; i < getSize(); i++) {
                    arrCard[i].GetComponent<RectTransform>().sizeDelta = new Vector3(40, 60);
                }
            }

            for (int i = 0; i < getSize(); i++) {
                arrCard[i].setId(arrIntCard[i]);
                arrCard[i].gameObject.SetActive(true);
            }
            for (int i = getSize(); i < maxCard; i++) {
                arrCard[i].gameObject.SetActive(false);
            }
            if (inone) {
                for (int i = 0; i < getSize(); i++) {
                    arrCard[i].transform.localPosition = new Vector3(0, 0, 0);
                }
            }
            else
            {
                //Debug.LogError("Is One == false ");
                if (align_Array == Align_Array.Horizontal)
                {
                    #region Ngang
                    if (maxW >= (getSize() * wCard)) {
                        disCard = wCard;
                    } else {
                        disCard = maxW / getSize();
                    }
                    if (type == 0) {
                        setPosWithAnchor();
                    } else if (type == 1) {
                        for (int i = 0; i < getSize(); i++) {
                            arrCard[i].transform.localPosition = new Vector3(i * disCard, 0, 0);
                            arrCard[i].transform.SetSiblingIndex(i);
                        }
                    } else if (type == 2) {
                        for (int i = 0; i < getSize(); i++) {
                            arrCard[i].transform.localPosition = new Vector3(
                                    (-(getSize() - 1) * disCard + i * disCard), 0, 0);

                            arrCard[i].transform.SetSiblingIndex(i);
                        }
                    }
                    #endregion Het Ngang
                } else {
                    #region Doc
                    if (maxH >= (getSize() * hCard)) {
                        disCard = hCard;
                    } else {
                        disCard = maxH / getSize();
                    }

                    if (type == 0) {
                        setPosWithAnchor();
                    } else if (type == 1) {
                        for (int i = 0; i < getSize(); i++) {
                            arrCard[i].transform.localPosition = new Vector3(0, i * disCard, 0);
                            arrCard[i].transform.SetSiblingIndex(i);
                        }
                    } else if (type == 2) {
                        for (int i = 0; i < getSize(); i++) {
                            arrCard[i].transform.localPosition = new Vector3(0,
                                    (-(getSize() - 1) * disCard + i * disCard), 0);
                            arrCard[i].transform.SetSiblingIndex(i);
                        }
                    }
                    #endregion Het Doc
                }
            }
        } catch (Exception e) {
            // TODO: handle exception
        }
    }

    void setPosWithAnchor() {
        switch (align_Anchor) {
            case Align_Anchor.Left:
                left();
                break;
            case Align_Anchor.Right:
                right();
                break;
            case Align_Anchor.Center:
                center();
                break;
            case Align_Anchor.Top:
                top();
                break;
            case Align_Anchor.Bot:
                bot();
                break;
            default:
                break;
        }
    }
    void left() {
        for (int i = 0; i < getSize(); i++) {
            arrCard[i].transform.localPosition = new Vector3(i * disCard, 0, 0);
            arrCard[i].transform.SetSiblingIndex(i);
        }
    }
    void right() {
        for (int i = getSize() - 1; i >= 0; i--) {
            arrCard[i].transform.localPosition = new Vector3(-(getSize() - 1 - i) * disCard, 0, 0);
            arrCard[i].transform.SetSiblingIndex(i);
        }
    }
    void center() {
        if (align_Array == Align_Array.Horizontal) {
            if (getSize() % 2 == 0) {
                for (int i = 0; i < getSize(); i++) {
                    arrCard[i].transform.localPosition = new Vector3(
                        -((int)getSize() / 2 - 0.5f)
                                * disCard + i * disCard, 0, 0);
                    arrCard[i].transform.SetSiblingIndex(i);
                }
            } else {
                for (int i = 0; i < getSize(); i++) {
                    arrCard[i].transform.localPosition = new Vector3(
                        -((int)getSize() / 2) * disCard
                                + i * disCard, 0, 0);
                    arrCard[i].transform.SetSiblingIndex(i);
                }
            }
        } else {
            if (getSize() % 2 == 0) {
                for (int i = 0; i < getSize(); i++) {
                    arrCard[i].transform.localPosition = new Vector3(0,
                        -((int)getSize() / 2 - 0.5f)
                                * disCard + i * disCard, 0);
                    arrCard[i].transform.SetSiblingIndex(i);
                }
            } else {
                for (int i = 0; i < getSize(); i++) {
                    arrCard[i].transform.localPosition = new Vector3(0,
                        -((int)getSize() / 2) * disCard
                                + i * disCard, 0);
                    arrCard[i].transform.SetSiblingIndex(i);
                }
            }
        }
    }
    void top() {

    }
    void bot() {

    }
    //Phom
    public void reAddAllCard(int[] cardMo) {
        try {
            for (int i = 0; i < maxCard; i++) {
                if (isSmall) {
                    arrCard[i].transform.localScale = new Vector3(sizeCardSmall, sizeCardSmall, sizeCardSmall);
                } else {
                    arrCard[i].transform.localScale = new Vector3(1, 1, 1);
                }
            }

            for (int i = 0; i < getSize(); i++) {
                arrCard[i].setId(arrIntCard[i]);
                arrCard[i].gameObject.SetActive(true);
                arrCard[i].setMo(false);
            }

            if (cardMo != null && cardMo.Length > 0) {
                for (int i = 0; i < getSize(); i++) {
                    for (int j = 0; j < cardMo.Length; j++) {
                        if (arrCard[i].getId() == cardMo[j] && !arrCard[i].isMo()) {
                            arrCard[i].setMo(true);
                        }
                    }
                }
            }

            for (int i = getSize(); i < maxCard; i++) {
                arrCard[i].gameObject.SetActive(false);
            }
            if (inone) {
                for (int i = 0; i < getSize(); i++) {
                    arrCard[i].transform.localPosition = new Vector3(0, 0, 0);
                }
            } else {
                if (align_Array == Align_Array.Horizontal) {
                    #region Ngang
                    if (maxW >= (getSize() * wCard)) {
                        disCard = wCard;
                    } else {
                        disCard = maxW / getSize();
                    }
                    if (type == 0) {
                        setPosWithAnchor();
                    } else if (type == 1) {
                        for (int i = 0; i < getSize(); i++) {
                            arrCard[i].transform.localPosition = new Vector3(i * disCard, 0, 0);

                            arrCard[i].transform.SetSiblingIndex(i);
                        }

                    } else if (type == 2) {
                        for (int i = 0; i < getSize(); i++) {
                            arrCard[i].transform.localPosition = new Vector3(
                                    (-(getSize() - 1) * disCard + i * disCard), 0, 0);
                            arrCard[i].transform.SetSiblingIndex(i);
                        }
                    }
                    #endregion Het Ngang
                } else {
                    #region Doc
                    if (maxH >= (getSize() * hCard)) {
                        disCard = hCard;
                    } else {
                        disCard = maxH / getSize();
                    }

                    if (type == 0) {
                        setPosWithAnchor();
                    } else if (type == 1) {
                        for (int i = 0; i < getSize(); i++) {
                            arrCard[i].transform.localPosition = new Vector3(0, i * disCard, 0);
                            arrCard[i].transform.SetSiblingIndex(i);
                        }
                    } else if (type == 2) {
                        for (int i = 0; i < getSize(); i++) {
                            arrCard[i].transform.localPosition = new Vector3(0,
                                    (-(getSize() - 1) * disCard + i * disCard), 0);
                            arrCard[i].transform.SetSiblingIndex(i);
                        }
                    }
                    #endregion Het Doc
                }
            }
        } catch (Exception e) {
            // TODO: handle exception
        }

    }

    public Card getCardbyID(int id) {
        try {
            for (int i = 0; i < getSize(); i++) {
                if (arrCard[i].getId() == id) {
                    return arrCard[i];
                }
            }
        } catch (Exception e) {
            // TODO: handle exception
        }

        return null;
    }

    public Card getCardbyPos(int pos) {
        if (pos > arrCard.Count - 1) {
            pos = arrCard.Count - 1;
        }
        return arrCard[pos];
    }

    public void removeCardByID(int id) {
        for (int i = 0; i < getSize(); i++) {
            if (arrIntCard[i] == id) {
                arrIntCard.RemoveAt(i);
                reAddAllCard();
                return;
            }
        }
    }

    public void removeCardByPos(int pos) {
        if (pos < getSize()) {
            arrIntCard.RemoveAt(pos);
            reAddAllCard();
        }
    }

    public bool isAllMo;

    public void setAllMo(bool isMo) {
        isAllMo = isMo;
        for (int i = 0; i < arrCard.Count; i++) {
            arrCard[i].setMo(isMo);
        }
    }

    public void setCardMoByID(int id, bool isMo) {
        getCardbyID(id).setMo(isMo);
    }

    public void removeAllCard() {
        arrIntCard.Clear();
        reAddAllCard();
    }

    public int getSizeArrayCard() {
        return arrCard.Count;
    }

    public void reSet() {
        setAllMo(false);
        for (int i = 0; i < arrCard.Count; i++) {
            arrCard[i].setChoose(false);
        }
    }

    public int getSize() {
        return arrIntCard.Count;
    }

    bool isHaPhom = false;
    public void setArrCard(int[] cards, bool isHaPhom) {
        this.isHaPhom = isHaPhom;
        arrIntCard.Clear();
        for (int i = 0; i < cards.Length; i++) {
            addCard(cards[i]);
        }
        //Debug.LogError("Set Arr Card " + cards.Length);
        reAddAllCard();
    }

    public void setArrCard(int[] cards, int[] cardMo) {
        arrIntCard.Clear();
        reAddAllCard(cardMo);
        for (int i = 0; i < cards.Length; i++) {
            addCard(cards[i], cardMo);
        }
    }

    public void setArrCard(int[] cards, bool isDearling, bool inone, bool isFlipCard, int pos) {
        if (cards == null) {
            setArrCard(new int[] { }, false);
            return;
        }

        this.inone = inone;
        setArrCard(cards, false);
        //cardImg.SetActive(true);
        for (int i = 0; i < getSize(); i++)
        {
            if (isDearling)
            {
                //Debug.LogError("Get Size " + getSize());
                Card card = getCardbyPos(i);
                Vector3 oldPos = card.gameObject.transform.localPosition;
                //card.transform.parent.SetParent(middleTransform);
                //card.transform.localPosition = middleTransform.transform.localPosition;
                card.transform.parent.SetParent(mainTransform);
                if (pos == 0 && BaseInfo.gI().gameID == GameID.MAUBINH) {
                    card.GetComponent<RectTransform>().sizeDelta = new Vector3(50, 70);
                }
                //Debug.LogError("VT Pos Center " + vtPosCenter);
                card.setvisible(false);
                //StartCoroutine(card.moveTo(oldPos, 0.25f, i * 0.15f, true));
                StartCoroutine(card.moveFromTo(new Vector3(160, 210), oldPos, 0.15f, i * 0.1f, true));
            }
        }
        if (inone) {
            Invoke("setSoBaiOnChia", 1f);
        }
    }

    public void setArrCard(int[] cards, int[] cardMo, bool isDearling,
        bool inone, bool isFlipCard) {
        if (cards == null) {
            setArrCard(new int[] { }, false);
            return;
        }
        if (cards.Length == 0) {
            setArrCard(cards, false);
            return;
        }
        this.inone = inone;
        setArrCard(cards, cardMo);
        for (int i = 0; i < getSize(); i++) {
            if (isDearling) {
                Card card = getCardbyPos(i);
                Vector3 oldPos = card.transform.localPosition;
                //card.transform.SetParent(middleTransform);
                //card.transform.localPosition = middleTransform.localPosition;
                card.transform.SetParent(mainTransform);
                card.setvisible(false);
                //StartCoroutine(card.moveTo(oldPos, 0.25f, i * 0.15f, true));
                StartCoroutine(card.moveFromTo(new Vector3(160, 210), oldPos, 0.15f, i * 0.1f, true));
            }
        }
        if (inone) {
            Invoke("setSoBaiOnChia", 1f);
        }
    }

    void setSoBaiOnChia() {
        setSobai(getSize());
    }

    public int[] getArrCardChoose() { // có sắp xếp, có trả về null
        // return super.getArrCard();
        int[] arr = null;
        for (int i = 0; i < getSize(); i++) {
            if (arrCard[i].isChoose) {
                arr = RTL.insertArray(arr, arrCard[i].getId());
            }
        }
        if (arr != null) {
            arr = RTL.sort(arr);
        }
        return arr;
    }
    public int[] getArrCardAll() { // có sắp xếp, không trả về null
        int[] arr = null;
        for (int i = 0; i < getSize(); i++) {
            arr = RTL.insertArray(arr, arrIntCard[i]);
        }
        if (arr != null) {
            arr = RTL.sort(arr);
        } else {

        }
        if (arr == null) {
            return new int[] { };
        }
        return arr;
    }
    public int[] getArrCardAll2() { // có sắp xếp, không trả về null
        int[] arr = null;
        for (int i = 0; i < getSize(); i++) {
            arr = RTL.insertArray(arr, arrIntCard[i]);
        }
        if (arr == null) {
            return new int[] { };
        }
        return arr;
    }
    public int[] getArrayCardAllTrue() { // không sắp xếp
        int[] arr = null;
        for (int i = 0; i < arrCard.Count; i++) {
            arr = RTL.insertArray(arr, arrCard[i].getId());
        }
        if (arr == null) {
            return new int[] { };
        }
        return arr;
    }
    public List<Card> getArrCardObj() {
        return this.arrCard;
    }
    public void onfireCard(int[] cards) {
        if (cards == null) {
            return;
        }
        //Debug.Log("===================            " + GameControl.instance);
        //sua
        if (BaseInfo.gI().gameID == GameID.TLMNsolo || BaseInfo.gI().gameID == GameID.TLMN || BaseInfo.gI().gameID == GameID.XAM /*|| gameControl.gameID == GameID.TAIXIU*/) {
            GameControl.instance.currentCasino.tableArrCard = cards;
        }
        if (GameControl.instance.currentCasino.tableArrCard2

                            .getArrCardAll2() != null) {
            GameControl.instance.currentCasino.tableArrCard1.
                setArrCard(GameControl.instance.currentCasino.tableArrCard2.getArrCardAll2(), false, false, false, -1);
            //foreach (Card card in gameControl.currentCasino.tableArrCard1.getArrCardObj()) {
            //    card.setDepth(0);
            //}
        }
        GameControl.instance.currentCasino.tableArrCard2.setArrCard(
                cards, false, false, false, -1);
        //foreach (Card card in gameControl.currentCasino.tableArrCard2.getArrCardObj()) {
        //    card.setDepth(1);
        //}
        GameControl.instance.currentCasino.tableArrCard2.gameObject.SetActive(false);
        for (int i = 0; i < cards.Length; i++) {
            Card card0 = getCardbyID(cards[i]);
            if (card0 == null) {
                card0 = arrCard[0];
            }
            if (card0 == null) {
                return;
            }
            card0.transform.parent = GameControl.instance.currentCasino.tableArrCard2.transform;
            Vector3 oldPos = card0.transform.localPosition;
            Card card1 = GameControl.instance.currentCasino.tableArrCard2.getCardbyID(cards[i]);
            Vector3 newPos = card1.transform.localPosition;
            card1.transform.localPosition = oldPos;
            StartCoroutine(card1.moveTo(newPos, 0.15f, 0f, true));
            card0.transform.parent = mainTransform;
        }

        StartCoroutine(setActiveTableArrCard2(cards));
    }
    IEnumerator setActiveTableArrCard2(int[] cards) {
        yield return new WaitForSeconds(0f);
        for (int i = 0; i < cards.Length; i++) {
            this.removeCardByID(cards[i]);
        }
        yield return new WaitForSeconds(0f);
        //sua
        GameControl.instance.currentCasino.tableArrCard2.gameObject.SetActive(true);
    }

    public void setSobai(int soBai) {
        this.soBai = soBai;
        if (lb_SoBai == null) {
            return;
        }
        if (soBai <= 0) {
            lb_SoBai.gameObject.SetActive(false);
        } else {
            lb_SoBai.gameObject.SetActive(true);
        }
        lb_SoBai.text = soBai + "";
    }

    public int getSoBai() {
        return soBai;
    }

    public void setTouch(bool isTouch) {
        this.isTouch = isTouch;
        for (int i = 0; i < arrCard.Count; i++) {
            arrCard[i].setTouchable(isTouch);
        }
    }
}
