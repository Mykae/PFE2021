using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropSlot : MonoBehaviour, IDropHandler
{
    public GameObject itemIn;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDropSlot");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            itemIn = eventData.pointerDrag.GetComponent<RectTransform>().gameObject;
        }
    }

    public GameObject getItemIn()
    {
        if (itemIn != null)
            return itemIn;
        else
            return null;
    }

}
