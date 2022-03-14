using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehavior : MonoBehaviour
{


    [SerializeField]
    private Transform castlePrefab;

    public PlayerMovement movement;
    public Text actionText;
    public string selectionMonologue = "TEXTE_DE_DEPART";
    private bool showMonologue = true;

    //Différents types de trigger box
    bool playerTriggerBox = false;
    bool pecheTriggerBox = false;
    bool chateauTriggerBox = false;
    bool pnjTriggerBox = false;

    private string lastEncounteredPNJ;
    public GameObject messageBox;
    public Text dialogNameBox;

    private GameObject LastEncounteredPlayer;

    private void OnEnable()
    {
        movement = GetComponent<PlayerMovement>();
        dialogNameBox.text = name;
        messageBox.SetActive(true);
        messageBox.GetComponentInChildren<Text>().text = selectionMonologue;
        Invoke("SelectionMonologueTimer", 10);
    }

    //Ce qu'il faut faire pour changer de joueur
    private void OnDisable()
    {
        if (LastEncounteredPlayer != null)
        {
            movement.enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            transform.Find("Main Camera").SetParent(LastEncounteredPlayer.transform);
            LastEncounteredPlayer.transform.Find("Main Camera").transform.localPosition = new Vector3(0, 0, -10);
            LastEncounteredPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            LastEncounteredPlayer.GetComponent<PlayerBehavior>().enabled = true;
            LastEncounteredPlayer.GetComponent<PlayerMovement>().enabled = true;
        }
    }

    void Update()
    {
        if (showMonologue)
        {
            if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))){
                //movement.enabled = true;
                messageBox.SetActive(false);
                showMonologue = false;
            }
            return;
        }

        UpdateTextActionButton();

        
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && movement.enabled)
            ActionButton();
        //si le mouvement est désactivé et que la boite de dialogue est active c'est qu'on est en train de parler.
        else if(movement.enabled == false && messageBox.activeSelf && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)))
        {
            Parler(lastEncounteredPNJ);
        }

    }


    private void ActionButton()
    {
        if (pecheTriggerBox)
        {
            Debug.Log("J'ai pêché hihihi");
        }
        if (chateauTriggerBox)
        {
            PlacerChateauDeSable();
        }
        if (playerTriggerBox)
        {
            GetComponent<PlayerBehavior>().enabled = false;
            
        }
        if (pnjTriggerBox)
            Parler(lastEncounteredPNJ);
    }

    private void UpdateTextActionButton()
    {
        if (pecheTriggerBox)
        {
            actionText.text = "Pêcher";
        }
        else if (chateauTriggerBox)
        {
            actionText.text = "Construire";
        }
        else if (playerTriggerBox)
        {
            actionText.text = "Changer de perso";
        }
        else if (pnjTriggerBox)
        {
            actionText.text = "Parler";
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.name != name)
        {
            playerTriggerBox = true;
            LastEncounteredPlayer = collision.gameObject;
        }
        if (collision.tag == "Peche")
        {
            pecheTriggerBox = true;
        }
        if (collision.tag == "Chateau")
        {
            chateauTriggerBox = true;
        }
        if (collision.tag == "PNJ")
        {
            pnjTriggerBox = true;
            lastEncounteredPNJ = collision.name;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.name != name)
        {
            playerTriggerBox = false;
            actionText.text = "Rien pour le moment";
        }
        if (collision.tag == "Peche")
        {
            pecheTriggerBox = false;
            actionText.text = "Rien pour le moment";
        }
        if (collision.tag == "Chateau")
        {
            chateauTriggerBox = false;
            actionText.text = "Rien pour le moment";
        }
        if (collision.tag == "PNJ")
        {
            pnjTriggerBox = false;
            actionText.text = "Rien pour le moment";
        }

    }

    void Parler(string nomPNJ)
    {
        dialogNameBox.text = nomPNJ;
        var dialogues = GameObject.Find(nomPNJ).GetComponent<DialogueParDefaut>();
        if (name == "Miranda")
        {
            dialogues = GameObject.Find(nomPNJ).GetComponent<DialoguesAvecMiranda>();
        }
        else if (name == "Timmy")
        {
            dialogues = GameObject.Find(nomPNJ).GetComponent<DialoguesAvecTimmy>();
        }
        

        if(dialogues.index == -1)
        {
            movement.enabled = false;
            messageBox.SetActive(true);
        }
        if(dialogues.DialogueSuivant())
        {
            messageBox.GetComponentInChildren<Text>().text = dialogues.dialogues[dialogues.index];
        }
        else
        {
            messageBox.SetActive(false);
            movement.enabled = true;
            dialogues.index = -1;
        }
    }

    void PlacerChateauDeSable()
    {
        var _castlePlacement = transform.position + Vector3.down;

        var _castle = Instantiate(castlePrefab, _castlePlacement, Quaternion.identity);
    }

    public void  SelectionMonologueTimer()
    {
        if (showMonologue)
        {
            messageBox.SetActive(false);
            showMonologue = false;
        }
    }
}
