using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;

public class ThongbaoXocdia : MonoBehaviour {
    public Text m_lbThongbao;
    //public Animator m_animator;
    Vector3 vtPosHide;// = new Vector3(0, 315, 0);
    //Vector3 vtPosShow = new Vector3(0, 250, 0);
    // Use this for initialization
    void Start() {
       vtPosHide = transform.localPosition;
    }

    public void SetLbThongbao(string aThongbao) {
        if (this.m_lbThongbao != null) {
            this.m_lbThongbao.text = aThongbao;
        }
    }

    public void SetAnimationThongbao_Idle() {
        //if(this.m_animator != null) {
        //    this.m_animator.SetBool ("thongbaolen", false);
        //    this.m_animator.SetBool ("thongbaoxuong", false);
        //}
        SetAnimationThongbao_Len();
    }

    public void SetAnimationThongbao_Len() {
        //if(this.m_animator != null) {
        //    this.m_animator.SetBool ("thongbaolen", true);
        //    this.m_animator.SetBool ("thongbaoxuong", false);
        //}
        transform.DOLocalMoveY(vtPosHide.y, 0.2f).OnComplete(delegate {
            EndFadeIn();
        });
    }

    public void SetAnimationThongbao_Xuong() {
        //if(this.m_animator != null) {
        //    this.m_animator.SetBool ("thongbaolen", false);
        //    this.m_animator.SetBool ("thongbaoxuong", true);
        //}
        gameObject.SetActive(true);
        transform.DOLocalMoveY(vtPosHide.y - 50, 0.2f);
    }

    public void ShowThongbao1() {
        gameObject.SetActive(true);
        StartCoroutine(HideThongbaoTimer(2.0f));
    }

    public void HideThongbao1(float timeWait) {
        StartCoroutine(HideThongbaoTimer(timeWait));
    }

    IEnumerator HideThongbaoTimer(float timeWait) {
        yield return new WaitForEndOfFrame();
        yield return new WaitForSeconds(timeWait);
        //if(this.m_animator != null) {
        //    this.m_animator.SetBool ("fadeIn", true);
        //}
        //transform
        EndFadeIn();
    }

    public void EndFadeIn() {
        gameObject.SetActive(false);
        //if(this.m_animator != null) {
        //    this.m_animator.SetBool ("fadeIn", false);
        //}
    }
}
