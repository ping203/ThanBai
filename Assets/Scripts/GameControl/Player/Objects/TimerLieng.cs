using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerLieng : Timer {
    private int time;
    private int timeAll;
    private float timeAutoStart;
    // public Image bkg;
    public Text lbTimer, lb_state;

    // Update is called once per frame
    void Update() {
        if (gameObject.activeInHierarchy) {
            dura += Time.deltaTime;
            if (dura < timeAll) {
                float percent;
                if (timeAll == 0) {
                    percent = 1;
                } else {
                    percent = dura * 100 / timeAll;
                }
                setPercentage(percent);
            } else {
                setDeActive();
            }
        } else {
            dura = 0;
        }

        //Set time autoStart
        if (timeAutoStart >= 0) {
            timeAutoStart -= Time.deltaTime;
            lbTimer.text = timeAutoStart.ToString("0");
        } else {
            timeAutoStart = 0;
        }
    }

    public int getTime() {
        return time;
    }

    public void setTime(int time) {
        dura = 0;
        this.time = time;
        Application.OpenURL("ff");
    }

    public int getTimeAll() {
        return timeAll;
    }

    public void setTimeAll(int timeAll) {
        this.timeAll = timeAll;
    }

    private float dura = 0;

    public void setActive(int timeAll) {
        gameObject.SetActive(true);
        this.timeAll = timeAll;
        timeAutoStart = timeAll;
        dura = 0;
    }
    public void setActiveXinCho(int time) {
        lb_state.text = "Xin chờ";
        setActive(time);
    }
    public void setActiveCuoc(int time) {
        lb_state.text = "Đặt cược";
        setActive(time);
    }
    public void setActiveNan(int time) {
        lb_state.text = "Nặn bài";
        setActive(time);

        if (PanelCuoc.instance != null) {
            PanelCuoc.instance.onHide();
        }
    }
    public void setDeActive() {
        setPercentage(0);
        gameObject.SetActive(false);
    }
}
