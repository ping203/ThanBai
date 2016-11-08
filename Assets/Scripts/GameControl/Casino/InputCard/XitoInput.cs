using UnityEngine;
using System.Collections;

public class XitoInput {

    private ArrayCard arrayCard;
    private Card card;
    private Xito xitoStage;

    public XitoInput(Xito xitoStage, ArrayCard arrayCard, Card card)
    {
        // TODO Auto-generated constructor stub
        this.arrayCard = arrayCard;
        this.card = card;
        this.xitoStage = xitoStage;
    }
    public void click()
    {
        if (this.xitoStage.isChooseCard)
        {
            this.xitoStage.isChooseCard = false;
            card.setMo(false);
            if (this.arrayCard.getCardbyPos(0).getId() == card.getId())
            {
                SendData.onFlipCard((byte)0);
            }
            else if (this.arrayCard.getCardbyPos(1).getId() == card.getId())
            {
                SendData.onFlipCard((byte)1);
            }
            // activity.showToast("Đã chọn, xin chờ...");
        }
    }
}
