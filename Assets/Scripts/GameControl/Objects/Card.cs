using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
public class Card : MonoBehaviour {
    public delegate void CallBack();
    public CallBack onClickOK;
    public Image cardMo;
    public GameObject child;
    public Button img_card;

    private bool isCardMo;
    private int id;

    float deltaY = 0;
    public bool isChoose;

    public static int[] pNumber = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12,
                            13,14,15, 16, 17, 18, 19, 20, 21, 22, 23, 24,25,
                            26,27, 28, 29, 30, 31,32, 33, 34, 35, 36,37, 38,
                            39,40, 41, 42, 43, 44, 45, 46, 47, 48,49, 50, 51,
                            52 };
    public static int[] aNumber = { 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 0, 1, 15, 16,
            17, 18, 19, 20, 21, 22, 23, 24, 25, 13, 14, 28, 29, 30, 31, 32, 33,
            34, 35, 36, 37, 38, 26, 27, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50,
            51, 39, 40, 52 };

    public static int[] cardPaint = aNumber;

    public static void setCardType(int type) {
        if (type == 0) {// phom
            cardPaint = pNumber;
        } else {
            cardPaint = aNumber;
        }
    }

    // Use this for initialization
    void Awake() {
        setId(52);
    }
    const float deltaHeight = 15;
    // Update is called once per frame
    void Update() {
        if (isChoose) {
            if (deltaY < deltaHeight) {
                deltaY = deltaY + 100 * Time.deltaTime;
            } else {
                deltaY = deltaHeight;
            }
        } else {
            if (deltaY > 0) {
                deltaY = deltaY - 100 * Time.deltaTime;
            } else {
                deltaY = 0;
            }
        }
        child.transform.localPosition = new Vector3(0, deltaY, 0);

    }
    public int getId() {
        return id;
    }

    public void setId(int id) {
        if (id > 52 || id < 0) {
            id = 52;
        }
        this.id = id;
        isChoose = false;
        //LoadAssetBundle.LoadSprite(img_card.image, "card", "cardall_" + cardPaint[id]);
        //Debug.LogError("Res.list_cards.Length " + Res.list_cards.Length);
        //Debug.LogError("img_card.image.sprite " + Res.getCardByID(cardPaint[id]));
        img_card.image.sprite = Res.getCardByID(cardPaint[id]);
    }

    public void setSprite(int i) {
        //Debug.LogError("img_card.image.sprite " + Res.getCardByID(cardPaint[id]));
        img_card.image.sprite = Res.getCardByID(id);
    }

    public void setMo(bool isCardMo) {
        this.isCardMo = isCardMo;
        cardMo.gameObject.SetActive(isCardMo);
    }

    public bool isMo() {
        return isCardMo;
    }

    public const float W_CARD = 50f;
    public const float H_CARD = 70f;
    public void OnClickCard() {
            if (onClickOK != null) {
                onClickOK.Invoke();
            }

    }
    public void setListenerClick(CallBack click) {
        this.onClickOK = click;
    }
    public IEnumerator moveFromTo(Vector3 from, Vector3 to, float dur, float wait, bool isDeal) {
        yield return new WaitForSeconds(wait);
        SoundManager.instance.startchiabaiAudio();
        setvisible(true);
        gameObject.transform.localPosition = from;
        transform.DOScale(Vector3.one, dur);
        transform.DOLocalMove(to, dur);
    }

    public IEnumerator moveTo(Vector3 to, float dur, float wait, bool isDeal) {

        int ids = id;
        if (isDeal) {
            setvisible(false);
            setId(52);
        }

        yield return new WaitForSeconds(wait);
        setvisible(true);
        if (isDeal) {
            setId(ids);
        }
        //Debug.LogError("Frome " + transform.localPosition + " To " + to);
        transform.DOLocalMove(to, dur);
        SoundManager.instance.startchiabaiAudio();
    }
    public void setChoose(bool isChoose) {
        this.isChoose = isChoose;
        if (isChoose) {
            transform.DOLocalMoveY(deltaHeight, 0.2f);
        } else {
            transform.DOLocalMoveY(0, 0.2f);
        }
    }

    public void setTouchable(bool isTouchable) {
        img_card.enabled = isTouchable;
        img_card.image.raycastTarget = isTouchable;
    }

    public void setvisible(bool isVisible) {
        gameObject.SetActive(isVisible);
    }
}
