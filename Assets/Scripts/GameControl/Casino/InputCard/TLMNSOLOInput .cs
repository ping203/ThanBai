using UnityEngine;
using System.Collections;

public class TLMNSOLOInput {
    private ArrayCard arrayCard;
    private Card card;
    private TLMNSOLO tlmnStagesolo;
    ABSUser player;

    public TLMNSOLOInput(ABSUser player, TLMNSOLO tlmn, ArrayCard arrayCard, Card card) {
        // TODO Auto-generated constructor stub
        this.arrayCard = arrayCard;
        this.card = card;
        this.tlmnStagesolo = tlmn;
        this.player = player;
    }
    public void click () {if (player.getName().Equals(BaseInfo.gI().mainInfo.nick)) {
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
					int value1 = TLMNChooseCard.getValue(card.getId());
					int value2 = TLMNChooseCard.getValue(arrayCard.getArrCardChoose()[len - 1]);
					if (!(value1 == value2)
							&& !(value1 == value2 + 1)
							&& !(RTL.getCardInfo(card.getId())[1] == RTL
									.getCardInfo(arrayCard.getArrCardChoose()[len - 1])[1] - 1)) {
						int valuve3 = RTL.getCardInfo(arrayCard.getArrCardChoose()[0])[1];
						if (!(value1 == valuve3) && !(value1 == valuve3 + 1) && !(value1 == valuve3 - 1)) {
							if (!TLMNChooseCard.issanh2(TLMNSOLOPlayer.sort(RTL.insertArray(arrayCard.getArrCardChoose(),
									card.getId())))
									&& !TLMNChooseCard
											.isdoi(RTL.insertArray(arrayCard.getArrCardChoose(), card.getId()))
									&& !TLMNChooseCard
											.issam(RTL.insertArray(arrayCard.getArrCardChoose(), card.getId()))) {

								for (int i = 0; i < arrayCard.getSize(); i++) {
									arrayCard.getCardbyPos(i).setChoose(false);
								}
								len = 0;
							}
						}

					}

					if (tlmnStagesolo != null && tlmnStagesolo.tableArrCard != null && arrayCard.getArrCardChoose() != null
							&& arrayCard.getArrCardChoose().Length > 0) {
						if (!TLMNChooseCard.issanh2(TLMNSOLOPlayer.sort(RTL.insertArray(arrayCard.getArrCardChoose(),
								card.getId())))
								&& !TLMNChooseCard.isdoi(RTL.insertArray(arrayCard.getArrCardChoose(), card.getId()))
								&& !TLMNChooseCard.issam(RTL.insertArray(arrayCard.getArrCardChoose(), card.getId()))) {
							for (int i = 0; i < arrayCard.getSize(); i++) {
								arrayCard.getCardbyPos(i).setChoose(false);
							}
							len = 0;
						}
					}
				}
			}

			card.setChoose(!card.isChoose);

			if (tlmnStagesolo != null && tlmnStagesolo.tableArrCard != null) {
				if (tlmnStagesolo.tableArrCard.Length > 0 && len == 0) {
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
							int[] index = TLMNChooseCard.getArrIndexCardNhac(indexCardChoose,
									arrayCard.getArrCardAll(), tlmnStagesolo.tableArrCard);
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
					int[] index = TLMNChooseCard.getArrIndexCardNhac(arrayCard.getArrCardChoose(),
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
						if (!TLMNChooseCard.issanh2(arrayCard.getArrCardChoose())) {
							for (int i = 0; i < arrayCard.getSize(); i++) {
								arrayCard.getCardbyPos(i).setChoose(false);
							}
							card.setChoose(!card.isChoose);
						}
					}
				} else if (arrayCard.getArrCardChoose().Length >= 2 && card.isChoose) {
					if (!TLMNChooseCard.issanh2(arrayCard.getArrCardChoose())) {
						for (int i = 0; i < arrayCard.getSize(); i++) {
							arrayCard.getCardbyPos(i).setChoose(false);
						}
						card.setChoose(!card.isChoose);
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
