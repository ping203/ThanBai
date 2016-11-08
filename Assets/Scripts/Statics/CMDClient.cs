using UnityEngine;
using System.Collections;

public class CMDClient
{

    public const sbyte PROVIDER_ID = 72;// ThanBai
    //  public const sbyte PROVIDER_ID = 43;// iHen
    //  public const sbyte PROVIDER_ID = 1;
    //	 public const sbyte PROVIDER_ID = 3;
    //	 public const sbyte PROVIDER_ID = 4;
    //	 public const sbyte PROVIDER_ID = 5;
    //	 public const sbyte PROVIDER_ID = 6;
    //	 public const sbyte PROVIDER_ID = 7;
    //	 public const sbyte PROVIDER_ID = 8;
    //	 public const sbyte PROVIDER_ID = 10;
    //	 public const sbyte PROVIDER_ID = 11;

    public const sbyte CMD_SESSION_ID = -27;
    public const sbyte CMD_LOGIN = 0;
    public const sbyte CMD_LIST_ROOM = 1;
    public const sbyte CMD_LIST_TABLE = 2;
    public const sbyte CMD_JOIN_TABLE = 3;
    public const sbyte CMD_FIRE_CARD = 4;
    public const sbyte CMD_GET_CARD = 5;
    public const sbyte CMD_EAT_CARD = 6;
    public const sbyte CMD_DROP_PHOM = 7;
    public const sbyte CMD_JOIN_GAME = 8;
    public const sbyte CMD_AVATAR_ME = 9;
    public const sbyte CMD_SHOP_AVATAR = 10;
    public const sbyte CMD_BUY_AVATAR = 11;
    public const sbyte CMD_PROFILE = 12;
    public const sbyte CMD_ADD_FRIEND = 13;
    public const sbyte CMD_TOP_PLAYER = 14;
    public const sbyte CMD_START_GAME = 15;
    public const sbyte CMD_EXIT_TABLE = 16;
    public const sbyte CMD_EXIT_ROOM = 17;
    public const sbyte CMD_UPDATE_CURRENT_TABLE = 18;
    public const sbyte CMD_USER_JOIN_TABLE = 19;
    public const sbyte CMD_JOIN_ROOM = 20;
    public const sbyte CMD_EXIT_GAME = 21;
    public const sbyte CMD_SET_TURN = 22;
    public const sbyte CMD_INFOMATION = 23;
    public const sbyte CMD_CHAT_MSG = 24;
    public const sbyte CMD_READY = 25;
    public const sbyte CMD_LIST_USER_IN_ROOM = 26;
    public const sbyte CMD_VESION = 27;
    public const sbyte CMD_MOM = 28;
    public const sbyte CMD_BALANCE = 29;
    public const sbyte CMD_U = 30;
    public const sbyte CMD_GUI_CARD = 31;
    public const sbyte CMD_ALLCARD_FINISH = 32;
    public const sbyte CMD_GAMEOVER = 33;
    public const sbyte CMD_INVITE_FRIEND = 34;
    public const sbyte CMD_ANSWER_INVITE_FRIEND = 35;
    public const sbyte CMD_RESPONSE_INVITE = 36;
    public const sbyte CMD_TOP_RICH = 37;
    public const sbyte CMD_REGISTER = 38;
    public const sbyte CMD_GET_CAPCHA = 39;
    public const sbyte CMD_VIEW = 40;
    public const sbyte CMD_KICK = 41;
    public const sbyte CMD_FRIEND_LIST = 42;
    public const sbyte CMD_UPDATE_WAITTING_ROOM = 43;
    public const sbyte CMD_UPDATE_ROOM = 44;
    public const sbyte CMD_UPDATE_PROFILE = 45;
    public const sbyte CMD_PING_PONG = 46;
    public const sbyte CMD_KILL_PIG = 47;// chat heo
    public const sbyte CMD_PASS = 48;// bo luot
    public const sbyte CMD_GET_INBOX_MESSAGE = 51;
    public const sbyte CMD_SEND_MESSAGE = 52;
    public const sbyte CMD_DEL_MESSAGE = 53;
    public const sbyte CMD_READ_MESSAGE = 54;
    public const sbyte CMD_UNREAD_MESSAGE = 55;
    public const sbyte CMD_GET_SMS_STRUCTURE = 56;
    public const sbyte CMD_ID_GAME = 57;
    public const sbyte CMD_FINISH = 58;// NICK RANK
    public const sbyte CMD_CUOC = 59;
    public const sbyte CMD_THEO = 60;
    public const sbyte CMD_HA_PHOM_TAY = 62;
    public const sbyte CMD_USE_ITEM = 63;
    public const sbyte CMD_ACTIVE = 64;
    public const sbyte CMD_REQUEST_GET_ALL_AVATAR = 65;
    public const sbyte CMD_SET_PASSWORD = 66;
    public const sbyte CMD_SET_NEW_MASTER = 67;
    public const sbyte CMD_CHANGERULETBL = 68;
    public const sbyte CMD_UPDATEMONEY_PLAYER_INTBL = 69;
    public const sbyte CMD_RANK = 70;
    public const sbyte CMD_FINISHTURNTLMN = 71;

    public const sbyte CMD_SERVER_MESSAGE = 101;
    public const sbyte CMD_TRANFER_MONEY = 102;
    public const sbyte CMD_GOP_Y = 103;
    public const sbyte CMD_VIEW_INFO_FRIEND = 104;
    public const sbyte CMD_SET_MONEY = 105;
    public const sbyte CMD_CHAT = 106;
    public const sbyte CMD_SMS = 107;

    public const sbyte PAYCARD = 108;

