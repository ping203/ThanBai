using UnityEngine;
using System.Collections;

public class LiengInput {
    private ArrayCard arrayCard;
    private Card card;
    private LiengPlayer player;
    public LiengInput(ArrayCard arrayCard, Card card, LiengPlayer player) {
        // TODO Auto-generated constructor stub
        this.arrayCard = arrayCard;
        this.card = card;
        if (player.pos == 0) {
            this.player = player;
        }
    }

    public void click() {
        if(player != null)
        Debug.Log(card.getId()+" player.getName() " + player.getName());
        if (card.gameObject.activeInHierarchy && card.getId() == 52 && player.getName().Equals(BaseInfo.gI().mainInfo.nick)) {
            card.gameObject.SetActive(false);
            int dem = 0;
            for (int i = 0; i < 3; i++) {
                if (!arrayCard.getCardbyPos(i).gameObject.activeInHierarchy) {
                    dem++;
                }
            }
            if (dem == 3) {
                SendData.onFlip3Cay();
                //sua
                //GameControl.instance.currentCasino.calculDiem();
            }
        }

    }

}
