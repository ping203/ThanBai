using UnityEngine;
using System.Collections.Generic;

public class WinXocdia : MonoBehaviour {
    public GameObject m_cuaChanWin;
    public GameObject m_cuaLeWin;
    public GameObject m_cuaNhoWin1;//4 do
    public GameObject m_cuaNhoWin2;//4 trang
    public GameObject m_cuaNhoWin3;//1 trang, 3 do.
    public GameObject m_cuaNhoWin4;//1 do, 3 trang.

    private ABSUser[] m_players;

    void Start() {
        m_players = null;

        this.m_cuaLeWin.SetActive(false);
        this.m_cuaNhoWin1.SetActive(false);
        this.m_cuaNhoWin2.SetActive(false);
        this.m_cuaNhoWin3.SetActive(false);
        this.m_cuaNhoWin4.SetActive(false);
    }

    public void SetWinXocdia(int numRed, ABSUser[] players) {
        this.m_players = players;

        switch (numRed) {
            case 0:
                this.m_cuaChanWin.SetActive(true);
                this.m_cuaLeWin.SetActive(false);
                this.m_cuaNhoWin1.SetActive(false);
                this.m_cuaNhoWin2.SetActive(true);
                this.m_cuaNhoWin3.SetActive(false);
                this.m_cuaNhoWin4.SetActive(false);

                for (int i = 0; i < players.Length; i++) {
                    if (players[i] != null) {
                        players[i].GetComponent<XocdiaPlayer>().ActionChipWin(true, false, false, true, false, false);
                        players[i].GetComponent<XocdiaPlayer>().ActionChipLose(false, true, true, false, true, true);
                    }
                }
                break;
            case 1:
                this.m_cuaChanWin.SetActive(false);
                this.m_cuaLeWin.SetActive(true);
                this.m_cuaNhoWin1.SetActive(false);
                this.m_cuaNhoWin2.SetActive(false);
                this.m_cuaNhoWin3.SetActive(false);
                this.m_cuaNhoWin4.SetActive(true);

                for (int i = 0; i < players.Length; i++) {
                    if (players[i] != null) {
                        players[i].GetComponent<XocdiaPlayer>().ActionChipWin(false, true, false, false, false, true);
                        players[i].GetComponent<XocdiaPlayer>().ActionChipLose(true, false, true, true, true, false);
                    }
                }
                break;
            case 2:
                this.m_cuaChanWin.SetActive(true);
                this.m_cuaLeWin.SetActive(false);
                this.m_cuaNhoWin1.SetActive(false);
                this.m_cuaNhoWin2.SetActive(false);
                this.m_cuaNhoWin3.SetActive(false);
                this.m_cuaNhoWin4.SetActive(false);


                for (int i = 0; i < players.Length; i++) {
                    if (players[i] != null) {
                        players[i].GetComponent<XocdiaPlayer>().ActionChipWin(true, false, false, false, false, false);
                        players[i].GetComponent<XocdiaPlayer>().ActionChipLose(false, true, true, true, true, true);
                    }
                }
                break;
            case 3:
                this.m_cuaChanWin.SetActive(false);
                this.m_cuaLeWin.SetActive(true);
                this.m_cuaNhoWin1.SetActive(false);
                this.m_cuaNhoWin2.SetActive(false);
                this.m_cuaNhoWin3.SetActive(true);
                this.m_cuaNhoWin4.SetActive(false);

                for (int i = 0; i < players.Length; i++) {
                    if (players[i] != null) {
                        players[i].GetComponent<XocdiaPlayer>().ActionChipWin(false, true, false, false, true, false);
                        players[i].GetComponent<XocdiaPlayer>().ActionChipLose(true, false, true, true, false, true);
                    }
                }
                break;
            case 4:
                this.m_cuaChanWin.SetActive(true);
                this.m_cuaLeWin.SetActive(false);
                this.m_cuaNhoWin1.SetActive(true);
                this.m_cuaNhoWin2.SetActive(false);
                this.m_cuaNhoWin3.SetActive(false);
                this.m_cuaNhoWin4.SetActive(false);

                for (int i = 0; i < players.Length; i++) {
                    if (players[i] != null) {
                        players[i].GetComponent<XocdiaPlayer>().ActionChipWin(true, false, true, false, false, false);
                        players[i].GetComponent<XocdiaPlayer>().ActionChipLose(false, true, false, true, true, true);
                    }
                }
                break;
        }

    }

    public void RemoveWinXocdia() {
        this.m_cuaChanWin.SetActive(false);
        this.m_cuaLeWin.SetActive(false);
        this.m_cuaNhoWin1.SetActive(false);
        this.m_cuaNhoWin2.SetActive(false);
        this.m_cuaNhoWin3.SetActive(false);
        this.m_cuaNhoWin4.SetActive(false);

        if (this.m_players == null) {
            return;
        } else {
            for (int i = 0; i < this.m_players.Length; i++) {
                this.m_players[i].GetComponent<XocdiaPlayer>().SetPlayerLose();
            }
            this.m_players = null;
        }
    }
}
