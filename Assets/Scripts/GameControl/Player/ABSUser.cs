using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;

public class ABSUser : MonoBehaviour {
    public BaseCasino casinoStage;
    public UIPlayer uiplayer;
    private Vector3 posChip = new Vector3();

    [HideInInspector]
    public GameObject master;
    [HideInInspector]
    public ArrayCard cardHand;
    [HideInInspector]
    public ArrayCard cardHand3Cay;
    // phom
    [HideInInspector]
    public ArrayCard[] cardPhom;
    // mau binh
    [HideInInspector]
    public ArrayCard[] cardMauBinh;
    [HideInInspector]
    public Vector3[] posCardMB;
    [HideInInspector]
    public GameObject body;
    [HideInInspector]
    public Timer timer;
    [HideInInspector]
    public Chat chat;
    [HideInInspector]
    public Button buttonInvite;
    [HideInInspector]
    public Toggle toggleAction;
    [HideInInspector]
    public Image img_avatar;
    [HideInInspector]
    public RawImage raw_avatar;
    [HideInInspector]
    public Text lb_name_sansang;
    [HideInInspector]
    public Text lb_money;
    [HideInInspector]
    public Image sp_sap3chi;
    [HideInInspector]
    public Image sp_thang;
    [HideInInspector]
    public Image sp_xoay;
    [HideInInspector]
    public Image sp_typeCard;
    [HideInInspector]
    public Image sp_action;
    [HideInInspector]
    public Image sp_xepXong;
    [HideInInspector]
    public Image sp_lung;
    [HideInInspector]
    public Image sp_baoSam;
    [HideInInspector]
    public GameObject lb_money_result2;
    [HideInInspector]
    public Chip chip;
    //[HideInInspector]
    //public NPCController npcController;

    public ChipBay chipBay;

    public int[] card_win = new int[3];
    public int[] allCardPhom;

    public int diem = 0;

    public string st_name;
    public string display_name;
    private long folowMoney;
    public int pos, serverPos;
    public int[][] cardhand_xepbai = null;
    public int id_xepbai = 0;
    public bool isUserXepbai = false;
    public sbyte gender;

    protected bool isMasters;
    private bool isReadys;
    private bool isSits;
    private bool isPlayings;
    public bool isVisibleXoays;
    protected bool isTurns;

    void Awake() {
        master = uiplayer.master;
        cardHand = uiplayer.cardHand;
        cardHand3Cay = uiplayer.cardHand3Cay;
        // phom
        cardPhom = uiplayer.cardPhom;

        // mau binh
        cardMauBinh = uiplayer.cardMauBinh;
        posCardMB = uiplayer.posCardMB;

        body = uiplayer.body;
        timer = uiplayer.timer;
        chat = uiplayer.chat;
        buttonInvite = uiplayer.buttonInvite;
        toggleAction = uiplayer.toggleAction;
        //buttonKick = uiplayer.buttonKick;
        //buttonInfo = uiplayer.buttonInfo;
        img_avatar = uiplayer.img_avatar;
        raw_avatar = uiplayer.raw_avatar;
        lb_name_sansang = uiplayer.lb_name_sansang;
        lb_money = uiplayer.lb_money;

        sp_sap3chi = uiplayer.sp_sap3chi;
        sp_thang = uiplayer.sp_thang;
        sp_xoay = uiplayer.sp_xoay;
        sp_typeCard = uiplayer.sp_typeCard;
        sp_action = uiplayer.sp_action;
        if (sp_action != null)
            vt_sp_action = sp_action.transform.localPosition;
        sp_xepXong = uiplayer.sp_xepXong;
        sp_lung = uiplayer.sp_lung;
        sp_baoSam = uiplayer.sp_baoSam;

        lb_money_result2 = uiplayer.lb_money_result2;
        vt_lb_money_result2 = lb_money_result2.transform.localPosition;

        chip = uiplayer.chip;
        if (chip != null) {
            posChip = chip.transform.localPosition;
        }
        //npcController = uiplayer.npcController;
        if (toggleAction != null)
            toggleAction.onValueChanged.AddListener(toggleActiononChange);
        if (buttonInvite != null)
            buttonInvite.onClick.AddListener(clickButtonInvite);
        //    delegate {
        //    clickButtonInvite();
        //});
    }

