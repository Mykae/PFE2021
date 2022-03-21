using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAchat : MonoBehaviour
{
    public DragDropSlot[] slots; //Check tous les slots d'achat
    public GameObject[] itemTypes; //Types d'objets disponibles
    public int[] itemTypesValue; //Valeur de chaque type d'objet
    public int[] itemTypesValueGoal; //Valeur demandée pour chaque type d'objet (pour gagner le mini-jeu)
    public GameObject[] achatItems; //Objets dans les slots d'achat
    public int[] achatItemsValue; //Valeur des objets dans les slots d'achat

    private void Awake()
    {
        achatItems = new GameObject[slots.Length];
        achatItemsValue = new int[slots.Length];
        itemTypesValue = new int[itemTypes.Length];
        itemTypesValueGoal = new int[itemTypes.Length];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < slots.Length; i++)                  //pour chaque item...
            {
                achatItems[i] = slots[i].getItemType();             //...get le type de l'item
                achatItemsValue[i] = slots[i].getItemValue();       //...get la valeur de l'item
                Debug.Log("achat item : " + achatItems[i]);
                Debug.Log("achat item value : " + achatItemsValue[i]);
                for (int y = 0; y < itemTypes.Length; y++)          //pour chaque type d'item...
                {
                    if (achatItems[i] != null && achatItems[i] == itemTypes[y])               //...ranger chaque item dans sa catégorie
                    {
                        itemTypesValue[y] += achatItemsValue[i];    //...compter le nombre d'items par catégorie
                    }
                }
            }

            for (int i = 0; i < itemTypes.Length; i++)
            {
                Debug.Log("Nous avons " + itemTypesValue[i] + " " + itemTypes[i] + ".");
                itemTypesValue[i] = 0;
            }
        }
    }
}
