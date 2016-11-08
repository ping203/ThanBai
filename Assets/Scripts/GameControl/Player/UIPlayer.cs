using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIPlayer : MonoBehaviour {

    public GameObject master;
    //public GameObject ready;

    public ArrayCard cardHand;
    public ArrayCard cardHand3Cay;
    // phom
    public ArrayCard[] cardPhom;

    // mau binh
    public ArrayCard[] cardMauBinh;
    public Vector3[] posCardMB;
    
    public GameObject body;
    public Timer timer;
    public Chat chat;
    public Button buttonInvite;
    public Toggle toggleAction;
    public Image img_avatar;
    public RawImage raw_avatar;
    public Text lb_name_sansang;
    public Text lb_money;

    public Image sp_sap3chi;
    public Image sp_thang;
    public Image sp_xoay;
    public Image sp_typeCard;
    public Image sp_action;
    public Image sp_xepXong;
    public Image sp_lung;
    public Image sp_baoSam;

    public GameObject lb_money_result2;
    //public Text lb_money_am;

    public Chip chip;

    //public NPCController npcController;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