    public void toggleActiononChange(bool isCheck) {
        if (BaseInfo.gI().isView) {
            return;
        } else {
            if (isCheck) {
                if (getName().Equals(BaseInfo.gI().mainInfo.nick)){
                    clickButtonInfo();
                    return;
                }
                LoadAssetBundle.LoadScene(Res.INFO_INGAME_AB, Res.INFO_INGAME_NAME, () => {
                    NPCController.instance.name_player = getName();
                    NPCController.instance.tg = toggleAction;
                    //clickButtonInfo();
                    //NPCController.instance.objMain.transform.DOLocalMoveX(250f, 0.4f);
                });
            }
            //if (npcController != null) {
            //    npcController.name_player = getName();
            //    npcController.setShowKick(isMasters);
            //}
            //if (casinoStage.players[0].getName().Equals(getName())) {
            //    clickButtonInfo();
            //    return;
            //}
        }
    }

    private float dura = 0;
    // Update is called once per frame
    public virtual void setInfo(string diem) {

    }
    bool isOne = true;
    public void Update() {
        if (isTurns && getName().Length != 0) {
            if (!timer.gameObject.activeInHierarchy)
                timer.gameObject.SetActive(true);
            dura += Time.deltaTime;
            if (dura < BaseInfo.gI().timerTurnTable) {
                float percent;
                if (casinoStage.timeReceiveTurn == 0) {
                    percent = 1;
                } else {
                    percent = dura * 100 / casinoStage.timeReceiveTurn;
                }
                float temp = 100f - percent;
                if (temp > 75) {
                    if (pos == 0 && casinoStage.isStart && isOne && !BaseInfo.gI().isView) {
                        SoundManager.instance.startCountDownAudio();
                        isOne = false;
                    }
                    if (pos == 0 && !BaseInfo.gI().isView) {
                        SoundManager.instance.pauseSound();
                    }
                    timer.sprite.color = Color.green;

                } else
                if (temp > 50) {
                    if (pos == 0 && !BaseInfo.gI().isView) {
                        SoundManager.instance.pauseSound();
                    }
                    timer.sprite.color = Color.green;
                } else if (temp > 25) {
                    if (pos == 0 && !BaseInfo.gI().isView) {
                        SoundManager.instance.pauseSound();
                    }
                    timer.sprite.color = Color.yellow;
                } else {
                    if (pos == 0 && !BaseInfo.gI().isView) {
                        SoundManager.instance.pauseSound();
                    }
                    //if (pos == 0 && isOne && !BaseInfo.gI().isView) {
                    //    SoundManager.instance.startCountDownAudio();
                    //    isOne = false;
                    //}

                    timer.sprite.color = Color.red;
                }

                timer.setPercentage(percent);
            }
            if (dura >= BaseInfo.gI().timerTurnTable) {
                if (pos == 0 && !BaseInfo.gI().isView) {
                    SoundManager.instance.pauseSound();
                }
            }
        } else if (timer.gameObject.activeInHierarchy) {
            dura = 0; // loop
            if (pos == 0 && !BaseInfo.gI().isView) {
                SoundManager.instance.pauseSound();
            }

            timer.gameObject.SetActive(false);
        }
        timeAction += Time.deltaTime;
        if (timeAction >= 10) {
            isAction = true;
        }
        //if (www != null) {
        //    if (www.isDone && !isOne) {
        //        raw_avatar.texture = www.texture;
        //        isOneAvata = true;
        //    }
        //}
    }

