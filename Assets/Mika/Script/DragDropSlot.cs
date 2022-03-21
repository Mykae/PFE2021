using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDropSlot : MonoBehaviour, IDropHandler, IBeginDragHandler
{
    public GameObject itemType;
    public int itemValue;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDropSlot");
        if (eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
            itemType = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<DragDrop>().type;
            Debug.Log("type : " + itemType);
            itemValue = eventData.pointerDrag.GetComponent<RectTransform>().gameObject.GetComponent<DragDrop>().value;
            Debug.Log("value : " + itemValue);
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Quit area");
        itemType = null;
        itemValue = 0;
    }

    public GameObject getItemType()
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
