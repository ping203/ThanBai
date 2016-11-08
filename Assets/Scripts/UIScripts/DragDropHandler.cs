using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class DragDropHandler : MonoBehaviour,
                                IBeginDragHandler,
                                IDragHandler,
                                IEndDragHandler,
                                IDropHandler {
    public static GameObject itemBeingDragged;
    public static Vector3 startPosition;
    public bool isDrop = false;

    public Card card;
    #region IBeginDragHandler implement
    public void OnBeginDrag(PointerEventData eventData) {
        if (MauBinh.instance.isTouchMB) {
            itemBeingDragged = gameObject;
            startPosition = transform.localPosition;
            GetComponent<CanvasGroup>().blocksRaycasts = false;
            isDrop = false;
            int id = card.getId();
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasBeginDrag(id));
        }
    }
    #endregion    

    #region IDragHandler implement
    public void OnDrag(PointerEventData eventData) {
        if (MauBinh.instance.isTouchMB) {
            transform.position = Input.mousePosition;
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasDrag());
        }
    }
    #endregion

    #region IEndDragHandler implement
    public void OnEndDrag(PointerEventData eventData) {
        if (MauBinh.instance.isTouchMB) {
            itemBeingDragged = null;
            if (!isDrop) {
                transform.localPosition = startPosition;
            }
            isDrop = false;
            ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasEndDrag());
        }

        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
    #endregion

    public void OnDrop(PointerEventData eventData) {
        //if (GameControl.instance.isTouchMB) {
        //itemBeingDragged.transform.localPosition = transform.localPosition;
        //transform.localPosition = startPosition;
        if (itemBeingDragged != null) {
            itemBeingDragged.transform.localPosition = startPosition;

            int id = itemBeingDragged.GetComponent<Card>().getId();

            itemBeingDragged.GetComponent<Card>().setId(card.getId());
            itemBeingDragged = null;
            card.setId(id);
        } else {
            transform.localPosition = startPosition;
        }
        isDrop = true;
        ExecuteEvents.ExecuteHierarchy<IHasChanged>(gameObject, null, (x, y) => x.HasDrop());
        GetComponent<CanvasGroup>().blocksRaycasts = true;
    }
}