    bool isAction = true;
    float timeAction = 0;
    public void setName(string name) {
        this.st_name = name;
    }
    public void setDisplayeName(string name) {
        this.display_name = name;
        string nameFN;
        if (name.Length > 8) {
            nameFN = name.Substring(0, 8) + "...";
        } else {
            nameFN = name;
        }

        lb_name_sansang.text = nameFN;
        lb_name_sansang.gameObject.SetActive(true);
    }
    public string getName() {
        return st_name;
    }
    // string moneyfly="";
    public void setMoney(long folowMoney) {
        lb_money.gameObject.SetActive(true);
        long m = folowMoney - this.folowMoney;
        if (m > 0) {
            flyMoney("+" + BaseInfo.formatMoneyDetailDot(m) + "");
        } else if (m < 0) {
            flyMoney("-" + BaseInfo.formatMoneyDetailDot(-m) + "");
        }

        this.folowMoney = folowMoney;
        if (folowMoney.ToString().Length < 7) {
            lb_money.text = BaseInfo.formatMoneyDetailDot(folowMoney);
        } else {
            lb_money.text = BaseInfo.formatMoneyNormal(folowMoney);
        }
    }
    Vector3 vt_lb_money_result2;// = lb_money_result2.transform.localPosition;
    public void flyMoney(string text) {
        lb_money_result2.SetActive(true);
        lb_money_result2.transform.DOKill();
        lb_money_result2.GetComponent<Text>().text = text;
        lb_money_result2.transform.localPosition = vt_lb_money_result2 + new Vector3(0, 120, 0);
        lb_money_result2.transform.DOLocalMoveY(0, 0.5f);
        //Fixed by chaunv.
        if (gameObject.activeInHierarchy == true) {
            StartCoroutine(setVisible(lb_money_result2, 2f));
        }
    }

    public void setMoneyMB(long money) {
        string m = "";
        if (money > 0)
            m = "+";
        else {
            money = Math.Abs(money);
            m = "-";
        }
        //string m2 = m;
        if (money.ToString().Length < 7) {
            m += ("" + BaseInfo.formatMoneyDetailDot(money));
        } else {
            m += ("" + BaseInfo.formatMoneyNormal(money));
        }

        flyMoney(m + "");
    }

    protected IEnumerator setVisible(GameObject obj, float dur) {
        yield return new WaitForSeconds(dur);
        obj.gameObject.SetActive(false);
        lb_money_result2.transform.DOKill();

    }
    public long getFgetFolowMoney() {
        return folowMoney;
    }
    public void setFollowMoney(long followmoney) {
        this.folowMoney = followmoney;
    }
    public void setSit(bool isSit) {
        this.isSits = isSit;
        //Debug.LogError("Image_avatar " + (img_avatar == null) + gameObject.name);
        //if()
        img_avatar.transform.parent.gameObject.SetActive(isSit);
    }
    public virtual void setExit() {
        //body.SetActive(false);
        setSit(false);
        setReady(false);
        setMaster(false);
        resetData();
        setMoneyChip(0);
        lb_money.text = "";

        //lb_money_result.gameObject.SetActive(false);
        if (lb_money_result2 != null)
            lb_money_result2.SetActive(false);

        //if (buttonKick != null)
        //    buttonKick.gameObject.SetActive(false);

        if (chat != null)
            chat.gameObject.SetActive(false);

        //lb_money_am.gameObject.SetActive(false);

        setInvite(true);
        setName("");
        setDisplayeName("");
    }

