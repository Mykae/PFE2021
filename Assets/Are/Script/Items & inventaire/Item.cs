using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item", order = 0)]
public class Item : ScriptableObject
{
    public bool stackable;
    public Sprite sprite;

    public virtual void Use()
    {
        Debug.Log("Use : " + name);
    }

}
