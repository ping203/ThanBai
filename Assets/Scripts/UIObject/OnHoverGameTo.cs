using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class OnHoverGameTo : MonoBehaviour {
    [SerializeField]
    GameObject obj_help;
    // Use this for initialization
    void Start() {
        EventTrigger.Entry eventtypeHover = new EventTrigger.Entry();
        eventtypeHover.eventID = EventTriggerType.PointerEnter;
        eventtypeHover.callback.AddListener((eventData) => { onHover(); });

        EventTrigger.Entry eventtypeExit = new EventTrigger.Entry();
        eventtypeExit.eventID = EventTriggerType.PointerExit;
        eventtypeExit.callback.AddListener((eventData) => { onExitHover(); });

        gameObject.AddComponent<EventTrigger>();
        gameObject.GetComponent<EventTrigger>().triggers.Add(eventtypeHover);
        gameObject.GetComponent<EventTrigger>().triggers.Add(eventtypeExit);

        Vector3 vt = obj_help.transform.position;
        vt.x = transform.position.x - 10;
        obj_help.transform.position = vt;
    }

    void onHover() {
        obj_help.SetActive(true);

    }

    void onExitHover() {
        obj_help.SetActive(false);
    }
}
