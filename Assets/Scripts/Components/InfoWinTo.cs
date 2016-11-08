using UnityEngine;
using System.Collections;

public class InfoWinTo {

    public string name; // ten người chơi
    public long money; // tiền thắng
    public sbyte type; // loại (hồi tiền hay thắng tiền) // 0: hồi, 1: thắng
    public sbyte typeCard; // loại bài
    public sbyte[] arrCard; // bài thắng. (gửi 5 con lớn nhất);
    public int rank;
}
