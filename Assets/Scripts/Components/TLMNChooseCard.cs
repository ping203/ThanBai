using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class TLMNChooseCard {
    public static int[] getArrIndexCardNhac (int[] cards, int[] cardHand) {
        int[] indexCardNhac = null;
        int indexFocus = 0;
        int indexFocus2 = 0;
        for(int i = 0; i < cardHand.Length; i++) {
            if(cardHand[i] == cards[0]) {
                indexFocus = i;
                break;
            }
        }
        for(int i = 0; i < cardHand.Length; i++) {
            if(cardHand[i] == cards[1]) {
                indexFocus2 = i;
                break;
            }
        }
        if(getValue (cards[0]) == getValue (cards[1])) {
            // nh?c 4 dôi thông
            indexCardNhac = nhac4doithong (indexFocus, cardHand, new int[] { 12 });
            if(indexCardNhac == null) {
                indexCardNhac = nhacdoithong (indexFocus, cardHand, new int[] { 12 });
            }
            if(indexCardNhac == null) {
                indexCardNhac = nhactuquy (indexFocus, cardHand, new int[] { 0, 13, 26, 39 });
            }
            if(indexCardNhac == null) {
                indexCardNhac = nhacsam (indexFocus, cardHand, new int[] { 0, 13, 26 });
            }
        } else if(getValue (cards[0]) == getValue (cards[1]) - 1) {
            indexCardNhac = nhacsanhAll (indexFocus2, cardHand);
        }
        return indexCardNhac;
    }

    private static int[] nhacsanhAll (int indexFocus, int[] cardHand) {
        // TODO Auto-generated method stub
        List<int> cards = new List<int> ();
        int valueCurrent = getValue (cardHand[indexFocus]);
        cards.Add (indexFocus);
        for(int i = indexFocus + 1; i < cardHand.Length; i++) {
            if((getValue (cardHand[i])) == valueCurrent + 1) {
                cards.Add (i);
                valueCurrent++;
            } else if(getValue (cardHand[i]) == 1 && valueCurrent == 13) {
                cards.Add (i);
                valueCurrent++;
            }
        }
        int[] indexCardNhac = new int[cards.Count];
        for(int i = 0; i < indexCardNhac.Length; i++) {
            indexCardNhac[i] = cards[i];
        }
        return indexCardNhac;
    }

    public static int[] getArrIndexCardNhac (int indexFocus, int[] cardHand, int[] cardFire) {
        int[] indexCardNhac = null;
        try {
            int typeC = getTypeOfListCardFire (cardFire);
            switch(typeC) {
                case 1:
                    // 1 con 2
                    indexCardNhac = nhacdoithong (indexFocus, cardHand, cardFire);
                    if(indexCardNhac == null) {
                        indexCardNhac = nhactuquy (indexFocus, cardHand, cardFire);
                    } else {
                        if(indexCardNhac.Length == 0) {
                            indexCardNhac = nhactuquy (indexFocus, cardHand, cardFire);
                        }
                    }
                    break;
                case 2:
                    indexCardNhac = nhac1doi (indexFocus, cardHand, cardFire);
                    // 1 doi
                    break;
                case 3:
                    // sam
                    indexCardNhac = nhacsam (indexFocus, cardHand, cardFire);
                    break;
                case 4:
                    // sanh
                    indexCardNhac = nhacsanh (indexFocus, cardHand, cardFire);
                    break;
                case 5:
                    // 3 doi thong
                    indexCardNhac = nhacdoithong (indexFocus, cardHand, cardFire);
                    break;
                case 6:
                    // tu quy
                    indexCardNhac = nhactuquy (indexFocus, cardHand, cardFire);
                    if(indexCardNhac == null) {
                        indexCardNhac = nhac4doithong (indexFocus, cardHand, cardFire);
                    } else {
                        if(indexCardNhac.Length == 0) {
                            indexCardNhac = nhac4doithong (indexFocus, cardHand, cardFire);
                        }
                    }
                    break;
                case 7:
                    indexCardNhac = nhac4doithong (indexFocus, cardHand, cardFire);
                    // 4 doi thong
                    break;
                case 10:
                    if(getValue (cardHand[indexFocus]) == 2) {
                        indexCardNhac = nhac1doi (indexFocus, cardHand, cardFire);
                    } else {
                        indexCardNhac = nhac4doithong (indexFocus, cardHand, cardFire);
                        if(indexCardNhac == null) {
                            indexCardNhac = nhactuquy (indexFocus, cardHand, cardFire);
                        } else {
                            if(indexCardNhac.Length == 0) {
                                indexCardNhac = nhactuquy (indexFocus, cardHand, cardFire);
                            }
                        }
                    }
                    // doi 2
                    break;
            }
        } catch(Exception e) {
            // TODO: handle exception
        }
        return indexCardNhac;
    }

    public static int getTypeOfListCardFire (int[] card) {
        int type = 0;
        int size = card.Length;
        switch(size) {
            case 1:
                if(getValue (card[0]) == 2) {
                    type = 1;
                }
                break;
            case 2:
                if(isdoi (card)) {
                    type = 2;
                    if(getValue (card[0]) == 2) {
                        type = 10;
                    }
                }
                break;
            case 3:
                if(issam (card)) {
                    type = 3;
                }
                if(issanh (card)) {
                    type = 4;
                }
                break;
            case 4:
                if(istu (card)) {
                    type = 6;
                }
                if(issanh (card)) {
                    type = 4;
                }
                break;
            case 5:
                if(issanh (card)) {
                    type = 4;
                }
                break;
            case 6:
                if(isbadoithong (card)) {
                    type = 5;
                }
                if(issanh (card)) {
                    type = 4;
                }
                break;
            case 7:
                if(issanh (card)) {
                    type = 4;
                }
                break;
            case 8:
                if(isbondoithong (card)) {
                    type = 7;
                }
                if(issanh (card)) {
                    type = 4;
                }
                break;
            case 9:
                if(issanh (card)) {
                    type = 4;
                }
                break;
            case 10:
                if(issanh (card)) {
                    type = 4;
                }
                break;
            case 11:
                if(issanh (card)) {
                    type = 4;
                }
                break;
        }
        return type;
    }

    public static bool isdoi (int[] card) {
        if(card.Length != 2)
            return false;
        return getValue (card[0]) == getValue (card[1]);
    }

    public static bool issam (int[] card) {
        if(card.Length != 3)
            return false;
        return (getValue (card[0]) == getValue (card[1])) && (getValue (card[1]) == getValue (card[2]));
    }

    public static bool issanh (int[] card) {
        for(int i = 0; i < card.Length - 1; i++) {
            if(getValue (card[i]) == 13 && getValue (card[i + 1]) == 1) {
                continue;
            }
            if(getValue (card[i]) + 1 == getValue (card[i + 1])) {
                continue;
            } else {
                return false;
            }
        }
        return true;
    }

    public static bool issanh2 (int[] card) {
		if (card == null || card.Length < 3) {
			return false;
		}
		for (int i = 0; i < card.Length - 1; i++) {
			if (getValue(card[i]) == 13 && getValue(card[i + 1]) == 1) {
				continue;
			}
			if (getValue(card[i]) + 1 == getValue(card[i + 1])) {
				continue;
			} else {
				return false;
			}
		}
		return true;
	}

    public static bool istu (int[] card) {
        for(int i = 0; i < card.Length - 1; i++) {
            if(getValue (card[i]) != getValue (card[i + 1])) {
                return false;
            }
        }
        return true;
    }

    public static bool isbadoithong (int[] card) {
        int[] pearList = null;
        for(int i = 0; i < card.Length - 1; i++) {
            if(getValue (card[i]) == getValue (card[i + 1])) {
                if(!isContain (pearList, card[i])) {
                    pearList = RTL.insertArray (pearList, getValue (card[i]));
                }
            }
        }
        if(pearList != null && pearList.Length == 3) {
            return true;
        }
        return false;
    }

    public static bool isbondoithong (int[] card) {
        int[] pearList = null;
        for(int i = 0; i < card.Length - 1; i++) {
            if(getValue (card[i]) == getValue (card[i + 1])) {
                if(!isContain (pearList, getValue (card[i]))) {
                    pearList = RTL.insertArray (pearList, getValue (card[i]));
                }
            }
        }
        if(pearList != null && pearList.Length == 4) {
            return true;
        }
        return false;
    }

    private static int[] nhacdoithong (int indexFocus, int[] cardHand, int[] cardFire) {
        int[] indexCardNhac = null;
        if(getValue (cardHand[indexFocus]) == 2 || cardHand.Length < 6) {
            return null;
        }
        int[] pearList = null;
        int temp = getValue (cardHand[indexFocus]);
        if(indexFocus < cardHand.Length - 1
                && getValue (cardHand[indexFocus]) + 1 == getValue (cardHand[indexFocus + 1])) {
            if(indexFocus > 0 && getValue (cardHand[indexFocus - 1]) == getValue (cardHand[indexFocus])) {
                indexFocus = indexFocus - 1;
            }
        }

        for(int i = indexFocus; i < cardHand.Length - 1; i++) {

            if((getValue (cardHand[i]) == 1 ? 14 : getValue (cardHand[i])) == temp
                    && (getValue (cardHand[i + 1]) == 1 ? 14 : getValue (cardHand[i + 1])) == temp) {
                pearList = RTL.insertArray (pearList, i);
                pearList = RTL.insertArray (pearList, i + 1);
                temp += 1;
            }
        }

        if(pearList != null && (pearList.Length == 6 || pearList.Length == 8)) {
            indexCardNhac = pearList;
        }
        return indexCardNhac;
    }

    private static int[] nhac1doi (int indexFocus, int[] cardHand, int[] cardFire) {
        int[] indexCardNhac = null;
        if(cardHand == null || cardHand.Length < 2) {
            return null;
        }
        int cPos = -1;
        int[] arrCardUp = new int[] { cardHand[indexFocus] };
        if(indexFocus + 1 <= cardHand.Length && indexFocus - 1 >= cardHand.Length) {
            if(getValue (cardHand[indexFocus + 1]) == cardHand[indexFocus]) {
                if(compareCard (RTL.insertArray (arrCardUp, cardHand[indexFocus + 1]), cardFire))
                    cPos = indexFocus + 1;
            } else if(getValue (cardHand[indexFocus - 1]) == cardHand[indexFocus]) {
                if(compareCard (RTL.insertArray (arrCardUp, cardHand[indexFocus - 1]), cardFire))
                    cPos = indexFocus - 1;
            }
        }
        int pos = -1;
        if(indexFocus - 1 >= 0 && getValue (cardHand[indexFocus - 1]) == getValue (cardHand[indexFocus])) {
            pos = indexFocus - 1;
        }
        if(indexFocus + 1 <= cardHand.Length - 1
                && getValue (cardHand[indexFocus + 1]) == getValue (cardHand[indexFocus]) && pos == -1) {
            pos = indexFocus + 1;
        }
        if(pos != -1 && compareCard (RTL.insertArray (arrCardUp, cardHand[pos]), cardFire)) {
            cPos = pos;
        }
        if(cPos != -1) {
            indexCardNhac = new int[] { cPos };
        }
        return indexCardNhac;
    }

    private static int[] nhacsam (int indexFocus, int[] cardHand, int[] cardFire) {
        int[] indexCardNhac = null;
        if(cardHand == null || cardHand.Length < 3) {
            return null;
        }
        int[] sam = null;
        int[] cardUp = null;
        for(int i = 0; i < cardHand.Length; i++) {
            if(getValue (cardHand[i]) == getValue (cardHand[indexFocus])) {
                sam = RTL.insertArray (sam, i);
                cardUp = RTL.insertArray (cardUp, cardHand[i]);
            }
            if(sam != null && sam.Length == 3) {
                break;
            }
        }
        if(sam != null && sam.Length == 3 && compareCard (cardUp, cardFire)) {
            indexCardNhac = sam;
        }
        return indexCardNhac;
    }

    private static int[] nhacsanh (int indexFocus, int[] cardHand, int[] cardFire) {
        int[] indexCardNhac = null;
        if(cardHand == null || cardHand.Length < cardFire.Length) {
            return null;
        }
        int temp = -1;
        int[] sanh = null;
        int[] cardUp = null;
        for(int i = indexFocus; i < cardHand.Length; i++) {
            if(temp == -1) {
                temp = getValue (cardHand[i]);
                sanh = RTL.insertArray (sanh, i);
                cardUp = RTL.insertArray (cardUp, cardHand[i]);
            } else if((getValue (cardHand[i]) == 1 ? 14 : getValue (cardHand[i])) == temp + 1) {
                sanh = RTL.insertArray (sanh, i);
                cardUp = RTL.insertArray (cardUp, cardHand[i]);
                temp = (getValue (cardHand[i]) == 1 ? 14 : getValue (cardHand[i]));
                if(sanh.Length == cardFire.Length)
                    break;
            }
        }
        if(sanh.Length != cardFire.Length
                && (getValue (cardHand[indexFocus]) >= getValue (cardFire[cardFire.Length - 1]) || getValue (cardHand[indexFocus]) == 1)) {
            if(getValue (cardHand[indexFocus]) == getValue (cardFire[cardFire.Length - 1])
                    && cardHand[indexFocus] < cardFire[cardFire.Length - 1]) {
                return null;
            }
            sanh = null;
            for(int i = indexFocus; i >= 0; i--) {
                if(temp == -1) {
                    temp = getValue (cardHand[i]);
                    sanh = RTL.insertArray (sanh, i);
                    cardUp = RTL.insertArray (cardUp, cardHand[i]);
                } else if((getValue (cardHand[i]) == 1 ? 14 : getValue (cardHand[i])) == temp - 1) {
                    sanh = RTL.insertArray (sanh, i);
                    cardUp = RTL.insertArray (cardUp, cardHand[i]);
                    temp = (getValue (cardHand[i]) == 1 ? 14 : getValue (cardHand[i]));
                }
                if(sanh.Length == cardFire.Length) {
                    break;
                }
            }
        }
        try {
            if(sanh != null && sanh.Length == cardFire.Length) {
                if(getValue (cardHand[sanh[sanh.Length - 1]]) == getValue (cardFire[cardFire.Length - 1])) {
                    if(sanh.Length < cardHand.Length) {
                        if(getValue (cardHand[sanh[sanh.Length - 1]]) == getValue (cardHand[sanh[sanh.Length - 1] + 1])) {
                            sanh[sanh.Length - 1] += 1;
                            cardUp[cardUp.Length - 1] = cardHand[sanh[sanh.Length - 1]];
                        }
                    }
                    if(sanh.Length < cardHand.Length) {
                        if(getValue (cardHand[sanh[sanh.Length - 1]]) == getValue (cardHand[sanh[sanh.Length - 1] + 1])) {
                            sanh[sanh.Length - 1] += 1;
                            cardUp[cardUp.Length - 1] = cardHand[sanh[sanh.Length - 1]];
                        }
                    }
                    if(sanh.Length < cardHand.Length) {
                        if(getValue (cardHand[sanh[sanh.Length - 1]]) == getValue (cardHand[sanh[sanh.Length - 1] + 1])) {
                            sanh[sanh.Length - 1] += 1;
                            cardUp[cardUp.Length - 1] = cardHand[sanh[sanh.Length - 1]];
                        }
                    }
                }
            }
        } catch(Exception e) {
            // TODO: handle exception
        }

        if(sanh != null && sanh.Length == cardFire.Length && compareCard (cardUp, cardFire)) {
            indexCardNhac = new int[cardFire.Length];
            for(int j = 0; j < cardFire.Length; j++) {
                indexCardNhac[j] = sanh[j];
            }
        }

        return indexCardNhac;
    }

    private static int[] nhactuquy (int indexFocus, int[] cardHand, int[] cardFire) {
        int[] indexCardNhac = null;
        if(cardHand == null || cardHand.Length < 4) {
            return null;
        }
        int[] tyquy = null;
        int[] cardUp = null;
        for(int i = 0; i < cardHand.Length; i++) {
            if(getValue (cardHand[i]) == getValue (cardHand[indexFocus])) {
                tyquy = RTL.insertArray (tyquy, i);
                cardUp = RTL.insertArray (cardUp, cardHand[i]);
            }
            if(tyquy != null && tyquy.Length == 4) {
                break;
            }
        }
        if(tyquy != null && tyquy.Length == 4 && compareCard (cardUp, cardFire)) {
            indexCardNhac = tyquy;
        }
        return indexCardNhac;
    }

    private static int[] nhac4doithong (int indexFocus, int[] cardHand, int[] cardFire) {
        int[] indexCardNhac = null;
        if(cardHand == null || cardHand.Length < 8) {
            return null;
        }
        int[] pearList = null;
        int[] cardUp = null;
        int temp = getValue (cardHand[indexFocus]);
        if(indexFocus < cardHand.Length - 1
                && getValue (cardHand[indexFocus]) + 1 == getValue (cardHand[indexFocus + 1])) {
            if(indexFocus > 0 && getValue (cardHand[indexFocus - 1]) == getValue (cardHand[indexFocus])) {
                indexFocus = indexFocus - 1;
            }
        }

        for(int i = indexFocus; i < cardHand.Length - 1; i++) {
            if((getValue (cardHand[i]) == 1 ? 14 : getValue (cardHand[i])) == temp
                    && (getValue (cardHand[i + 1]) == 1 ? 14 : getValue (cardHand[i + 1])) == temp) {
                pearList = RTL.insertArray (pearList, i);
                pearList = RTL.insertArray (pearList, i + 1);
                cardUp = RTL.insertArray (cardUp, cardHand[i]);
                cardUp = RTL.insertArray (cardUp, cardHand[i + 1]);
                temp += 1;
            }
        }
        if(pearList != null && pearList.Length == 8 && compareCard (cardUp, cardFire)) {
            indexCardNhac = pearList;
        }
        return indexCardNhac;
    }

    public static int getValue (int i) {
        return Card.cardPaint[i] % 13 + 1;
    }

    public static int getType (int i) {
        return Card.cardPaint[i] / 13;
    }

    public static bool isContain (int[] a, int v) {
        if(a == null) {
            return false;
        }
        for(int i = 0; i < a.Length; i++) {
            if(a[i] == v) {
                return true;
            }
        }
        return false;
    }

    public static bool compareCard (int[] cardHand, int[] cardFire) {
        int typeC = getTypeOfListCardFire (cardFire);
        if(cardHand.Length != cardFire.Length) {
            int typeHand = getTypeOfListCardFire (cardHand);
            if(typeC == 1 && (typeHand == 5 || typeHand == 6 || typeHand == 7)) {
                return true;
            } else if(typeC == 10 && (typeHand == 6 || typeHand == 7)) {
                return true;
            } else if(typeC == 5 && (typeHand == 6 || typeHand == 7)) {
                return true;
            } else if(typeC == 6 && typeHand == 7) {
                return true;
            } else {
                return false;
            }
        }
        if(getValue (cardHand[cardHand.Length - 1]) == getValue (cardFire[cardFire.Length - 1])) {
            if(cardHand[cardHand.Length - 1] < cardFire[cardFire.Length - 1]) {
                return false;
            }
        }
        if((cardHand.Length == 2 || cardHand.Length == 1) && getValue (cardHand[0]) == 2) {
            return true;
        }
        if((cardFire.Length == 2 || cardFire.Length == 1) && getValue (cardFire[0]) == 2) {
            return false;
        }
        if(getValue (cardHand[cardHand.Length - 1]) == 1) {
            return true;
        }
        if(getValue (cardFire[cardFire.Length - 1]) == 1) {
            return false;
        }
        int vh = getValue (cardHand[cardHand.Length - 1]) == 2 ? 14 : getValue (cardHand[cardHand.Length - 1]);
        int vf = getValue (cardFire[cardFire.Length - 1]) == 2 ? 14 : getValue (cardFire[cardFire.Length - 1]);

        if(vh < vf || getValue (cardFire[cardFire.Length - 1]) == 1) {
            return false;
        }
        // if (getValue(cardHand[cardHand.Length - 1]) <
        // getValue(cardFire[cardFire.Length - 1])
        // || getValue(cardFire[cardFire.Length - 1]) == 1) {
        // return false;
        // }
        return true;
    }
}
