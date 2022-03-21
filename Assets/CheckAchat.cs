using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAchat : MonoBehaviour
{
    public DragDropSlot[] slots;
    public GameObject[] achatItems;
    private int totalItems = 0;

    private void Awake()
    {
        achatItems = new GameObject[slots.Length];
    }

    private void Update()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            achatItems[i] = slots[i].getItemIn();
            if(achatItems[i] != null)
            {
                totalItems++;
            }
        }
        if (totalItems == slots.Length)
        {
            Debug.Log("Tout est plein");
        }
        totalItems = 0;
    }
}
