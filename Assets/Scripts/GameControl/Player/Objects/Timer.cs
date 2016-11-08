using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Timer : MonoBehaviour {
    public Image sprite, kim_dong_ho;
    // Use this for initialization
    void Start() {
        if (kim_dong_ho != null) {
            //kim_dong_ho.transform.Rotate(0, 0, 180);
            Vector3 curEuler = kim_dong_ho.transform.eulerAngles;
            kim_dong_ho.transform.eulerAngles = curEuler;
        }
        setPercentage(100);
    }

    // Update is called once per frame
    //void Update() {

    //}
    public void setPercentage(float percent) {
        float per = percent;
        percent = 1 - percent / 100;
        sprite.fillAmount = percent;
        sprite.color = new Color(1f - percent, percent, 0, 1f);

        if (kim_dong_ho != null) {
            float ro = 360 * percent;
            Vector3 curEuler = kim_dong_ho.transform.eulerAngles;
            curEuler.z = ro;
            kim_dong_ho.transform.eulerAngles = curEuler;
        }
    }
}
