using UnityEngine;
using System.Collections;

public class XamInput {

    private ArrayCard arrayCard;
    private Card card;
    private Xam xamStage;
    ABSUser player;

    public XamInput (ABSUser player, Xam xam, ArrayCard arrayCard, Card card) {
        this.player = player;
        this.arrayCard = arrayCard;
        this.card = card;
        this.xamStage = xam;
    }

    public void click () {
        if (player.getName().Equals(BaseInfo.gI().mainInfo.nick)) {
			int len = 0;
			if (arrayCard == null) {
				return;
			}
			if (arrayCard.getArrCardChoose() != null) {
				len = arrayCard.getArrCardChoose().Length;
			}

			if (len != 0) {
				if (card.isChoose) {
				} else {
					int value1 = SamChooseCard.getValue(card.getId());
					int value2 = SamChooseCard.getValue(arrayCard.getArrCardChoose()[len - 1]);
					if (!(value1 == value2)
							&& !(value1 == value2 + 1)
							&& !(RTL.getCardInfo(card.getId())[1] == RTL
									.getCardInfo(arrayCard.getArrCardChoose()[len - 1])[1] - 1)) {
						int valuve3 = RTL.getCardInfo(arrayCard.getArrCardChoose()[0])[1];
						if (!(value1 == valuve3) && !(value1 == valuve3 + 1) && !(value1 == valuve3 - 1)) {
							if (!SamChooseCard.issanh2(XamPlayer.sort(RTL.insertArray(arrayCard.getArrCardChoose(),
									card.getId())))
									&& !SamChooseCard
											.isdoi(RTL.insertArray(arrayCard.getArrCardChoose(), card.getId()))
									&& !SamChooseCard
											.issam(RTL.insertArray(arrayCard.getArrCardChoose(), card.getId()))) {
								for (int i = 0; i < arrayCard.getSize(); i++) {
									arrayCard.getCardbyPos(i).setChoose(false);
								}
								len = 0;
							}
						}

					}
					if (xamStage != null && xamStage.tableArrCard != null && arrayCard.getArrCardChoose() != null
							&& arrayCard.getArrCardChoose().Length > 0) {
						if (!SamChooseCard.issanh2(XamPlayer.sort(RTL.insertArray(arrayCard.getArrCardChoose(),
								card.getId())))
								&& !SamChooseCard.isdoi(RTL.insertArray(arrayCard.getArrCardChoose(), card.getId()))
								&& !SamChooseCard.issam(RTL.insertArray(arrayCard.getArrCardChoose(), card.getId()))) {
							for (int i = 0; i < arrayCard.getSize(); i++) {
								arrayCard.getCardbyPos(i).setChoose(false);
							}
							len = 0;
						}
					}
				}
			}

			card.setChoose(!card.isChoose);
			// Debug.Log("card = " + card.isChoose);

			if (xamStage != null && xamStage.tableArrCard != null) {
				if (xamStage.tableArrCard.Length > 0 && len == 0) {
					int indexCardChoose = -1;
					if (arrayCard.getArrCardChoose() == null) {
						return;
					}
					if (arrayCard.getArrCardChoose().Length == 1) {
						for (int i = 0; i < arrayCard.getArrCardAll().Length; i++) {
							if (arrayCard.getArrCardAll()[i] == card.getId()) {
								indexCardChoose = i;
							}
						}
						if (indexCardChoose != -1) {
							int[] index = SamChooseCard.getArrIndexCardNhac(indexCardChoose, arrayCard.getArrCardAll(),
									xamStage.tableArrCard);
							if (index != null) {
								for (int i = 0; i < index.Length; i++) {
									Card cardNhac = arrayCard.getCardbyPos(index[i]);
									if (cardNhac != null) {
										cardNhac.setChoose(true);
									}

								}
							}
						}
					}

				}
			} else {
				if (arrayCard.getArrCardChoose() == null) {
					return;
				}
				if (arrayCard.getArrCardChoose().Length == 2 && card.isChoose) {
					int[] index = SamChooseCard.getArrIndexCardNhac(arrayCard.getArrCardChoose(),
							arrayCard.getArrCardAll());
					if (index != null) {
						for (int i = 0; i < index.Length; i++) {
							Card cardNhac = arrayCard.getCardbyPos(index[i]);
							if (cardNhac != null) {
								cardNhac.setChoose(true);
							}
						}
					}

					if (index != null && index.Length < 2) {
						if (!SamChooseCard.issanh2(arrayCard.getArrCardChoose())) {
							for (int i = 0; i < arrayCard.getSize(); i++) {
								arrayCard.getCardbyPos(i).setChoose(false);
							}
							card.setChoose(!card.isChoose);
						}
					}

				} else if (arrayCard.getArrCardChoose().Length >= 2 && !card.isChoose) {
					// int indexCardChoose = -1;
					// for (int i = 0; i < arrayCard.getArrCardAll().Length;
					// i++) {
					// if (arrayCard.getArrCardAll()[i] == card.getId()) {
					// indexCardChoose = i;
					// }
					// }
					// if (indexCardChoose != -1) {
					// for (int i = indexCardChoose; i <
					// arrayCard.getArrCardAll().Length; i++) {
					// arrayCard.getCardbyPos(i).setChoose(false);
					// }
					// }
				}
			}
		}
    }
}
