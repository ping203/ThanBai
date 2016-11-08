using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TimerXam : MonoBehaviour {
    public Xam xam;
    private int time;
    private int timeAll;
    private bool isActives;
    public Timer timer;
    public Button btn;

    // Update is called once per frame
    void Update() {
        if (isActives) {
            dura += Time.deltaTime;
            if (dura < timeAll) {
                float percent;
                if (timeAll == 0) {
                    percent = 1;
                }
                else {
                    percent = dura * 100 / timeAll;
                }
                timer.setPercentage(percent);
            }
            else {
                xam.hetGioBaoXam();
                setDeActive();
            }
        }
        else {
            dura = 0;
        }
    }

    public int getTime() {
        return time;
    }

    public void setTime(int time) {
        dura = 0;
        this.time = time;
    }

    public int getTimeAll() {
        return timeAll;
    }

    public void setTimeAll(int timeAll) {
        this.timeAll = timeAll;
    }

    private float dura = 0;

    public bool isActive() {
        return isActives;
    }

    public void setActive(int timeAll) {
        this.timeAll = timeAll;
        dura = 0;
        isActives = true;
    }

    public void setDeActive() {
        timer.setPercentage(0);
        isActives = false;
    }

    public void setEnableClick(bool isClick) {
        btn.enabled = isClick;
    }
}