    public const sbyte CMD_UPDATE_MONEY = 109;
    public const sbyte CMD_UPDATE_VERSION = 110;
    public const sbyte CMD_GET_PASS = 111;
    public const sbyte CMD_ADD_FRIEND_CHAT = 113;
    public const sbyte CMD_LIST_INVITE = 114;
    public const sbyte CMD_SEND_PROVIDER = -1;
    public const sbyte MATCH_TURN = -2;

    public const sbyte INTRODUCE_FRIEND = -118;

    public const sbyte CMD_JOINCHATROOM = -114;
    public const sbyte CMD_MSGCHATROOM = -113;
    public const sbyte CMD_AUTOJOINTABLE = -112;
    public const sbyte CMD_TBLID = -111;
    public const sbyte CMD_INFOPOCKERTABLE = -109;
    public const sbyte CMD_ADDCARDTABLE_POCKER = -106;
    public const sbyte CMD_GETPHONECSKH = -58;
    public const sbyte CMD_ALERT_LINK = -103;
    public const sbyte CMD_INFO_WINPLAYER = -101;
    public const sbyte CMD_INFOPLAYER_TBL = -100;
    public const sbyte CMD_INFO_GIFT = -99;
    public const sbyte CMD_SEND_GIFT = -98;

    public const sbyte CMD_START_GAME_VIEW = -55;
    public const sbyte CMD_LIST_PRODUCT = -54;
    public const sbyte CMD_TIEN_VAY = -50;
    public const sbyte CMD_GET_MONEY = -97;
    public const sbyte CMD_TIME_AUTOSTART = -96;

    public const sbyte CMD_START_FLIP = -95;
    public const sbyte CMD_FLIP_CARD = -94;

    public const sbyte CMD_UPDATE_VERSION_NEW = -93;
    public const sbyte CMD_REGISTER_GCM = -92;

    public const sbyte CMD_FINAL_MAUBINH = -91;
    public const sbyte CMD_CALMB_RANKS = -90;
    public const sbyte CMD_WINMAUBINH = -89;
    public const sbyte CMD_INFO_ME = -88;

    public const sbyte CMD_BEGINRISE_3CAY = -87;
    public const sbyte CMD_FLIP_3CAY = -86;
    public const sbyte CMD_CUOC_3CAY = -85;
    public const sbyte CMD_ADD_MONEY = -84;
    public const sbyte CMD_FOR_VIEW = -83;
    public const sbyte CMD_EXIT_VIEW = -82;

    public const sbyte CMD_LOGIN_NEW = -81;

    public const sbyte CMD_CHANGE_NAME = -80;

    public const sbyte CMD_UPDATE_AVATA = -78;

    public const sbyte CMD_CREATE_TABLE = -77;
    public const sbyte CMD_POPUP_NOTIFY = -61;
    public const sbyte CMD_LIST_BET_MONEY = -62;
    public const sbyte CMD_AN_TAB_NAP_TIEN = -63;

    public const sbyte CMD_LIST_ITEM = -76;
    public const sbyte CMD_BUY_ITEM = -75;
    public const sbyte CMD_RECEIVE_FREE_MONEY = -74;
    public const sbyte CMD_XU_TO_CHIP = -73;
    public const sbyte CMD_XU_TO_NICK = -72;
    public const sbyte CMD_TOP = -71;
    public const sbyte CMD_RATE_SCRATCH_CARD = -70;
    public const sbyte CMD_LIST_EVENT = -69;
    public const sbyte CMD_CHANGE_BETMONEY = -68;
    public const sbyte CMD_CHIP_TO_XU = -67;
    public const sbyte CMD_SEND_REGISTER_ID = -66;
    public const sbyte CMD_JOIN_TABLE_PLAY = -65;// vào chơi, không qua
    // join view;

    public const sbyte CMD_BAOXAM = -59;

    public const sbyte CMD_INFO_GIFT2 = -57;
    public const sbyte CMD_SEND_GIFT2 = -56;
    public const sbyte CMD_SMS_9029 = -55;

    public const sbyte CMD_PHOM_HA = -60;
    //xocdia
    //After join table success. Receive: -65, -110, -117, -96, -102, -105, -120, 69, 33
    //public const sbyte CMD_BUY_SHIT = -53;
    //public const sbyte CMD_CARD_XEP_MB = -52;

    //public const sbyte CMD_BEGIN_XOCDIA = -102;
    //public const sbyte CMD_BEGIN_XOCDIA_CUOC = -105;
    //public const sbyte CMD_MO_BAT = -122;
    //public const sbyte CMD_XOCDIA_DATCUOC = -108;
    //public const sbyte CMD_ARR_BET_XD = -110;//chua xu ly

    public const sbyte CMD_BEGIN_XOCDIA = -102;
    public const sbyte CMD_BEGIN_XOCDIA_CUOC = -105;
    public const sbyte CMD_MO_BAT = -120;
    public const sbyte CMD_XOCDIA_DATCUOC = -108;
    public const sbyte CMD_ARR_BET_XD = -110;
    public const sbyte CMD_LAMCAI = -113;
    public const sbyte CMD_DATLAI = -116;// xd
    public const sbyte CMD_GAPDOI = -115;// xd
    public const sbyte CMD_HUYCUOC = -114;// xd
    public const sbyte CMD_UPDATE_CUA = -121;
    public const sbyte CMD_HISTORY_XD = -117;// xd
    public const sbyte CMD_CHUCNANG_HUYCUA = -119;// xd
    public const sbyte CMD_BEGIN_XOCDIA_DUNGCUOC = -107;// xd
    //xocdia
}
