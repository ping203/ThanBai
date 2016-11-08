//using UnityEngine;
//using UnityEngine.UI;
//using System.Collections;

//public class ToggleShowPopup : MonoBehaviour {
//    public Toggle tg;

//    public NPCController OBJ;
//    public bool isStart;

//    ABSUser user;
//    // Use this for initialization
//    void Awake() {
//        // tg = GetComponent<Toggle>();
//        //if (tg != null) {
//        //    tg.onValueChanged.AddListener(change);
//        //}
//        if (OBJ != null) {
//            OBJ.gameObject.SetActive(isStart);
//        }
//        user = GetComponent<ABSUser>();
//    }
//    void Update() {
//        //if (Input.GetButtonDown("Fire1") && rect1 != null) {
//        //    if (RectTransformUtility.RectangleContainsScreenPoint(rect1, Input.mousePosition)
//        //        || (rect_player != null && RectTransformUtility.RectangleContainsScreenPoint(rect_player, Input.mousePosition))) {
//        //        return;
//        //    } else {
//        //        tg.isOn = false;
//        //    }
//        //}
//    }

//    //void change(bool isCheck) {
//    //    Vector3 vt = transform.localPosition;
//    //    switch (_align) {
//    //        case Align.None:
//    //            break;
//    //        case Align.Left:
//    //            vt.x = transform.localPosition.x - rect1.rect.width / 2 - 10;
//    //            break;
//    //        case Align.Right:
//    //            vt.x = transform.localPosition.x + rect1.rect.width / 2 + 10;
//    //            break;
//    //        case Align.Top:
//    //            vt.y = transform.localPosition.y + rect1.rect.height / 2 + 10;
//    //            break;
//    //        case Align.Bot:
//    //            vt.y = transform.localPosition.y - rect1.rect.height / 2 - 10;
//    //            break;
//    //    }
//    //    if (OBJ != null) {
//    //        OBJ.transform.localPosition = vt;
//    //        OBJ.gameObject.SetActive(isCheck);
//    //        if (user != null)
//    //            OBJ.name_player = user.getName();
//    //        OBJ.tg = this.tg;
//    //    }
//    //}
//}
