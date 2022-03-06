using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotDisplay : MonoBehaviour
{
    public Image spriteImage;
    public Text quantityText;

    public InventorySlot relatedSlot;

    public ItemPicker itemPicker;

    private void Awake()
    {
        itemPicker = GameObject.FindObjectOfType<ItemPicker>();
        itemPicker.OnInventoryChange += UpdateDisplay; 
    }

    public void UpdateDisplay()
    {
       
        spriteImage.sprite = relatedSlot.item != null ? relatedSlot.item.sprite : null;
        if(relatedSlot.item != null)
            quantityText.text = relatedSlot.item.stackable ? relatedSlot.quantity + "/" + relatedSlot.maxQuantity : "";
    }
}
