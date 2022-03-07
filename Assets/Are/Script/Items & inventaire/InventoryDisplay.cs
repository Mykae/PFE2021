using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryDisplay : MonoBehaviour
{
    public Inventory inventory;
    public SlotDisplay SlotDisplayPrefab;


    private void Start()
    {
        foreach (InventorySlot slot in inventory.slots)
        {
            SlotDisplay slotDisplay = Instantiate(SlotDisplayPrefab, transform);
            slotDisplay.relatedSlot = slot;
            slotDisplay.UpdateDisplay();
        }
    }
}
