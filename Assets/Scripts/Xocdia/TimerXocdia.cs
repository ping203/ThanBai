using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimerXocdia : Timer {
    private string m_timeString;
    private bool m_isShow = false;

    private float m_timeAutoStart;
    private float m_timeBeginXocdia;
    private float m_timeBeginDatcuoc;
    private float m_timeBeginDungcuoc;

    public Text m_timeLabel;
    public ThongbaoXocdia m_thongbaoXocdia;
    public DiaComponent m_diaComponent;
    public WinXocdia m_winXocdia;

    private bool m_isInUpdate = false;

    void Start () {
        this.m_isInUpdate = true;
    }

    void Update () {
        if(this.m_timeAutoStart > 0) {
            this.m_isShow = true;
            this.m_timeString = this.m_timeAutoStart.ToString ("0");
            this.m_timeAutoStart -= Time.deltaTime;

            //Disable win animation.
            if(this.m_timeString.Equals ("2")) {
                if(this.m_winXocdia != null) {
                    this.m_winXocdia.RemoveWinXocdia ();
                }
            }

            if(this.m_timeString.Equals ("1")) {
                this.m_thongbaoXocdia.SetAnimationThongbao_Len ();
            }
        } else {
            this.m_timeAutoStart = 0;
            this.m_timeString = this.m_timeAutoStart.ToString ("0");
            hideTimeWaiting ();
        }

        //Dem nguoc time bat dau xoc dia.
        if(this.m_timeAutoStart <= 0) {
            if(this.m_timeBeginXocdia > 0) {
                this.m_timeBeginXocdia -= Time.deltaTime;

                //if(this.m_timeBeginXocdia.ToString("0").Equals("1") && this.m_isInUpdate == true) {
                //    this.m_thongbaoXocdia.SetAnimationThongbao_Len ();
                //    if(this.m_diaComponent != null) {
                //        this.m_diaComponent.SetAnimationXocdiaIdle ();
                //    }
                //    this.m_isInUpdate = false;
                //}

                if(this.m_timeBeginXocdia != 0 && this.m_timeBeginXocdia < 1.0f) {
                    this.m_thongbaoXocdia.SetAnimationThongbao_Len ();
                    if(this.m_diaComponent != null) {
                        this.m_diaComponent.SetAnimationXocdiaIdle ();
                    }
                }
            } else {
                this.m_timeBeginXocdia = 0;
            }
        }

        //Dem nguoc time dat cuoc.
        if(this.m_timeBeginXocdia <= 0 && this.m_timeAutoStart <= 0) {
            if(this.m_timeBeginDatcuoc > 0) {
                this.m_isShow = true;
                this.m_timeString = this.m_timeBeginDatcuoc.ToString ("0");
                this.m_timeBeginDatcuoc -= Time.deltaTime;
                if(this.m_timeString.Equals ("1")) {
                    this.m_thongbaoXocdia.SetAnimationThongbao_Len ();
                }
            } else {
                this.m_timeBeginDatcuoc = 0;
                this.m_timeString = this.m_timeBeginDatcuoc.ToString ("0");
                hideTimeWaiting ();
            }
        }

        //Dem nguoc time dung cuoc
        if(this.m_timeBeginDatcuoc <= 0 && this.m_timeAutoStart <= 0) {
            if(this.m_timeBeginDungcuoc > 0) {
                this.m_isShow = true;
                this.m_timeString = this.m_timeBeginDungcuoc.ToString ("0");
                this.m_timeBeginDungcuoc -= Time.deltaTime;
                if(this.m_timeString.Equals ("1")) {
                    this.m_thongbaoXocdia.SetAnimationThongbao_Len ();
                }
            } else {
                this.m_timeBeginDungcuoc = 0;
                this.m_timeString = this.m_timeBeginDungcuoc.ToString ("0");
                hideTimeWaiting ();
            }
        }
    }

    void OnGUI () {
        setTimeLabel (this.m_timeString);
    }

    public void setTimeAutoStart (int time) {
        this.m_timeAutoStart = time;
        this.m_isInUpdate = true;
    }

    public void setTimeBeginXocdia (int time) {
        this.m_timeBeginXocdia = time;
        this.m_isInUpdate = true;
    }

    public void setTimeBeginDatcuoc (int time) {
        this.m_timeBeginDatcuoc = time;
    }

    public void setTimeBeginDungcuoc (int time) {
        this.m_timeBeginDungcuoc = time;
    }

    public void hideTimeWaiting () {
        if(this.m_isShow) {
            this.m_isShow = false;
        }
    }

    private void setTimeLabel (string aTimeString) {
        if(this.m_isShow && this.m_timeString != null) {
            if(System.Convert.ToInt32(aTimeString) < 10)
                this.m_timeLabel.text = "0" + aTimeString;
            else
                this.m_timeLabel.text = aTimeString;
        }
        else {
            this.m_timeLabel.text = "";
        }
    }

    public void ResetAllTimer () {
        this.m_timeAutoStart = 0.0f;
        this.m_timeBeginXocdia = 0.0f;
        this.m_timeBeginDatcuoc = 0.0f;
        this.m_timeBeginDungcuoc = 0.0f;
        hideTimeWaiting ();
    }
}
