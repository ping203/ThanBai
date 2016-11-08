using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using DG.Tweening;

public class OnHoverObject : MonoBehaviour {

    // Use this for initialization
    void Start() {
        //img = GetComponent<Image>();
        //vtDefault = transform.localPosition;
        //if (toast != null) {
            EventTrigger.Entry eventtypeHover = new EventTrigger.Entry();
            eventtypeHover.eventID = EventTriggerType.PointerEnter;
            eventtypeHover.callback.AddListener((eventData) => { onHover(); });

            EventTrigger.Entry eventtypeExit = new EventTrigger.Entry();
            eventtypeExit.eventID = EventTriggerType.PointerExit;
            eventtypeExit.callback.AddListener((eventData) => { onExitHover(); });

            gameObject.AddComponent<EventTrigger>();
            gameObject.GetComponent<EventTrigger>().triggers.Add(eventtypeHover);
            gameObject.GetComponent<EventTrigger>().triggers.Add(eventtypeExit);

            //onExitHover();
        //}
    }
    void onHover() {
        transform.DOScale(1.05f, 0.1f);
        //toast.transform.localPosition = vtDefault;
        //Vector3 vt = toast.transform.localPosition;
        //switch (_align) {
        //    case Align.Left:
        //        vt.x = transform.localPosition.x - toast.width / 2;
        //        break;
        //    case Align.Center:
        //        vt.x = transform.localPosition.x;
        //        break;
        //    case Align.Right:
        //        vt.x = transform.localPosition.x + toast.width / 2;
        //        break;
        //}
        ////vt.y = img.rectTransform.rect.height - toast.height;
        //vt.y = transform.localPosition.y - toast.height;
        //toast.transform.localPosition = vt;

        //if (!str.Equals(""))
        //    toast.setInfo(str);

        //toast.gameObject.SetActive(true);
    }

    void onExitHover() {
        transform.DOScale(1f, 0.1f);
    }
}
