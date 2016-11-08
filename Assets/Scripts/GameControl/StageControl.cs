using UnityEngine;
using System.Collections;

public class StageControl : MonoBehaviour {
    public GameControl gameControl;
    public bool isActive;
    public virtual void Appear() {
        isActive = true;
        gameObject.SetActive(isActive);
    }
    public void DisAppear() {
        isActive = false;
        gameObject.SetActive(isActive);
    }
    public virtual void onBack() {

    }
}
