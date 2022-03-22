using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropSlot : MonoBehaviour, IDropHandler
{
    private GameObject itemType;
    private int itemValue;

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("OnDropSlot");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            itemType = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<DragDrop>().type;
            itemValue = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<DragDrop>().value;
        }
    }

    public GameObject getItemIn()
    {
        if (itemType != null)
            return itemType;
        else
            return null;
    }

    public int getItemValue()
    {
        if (itemType != null)
            return itemValue;
        else
            return 0;
    }

}
