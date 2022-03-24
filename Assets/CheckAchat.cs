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
    public int[] slotItemGoal;
    public int validate;
    public bool isWon;
    public bool reset;

    private void OnEnable()
    {
        achatItemTypes = new GameObject[slots.Length];
        achatItemValue = new int[slots.Length];
        slotItemValue = new int[slotItemTypes.Length];
        validate = 0;
        isWon = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < slots.Length; i++)
            {
                achatItemTypes[i] = slots[i].getItemIn();
                achatItemValue[i] = slots[i].getItemValue();
                //Debug.Log("type : " + achatItemTypes[i]);
                for (int y = 0; y < slotItemTypes.Length; y++)
                {
                    if (achatItemTypes[i] == slotItemTypes[y])
                    {
                        //Debug.Log("achat item value : " + achatItemValue[i]);
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
                //Debug.Log("Type : " + slotItemTypes[i] + " | Quantity : " + slotItemValue[i]);
                if (slotItemValue[i] == slotItemGoal[i])
                {
                    validate++;
                    //Debug.Log("validate :" + validate);
                }
                if(validate == slotItemTypes.Length)
                {
                    Debug.Log("C'est gagné");
                    isWon = true;
                }
                else
                {
                    //ResetGame();
                }
                slotItemValue[i] = 0;
            }
            validate = 0;
        }

        if(isWon == true)
        {

        }
    }

   /* public void ResetGame()
    {
        reset = true;
        this.gameObject.SetActive(false);
    }

    /////////////////RESET LE JEU SI RESULTAT FAUX

    public void OnDisable()
    {
        if(reset == true)
        {
            this.gameObject.SetActive(true);
            reset = false;
        }
    }*/
}
