using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ListTypeCard : MonoBehaviour {
    /// <summary>
    /// 0 - mau thau/1 - doi/2 - thu/3 - sam co/4 - sanh/5 - thung/6 - cu lu/7 - tu quy/8 - thung pha sanh
    /// </summary>
    [SerializeField]
    Toggle[] tg_typeCard;
    ///// <summary>
    ///// 0 - mau thau/1 - doi/2 - thu/3 - sam co/4 - sanh/5 - thung/6 - cu lu/7 - tu quy/8 - thung pha sanh
    ///// </summary>
    ///// 
    //[SerializeField]
    //public GameObject[] help_typeCard;

    public void setTg(int index) {
        for (int i = 0; i < tg_typeCard.Length; i++) {
            tg_typeCard[i].isOn = false;
        }
        tg_typeCard[index].isOn = true;
    }

    public void setOffAll() {
        for (int i = 0; i < tg_typeCard.Length; i++) {
            tg_typeCard[i].isOn = false;
        }
    }
}
