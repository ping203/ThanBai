using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MyScrollView : LoopScrollRect 
{
	public GameObject prfObject;
	public UnityAction<GameObject> RecycleItem;
	public UnityAction<GameObject, int> UpdateInfo;

    protected override void Awake()
    {
        base.Awake();
    }

    public override GameObject GetItemToAdd ()
	{
        GameObject obj = Instantiate(prfObject, Vector3.zero, Quaternion.identity) as GameObject;
        obj.transform.localScale = Vector3.zero;
        return obj;

    }

	public override void RemoveItem (GameObject go)
	{
		if (RecycleItem != null) RecycleItem (go);
		else Destroy(go);
	}

	public override void UpdateItemInfo (GameObject item, int index)
	{
		if (UpdateInfo != null)
			UpdateInfo (item, index);
	}

	public void OnStartFillItem(GameObject obj,int length){
        prfObject = obj;
        totalCount = length;
        //Debug.LogError("totalCount " + totalCount);
        RefillCells ();
        //Debug.LogError("RefillCells ");
    }
}
