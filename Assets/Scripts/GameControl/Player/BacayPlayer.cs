using UnityEngine;
using System.Collections;

public class BacayPlayer : LiengPlayer {
    //GameObject objWinEffect;
    public override void setMaster(bool isMaster) {
        isMasters = isMaster;
        master.gameObject.SetActive(isMaster);
    }

    public override void setRank(int rank) {
        sp_typeCard.StopAllCoroutines();
        switch (rank) {
            case 0:
                if (pos == 0) {
                    SoundManager.instance.startLostAudio();
                }
                break;
            case 1:
                LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Win_Effect", (obj) => {
                    objWinEffect = obj;
                    objWinEffect.transform.SetParent(gameObject.transform);
                    objWinEffect.transform.localPosition = Vector3.zero;
                    //Debug.LogError("Load Win Effect 55");
                });
                if (pos == 0) {
                    SoundManager.instance.startWinAudio();
                }
                break;
            case 2:
            case 3:
            case 4:
                if (pos == 0) {
                    SoundManager.instance.startLostAudio();
                }
                break;
            case 5:
                LoadAssetBundle.LoadPrefab(Res.AS_PREFABS, "Win_Effect", (obj) => {
                    objWinEffect = obj;
                    objWinEffect.transform.SetParent(gameObject.transform);
                    objWinEffect.transform.localPosition = Vector3.zero;
                    //Debug.LogError("Load Win Effect 55");
                });
                if (pos == 0) {
                    SoundManager.instance.startWinAudio();
                }
                break;
        }
        Invoke("setVisibleThang", 3f);
    }
    public new void setVisibleThang()
    {
        if (objWinEffect != null)
            Destroy(objWinEffect);
    }

}