    public void CreateInfoPlayer(PlayerInfo pl) {
        body.SetActive(true);
        setMoneyChip(0);
        gender = pl.gender;
        //setGendel(gender);
        setName(pl.name);
        setDisplayeName(pl.displayname);
        folowMoney = pl.folowMoney;
        lb_money.gameObject.SetActive(true);
        if (folowMoney.ToString().Length < 7) {
            lb_money.text = ("" + BaseInfo.formatMoneyDetailDot(folowMoney));
        } else {
            lb_money.text = ("" + BaseInfo.formatMoneyNormal(folowMoney));
        }
        setMaster(pl.isMaster);
        if (!isMaster()) {
            //setReady(pl.isReady);
            isReadys = pl.isReady;
        }
        setInvite(pl.isVisibleInvite);
        serverPos = pl.posServer;
        setSit(true);

        isSits = true;
        isPlayings = false;

        if (cardHand != null) {
            cardHand.removeAllCard();
        }

        if (sp_sap3chi != null) {
            sp_sap3chi.gameObject.SetActive(false);
        }

        if (sp_action != null) {
            sp_action.gameObject.SetActive(false);
        }
        if (sp_xoay != null) {
            sp_xoay.gameObject.SetActive(false);
        }

        if (sp_thang != null) {
            sp_thang.gameObject.SetActive(false);
        }

        if (sp_baoSam != null) {
            sp_baoSam.gameObject.SetActive(false);
        }
        if (sp_xepXong != null) {
            sp_xepXong.gameObject.SetActive(false);
        }
        if (sp_lung != null) {
            sp_lung.gameObject.SetActive(false);
        }

        if (lb_money_result2 != null)
            lb_money_result2.SetActive(false);

        //www = null;
        ///isOneAvata = false;
        if (pl.link_avatar != "") {
            //www = new WWW(pl.link_avatar);
            // img_avatar.gameObject.SetActive(false);
            // raw_avatar.gameObject.SetActive(true);
            StartCoroutine(getAvata(pl.link_avatar));
        } else {
            img_avatar.gameObject.SetActive(true);
            raw_avatar.gameObject.SetActive(false);
            //img_avatar.sprite = Res.getAvataByID(pl.idAvata);//Res.list_avata[idAvata + 1];
            //Debug.LogError("pl.idAvata " + pl.idAvata);
            //pl.idAvata++;
            if (pl.idAvata <= 1) {
                pl.idAvata = 1;
            } else if (pl.idAvata >= 60) {
                pl.idAvata = 60;
            }
            LoadAssetBundle.LoadSprite(img_avatar, Res.AS_AVATA, "" + pl.idAvata);
        }
    }

    // WWW www;
    //   bool isOneAvata = false;

    IEnumerator getAvata(string link) {
        WWW www = new WWW(link);
        yield return www;
        img_avatar.gameObject.SetActive(false);
        raw_avatar.gameObject.SetActive(true);
        raw_avatar.texture = www.texture;
        www.Dispose();
        www = null;
    }

    public bool isSit() {
        return isSits;
    }

    public void setInvite(bool p) {
        if (buttonInvite != null) {
            buttonInvite.gameObject.SetActive(p);
        }

        if (toggleAction != null) {
            //toggleAction.isOn = false;
            toggleAction.gameObject.SetActive(!p);
        }
    }

    public bool isMaster() {
        return isMasters;
    }

    public virtual void setMaster(bool isMaster) {
        this.isMasters = isMaster;
        if (master != null)
            master.SetActive(isMaster);
    }


    public bool isReady() {
        return isReadys;
    }

    public void setReady(bool isReady) {
        isReadys = isReady;
        //ready.SetActive(isReady);
        if (isReady) {
            lb_name_sansang.text = Res.TXT_SANSANG;
        } else {
            setDisplayeName(display_name);
        }
    }

    public void setPlaying(bool isPlaying) {
        this.isPlayings = isPlaying;
        PlayerPrefs.SetString("isplaying", isPlaying.ToString());
        Debug.LogError("Is Playing " + isPlaying);
        PlayerPrefs.Save();
    }

    public bool isPlaying() {
        return isPlayings;
    }

    public bool isTurn() {
        return isTurns;
    }

    public void setTurn(bool isTurn) {
        this.isTurns = isTurn;
    }

