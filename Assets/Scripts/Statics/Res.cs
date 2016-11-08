using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Res {
    public static string version = "1.3.4";
    //public static string IP = "choibaidoithuong.org";
    //public static string IP = "123.31.45.20";
    public static int PORT = 4322;
    public static string IP = "192.168.1.112";
    //public static int PORT = 4326;
    //public static string IP = "101.99.3.25";
    public static string TXT_PhoneNumber = "0999999999";

    public static int ROOMFREE = 1;
    public static int ROOMVIP = 2;
    //---------------string 
    public static string MONEY_FREE = " Chip";
    public static string MONEY_VIP = " Xu";
    public static string MONEY_FREE_UPPERCASE = " Chip";
    public static string MONEY_VIP_UPPERCASE = " XU";
    public static string TXT_SANSANG = "Sẵn sàng";
    public static string TXT_BOSANSANG = "Bỏ sẵn sàng";
    public static string TXT_BATDAU = "Bắt đầu";

    public static string TXT_DAT = "Đặt";
    public static string TXT_FOLD = "Bỏ";
    public static string TXT_CHECK_FOLD = "Xem bài/Bỏ";
    public static string TXT_CHECK = "Xem bài";
    public static string TXT_CALL = "Theo";
    public static string TXT_CALL_ANY = "Theo mọi cược";
    public static string TXT_RAISE = "Tố";
    public static string TXT_ALLIN = "Tất tay";

    public static string TXT_BOLUOT = "Bỏ lượt";
    public static string TXT_DONGY = "Đồng ý";

    public static string TXT_DOILUAT = "Đổi luật";
    public static string TXT_XINCHO = "Xin chờ...";
    public static string TXT_DANH = "Đánh";
    public static string TXT_BOC = "Bốc";
    public static string TXT_AN = "Ăn";
    public static string TXT_HA = "Hạ Phỏm";

    public static string TXT_RUTTIEN = "Rút lại $";
    public static string TXT_CHONBAI = "Chọn 1 quân bài để mở: ";

    public static int AC_XEMBAI = 0;
    public static int AC_BOLUOT = 1;
    public static int AC_THEO = 2;
    public static int AC_UPBO = 3;
    public static int AC_TO = 4;

    public static float speedCard = 0.15f;

    public static string[] TypeCard_Name = new string[] { "mauthau", "doi", "thu", "samco", "sanh", "thung", "culu", "tuquy", "thungphasanh" };
    public static string[] TYPECARD = { "Mậu thầu", "Đôi", "Thú", "Sám cô", "Sảnh", "Thùng", "Cù lũ", "Tứ quý", "Thùng phá sảnh" };

    // public static Sprite[] list_avata = new Sprite[60];// = new List<Sprite>();
    public static Sprite[] list_cards;// = new List<Sprite>();
    //public static Sprite[] list_emotions;// = new List<Sprite>();
    public const int EMOTION_COUNT = 28;
    public const int AVATA_COUNT = 60;

    public static string[] spriteName = new string[] { "phom", "tlmn", "poker", "bacay", "xito", "sam", "lieng", "maubinh", "xocdia" };
    
    public static string[] logoName = new string[] { "icon_phom", "icon_tlmn", "icon_xito", "icon_maubinh", "icon_bacay", "icon_lieng", "icon_sam", "CHUONG", "icon_poker", "icon_xocdia" };

    public static string[] gameName = new string[] { "Phỏm", "TLMN", "POKER", "BA CÂY", "XÌ TỐ", "SÂM", "LIÊNG", "MẬU BINH", "XÓC ĐĨA" };
    public static int[] idGame = new int[] { 0, 1, 8, 4, 2, 6, 5, 3, 9 };
    public static string[] action_play = new string[] { "action_xembai", "action_boluot", "action_theo", "action_upbo", "action_to" };
    public static string[] ani_thang = new string[] { "phomu", "hu_thang", "thangtrang", "mom", "cong", "lung" };

    /*public static Sprite getAvataByID(int id) {
        for (int i = 0; i < list_avata.Length; i++) {
            if (id == int.Parse(list_avata[i].name)) {
                return list_avata[i];
            }
        }
        return null;
    }*/

    public static Sprite getCardByID(int id) {
        string str = "cardall_" + id;
        for (int i = 0; i < list_cards.Length; i++) {
            if (str.Trim().Equals(list_cards[i].name.Trim())) {
                return list_cards[i];
            }
        }
        return null;
    }
    //public static Sprite getSmileByName(string name) {
    //    for (int i = 0; i < list_emotions.Length; i++) {
    //        if (name.Trim().Equals(list_emotions[i].name.Trim())) {
    //            return list_emotions[i];
    //        }
    //    }
    //    return null;
    //}


    public const string AS_PREFABS = "prefabs";
    public const string AS_AVATA = "avata";
    public const string AS_MAINSCENE = "mainscene";
    public const string AS_UI = "ui";
    public const string AS_CARD = "card";
    public const string AS_ANIM = "animation";
    public const string AS_CHIP = "chip";
    public const string LOGIN_AB = "sub_login";
    public const string LOGIN_NAME = "subLogin";
    public const string MAIN_AB = "mainscene";
    public const string MAIN_NAME = "main";
    public const string REGISTER_AB = "sub_register";
    public const string REGISTER_NAME = "subRegister";
    public const string ROOM_AB = "roomscene";
    public const string ROOM_NAME = "room";
    public const string SETTING_AB = "sub_setting";
    public const string SETTING_NAME = "subSetting";
    public const string INFOPLAYER_AB = "sub_info_player";
    public const string INFOPLAYER_NAME = "subInfoPlayer";
    public const string ADDCOIN_AB = "sub_add_coin";
    public const string ADDCOIN_NAME = "subAddCoin";
    public const string RUTTIEN_AB = "sub_rut_tien";
    public const string RUTTIEN_NAME = "subRutTien";
    public const string INPUT_AB = "sub_input";
    public const string INPUT_NAME = "subInput";
    public const string DATCUOC_AB = "sub_dat_cuoc";
    public const string DATCUOC_NAME = "subDatCuoc";
    public const string CHAT_AB = "sub_chat";
    public const string CHAT_NAME = "subChat";
    public const string DOITHUONG_AB = "sub_doi_thuong";
    public const string DOITHUONG_NAME = "subDoiThuong";
    public const string MOICHOI_AB = "sub_moi_choi";
    public const string MOICHOI_NAME = "subMoiChoi";
    public const string NOTIDOITHUONG_AB = "sub_noti_doithuong";
    public const string NOTIDOITHUONG_NAME = "subNotiDoiThuong";
    public const string CHATADMIN_AB = "sub_chat_admin";
    public const string CHATADMIN_NAME = "subChatAdmin";
    public const string CUOC_AB = "sub_cuoc";
    public const string CUOC_NAME = "subCuoc";
    public const string HELP_AB = "sub_help";
    public const string HELP_NAME = "subHelp";
    public const string TLMN_AB = "tlmn";
    public const string TLMN_NAME = "tlmn";
    public const string PHOM_AB = "phom";
    public const string PHOM_NAME = "phom";
    public const string BACAY_AB = "bacay";
    public const string BACAY_NAME = "bacay";
    public const string LIENG_AB = "lieng";
    public const string LIENG_NAME = "lieng";
    public const string XITO_AB = "xito";
    public const string XITO_NAME = "xito";
    public const string XOCDIA_AB = "xocdia";
    public const string XOCDIA_NAME = "xocdia";
    public const string MAUBINH_AB = "maubinh";
    public const string MAUBINH_NAME = "maubinh";
    public const string POKER_AB = "poker";
    public const string POKER_NAME = "poker";
    public const string SAM_AB = "sam";
    public const string SAM_NAME = "sam";
    public const string GIFT_AB = "sub_gift";
    public const string GIFT_NAME = "subGift";
    public const string CREATETABLE_AB = "sub_create_table";
    public const string CREATETABLE_NAME = "subCreateTable";
    public const string TOIBAN_AB = "sub_toi_ban";
    public const string TOIBAN_NAME = "subToiBan";
    public const string CHOINGAY_AB = "sub_choi_ngay";
    public const string CHOINGAY_NAME = "subChoiNgay";
    public const string INFO_INGAME_AB = "sub_info_ingame";
    public const string INFO_INGAME_NAME = "sub_info_ingame";
    public const string CHANGE_PASS_AB = "sub_change_pass";
    public const string CHANGE_PASS_NAME = "subChangePass";
    public const string CHANGE_AVATAR_AB = "sub_change_avatar";
    public const string CHANGE_AVATAR_NAME = "subChangeAvatar";
    public const string CHANGE_NAME_AB = "sub_change_name";
    public const string CHANGE_NAME_NAME = "subChangeName";
}

public enum Align {
    None,
    Left,
    Center,
    Right,
    Top,
    Bot
};

