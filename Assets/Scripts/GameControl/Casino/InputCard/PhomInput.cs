using UnityEngine;
using System.Collections;

public class PhomInput : MonoBehaviour {
    private ArrayCard arrayCard;
    private Card card;
    private PHOM phomStage;

    public int fireCard = -1;
    private int type = 0;

    public PhomInput(PHOM phomStage, ArrayCard arrayCard, Card card) {
        // TODO Auto-generated constructor stub
        this.arrayCard = arrayCard;
        this.card = card;
        this.phomStage = phomStage;
    }

    public void click() {
        // TODO Auto-generated method stub

        bool isc = card.isChoose;
        if (!phomStage.isHaphom) {
            for (int i = 0; i < arrayCard.getSize(); i++) {
                if (arrayCard.getCardbyPos(i).isChoose)
                    arrayCard.getCardbyPos(i).setChoose(false);
            }
        }
        card.setChoose(!isc);
    }
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
