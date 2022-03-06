using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class InventorySlot
{
    public Item item;
    public int quantity;
    public int maxQuantity = 25;


    public bool IsSlotAvailable()
    {
        return (item == null);
    }

    public bool SlotFull()
    {
        return (quantity >= maxQuantity && item != null);
    }
}

public class Inventory : MonoBehaviour
{
    public InventorySlot[] slots;

    

    public int FirstAvailableSlot()
    {
        var returnValue = 0;
        var emptySlotFound = false;

        while (emptySlotFound == false && returnValue < slots.Length)
        {
            if (slots[returnValue].IsSlotAvailable())
                emptySlotFound = true;
            else 
                returnValue++;
        }

        
        if (returnValue >= slots.Length)
            returnValue = -1;
        return returnValue;
    }

    public int isItemInInventory(Item item)
    {
        var returnValue = 0;
        var itemFound = false;

        while (!itemFound && returnValue < slots.Length)
        {
            if (slots[returnValue].IsSlotAvailable())
                returnValue++;
            else
            {
                if (slots[returnValue].item == item && slots[returnValue].SlotFull() == false)
                {
                    itemFound = true;
                }                    
                else
                    returnValue++;
            }
                
        }

        if (returnValue >= slots.Length)
            returnValue = -1;
        return returnValue;
    }




}
