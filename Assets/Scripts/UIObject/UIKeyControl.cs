using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIKeyControl : MonoBehaviour {
    public Button btn;
    public InputField[] inputs;
    int index_ip = 0;

    //OnEnable is called when this class active
    void OnEnable() {
        if (inputs != null) {
            if (inputs.Length > 0) {
                index_ip = 0;
                inputs[index_ip].OnPointerClick(new PointerEventData(EventSystem.current));
            }
        }
    }
    // Update is called once per frame
    void Update() {
        if (gameObject.activeInHierarchy) {
            if (Input.GetKeyDown(KeyCode.Tab) && inputs != null) {
                index_ip++;
                if (index_ip > inputs.Length - 1)
                    index_ip = 0;
                inputs[index_ip].OnPointerClick(new PointerEventData(EventSystem.current));
            }

            if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return)) {
                ExecuteEvents.Execute(btn.gameObject, new PointerEventData(EventSystem.current), ExecuteEvents.pointerClickHandler);
            }
        }
    }
}
