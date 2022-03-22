using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAchat : MonoBehaviour
{
    public DragDropSlot[] slots;
    public GameObject[] achatItemTypes;
    public GameObject[] slotItemTypes;
    public int[] achatItemValue;
    public int[] slotItemValue;

    private void Awake()
    {
        achatItemTypes = new GameObject[slots.Length];
        achatItemValue = new int[slots.Length];
        slotItemValue = new int[slotItemTypes.Length];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < slots.Length; i++)
            {
                achatItemTypes[i] = slots[i].getItemIn();
                achatItemValue[i] = slots[i].getItemValue();
                Debug.Log("type : " + achatItemTypes[i]);
                for (int y = 0; y < slotItemTypes.Length; y++)
                {
                    if (achatItemTypes[i] == slotItemTypes[y])
                    {
                        Debug.Log("achat item value : " + achatItemValue[i]);
                        slotItemValue[y] += achatItemValue[i];
                    }
                }
                
            }

            for (int i = 0; i < slots.Length; i++)
            {
                achatItemTypes[i] = null;
                achatItemValue[i] = 0;
            }

                for (int i = 0; i < slotItemTypes.Length; i++)
            {
                Debug.Log("Type : " + slotItemTypes[i] + " | Quantity : " + slotItemValue[i]);
                slotItemValue[i] = 0;
            }

            
        }
       
        




        /*for (int i = 0; i < slots.Length; i++)
        {
            achatItemTypes[i] = slots[i].getItemIn();
            if(achatItemTypes[i] != null)
            {
                totalItems++;
            }
        }
        if (totalItems == slots.Length)
        {
            Debug.Log("Tout est plein");
        }
        totalItems = 0;*/
    }
}