    private IEnumerator TimeWaitAnimation(float waitTime) {
        setTurn(false);

        yield return new WaitForSeconds(waitTime);
        // TODO Auto-generated method stub
        //if (cardHand != null)
        //{
        //    cardHand.reSet();
        //    cardHand.setSobai(0);
        //    cardHand.setAllMo(false);
        //    cardHand.removeAllCard();
        //}

        if (sp_sap3chi != null) {
            sp_sap3chi.gameObject.SetActive(false);
        }

        if (sp_thang) {
            sp_thang.gameObject.SetActive(false);
        }

        if (sp_xoay) {
            sp_xoay.gameObject.SetActive(false);
        }

        //setDisplayeName(display_name);
        if (sp_typeCard != null) {
            sp_typeCard.gameObject.SetActive(false);
        }
        if (sp_action != null) {
            sp_action.gameObject.SetActive(false);
        }

        if (sp_xepXong != null)
            sp_xepXong.gameObject.SetActive(false);
        if (sp_baoSam != null)
            sp_baoSam.gameObject.SetActive(false);
        if (sp_lung != null) {
            sp_lung.gameObject.SetActive(false);
        }
    }

    public virtual void resetData() {
        //Debug.LogError("resetData===============");
        try {
            if (gameObject.activeInHierarchy == true) {
                StartCoroutine(TimeWaitAnimation(1.5f));
            }
            setDisplayeName(display_name);
            if (cardHand != null) {
                cardHand.reSet();
                cardHand.setSobai(0);
                cardHand.setAllMo(false);
                cardHand.removeAllCard();
            }

            if (cardHand3Cay != null) {
                cardHand3Cay.setAllMo(false);
                cardHand3Cay.removeAllCard();
            }
            if (cardMauBinh != null) {
                for (int i = 0; i < cardMauBinh.Length; i++) {
                    cardMauBinh[i].setAllMo(false);
                    cardMauBinh[i].removeAllCard();
                }
            }
            if (chipBay != null) {
                chipBay.onMoveto(0, 1);
            }
            if (sp_typeCard != null) {
                sp_typeCard.gameObject.SetActive(false);
            }
        } catch (Exception e) {
            Debug.LogException(e);
        }
    }

    public void setVisibleRank(bool isVi) {
        sp_thang.gameObject.SetActive(isVi);
        sp_xoay.gameObject.SetActive(isVi);
        if (!isVi) {
            sp_thang.gameObject.SetActive(false);
            sp_xoay.gameObject.SetActive(false);
            if (sp_baoSam != null) {
                sp_baoSam.gameObject.SetActive(false);
            }
            if (sp_xepXong != null) {
                sp_xepXong.gameObject.SetActive(false);
            }
            if (sp_lung != null) {
                sp_lung.gameObject.SetActive(false);
            }
            cardHand.setAllMo(false);
        }
    }

    public virtual void setCardHand(int[] card, bool isDearling,
            bool inone, bool isFlipCard) {
        cardHand.setArrCard(card, isDearling, inone, isFlipCard, -1);
    }

    public virtual void setCardHand(int[] card, int[] cardMo, bool isDearling,
        bool inone, bool isFlipCard) {
        cardHand.setArrCard(card, cardMo, isDearling, inone, isFlipCard);
    }

    public void setReceiveTurnTime(long timeReceiveTurn) {
        // TODO Auto-generated method stub
        isTurns = true;
        dura = 0;
        isOne = true;
    }

    public virtual void setCardHandInFinishGame(int[] cards) {
        cardHand.setSobai(0);
        cardHand.StopAllCoroutines();
        cardHand.setArrCard(cards, false, false, false, -1);
        Invoke("resetData2", 10f);
    }
    void resetData2() {
        if (!casinoStage.isStart) {
            resetData();
        }
    }
    //public string[] ani_thang = new string[] { "rank_u", "rank_thang",
    //          "rank_thangtrang", "rank_mom", "rank_cong", "rank_lung"};

