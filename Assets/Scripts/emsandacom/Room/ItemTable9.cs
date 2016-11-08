using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;

public class ItemTable9 : MonoBehaviour
{

    private int id;
    public Text tableIdTxt;
    public Text blindTxt;
    private int userInTable;
    private int maxUser;
    private long blind;
    private long needMoney;
    public Image[] userSpr;

    public void UpdateTable(int id, long blind, long needmoney, int user, int maxuser)
    {
        gameObject.name = id.ToString();
        this.id = id;
        tableIdTxt.text = "ID: " + id;
        blindTxt.text = "Blind: " + BaseInfo.formatMoneyNormal(blind);
        needMoney = needmoney;
        this.blind = blind;
        userInTable = user;
        maxUser = maxuser;
        for (int i = 0; i < userInTable; i++)
        {
            LoadAssetBundle.LoadSprite(userSpr[i], "ui", "user_online");
        }
    }

    public void ClickItemTable()
    {
        RoomViewScript.instance.ClickTable(id, blind, needMoney, maxUser, gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        gameObject.transform.DOScale(1.2f, 0.1f);
    }

    void OnTriggerExit(Collider other)
    {
        gameObject.transform.DOScale(1f, 0.1f);
    }
}
