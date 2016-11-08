using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class XocdiaPlayer : ABSUser {
    //public GameObject m_playerWin;
    public ThongbaoXocdia m_thongbaoXocdia;
    public List<GameObject> m_chips;
    public GameObject m_chipXocdiaParent;
    public Transform m_batTransfrom;

    //Dat cuoc.
    public List<GameObject> m_chipSpawn0 = new List<GameObject>();
    public List<GameObject> m_chipSpawn1 = new List<GameObject>();
    public List<GameObject> m_chipSpawn2 = new List<GameObject>();
    public List<GameObject> m_chipSpawn3 = new List<GameObject>();
    public List<GameObject> m_chipSpawn4 = new List<GameObject>();
    public List<GameObject> m_chipSpawn5 = new List<GameObject>();
    private Vector3 m_originPos;
    private Vector3 m_targetPos;

    //Vi tri chip nam tren cua lon
    private Vector2 m_cuaChanPosX = new Vector2(-230.0f, -125.0f);
    private Vector2 m_cuaLePosX = new Vector2(125.0f, 230.0f);
    private Vector2 m_cuaLonPosY = new Vector2(50.0f, 92.0f);

    //Vi tri chip nam tren cua nho.
    private Vector2 m_cuaNhoPosX_1 = new Vector2(-250.0f, -185.0f);
    private Vector2 m_cuaNhoPosX_2 = new Vector2(-126.0f, -12.0f);
    private Vector2 m_cuaNhoPosX_3 = new Vector2(24.0f, 137.0f);
    private Vector2 m_cuaNhoPosX_4 = new Vector2(198.0f, 250.0f);
    private Vector2 m_cuaNhoPosY = new Vector2(-60.0f, -15.0f);

    //Cache chip dat cuoc.
    private List<GameObject> m_chipDatCuoc0 = new List<GameObject>();
    private List<GameObject> m_chipDatCuoc1 = new List<GameObject>();
    private List<GameObject> m_chipDatCuoc2 = new List<GameObject>();
    private List<GameObject> m_chipDatCuoc3 = new List<GameObject>();
    private List<GameObject> m_chipDatCuoc4 = new List<GameObject>();
    private List<GameObject> m_chipDatCuoc5 = new List<GameObject>();
    //Chi tao ra toi da 20 chip object gap doi.
    private const int MAX_CHIP_DOUBLE = 15;

    public void SetPlayerWin() {
        //if(this.m_playerWin != null) {
        //    this.m_playerWin.SetActive (true);
        //}
        sp_xoay.gameObject.SetActive(true);
    }

    public void SetPlayerLose() {
        //if(this.m_playerWin != null) {
        //    this.m_playerWin.SetActive (false);
        //}
        sp_xoay.gameObject.SetActive(false);
    }

    public void DatCuoc(string aCuaCuoc, long aMucCuoc, bool isDatCuocCua) {
        switch (aCuaCuoc) {
            case "cuachan":
                if (isDatCuocCua) {
                    //if (XocDia.isDatTatTay) {
                    //    SendData.onSendXocDiaDatCuoc((byte)0, aMucCuoc);
                    //} else {
                    SendData.onSendXocDiaDatCuoc((byte)0, aMucCuoc);
                    //}
                    //SendData.onSendXocDiaDatCuoc ((byte) 0, aMucCuoc,false);
                } else {
                    if (this.m_thongbaoXocdia != null) {
                        this.m_thongbaoXocdia.SetLbThongbao("Chưa đến thời gian đặt cược !");
                        this.m_thongbaoXocdia.ShowThongbao1();
                    }
                }

                break;
            case "cuale":
                if (isDatCuocCua) {
                    //if (XocDia.isDatTatTay) {
                    //    SendData.onSendXocDiaDatCuoc((byte)1, aMucCuoc);
                    //} else {
                    SendData.onSendXocDiaDatCuoc((byte)1, aMucCuoc);
                    //}

                } else {
                    if (this.m_thongbaoXocdia != null) {
                        this.m_thongbaoXocdia.SetLbThongbao("Chưa đến thời gian đặt cược !");
                        this.m_thongbaoXocdia.ShowThongbao1();
                    }
                }

                break;
            case "cuanho1":
                if (isDatCuocCua) {
                    //4 do
                    //if (XocDia.isDatTatTay) {
                    //    SendData.onSendXocDiaDatCuoc((byte)2, aMucCuoc);
                    //} else {
                    SendData.onSendXocDiaDatCuoc((byte)2, aMucCuoc);
                    //}
                } else {
                    if (this.m_thongbaoXocdia != null) {
                        this.m_thongbaoXocdia.SetLbThongbao("Chưa đến thời gian đặt cược !");
                        this.m_thongbaoXocdia.ShowThongbao1();
                    }
                }

                break;
            case "cuanho2":
                if (isDatCuocCua) {
                    //4 trang
                    //if (XocDia.isDatTatTay) {
                    //    SendData.onSendXocDiaDatCuoc((byte)3, aMucCuoc);
                    //} else {
                    SendData.onSendXocDiaDatCuoc((byte)3, aMucCuoc);
                    //}
                } else {
                    if (this.m_thongbaoXocdia != null) {
                        this.m_thongbaoXocdia.SetLbThongbao("Chưa đến thời gian đặt cược !");
                        this.m_thongbaoXocdia.ShowThongbao1();
                    }
                }

                break;
            case "cuanho3":
                if (isDatCuocCua) {
                    //1 trang, 3 do.
                    //if (XocDia.isDatTatTay) {
                    //    SendData.onSendXocDiaDatCuoc((byte)4, aMucCuoc);
                    //} else {
                    SendData.onSendXocDiaDatCuoc((byte)4, aMucCuoc);
                    //}
                } else {
                    if (this.m_thongbaoXocdia != null) {
                        this.m_thongbaoXocdia.SetLbThongbao("Chưa đến thời gian đặt cược !");
                        this.m_thongbaoXocdia.ShowThongbao1();
                    }
                }

                break;
            case "cuanho4":
                if (isDatCuocCua) {
                    //1 do, 3 trang.
                    //if (XocDia.isDatTatTay) {
                    //    SendData.onSendXocDiaDatCuoc((byte)5, aMucCuoc);
                    //} else {
                    Debug.LogError("cuanho3 ");
                    SendData.onSendXocDiaDatCuoc((byte)5, aMucCuoc);
                    //}
                    //SendData.onSendXocDiaDatCuoc ((byte) 5, aMucCuoc);
                } else {
                    if (this.m_thongbaoXocdia != null) {
                        this.m_thongbaoXocdia.SetLbThongbao("Chưa đến thời gian đặt cược !");
                        this.m_thongbaoXocdia.ShowThongbao1();
                    }
                }

                break;
        }
    }

    public void ActionChipDatGapDoi(sbyte cua) {
        int size = 0;
        float xPos;
        float yPos;
        float zPos = 0.0f;
        GameObject go;
        float speed = 0.0f;
        m_originPos = transform.localPosition;

        switch (cua) {
            case 0:
                size = this.m_chipDatCuoc0.Count;
                if (size > MAX_CHIP_DOUBLE) {
                    size = MAX_CHIP_DOUBLE;
                }
                for (int i = 0; i < size; i++) {
                    GameObject chipPrefab = this.m_chipDatCuoc0[i];

                    xPos = Random.Range(this.m_cuaChanPosX.x, this.m_cuaChanPosX.y);
                    yPos = Random.Range(this.m_cuaLonPosY.x, this.m_cuaLonPosY.y);
                    this.m_targetPos = new Vector3(xPos, yPos, zPos);

                    //go = Instantiate (chipPrefab) as GameObject;
                    go = ObjectPool.current.GetObject(chipPrefab);
                    go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = this.m_originPos;
                    go.SetActive(true);

                    speed = 600.0f;
                    go.GetComponent<ChipXocdia>().MoveChip(true, this.m_targetPos, speed, getName());

                    this.m_chipSpawn0.Add(go);
                    this.m_chipDatCuoc0.Add(chipPrefab);
                }
                break;
            case 1:
                size = this.m_chipDatCuoc1.Count;
                if (size > MAX_CHIP_DOUBLE) {
                    size = MAX_CHIP_DOUBLE;
                }
                for (int i = 0; i < size; i++) {
                    GameObject chipPrefab = this.m_chipDatCuoc1[i];

                    xPos = Random.Range(this.m_cuaLePosX.x, this.m_cuaLePosX.y);
                    yPos = Random.Range(this.m_cuaLonPosY.x, this.m_cuaLonPosY.y);
                    this.m_targetPos = new Vector3(xPos, yPos, zPos);

                    //go = Instantiate (chipPrefab) as GameObject;
                    go = ObjectPool.current.GetObject(chipPrefab);
                    go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = this.m_originPos;
                    go.SetActive(true);

                    speed = 600.0f;
                    go.GetComponent<ChipXocdia>().MoveChip(true, this.m_targetPos, speed, getName());

                    this.m_chipSpawn1.Add(go);
                    this.m_chipDatCuoc1.Add(chipPrefab);
                }
                break;
            case 2:
                size = this.m_chipDatCuoc2.Count;
                if (size > MAX_CHIP_DOUBLE) {
                    size = MAX_CHIP_DOUBLE;
                }
                for (int i = 0; i < size; i++) {
                    GameObject chipPrefab = this.m_chipDatCuoc2[i];

                    xPos = Random.Range(this.m_cuaNhoPosX_1.x, this.m_cuaNhoPosX_1.y);
                    yPos = Random.Range(this.m_cuaNhoPosY.x, this.m_cuaNhoPosY.y);
                    this.m_targetPos = new Vector3(xPos, yPos, zPos);

                    //go = Instantiate (chipPrefab) as GameObject;
                    go = ObjectPool.current.GetObject(chipPrefab);
                    //go.SetActive (true);
                    go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = this.m_originPos;
                    go.SetActive(true);

                    speed = 600.0f;
                    go.GetComponent<ChipXocdia>().MoveChip(true, this.m_targetPos, speed, getName());

                    this.m_chipSpawn2.Add(go);
                    this.m_chipDatCuoc2.Add(chipPrefab);
                }
                break;
            case 3:
                size = this.m_chipDatCuoc3.Count;
                if (size > MAX_CHIP_DOUBLE) {
                    size = MAX_CHIP_DOUBLE;
                }
                for (int i = 0; i < size; i++) {
                    GameObject chipPrefab = this.m_chipDatCuoc3[i];

                    xPos = Random.Range(this.m_cuaNhoPosX_2.x, this.m_cuaNhoPosX_2.y);
                    yPos = Random.Range(this.m_cuaNhoPosY.x, this.m_cuaNhoPosY.y);
                    this.m_targetPos = new Vector3(xPos, yPos, zPos);

                    //go = Instantiate (chipPrefab) as GameObject;
                    go = ObjectPool.current.GetObject(chipPrefab);
                    //go.SetActive (true);
                    go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = this.m_originPos;
                    go.SetActive(true);

                    speed = 600.0f;
                    go.GetComponent<ChipXocdia>().MoveChip(true, this.m_targetPos, speed, getName());

                    this.m_chipSpawn3.Add(go);
                    this.m_chipDatCuoc3.Add(chipPrefab);
                }
                break;
            case 4:
                size = this.m_chipDatCuoc4.Count;
                if (size > MAX_CHIP_DOUBLE) {
                    size = MAX_CHIP_DOUBLE;
                }
                for (int i = 0; i < size; i++) {
                    GameObject chipPrefab = this.m_chipDatCuoc4[i];

                    xPos = Random.Range(this.m_cuaNhoPosX_3.x, this.m_cuaNhoPosX_3.y);
                    yPos = Random.Range(this.m_cuaNhoPosY.x, this.m_cuaNhoPosY.y);
                    this.m_targetPos = new Vector3(xPos, yPos, zPos);

                    //go = Instantiate (chipPrefab) as GameObject;
                    go = ObjectPool.current.GetObject(chipPrefab);
                    //go.SetActive (true);
                    go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = this.m_originPos;
                    go.SetActive(true);

                    speed = 600.0f;
                    go.GetComponent<ChipXocdia>().MoveChip(true, this.m_targetPos, speed, getName());

                    this.m_chipSpawn4.Add(go);
                    this.m_chipDatCuoc4.Add(chipPrefab);
                }
                break;
            case 5:
                size = this.m_chipDatCuoc5.Count;
                if (size > MAX_CHIP_DOUBLE) {
                    size = MAX_CHIP_DOUBLE;
                }
                for (int i = 0; i < size; i++) {
                    GameObject chipPrefab = this.m_chipDatCuoc5[i];

                    xPos = Random.Range(this.m_cuaNhoPosX_4.x, this.m_cuaNhoPosX_4.y);
                    yPos = Random.Range(this.m_cuaNhoPosY.x, this.m_cuaNhoPosY.y);
                    this.m_targetPos = new Vector3(xPos, yPos, zPos);

                    //go = Instantiate (chipPrefab) as GameObject;
                    go = ObjectPool.current.GetObject(chipPrefab);
                    //go.SetActive (true);
                    go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = this.m_originPos;
                    go.SetActive(true);

                    speed = 600.0f;
                    go.GetComponent<ChipXocdia>().MoveChip(true, this.m_targetPos, speed, getName());

                    this.m_chipSpawn5.Add(go);
                    this.m_chipDatCuoc5.Add(chipPrefab);
                }
                break;
        }
    }

    public void ActionChipDatcuoc(sbyte cua, int typeCHIP) {
        float xPos;
        float yPos;
        float zPos = 0.0f;
        GameObject go;
        float speed = 0.0f;
        GameObject aChipCuocPrefab = null;
        m_originPos = transform.localPosition;

        switch (typeCHIP) {
            case 0:
                aChipCuocPrefab = this.m_chips[0];
                break;
            case 1:
                aChipCuocPrefab = this.m_chips[1];
                break;
            case 2:
                aChipCuocPrefab = this.m_chips[2];
                break;
            //case 3:
            default:
                aChipCuocPrefab = this.m_chips[3];
                break;
                //case 4:
                //    aChipCuocPrefab = this.m_chips[4];
                //    break;
        }

        switch (cua) {
            case 0://cua chan
                xPos = Random.Range(this.m_cuaChanPosX.x, this.m_cuaChanPosX.y);
                yPos = Random.Range(this.m_cuaLonPosY.x, this.m_cuaLonPosY.y);
                this.m_targetPos = new Vector3(xPos, yPos, zPos);

                //go = Instantiate (aChipCuocPrefab) as GameObject;
                go = ObjectPool.current.GetObject(aChipCuocPrefab);
                //go.SetActive (true);
                go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = this.m_originPos;
                go.SetActive(true);

                speed = 600.0f;
                go.GetComponent<ChipXocdia>().MoveChip(true, this.m_targetPos, speed, getName());

                this.m_chipSpawn0.Add(go);
                this.m_chipDatCuoc0.Add(aChipCuocPrefab);
                break;
            case 1://cua le
                xPos = Random.Range(this.m_cuaLePosX.x, this.m_cuaLePosX.y);
                yPos = Random.Range(this.m_cuaLonPosY.x, this.m_cuaLonPosY.y);
                this.m_targetPos = new Vector3(xPos, yPos, zPos);

                //go = Instantiate (aChipCuocPrefab) as GameObject;
                go = ObjectPool.current.GetObject(aChipCuocPrefab);
                //go.SetActive (true);
                go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = this.m_originPos;
                go.SetActive(true);

                speed = 600.0f;
                go.GetComponent<ChipXocdia>().MoveChip(true, this.m_targetPos, speed, getName());

                this.m_chipSpawn1.Add(go);
                this.m_chipDatCuoc1.Add(aChipCuocPrefab);
                break;
            case 2://cua nho 1, 4 do.
                xPos = Random.Range(this.m_cuaNhoPosX_1.x, this.m_cuaNhoPosX_1.y);
                yPos = Random.Range(this.m_cuaNhoPosY.x, this.m_cuaNhoPosY.y);
                this.m_targetPos = new Vector3(xPos, yPos, zPos);

                //go = Instantiate (aChipCuocPrefab) as GameObject;
                go = ObjectPool.current.GetObject(aChipCuocPrefab);
                //go.SetActive (true);
                go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = this.m_originPos;
                go.SetActive(true);

                speed = 600.0f;
                go.GetComponent<ChipXocdia>().MoveChip(true, this.m_targetPos, speed, getName());

                this.m_chipSpawn2.Add(go);
                this.m_chipDatCuoc2.Add(aChipCuocPrefab);
                break;
            case 3://cua nho 2, 4 trang.
                xPos = Random.Range(this.m_cuaNhoPosX_2.x, this.m_cuaNhoPosX_2.y);
                yPos = Random.Range(this.m_cuaNhoPosY.x, this.m_cuaNhoPosY.y);
                this.m_targetPos = new Vector3(xPos, yPos, zPos);

                //go = Instantiate (aChipCuocPrefab) as GameObject;
                go = ObjectPool.current.GetObject(aChipCuocPrefab);
                //go.SetActive (true);
                go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = this.m_originPos;
                go.SetActive(true);

                speed = 600.0f;
                go.GetComponent<ChipXocdia>().MoveChip(true, this.m_targetPos, speed, getName());

                this.m_chipSpawn3.Add(go);
                this.m_chipDatCuoc3.Add(aChipCuocPrefab);
                break;
            case 4://cua nho 3, 1 trang + 3 do.
                xPos = Random.Range(this.m_cuaNhoPosX_3.x, this.m_cuaNhoPosX_3.y);
                yPos = Random.Range(this.m_cuaNhoPosY.x, this.m_cuaNhoPosY.y);
                this.m_targetPos = new Vector3(xPos, yPos, zPos);

                //go = Instantiate (aChipCuocPrefab) as GameObject;
                go = ObjectPool.current.GetObject(aChipCuocPrefab);
                //go.SetActive (true);
                go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = this.m_originPos;
                go.SetActive(true);

                speed = 600.0f;
                go.GetComponent<ChipXocdia>().MoveChip(true, this.m_targetPos, speed, getName());

                this.m_chipSpawn4.Add(go);
                this.m_chipDatCuoc4.Add(aChipCuocPrefab);
                break;
            case 5://cua nho 4, 1 do + 3 trang.
                xPos = Random.Range(this.m_cuaNhoPosX_4.x, this.m_cuaNhoPosX_4.y);
                yPos = Random.Range(this.m_cuaNhoPosY.x, this.m_cuaNhoPosY.y);
                this.m_targetPos = new Vector3(xPos, yPos, zPos);

                //go = Instantiate (aChipCuocPrefab) as GameObject;
                go = ObjectPool.current.GetObject(aChipCuocPrefab);
                //go.SetActive (true);
                go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = this.m_originPos;
                go.SetActive(true);

                speed = 600.0f;
                go.GetComponent<ChipXocdia>().MoveChip(true, this.m_targetPos, speed, getName());

                this.m_chipSpawn5.Add(go);
                this.m_chipDatCuoc5.Add(aChipCuocPrefab);
                break;
        }
    }

    public void ActionChipWin(bool cuachan, bool cuale, bool cuanho1, bool cuanho2, bool cuanho3, bool cuanho4) {

        //Xac dinh vi tri cua nha cai.
        Vector2 tempX0 = Vector2.zero;
        Vector2 tempY0 = Vector2.zero;
        if (XocDia.SystemIsMaster == true) {
            tempX0 = new Vector2(this.m_batTransfrom.localPosition.x - 20,
                this.m_batTransfrom.localPosition.x + 20);
            tempY0 = new Vector2(this.m_batTransfrom.localPosition.y - 20,
                this.m_batTransfrom.localPosition.y + 20);
        } else {
            if (XocDia.PlayerLamcai != null) {
                tempX0 = new Vector2(XocDia.PlayerLamcai.localPosition.x - 15,
                    XocDia.PlayerLamcai.localPosition.x + 15);
                tempY0 = new Vector2(XocDia.PlayerLamcai.localPosition.y - 20,
                    XocDia.PlayerLamcai.localPosition.y + 20);
            }
        }

        int size = 0;
        float xPos;
        float yPos;
        float zPos = 0.0f;
        GameObject go;
        float speed = 0.0f;
        m_originPos = transform.localPosition;

        if (cuachan) {
            size = this.m_chipSpawn0.Count;
            if (size > MAX_CHIP_DOUBLE) {
                size = MAX_CHIP_DOUBLE;
            }

            for (int i = 0; i < size; i++) {
                //Spawn chip tu nha cai.
                GameObject chipPrefab = this.m_chipSpawn0[i];
                xPos = Random.Range(tempX0.x, tempX0.y);
                yPos = Random.Range(tempY0.x, tempY0.y);
                this.m_originPos = new Vector3(xPos, yPos, zPos);
                //go = Instantiate (chipPrefab) as GameObject;
                go = ObjectPool.current.GetObject(chipPrefab);
                //go.SetActive (true);
                go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = this.m_originPos;
                go.SetActive(true);

                //Move chip tu nha cai toi cua cuoc.
                xPos = Random.Range(this.m_cuaChanPosX.x, this.m_cuaChanPosX.y);
                yPos = Random.Range(this.m_cuaLonPosY.x, this.m_cuaLonPosY.y);
                this.m_targetPos = new Vector3(xPos, yPos, zPos);

                speed = 300.0f;
                if (i == size - 1) {
                    go.GetComponent<ChipXocdia>().SequenceMove(true, this.m_targetPos, speed, getName(),
                        onSequenceMoveComplete, 0);
                } else {
                    go.GetComponent<ChipXocdia>().SequenceMove(true, this.m_targetPos, speed, getName());
                }

                this.m_chipSpawn0.Add(go);
            }
        }

        if (cuale) {
            size = this.m_chipSpawn1.Count;
            if (size > MAX_CHIP_DOUBLE) {
                size = MAX_CHIP_DOUBLE;
            }

            for (int i = 0; i < size; i++) {
                //Spawn chip tu nha cai.
                GameObject chipPrefab = this.m_chipSpawn1[i];
                xPos = Random.Range(tempX0.x, tempX0.y);
                yPos = Random.Range(tempY0.x, tempY0.y);
                this.m_originPos = new Vector3(xPos, yPos, zPos);
                //go = Instantiate (chipPrefab) as GameObject;
                go = ObjectPool.current.GetObject(chipPrefab);
                //go.SetActive (true);
                go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = this.m_originPos;
                go.SetActive(true);

                //Move chip tu nha cai toi cua cuoc.
                xPos = Random.Range(this.m_cuaLePosX.x, this.m_cuaLePosX.y);
                yPos = Random.Range(this.m_cuaLonPosY.x, this.m_cuaLonPosY.y);
                this.m_targetPos = new Vector3(xPos, yPos, zPos);

                speed = 300.0f;
                if (i == size - 1) {
                    go.GetComponent<ChipXocdia>().SequenceMove(true, this.m_targetPos, speed, getName(),
                        onSequenceMoveComplete, 1);
                } else {
                    go.GetComponent<ChipXocdia>().SequenceMove(true, this.m_targetPos, speed, getName());
                }

                this.m_chipSpawn1.Add(go);
            }

        }

        if (cuanho1) {
            size = this.m_chipSpawn2.Count;
            if (size > MAX_CHIP_DOUBLE) {
                size = MAX_CHIP_DOUBLE;
            }

            for (int i = 0; i < size; i++) {
                //Spawn chip tu nha cai.
                GameObject chipPrefab = this.m_chipSpawn2[i];
                xPos = Random.Range(tempX0.x, tempX0.y);
                yPos = Random.Range(tempY0.x, tempY0.y);
                this.m_originPos = new Vector3(xPos, yPos, zPos);
                //go = Instantiate (chipPrefab) as GameObject;
                go = ObjectPool.current.GetObject(chipPrefab);
                //go.SetActive (true);
                go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = this.m_originPos;
                go.SetActive(true);

                //Move chip tu nha cai toi cua cuoc.
                xPos = Random.Range(this.m_cuaNhoPosX_1.x, this.m_cuaNhoPosX_1.y);
                yPos = Random.Range(this.m_cuaNhoPosY.x, this.m_cuaNhoPosY.y);
                this.m_targetPos = new Vector3(xPos, yPos, zPos);

                speed = 300.0f;
                if (i == size - 1) {
                    go.GetComponent<ChipXocdia>().SequenceMove(true, this.m_targetPos, speed, getName(),
                        onSequenceMoveComplete, 2);
                } else {
                    go.GetComponent<ChipXocdia>().SequenceMove(true, this.m_targetPos, speed, getName());
                }

                this.m_chipSpawn2.Add(go);
            }

        }

        if (cuanho2) {
            size = this.m_chipSpawn3.Count;
            if (size > MAX_CHIP_DOUBLE) {
                size = MAX_CHIP_DOUBLE;
            }

            for (int i = 0; i < size; i++) {
                //Spawn chip tu nha cai.
                GameObject chipPrefab = this.m_chipSpawn3[i];
                xPos = Random.Range(tempX0.x, tempX0.y);
                yPos = Random.Range(tempY0.x, tempY0.y);
                this.m_originPos = new Vector3(xPos, yPos, zPos);
                //go = Instantiate (chipPrefab) as GameObject;
                go = ObjectPool.current.GetObject(chipPrefab);
                //go.SetActive (true);
                go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = this.m_originPos;
                go.SetActive(true);

                //Move chip tu nha cai toi cua cuoc.
                xPos = Random.Range(this.m_cuaNhoPosX_2.x, this.m_cuaNhoPosX_2.y);
                yPos = Random.Range(this.m_cuaNhoPosY.x, this.m_cuaNhoPosY.y);
                this.m_targetPos = new Vector3(xPos, yPos, zPos);

                speed = 300.0f;
                if (i == size - 1) {
                    go.GetComponent<ChipXocdia>().SequenceMove(true, this.m_targetPos, speed, getName(),
                        onSequenceMoveComplete, 3);
                } else {
                    go.GetComponent<ChipXocdia>().SequenceMove(true, this.m_targetPos, speed, getName());
                }

                this.m_chipSpawn3.Add(go);
            }

        }

        if (cuanho3) {
            size = this.m_chipSpawn4.Count;
            if (size > MAX_CHIP_DOUBLE) {
                size = MAX_CHIP_DOUBLE;
            }

            for (int i = 0; i < size; i++) {
                //Spawn chip tu nha cai.
                GameObject chipPrefab = this.m_chipSpawn4[i];
                xPos = Random.Range(tempX0.x, tempX0.y);
                yPos = Random.Range(tempY0.x, tempY0.y);
                this.m_originPos = new Vector3(xPos, yPos, zPos);
                //go = Instantiate (chipPrefab) as GameObject;
                go = ObjectPool.current.GetObject(chipPrefab);
                //go.SetActive (true);
                go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = this.m_originPos;
                go.SetActive(true);

                //Move chip tu nha cai toi cua cuoc.
                xPos = Random.Range(this.m_cuaNhoPosX_3.x, this.m_cuaNhoPosX_3.y);
                yPos = Random.Range(this.m_cuaNhoPosY.x, this.m_cuaNhoPosY.y);
                this.m_targetPos = new Vector3(xPos, yPos, zPos);

                speed = 300.0f;
                if (i == size - 1) {
                    go.GetComponent<ChipXocdia>().SequenceMove(true, this.m_targetPos, speed, getName(),
                        onSequenceMoveComplete, 4);
                } else {
                    go.GetComponent<ChipXocdia>().SequenceMove(true, this.m_targetPos, speed, getName());
                }

                this.m_chipSpawn4.Add(go);
            }

        }

        if (cuanho4) {
            size = this.m_chipSpawn5.Count;
            if (size > MAX_CHIP_DOUBLE) {
                size = MAX_CHIP_DOUBLE;
            }

            for (int i = 0; i < size; i++) {
                //Spawn chip tu nha cai.
                GameObject chipPrefab = this.m_chipSpawn5[i];
                xPos = Random.Range(tempX0.x, tempX0.y);
                yPos = Random.Range(tempY0.x, tempY0.y);
                this.m_originPos = new Vector3(xPos, yPos, zPos);
                //go = Instantiate (chipPrefab) as GameObject;
                go = ObjectPool.current.GetObject(chipPrefab);
                //go.SetActive (true);
                go.transform.SetParent(this.m_chipXocdiaParent.transform, true);
                go.transform.localScale = Vector3.one;
                go.transform.localPosition = this.m_originPos;
                go.SetActive(true);

                //Move chip tu nha cai toi cua cuoc.
                xPos = Random.Range(this.m_cuaNhoPosX_4.x, this.m_cuaNhoPosX_4.y);
                yPos = Random.Range(this.m_cuaNhoPosY.x, this.m_cuaNhoPosY.y);
                this.m_targetPos = new Vector3(xPos, yPos, zPos);

                speed = 300.0f;
                if (i == size - 1) {
                    go.GetComponent<ChipXocdia>().SequenceMove(true, this.m_targetPos, speed, getName(),
                        onSequenceMoveComplete, 5);
                } else {
                    go.GetComponent<ChipXocdia>().SequenceMove(true, this.m_targetPos, speed, getName());
                }

                this.m_chipSpawn5.Add(go);
            }

        }
    }

    private void onSequenceMoveComplete(int cua) {
        //Xac dinh vi tri chip tai playerwin.
        Vector2 tempX = new Vector2(gameObject.transform.localPosition.x - 15.0f,
            gameObject.transform.localPosition.x + 15.0f);
        Vector2 tempY = new Vector2(gameObject.transform.localPosition.y - 20.0f,
            gameObject.transform.localPosition.y + 20);
        Vector3 targetPos2 = gameObject.transform.localPosition;

        switch (cua) {
            case 0:

                for (int i = 0; i < this.m_chipSpawn0.Count; i++) {
                    //Random position for chip move to player win
                    float posX = Random.Range(tempX.x, tempX.y);
                    float posY = Random.Range(tempY.x, tempY.y);
                    targetPos2 = new Vector3(posX, posY, 0.0f);

                    if (this.m_chipSpawn0[i] != null)
                        this.m_chipSpawn0[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos2, 300.0f, getName(),
                            onActionComplete, 0, i);
                }
                break;
            case 1:

                for (int i = 0; i < this.m_chipSpawn1.Count; i++) {
                    //Random position for chip move to player win
                    float posX = Random.Range(tempX.x, tempX.y);
                    float posY = Random.Range(tempY.x, tempY.y);
                    targetPos2 = new Vector3(posX, posY, 0.0f);

                    if (this.m_chipSpawn1[i] != null)
                        this.m_chipSpawn1[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos2, 300.0f, getName(),
                            onActionComplete, 1, i);
                }
                break;
            case 2:
                for (int i = 0; i < this.m_chipSpawn2.Count; i++) {
                    //Random position for chip move to player win
                    float posX = Random.Range(tempX.x, tempX.y);
                    float posY = Random.Range(tempY.x, tempY.y);
                    targetPos2 = new Vector3(posX, posY, 0.0f);

                    if (this.m_chipSpawn2[i] != null)
                        this.m_chipSpawn2[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos2, 300.0f, getName(),
                            onActionComplete, 2, i);
                }

                break;
            case 3:
                for (int i = 0; i < this.m_chipSpawn3.Count; i++) {
                    //Random position for chip move to player win
                    float posX = Random.Range(tempX.x, tempX.y);
                    float posY = Random.Range(tempY.x, tempY.y);
                    targetPos2 = new Vector3(posX, posY, 0.0f);

                    if (this.m_chipSpawn3[i] != null)
                        this.m_chipSpawn3[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos2, 300.0f, getName(),
                            onActionComplete, 3, i);
                }

                break;
            case 4:
                for (int i = 0; i < this.m_chipSpawn4.Count; i++) {
                    //Random position for chip move to player win
                    float posX = Random.Range(tempX.x, tempX.y);
                    float posY = Random.Range(tempY.x, tempY.y);
                    targetPos2 = new Vector3(posX, posY, 0.0f);

                    if (this.m_chipSpawn4[i] != null)
                        this.m_chipSpawn4[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos2, 300.0f, getName(),
                            onActionComplete, 4, i);
                }

                break;
            case 5:
                for (int i = 0; i < this.m_chipSpawn5.Count; i++) {
                    //Random position for chip move to player win
                    float posX = Random.Range(tempX.x, tempX.y);
                    float posY = Random.Range(tempY.x, tempY.y);
                    targetPos2 = new Vector3(posX, posY, 0.0f);

                    if (this.m_chipSpawn5[i] != null)
                        this.m_chipSpawn5[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos2, 300.0f, getName(),
                            onActionComplete, 5, i);
                }

                break;
        }
    }

    private void onActionComplete(int idSender, int index, GameObject self) {
        ObjectPool.current.PoolObject(self);
        switch (idSender) {
            case 0:
                //Destroy (self);
                //ObjectPool.current.PoolObject (self);
                if (index == this.m_chipSpawn0.Count - 1) {
                    this.m_chipSpawn0.Clear();
                    this.m_chipDatCuoc0.Clear();
                }
                break;
            case 1:
                //Destroy (self);
                if (index == this.m_chipSpawn1.Count - 1) {
                    this.m_chipSpawn1.Clear();
                    this.m_chipDatCuoc1.Clear();
                }
                break;
            case 2:
                //Destroy (self);
                if (index == this.m_chipSpawn2.Count - 1) {
                    this.m_chipSpawn2.Clear();
                    this.m_chipDatCuoc2.Clear();
                }
                break;
            case 3:
                //Destroy (self);
                if (index == this.m_chipSpawn3.Count - 1) {
                    this.m_chipSpawn3.Clear();
                    this.m_chipDatCuoc3.Clear();
                }
                break;
            case 4:
                //Destroy (self);
                if (index == this.m_chipSpawn4.Count - 1) {
                    this.m_chipSpawn4.Clear();
                    this.m_chipDatCuoc4.Clear();
                }
                break;
            case 5:
                //Destroy (self);
                if (index == this.m_chipSpawn5.Count - 1) {
                    this.m_chipSpawn5.Clear();
                    this.m_chipDatCuoc5.Clear();
                }
                break;
        }
    }

    public void ActionChipLose(bool cuachan, bool cuale, bool cuanho1, bool cuanho2, bool cuanho3, bool cuanho4) {
        Vector2 tempX = Vector2.zero;
        Vector2 tempY = Vector2.zero;

        if (XocDia.SystemIsMaster == true) {
            tempX = new Vector2(this.m_batTransfrom.localPosition.x - 20,
                this.m_batTransfrom.localPosition.x + 20);
            tempY = new Vector2(this.m_batTransfrom.localPosition.y - 20,
                this.m_batTransfrom.localPosition.y + 20);
        } else {
            if (XocDia.PlayerLamcai != null) {
                tempX = new Vector2(XocDia.PlayerLamcai.localPosition.x - 15,
                    XocDia.PlayerLamcai.localPosition.x + 15);
                tempY = new Vector2(XocDia.PlayerLamcai.localPosition.y - 20,
                    XocDia.PlayerLamcai.localPosition.y + 20);
            }
        }


        Vector3 targetPos2 = this.m_batTransfrom.localPosition;

        if (cuachan) {
            for (int i = 0; i < this.m_chipSpawn0.Count; i++) {
                //Random position for chip movement.
                float posX = Random.Range(tempX.x, tempX.y);
                float posY = Random.Range(tempY.x, tempY.y);
                targetPos2 = new Vector3(posX, posY, 0.0f);

                if (this.m_chipDatCuoc0[i] != null)
                    this.m_chipSpawn0[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos2,
                        300.0f, getName(), onActionComplete, 0, i);
            }
        }

        if (cuale) {
            for (int i = 0; i < this.m_chipSpawn1.Count; i++) {
                //Random position for chip movement.
                float posX = Random.Range(tempX.x, tempX.y);
                float posY = Random.Range(tempY.x, tempY.y);
                targetPos2 = new Vector3(posX, posY, 0.0f);

                if (this.m_chipDatCuoc1[i] != null)
                    this.m_chipSpawn1[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos2,
                        300.0f, getName(), onActionComplete, 1, i);
            }
        }

        if (cuanho1) {
            for (int i = 0; i < this.m_chipSpawn2.Count; i++) {
                //Random position for chip movement.
                float posX = Random.Range(tempX.x, tempX.y);
                float posY = Random.Range(tempY.x, tempY.y);
                targetPos2 = new Vector3(posX, posY, 0.0f);

                if (this.m_chipDatCuoc2[i] != null)
                    this.m_chipSpawn2[i].GetComponent<ChipXocdia>().MoveChip(true,
                        targetPos2, 300.0f, getName(), onActionComplete, 2, i);
            }
        }

        if (cuanho2) {
            for (int i = 0; i < this.m_chipSpawn3.Count; i++) {
                //Random position for chip movement.
                float posX = Random.Range(tempX.x, tempX.y);
                float posY = Random.Range(tempY.x, tempY.y);
                targetPos2 = new Vector3(posX, posY, 0.0f);

                if (this.m_chipDatCuoc3[i] != null)
                    this.m_chipSpawn3[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos2,
                        300.0f, getName(), onActionComplete, 3, i);
            }
        }

        if (cuanho3) {
            for (int i = 0; i < this.m_chipSpawn4.Count; i++) {
                //Random position for chip movement.
                float posX = Random.Range(tempX.x, tempX.y);
                float posY = Random.Range(tempY.x, tempY.y);
                targetPos2 = new Vector3(posX, posY, 0.0f);

                if (this.m_chipDatCuoc4[i] != null)
                    this.m_chipSpawn4[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos2,
                        300.0f, getName(), onActionComplete, 4, i);
            }
        }

        if (cuanho4) {
            for (int i = 0; i < this.m_chipSpawn5.Count; i++) {
                //Random position for chip movement.
                float posX = Random.Range(tempX.x, tempX.y);
                float posY = Random.Range(tempY.x, tempY.y);
                targetPos2 = new Vector3(posX, posY, 0.0f);

                if (this.m_chipDatCuoc5[i] != null)
                    this.m_chipSpawn5[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos2,
                        500.0f, getName(), onActionComplete, 5, i);
            }
        }

    }

    public void ActionTraTienCuoc(long cua0, long cua1, long cua2, long cua3, long cua4, long cua5) {
        //Xac dinh vi tri chip tai cua cuoc.
        Vector2 tempX = new Vector2(transform.localPosition.x - 15.0f,
            transform.localPosition.x + 15.0f);
        Vector2 tempY = new Vector2(transform.localPosition.y - 20.0f,
            transform.localPosition.y + 20);
        Vector3 targetPos = transform.localPosition;

        if (cua0 > 0) {
            for (int i = 0; i < this.m_chipSpawn0.Count; i++) {
                float posX = Random.Range(tempX.x, tempX.y);
                float posY = Random.Range(tempY.x, tempY.y);
                targetPos = new Vector3(posX, posY, 0.0f);

                if (this.m_chipSpawn0[i] != null)
                    this.m_chipSpawn0[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos, 300.0f, getName(),
                         onActionComplete, 0, i);
            }
        }

        if (cua1 > 0) {
            for (int i = 0; i < this.m_chipSpawn1.Count; i++) {
                float posX = Random.Range(tempX.x, tempX.y);
                float posY = Random.Range(tempY.x, tempY.y);
                targetPos = new Vector3(posX, posY, 0.0f);

                if (this.m_chipSpawn1[i] != null)
                    this.m_chipSpawn1[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos, 300.0f, getName(),
                         onActionComplete, 1, i);
            }
        }

        if (cua2 > 0) {
            for (int i = 0; i < this.m_chipSpawn2.Count; i++) {
                float posX = Random.Range(tempX.x, tempX.y);
                float posY = Random.Range(tempY.x, tempY.y);
                targetPos = new Vector3(posX, posY, 0.0f);

                if (this.m_chipSpawn2[i] != null)
                    this.m_chipSpawn2[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos, 300.0f, getName(),
                         onActionComplete, 2, i);
            }
        }

        if (cua3 > 0) {
            for (int i = 0; i < this.m_chipSpawn3.Count; i++) {
                float posX = Random.Range(tempX.x, tempX.y);
                float posY = Random.Range(tempY.x, tempY.y);
                targetPos = new Vector3(posX, posY, 0.0f);

                if (this.m_chipSpawn3[i] != null)
                    this.m_chipSpawn3[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos, 300.0f, getName(),
                         onActionComplete, 3, i);
            }
        }

        if (cua4 > 0) {
            for (int i = 0; i < this.m_chipSpawn4.Count; i++) {
                float posX = Random.Range(tempX.x, tempX.y);
                float posY = Random.Range(tempY.x, tempY.y);
                targetPos = new Vector3(posX, posY, 0.0f);

                if (this.m_chipSpawn4[i] != null)
                    this.m_chipSpawn4[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos, 300.0f, getName(),
                         onActionComplete, 4, i);
            }
        }

        if (cua5 > 0) {
            for (int i = 0; i < this.m_chipSpawn5.Count; i++) {
                float posX = Random.Range(tempX.x, tempX.y);
                float posY = Random.Range(tempY.x, tempY.y);
                targetPos = new Vector3(posX, posY, 0.0f);

                if (this.m_chipSpawn5[i] != null)
                    this.m_chipSpawn5[i].GetComponent<ChipXocdia>().MoveChip(true, targetPos, 300.0f, getName(),
                         onActionComplete, 5, i);
            }
        }
    }

    public void RemoveAllChip() {
        if (this.m_chipSpawn0 != null) {
            for (int i = 0; i < this.m_chipSpawn0.Count; i++) {
                if (this.m_chipSpawn0[i] != null) {
                    //Destroy (this.m_chipSpawn0[i]);
                    ObjectPool.current.PoolObject(this.m_chipSpawn0[i]);
                }
            }
            this.m_chipSpawn0.Clear();
            this.m_chipDatCuoc0.Clear();
        }

        if (this.m_chipSpawn1 != null) {
            for (int i = 0; i < this.m_chipSpawn1.Count; i++) {
                if (this.m_chipSpawn1[i] != null) {
                    //Destroy (this.m_chipSpawn1[i]);
                    ObjectPool.current.PoolObject(this.m_chipSpawn1[i]);
                }
            }
            this.m_chipSpawn1.Clear();
            this.m_chipDatCuoc1.Clear();
        }

        if (this.m_chipSpawn2 != null) {
            for (int i = 0; i < this.m_chipSpawn2.Count; i++) {
                if (this.m_chipSpawn2[i] != null) {
                    //Destroy (this.m_chipSpawn2[i]);
                    ObjectPool.current.PoolObject(this.m_chipSpawn2[i]);
                }
            }
            this.m_chipSpawn2.Clear();
            this.m_chipDatCuoc2.Clear();
        }

        if (this.m_chipSpawn3 != null) {
            for (int i = 0; i < this.m_chipSpawn3.Count; i++) {
                if (this.m_chipSpawn3[i] != null) {
                    //Destroy (this.m_chipSpawn3[i]);
                    ObjectPool.current.PoolObject(this.m_chipSpawn3[i]);
                }
            }
            this.m_chipSpawn3.Clear();
            this.m_chipDatCuoc3.Clear();
        }

        if (this.m_chipSpawn4 != null) {
            for (int i = 0; i < this.m_chipSpawn4.Count; i++) {
                if (this.m_chipSpawn4[i] != null) {
                    //Destroy (this.m_chipSpawn4[i]);
                    ObjectPool.current.PoolObject(this.m_chipSpawn4[i]);
                }
            }
            this.m_chipSpawn4.Clear();
            this.m_chipDatCuoc4.Clear();
        }

        if (this.m_chipSpawn5 != null) {
            for (int i = 0; i < this.m_chipSpawn5.Count; i++) {
                if (this.m_chipSpawn5[i] != null) {
                    //Destroy (this.m_chipSpawn5[i]);
                    ObjectPool.current.PoolObject(this.m_chipSpawn5[i]);
                }
            }
            this.m_chipSpawn5.Clear();
            this.m_chipDatCuoc5.Clear();
        }
    }
}
