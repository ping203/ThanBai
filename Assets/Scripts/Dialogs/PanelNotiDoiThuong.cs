using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelNotiDoiThuong : PanelGame {

    public static PanelNotiDoiThuong instance;
	/*public Text[] textContents;
	public Text[] textTitles;
	public UIPanel[] panels;*/

    //public GameObject tgTemp;
    //public GameObject lbTemp;

    //public GameObject tgParent;
    //public GameObject lbParent;

    void Awake() {
        instance = this;
    }

    public void setActiveTab(string title, string content)
    {
            //GameObject pn = Instantiate(tgTemp) as GameObject;
            //GameObject lb = Instantiate(lbTemp) as GameObject;

            //pn.transform.parent = tgParent.transform;
            //lb.transform.parent = lbParent.transform;

            //pn.transform.localPosition = new Vector3(0, 0, 0);
            //float s = lb.GetComponent<Text>().height;
            //lb.transform.localPosition = new Vector3(0, 160 - s, 0);


            //pn.transform.localScale = new Vector3(1, 1, 1);
            //lb.transform.localScale = new Vector3(1, 1, 1);

            //pn.GetComponent<ToggledObjects>().activate.Add(lb);

            //pn.transform.FindChild("LabelTitle").GetComponent<Text>().text = title;
            //lb.GetComponent<Text>().text = content;
	}
}
