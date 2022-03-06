using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PickableItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer rend;

    private void OnValidate()
    {
        UpdateDisplay();
    }

    private void Start()
    {
        UpdateDisplay();
    }

    private void UpdateDisplay()
    {
        if (item != null)
        {
            rend.sprite = item.sprite;
        }
    }
}
