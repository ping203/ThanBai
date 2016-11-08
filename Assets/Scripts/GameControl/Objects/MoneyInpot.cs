using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MoneyInpot : MonoBehaviour {
    public GameObject prefab;
    public Chip chip;
    public long moneyInPot = 0;
    public List<MoneyChip> chipsPlayer = new List<MoneyChip>();

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }

    public void resetData() {
        chipsPlayer.Clear();
        setmMoneyInPotNonModifier(0);
    }

    public void setmMoneyInPotNonModifier(long moneyInPot) {
        if (moneyInPot <= 0) {
            moneyInPot = 0;
        }
        this.moneyInPot = moneyInPot;
        chip.setMoneyChip(moneyInPot);
    }

    public void setMoneyInPot(long moneyInPot) {
        if (moneyInPot <= 0) {
            moneyInPot = 0;
        }
        this.moneyInPot = moneyInPot;
        chip.setMoneyChip(moneyInPot);
    }

    public void addChip(long money, string name, bool isSkip) {
        if (money == 0) {
            return;
        }
        this.moneyInPot = this.moneyInPot + money;
        if (!name.Equals("")) {
            if (!checkExist(name)) {
                chipsPlayer.Add(new MoneyChip(name, money, isSkip));
            } else {
                for (int i = 0; i < chipsPlayer.Count; i++) {
                    if (name.Equals(chipsPlayer[i].name)) {
                        chipsPlayer[i].money += money;
                        break;
                    }
                }
            }

        }
    }

    public void addChip2(long money, string name, bool isSkip) {
        if (money == 0) {
            return;
        }
        if (!name.Equals("")) {
            if (!checkExist(name)) {
                chipsPlayer.Add(new MoneyChip(name, money, isSkip));
            } else {
                for (int i = 0; i < chipsPlayer.Count; i++) {
                    if (name.Equals(chipsPlayer[i].name)) {
                        chipsPlayer[i].money += money;
                        break;
                    }
                }
            }

        }

    }

    public bool checkExist(string name) {
        bool check = false;
        for (int i = 0; i < chipsPlayer.Count; i++) {
            if (name.Equals(chipsPlayer[i].name)) {
                check = true;
                break;
            }
        }
        return check;
    }

    public void chiaChipToChip(long money, MoneyInpot toChip) {
        // setSubChip(money);
        if (money == 0) {
            return;
        }
        setmMoneyInPotNonModifier(moneyInPot - money);
        GameObject obj = (GameObject)Instantiate(prefab);
        obj.transform.localScale = new Vector3(1, 1, 1);
        Chip subChip = obj.GetComponent<Chip>();
        subChip.setMoneyChip(money);
        subChip.transform.parent = gameObject.transform;
        subChip.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        subChip.transform.parent = toChip.gameObject.transform;
        subChip.transform.localScale = new Vector3(1, 1, 1);
        //TweenPosition tp = TweenPosition.Begin(obj, 1, new Vector3(0, 0, 0));
        //tp.AddOnFinished(delegate {
        //    toChip.setmMoneyInPotNonModifier(toChip.moneyInPot
        //                    + money);
        //    Destroy(subChip.gameObject);
        //});

    }

    public void chiaChipToPlayer(long money, ABSUser player) {
        if (money == 0) {
            return;
        }
        setmMoneyInPotNonModifier(moneyInPot - money);
        GameObject obj = (GameObject)Instantiate(prefab);
        obj.transform.localScale = new Vector3(1, 1, 1);
        Chip subChip = obj.GetComponent<Chip>();

        subChip.setMoneyChip(money);
        subChip.transform.parent = gameObject.transform;
        subChip.gameObject.transform.localPosition = new Vector3(0, 0, 0);
        subChip.transform.parent = player.gameObject.transform;
        subChip.transform.localScale = new Vector3(1, 1, 1);
        //TweenPosition tp = TweenPosition.Begin(obj, 1, new Vector3(0, 0, 0));

        //tp.AddOnFinished(delegate {
        //    Destroy(subChip.gameObject);
        //});


    }

    public void rutChip(string name, long money) {
        if (money == 0) {
            return;
        }
        for (int i = 0; i < chipsPlayer.Count; i++) {
            if (chipsPlayer[i].name.Equals(name)) {
                if (chipsPlayer[i].money >= money) {
                    chipsPlayer[i].money = chipsPlayer[i].money - money;
                }
                break;
            }

        }

    }

    public long minIsPlaying() {
        long min = -1;
        for (int i = 0; i < chipsPlayer.Count; i++) {
            if (!chipsPlayer[i].isSkip) {
                min = chipsPlayer[i].money;
                break;
            }

        }
        for (int i = 0; i < chipsPlayer.Count; i++) {
            if (!chipsPlayer[i].isSkip) {
                if (chipsPlayer[i].money <= min || min == -1) {
                    min = chipsPlayer[i].money;
                }
            }

        }
        return min;
    }

    public void clearChipsPlayer() {
        for (int i = 0; i < chipsPlayer.Count; i++) {
            if (chipsPlayer[i].money <= 0) {
                chipsPlayer.RemoveAt(i);
                clearChipsPlayer();
                break;
            }
        }
    }
}
