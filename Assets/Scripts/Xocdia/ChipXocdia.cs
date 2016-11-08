using UnityEngine;
using System.Collections;
public delegate void ActionFinishDelegate (int idSender, int index, GameObject self);
public delegate void SequenceMoveFinishDelegate(int cua);
public delegate void ChipMoveDelegate(string playerName);
public delegate void ChipMoveFinishDelegate(string playerName);

public class ChipXocdia : MonoBehaviour {
    public static event ChipMoveDelegate onChipMove;
    public static event ChipMoveFinishDelegate onChipMoveFinish;

    private string m_playerName;

    private Vector3 m_targetPos;
    private float m_speed = 300.0f;
    private bool m_isMove = false;
    private ActionFinishDelegate m_actionFinish;
    private SequenceMoveFinishDelegate m_sequenceMoveFinish;

    private int m_idSender;
    private int m_index;
    private int m_cua;
    private Vector3 velocity = Vector3.zero;
    private float closeDistance = 1.0F;

    void OnEnable () {
        
    }

    //void OnDisable () {
    //    gameObject.SetActive (false);
    //}

    private void Update () {
        if(this.m_isMove) {
            float step = m_speed * Time.deltaTime;
            transform.localPosition = Vector3.SmoothDamp (transform.localPosition, this.m_targetPos,ref velocity, 0.3f, 600, Time.deltaTime);
            if(onChipMove != null) {
                onChipMove (this.m_playerName);
            }
            if(Vector3.Distance (transform.localPosition, this.m_targetPos) <= closeDistance) {
                this.m_isMove = false;
                if(this.m_actionFinish != null) {
                    this.m_actionFinish (this.m_idSender, this.m_index, gameObject);
                    this.m_actionFinish = null;
                }
                if(this.m_sequenceMoveFinish != null) {
                    this.m_sequenceMoveFinish (m_cua);
                    this.m_sequenceMoveFinish = null;
                }
                if(onChipMoveFinish != null) {
                    onChipMoveFinish (this.m_playerName);
                }
            }
        }
    }

    public void MoveChip (bool aMove, Vector3 aTargetPos, float aSpeed, string aPlayerName,
                         ActionFinishDelegate onActionComplete = null, int aIdSender = -1, int aIndex = -1) {
        //Debug.LogError("Target Pos " + aTargetPos);
        this.m_isMove = aMove;
        this.m_targetPos = aTargetPos;
        //this.m_speed = aSpeed;
        this.m_speed = 700.0f;
        this.m_actionFinish = onActionComplete;
        this.m_idSender = aIdSender;
        this.m_index = aIndex;
        this.m_playerName = aPlayerName;
    }

    public void SequenceMove (bool aMove, Vector3 aTargetPos, float aSpeed, string aPlayerName,
                              SequenceMoveFinishDelegate onSequenceMoveComplete = null, int cua = -1) {
        this.m_isMove = aMove;
        this.m_targetPos = aTargetPos;
        //this.m_speed = aSpeed;
        this.m_speed = 700.0f;
        this.m_sequenceMoveFinish = onSequenceMoveComplete;
        this.m_cua = cua;
        this.m_playerName = aPlayerName;
    }
}
