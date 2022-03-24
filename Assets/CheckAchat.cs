using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckAchat : MonoBehaviour
{
    public DragDropSlot[] slots;
    public GameObject[] achatItemTypes;
    public GameObject[] slotItemTypes;
    public int[] achatItemValue;
    public int[] slotItemValue;
    public int[] slotItemGoal;
    public int validate;
    public bool checkButton = false;
    public bool isWon;
    public bool reset;

    public Canvas marketGame;
    public GameObject player;
    public GameObject zoneDinteractionAFermerSiAchatReussi;
    public GameObject[] itemsToInstantiateOnWin;

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
        if (Input.GetKeyDown(KeyCode.Space) || checkButton == true)
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
                    //Debug.Log("C'est gagné");
                    isWon = true;
                    gameIsWon();
                }
                else
                {
                    //ResetGame();
                }
                slotItemValue[i] = 0;
            }
            validate = 0;
            checkButton = false;
        }
    }

    public void gameIsWon()
    {
        if(isWon == true)
        {
            isWon = false;
            zoneDinteractionAFermerSiAchatReussi.GetComponent<OpenGame>().canGameBeReplayed = false;
            player.GetComponent<PlayerBehavior>().restartMonologue(new string[2] 
            { "Paaarfait, j’ai tout récupéré. Il est maintenant temps de rentrer chez moi pour préparer ce délicieux gâteau.", 
                "Mais avant ça, il faudrait que je trouve quelqu’un qui puisse inviter tout le monde à la fête..." });
            GameObject.Find("Timmy").GetComponent<DialogueParDefaut>().dialogues = new string[1] 
            { "J’aimerais beaucoup aller construire des châteaux de sable sur la plage, mais je peux sans soucis aller " +
            "inviter tout le monde pour ce soir !" };
            player.GetComponent<PlayerMovement>().enabled = true;
            var i = player.GetComponent<PlaySound>();
            i.Play(0);
            foreach (GameObject itemsAInstantier in itemsToInstantiateOnWin)
            {
                //Debug.Log(itemsAInstantier);
                Instantiate(itemsAInstantier, player.transform.position, player.transform.rotation);
            }
            marketGame.gameObject.SetActive(false);
        }
        
    }

    public void setButton()
    {
        checkButton = true;
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
