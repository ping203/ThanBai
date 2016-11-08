using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
public class BaseInfo {
    private static BaseInfo instance;
    public static BaseInfo gI() {
        if (instance == null) {
            instance = new BaseInfo();

        }
        return instance;
    }
    public string cskh = "";
    public bool isView = false;
    public bool regOuTable = false;
    public string pass = "";
    public string username = "";

    public string SMS_CHANGE_PASS_SYNTAX;
    public string SMS_CHANGE_PASS_NUMBER;
    public sbyte isDoiThuong = 0;// disable, 1: enable

    public int idRoom = 1;
    public string nameTale;

    public MainInfo mainInfo = new MainInfo();

    protected static string strMoney = "";
    public int choinhanh = 0;
    public long moneyNeedTable;
    public int numberPlayer = 4;
    public short idTable;
    public long moneyTable, moneyMinTo;
    public string moneyName = "";
    public long betMoney;
    public int timerTurnTable = 30;
    public long needMoney = 0;
    public long maxMoney = 0;

    public List<InfoWin> infoWin = new List<InfoWin>();
    public List<AlertMess> msgAlert = new List<AlertMess>();
    public int soTinNhan = 0;

    public String name10, name15;
    public String syntax10, syntax15;
    public String port10 = "", port15 = "";
    public int sms10 = 0, sms15 = 0;
    public int tyle_xu_sang_chip = 0, tyle_chip_sang_xu = 0;
    public List<TyGia> list_tygia = new List<TyGia>();
    public int isCharging = 0;
    public int isHidexuchip = 5;
    public bool is9029 = false;

    public bool tuDongRutTien = false;
    public long soTienRut;

    public long currentMaxMoney;
    public long currentMinMoney;
    public long moneyto;

    public List<InfoWinTo> infoWinTo;
    public bool isHideTabeFull = true;
    public bool nhacnen = true;
    public bool rung = true;
    public bool isNhanLoiMoiChoi = true;
    public int typetableLogin = Res.ROOMVIP;
    //public List<MessInfo> allMess = new List<MessInfo>();
    //public List<GiftInfo> giftTheCao = new List<GiftInfo>();
    //public List<GiftInfo> giftPhanQua = new List<GiftInfo>();
    //public List<VuaBaiEvent> listEvent = new List<VuaBaiEvent>();
    public int soDu = 50000;
    public bool isPurchase = false;

    public List<BetMoney> listBetMoneysVIP = new List<BetMoney>();
    public List<BetMoney> listBetMoneysFREE = new List<BetMoney>();

    public int type_sort = 0;
    public bool sort_giam_dan_bancuoc, sort_giam_dan_muccuoc, sort_giam_dan_nguoichoi;

    public bool isSound = PlayerPrefs.GetInt("sound") == 0 ? true : false;
    public bool isVibrate = PlayerPrefs.GetInt("rung") == 0 ? true : false;

    public int TELCO_CODE = 1;
    public bool isAutoReady = false;
    public int gameID;
    public Message OnJoinTableSuccess;
    public Message InfoCardPlayerInTbl;
    public List<Mail> listMail = new List<Mail>();
    public List<Event> listEvent = new List<Event>();
    public bool isInGame = false;
    public List<Item9029> list9029 = new List<Item9029>();
    public List<InfoTheCao> listTheCao = new List<InfoTheCao>();
    public List<InfoVatPham> listVatPham = new List<InfoVatPham>();
    public List<long> listMucCuocXocDia = new List<long>();
    public List<int> listIdHistoryXocDia = new List<int>();
    public Message onXocDiaUpdateCua;
    public Message onInfoMe;

    public static string formatMoney(long money) {
        try {
            if (money < 0) {
                money = 0;
            }
            // strMoney.delete(0, strMoney.length());
            long strm = (long)(money / 1000000);
            long strk = 0;
            long strh = 0;
            if (strm > 0) {
                strk = (long)((money % 1000000) / 1000);
                if (strk > 100) {
                    strMoney = strm + "," + strk + "M";
                } else if (strMoney.Length > 0) {
                    strMoney = strm + "," + "0" + strk + "M";
                }

            } else {
                strk = (long)(money / 1000);
                if (strk > 0) {
                    strh = (money % 1000 / 100);
                    if (strh > 0) {
                        strMoney = strk + "," + strh + "K";
                    } else if (strMoney.Length >= 0) {
                        strMoney = strk + "K";
                    }

                } else if (strMoney.Length >= 0) {
                    strMoney = money + "";
                }
            }
        } catch (Exception e) {
            Debug.LogException(e);

        }
        return strMoney.ToString();
    }

    public static string formatMoneyNormal(long m) {
        //return m.ToString ("###.###");
        string str = m + "";// = m.ToString("000,000");//String.Format ("{0: 000.000}", m).ToString ();


        if (m < 1000000 && m > 0) {
            str = m.ToString("0,0");
        } else if (m >= 1000000 && m < 100000000) {
            str = (m / 1000).ToString("0,0K");
        } else if (m >= 100000000) {
            str = (m / 1000000).ToString("0,0M");
        }
        return str;
    }