    public virtual void setRank(int rank) {
        // 0 mom, 1 nhat, 2 nhi, 3 ba, 4 bet, 5 u

        //Debug.Log("setRank============ > " + rank);
        int idTR = -1;
        switch (rank) {
            case 0:
                idTR = 3;
                if (pos == 0 && !BaseInfo.gI().isView) {
                    SoundManager.instance.startMomAudio();
                }
                cardHand.setAllMo(true);
                break;
            case 1:
                idTR = 1;
                //sp_xoay.gameObject.SetActive(true);
                LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Win_Effect", (obj) => {
                    objWinEffect = obj;
                    objWinEffect.transform.SetParent(gameObject.transform);
                    objWinEffect.transform.localPosition = Vector3.zero;
                });
                //Debug.LogError("Load Win Effect 11");
                if (pos == 0 && !BaseInfo.gI().isView) {
                    SoundManager.instance.startWinAudio();
                }
                cardHand.setAllMo(false);
                break;
            case 2:
            case 3:
                if (pos == 0) {
                    SoundManager.instance.startBaAudio();
                }
                break;
            case 4:
                if (pos == 0 && !BaseInfo.gI().isView) {
                    SoundManager.instance.startLostAudio();
                }
                cardHand.setAllMo(true);
                break;
            case 5:
                idTR = 0;
                //sp_xoay.gameObject.SetActive(true);
                LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Win_Effect", (obj) => {
                    objWinEffect = obj;
                    objWinEffect.transform.SetParent(gameObject.transform);
                    objWinEffect.transform.localPosition = Vector3.zero;
                    //Debug.LogError("Load Win Effect 55");
                });

                if (pos == 0 && !BaseInfo.gI().isView) {
                    SoundManager.instance.startUAudio();
                }
                break;

            default:
                break;
        }

        if (idTR >= 0 && idTR <= 5) {
            sp_thang.gameObject.SetActive(true);
            //Debug.Log(ani_thang.Length + " rank thang============ > " + idTR);
            //sp_thang.sprite = ani_thang[idTR];
            LoadAssetBundle.LoadSprite(sp_thang, Res.AS_UI, Res.ani_thang[idTR], () => {
                sp_thang.SetNativeSize();
            });
        }
        Invoke("setVisibleThang", 3f);
    }

    protected GameObject objWinEffect;

    public void setVisibleThang() {
        sp_thang.gameObject.SetActive(false);
        //sp_xoay.gameObject.SetActive(false);
        Destroy(objWinEffect);
        //Debug.LogError("Destroy Win Effect");
        if (sp_baoSam != null) {
            sp_baoSam.gameObject.SetActive(false);
        }
        if (sp_xepXong != null) {
            sp_xepXong.gameObject.SetActive(false);
        }
        if (sp_lung != null) {
            sp_lung.gameObject.SetActive(false);
        }
    }
    // String[] action = new String[] { "action_xembai", "action_boluot",
    //            "action_check", "action_theo", "action_call", "action_upbo",
    //            "action_fold", "action_to", "action_raise" };
    Vector3 vt_sp_action;
    internal void setAction(int id) {
        if (id < 0 || id > 8) {
            return;
        }
        //sp_action.sprite = GameControl.instance.list_actions_ingame[id];
        sp_action.gameObject.SetActive(true);
        LoadAssetBundle.LoadSprite(sp_action, Res.AS_UI, Res.action_play[id], () => {
            sp_action.SetNativeSize();
        });

        sp_action.transform.localPosition = vt_sp_action;
        sp_action.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        sp_action.transform.DOLocalMoveY(vt_sp_action.y + 40, 0.5f);

        Invoke("setVisibleAction", 2f);
    }
    void setVisibleAction() {
        sp_action.gameObject.SetActive(false);
        sp_action.DOKill();
    }

    internal void setTextChat(string mess) {
        if (mess.Equals("")) {
            return;
        }
        chat.setText(mess);
        bool isMe = false;
        if (pos == 0) {
            isMe = true;
        }
        //casinoStage.historychat.addChat(getName(), mess, isMe);
    }
    public virtual int[] getEatCard() {
        return null;
    }

    public virtual void addToCardFromCard(ArrayCard arrCFrom, ArrayCard arrCTo, int idCard, bool isTouch) {
        Card cardFrom = arrCFrom.getCardbyID(idCard);
        if (arrCFrom.getSize() == 0) {
            return;
        }
        if (cardFrom == null) {
            cardFrom = arrCFrom.getCardbyPos(0);
        }
        Vector3 beginPos = cardFrom.transform.localPosition;

        arrCFrom.removeCardByID(idCard);
        arrCTo.addCard(idCard);

        Card card = arrCTo.getCardbyID(idCard);
        if (card == null) {
            card = arrCTo.getCardbyPos(arrCTo.getSize() - 1);
        }

        Vector3 endPos = card.transform.localPosition;
        card.transform.parent = arrCFrom.transform;
        card.transform.localPosition = beginPos;

        card.transform.parent = arrCTo.transform;
        StartCoroutine(card.moveTo(endPos, 0.2f, 0f, false));

    }

    public virtual void addToCardHand(int card, bool p) {

    }

    public virtual void onEatCard(int card) {

    }

    public virtual void setCardPhom(int[] arrayPhom, bool isHaPhom) {

    }

    public virtual int[] getAllCardPhom() {
        return null;
    }

    public virtual void addToCard(ArrayCard arrC, int idCard, bool isDearling, bool isTouch, bool isSort, bool isEatCard) {
        arrC.addCard(idCard);
        if (isSort) {
            int[] temp = RTL.sort(cardHand.getArrCardAll());
            if (pos == 0) {
                cardHand.setArrCard(temp, false);
            }
        }
        Card card = arrC.getCardbyID(idCard);
        if (card == null) {
            card = arrC.getCardbyPos(arrC.getSize() - 1);
        }
        if (isDearling) {
            Vector3 oldPos = card.gameObject.transform.localPosition;
            card.gameObject.transform.parent = arrC.mainTransform;
            card.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            Debug.LogError("Boc Bai");
            card.gameObject.transform.parent = arrC.transform;
            StartCoroutine(card.moveTo(oldPos, 0.25f, 0, true));
        }
    }

    public long getMoneyChip() {
        if (chip == null) {
            return 0;
        }
        return chip.getMoneyChip();
    }

    public void setSoChip(long money) {
        if (chip != null) {
            chip.setSoChip(money);
        }

    }

    public void setMoneyChip(long money) {
        if (chip != null) {
            chip.setMoneyChipBay(money);
        }
        //if(money > 0)   
        //    ChipBay.instance.InstantiateChip(money);
    }

    internal long getFolowMoney() {
        return folowMoney;
    }

    internal void flyMoney() {

        if (chip != null) {
            if (chip.gameObject.activeInHierarchy) {
                setSoChip(0);
                chip.StopAllCoroutines();
                chip.gameObject.transform.parent = this.gameObject.transform.parent;
                chip.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                chip.gameObject.transform.parent = this.gameObject.transform;
                Vector3 posTo = chip.gameObject.transform.localPosition;
                chip.gameObject.transform.localPosition = posChip;
                chip.transform.DOMove(posTo, 0.6f).OnComplete(ChipMoveFinish);

                //TweenPosition.Begin();
            }

        }
    }

    void ChipMoveFinish() {
        chip.gameObject.transform.localPosition = posChip;
        setMoneyChip(0);
    }

    /*
    internal void flyMoney() {
        if (chip != null) {
            if (chip.gameObject.activeInHierarchy) {
                setSoChip(0);
                chip.StopAllCoroutines();
                chip.gameObject.transform.parent = this.gameObject.transform.parent;
                chip.gameObject.transform.localPosition = new Vector3(0, 0, 0);
                chip.gameObject.transform.parent = this.gameObject.transform;
                Vector3 posTo = chip.gameObject.transform.localPosition;
                chip.gameObject.transform.localPosition = posChip;

                Tweener tp = chip.transform.DOLocalMove(posTo, 0.6f).OnComplete(delegate {
                    chip.gameObject.transform.localPosition = posChip;
                    setMoneyChip(0);
                });
            }
        }
    }
    */
    public virtual void setLoaiBai(int p) {
    }
    public void clickButtonInvite() {
        if (BaseInfo.gI().isView) {
            SendData.onJoinTable(BaseInfo.gI().idTable, "", -1);
        } else {
            //casinoStage.gameControl.panelWaiting.onShow();
            PanelWaiting.instance.ShowLoading();
            SendData.onGetListInvite();
        }
    }

    /*public virtual void setXepXong (int p) {
        //throw new NotImplementedException ();
        //Debug.Log ("setXepXong");
    }*/

    public void clickButtonInfo() {
        //SendData.onViewInfoFriend(getName());
        LoadAssetBundle.LoadScene(Res.INFOPLAYER_AB, Res.INFOPLAYER_NAME, ()=> {
            PanelInfoPlayer.instance.InfoMe();
        });
        //casinoStage.gameControl.panelWaiting.onShow();
        //PopupAndLoadingScript.instance.ShowLoading();
        //if (buttonInfo != null)
        //    buttonInfo.gameObject.SetActive(false);
        //if (buttonKick != null)
        //    buttonKick.gameObject.SetActive(false);

        //if (toggleAction != null)
        //    toggleAction.isOn = false;
    }
    public void clickButtonDuoi() {
        SendData.onKick(getName());
        //if (buttonInfo != null)
        //    buttonInfo.gameObject.SetActive(false);
        //if (buttonKick != null)
        //    buttonKick.gameObject.SetActive(false);
        //if (toggleAction != null)
        //    toggleAction.isOn = false;
    }

    public void setLung(bool islung) {
        sp_lung.gameObject.SetActive(islung);
        if (islung) {
            sp_lung.transform.DOScale(1.2f, 0.5f);
            StartCoroutine(waitLung(sp_lung.gameObject));
        }
    }

    IEnumerator waitLung(GameObject obj) {
        yield return new WaitForSeconds(2f);
        //TweenScale.Begin(obj, 0.5f, new Vector3(0, 0, 0));
        obj.transform.DOScale(0, 0.5f);
        yield return new WaitForSeconds(0.1f);
        obj.SetActive(false);
    }

    public void baoSam() {
        sp_baoSam.gameObject.SetActive(true);
    }

    public virtual void setThangTrang(int type) {

    }

    public void resetPositionSp_TypeCard() {
        if (sp_typeCard != null) {
            sp_typeCard.transform.localPosition = new Vector3(0, -50, 0);
        }
    }

    public void onMoveSp_TypeCardToPlayer() {
        sp_typeCard.transform.DOLocalMove(Vector3.zero, 0.6f);
    }

    public void setXepXong(bool isXong) {
        sp_xepXong.gameObject.SetActive(isXong);
        if (isXong) {
            sp_xepXong.transform.DOKill();
            sp_xepXong.transform.DOScale(1.2f, 0.4f).SetLoops(-1);
        }
    }

    public void setSap3Chi(bool isSap) {
        sp_sap3chi.gameObject.SetActive(isSap);
        if (isSap)
            sp_sap3chi.transform.DOScale(1.2f, 0.4f).SetLoops(-1);
        Invoke("setVisibleSap3Chi", 2);
    }

    void setVisibleSap3Chi() {
        sp_sap3chi.gameObject.SetActive(false);
    }

    public void setTextMDSmall(string text) {
        lb_money_result2.SetActive(true);
        lb_money_result2.GetComponent<Text>().text = text;
        lb_money_result2.transform.localPosition = new Vector3(0, -75, 0);
        lb_money_result2.transform.DOMove(new Vector3(0, 20, 0), .5f);
        StartCoroutine(setVisible(lb_money_result2, 3f));
    }
}
