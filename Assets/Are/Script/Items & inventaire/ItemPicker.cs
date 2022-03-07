using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ItemPicker : MonoBehaviour
{
    public Inventory inventory;

    public event Action OnInventoryChange;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collectible")
        {
            var item = other.GetComponent<PickableItem>().item;
            var itemAjoute = false;

            if (item.stackable == true) 
            {
                var i = inventory.isItemInInventory(item);
                if (i >= 0)
                {
                    inventory.slots[i].quantity++;
                    Destroy(other.gameObject);
                    itemAjoute = true;
                    OnInventoryChange?.Invoke();
                }
            }

            if (itemAjoute == false) {
                
                var i = inventory.FirstAvailableSlot();
                if (i >= 0)
                {
                    inventory.slots[i].item = item;
                    inventory.slots[i].quantity++;
                    Destroy(other.gameObject);
                    OnInventoryChange?.Invoke();
                }
            }
            
        }
    }
}
