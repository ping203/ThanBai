using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PanelRank : PanelGame {
    //public GameObject itemRanking;
    public Transform parentItem;

    public List<GameObject> list_top = new List<GameObject>();

    public void InstanceItem(int rank, int idAvata, string playername, long money) {
        LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "ItemRank", (prefabAB) => {
            GameObject go = (GameObject)Instantiate(prefabAB);
            go.transform.SetParent(parentItem);
            go.transform.localScale = Vector3.one;
            go.GetComponent<ItemRanking>().SetData(rank, idAvata, playername, money);

            list_top.Add(go);
        });
    }

    public void clearList() {
        foreach (GameObject it in list_top) {
            Destroy(it.gameObject);
        }
        list_top.Clear();
    }
}
