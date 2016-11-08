using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimeCountDown : MonoBehaviour {
    float time = 0;
    public Text text_time;

    // Update is called once per frame
    void Update() {
        if (time > 0) {
            time -= Time.deltaTime;
            int min = ((int)time) / 60;
            int sec = ((int)time % 60);
            if (min <= 0 && sec <= 0) {
                text_time.text = "00:00";
            } else {
                text_time.text = (min >= 10 ? (min + "") : ("0" + min)) + ":" + (sec >= 10 ? (sec + "") : ("0" + sec));
            }
        }
    }

    public void setTime(float time) {
        this.time = time;
        if (time > 0) {
            gameObject.SetActive(true);
        } else {
            gameObject.SetActive(false);
        }
    }
}