    public static string formatMoneyNormal2(long money) {
        //try {
        if (money < 0) {
            money = 0;
        }

        string str = money + "";
        string s = str;
        if (money < 1000000 && money > 0) {
            if (str.Length >= 4)
                s = str.Substring(0, str.Length - 3) + "," + str.Substring(str.Length - 3, 3);
            else
                s = str.Substring(str.Length - 3, 3);
        } else if (money >= 1000000 && money < 100000000) {
            str = money / 1000 + "";
            if (str.Length >= 4)
                s = str.Substring(0, str.Length - 3) + "," + str.Substring(str.Length - 3, 3) + "K";
            else
                s = str.Substring(str.Length - 3, 3) + "K";
        } else if (money >= 100000000) {
            str = (money / 1000000) + "";
            if (str.Length >= 4)
                s = str.Substring(0, str.Length - 3) + "," + str.Substring(str.Length - 3, 3) + "M";
            else
                s = str.Substring(str.Length - 3, 3) + "M";
        }
        // strMoney.delete(0, strMoney.length());
        /*long strm = (long) (money / 1000000);
        long strk = 0;
        long strh = 0;
        if(strm > 0) {
            strk = (long) ((money % 1000000) / 1000);
            if(strk > 100) {
                strMoney = strm + "." + strk + "K";
            } else if(strMoney.Length > 0) {
                strMoney = strm + "." + "0" + strk + "K";
            }

        } else {
            strk = (long) (money / 1000);
            if(strk > 0) {
                strh = (money % 1000);
                if(strh > 0) {
                    if(strh > 100)
                        strMoney = strk + "." + strh + "";
                    else if(strh > 10)
                        strMoney = strk + ".0"+ strh + "";
                    else
                        strMoney = strk + ".00" + strh + "";
                } else if(strh == 0) {
                    strMoney = strk + "." + "000";
                } else if(strMoney.Length >= 0) {
                    strMoney = strk + "";
                }

            } else if(strMoney.Length >= 0) {
                strMoney = money + "";
            }
        }*/

        return s.ToString();
        /*} catch(Exception e) {
            Debug.LogException (e);
        }*/
    }

    public static string formatMoneyDetail(long money) {
        if (money < 0) {
            money = 0;
        }
        String st = "";
        String rs = "";
        st = money + "";
        for (int i = 0; i < st.Length; i++) {
            rs = rs + st[(st.Length - i - 1)];
            if ((i + 1) % 3 == 0 && i < st.Length - 1) {
                rs = rs + ",";
            }
        }
        st = "";
        for (int i = 0; i < rs.Length; i++) {
            st = st + rs[(rs.Length - i - 1)];
        }
        return st;

    }

    public static string formatMoneyDetailDot(long money) {
        if (money < 0) {
            money = 0;
        }
        String st = "";
        String rs = "";
        st = money + "";
        for (int i = 0; i < st.Length; i++) {
            rs = rs + st[(st.Length - i - 1)];
            if ((i + 1) % 3 == 0 && i < st.Length - 1) {
                rs = rs + ".";
            }
        }
        st = "";
        for (int i = 0; i < rs.Length; i++) {
            st = st + rs[(rs.Length - i - 1)];
        }
        return st;

    }


    public bool isHaPhom { get; set; }

    public bool checkNumber(string test) {
        for (int i = 0; i < test.Length; i++) {
            char c = test[i];
            if ((('0' > c) || (c > '9'))) {
                return false;
            }
        }
        return true;
    }

    private static int[] sortValue(int[] arr) {// mang cac so thu tu quan bai tu
        // 0-51
        int[] turn = arr;
        int length = turn.Length;
        for (int i = 0; i < length - 1; i++) {
            int min = i;
            for (int j = i + 1; j < length; j++) {
                if (((getValue(turn[j]) < getValue(turn[min])) || getValue(turn[min]) == 1) && getValue(turn[j]) != 1) {
                    // swap
                    min = j;
                }
            }
            int temp = turn[i];
            turn[i] = turn[min];
            turn[min] = temp;
        }
        return turn;
    }

    public static string tinhDiem(int[] cardhand) {
        cardhand = sortValue(cardhand);
        if (isSap(cardhand)) {
            return "Sáp";
        } else if (isLieng(cardhand)) {
            return "Liêng";
        } else if (isHinh(cardhand)) {
            return "Ảnh";
        } else if (getScoreFinal(cardhand) >= 0) {
            return getScoreFinal(cardhand) % 10 + " điểm";
        } else {
            return "";
        }

    }

    private static bool isHinh(int[] cardhand) {
        if (cardhand == null || cardhand.Length < 3) {
            return false;
        }
        for (int i = 0; i < cardhand.Length; i++) {
            if (getValue(cardhand[i]) < 11) {
                return false;
            }
        }
        return true;
    }

    private static bool isLieng(int[] cardhand) {
        if (cardhand == null) {
            return false;
        }
        if (cardhand.Length < 3) {
            return false;
        }

        if (getValue(cardhand[0]) == 2 && getValue(cardhand[1]) == 3 && getValue(cardhand[2]) == 1) {
            return true;
        }

        if (getValue(cardhand[0]) == 12 && getValue(cardhand[1]) == 13 && getValue(cardhand[2]) == 1) {
            return true;
        }

        for (int i = 0; i < cardhand.Length - 1; i++) {
            int value1 = getValue(cardhand[i]);
            int value2 = getValue(cardhand[i + 1]);
            if ((Math.Abs(value2 - value1) > 1) || (value2 == value1)) {
                return false;
            }

        }
        return true;

    }


    public static int getScoreFinal(int[] src) {
        if (src == null || src.Length < 3) {
            return -1;
        }
        int sc = 0;
        for (int i = 0; i < src.Length; i++) {
            sc += (getValue(src[i]) > 10 ? 0 : getValue(src[i]));
        }
        return sc;
    }

    private static int getValue(int i) {
        return i % 13 + 1;
    }

    private static bool isSap(int[] cardhand) {
        if (cardhand == null || cardhand.Length < 3) {
            return false;
        }
        for (int i = 0; i < cardhand.Length; i++) {
            if (getValue(cardhand[i]) != getValue(cardhand[0])
                    || cardhand[i] == 52) {
                return false;
            }
        }
        return true;
    }
    public bool checkHettien() {
        if (mainInfo.moneyChip < needMoney) {
            return true;
        }
        return false;
    }
}
