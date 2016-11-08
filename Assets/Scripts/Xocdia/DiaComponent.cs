using UnityEngine;
using System.Collections;
using DG.Tweening;

public class DiaComponent : MonoBehaviour {
    private bool m_mobat = false;
    public Transform bat;
    Vector3 vtBat;
    Vector3 vtDia;
    // Use this for initialization
    void Start() {
        vtBat = new Vector3(0, 16.43f, 0);
        vtDia = transform.localPosition;
    }

    public void SetAnimationXocdia() {
        //if(this.m_animator != null) {
        //    this.m_animator.SetBool ("isXocdia", true);
        //    this.m_animator.SetBool ("mobat", false);
        //    this.m_animator.SetBool ("upbat", false);
        //}
        Tween tw1 = transform.DOLocalMoveX(vtDia.x - 10, 0.01f);
        Tween tw2 = transform.DOLocalMoveX(vtDia.x, 0.01f);
        Tween tw3 = transform.DOLocalMoveX(vtDia.x + 10, 0.01f);
        Tween tw4 = transform.DOLocalMoveX(vtDia.x, 0.01f);
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(tw1);
        mySequence.Append(tw2);
        mySequence.Append(tw3);
        mySequence.Append(tw4);
        mySequence.SetLoops(50);
        mySequence.Play();
    }

    public void SetAnimationXocdiaIdle() {
        //if(this.m_animator != null) {
        //    this.m_animator.SetBool ("isXocdia", false);
        //    this.m_animator.SetBool ("mobat", false);
        //    this.m_animator.SetBool ("upbat", false);
        //}
        bat.localPosition = vtBat;
        transform.localPosition = vtDia;
        bat.DOKill();
    }

    public void SetAnimationMobat() {
        //if(this.m_animator != null) {
        //    this.m_animator.SetBool ("mobat", true);
        //    this.m_animator.SetBool ("upbat", false);
        //    this.m_animator.SetBool ("isXocdia", false);
        //}
        bat.DOLocalMoveY(450, 1);
        m_mobat = true;
    }

    public void SetAnimationUpbat() {
        if (!m_mobat) {
            return;
        }

        //if(this.m_animator != null) {
        //    this.m_animator.SetBool ("upbat", true);
        //    this.m_animator.SetBool ("isXocdia", false);
        //    this.m_animator.SetBool ("mobat", false);
        //    this.m_mobat = false;
        //}

        bat.DOLocalMove(vtBat, 1);
        m_mobat = false;
    }
}
