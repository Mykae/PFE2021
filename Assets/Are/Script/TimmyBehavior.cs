using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimmyBehavior : MonoBehaviour
{


    [SerializeField]
    Transform castlePrefab;

    public PlayerMovement movement;

    //Différents types de trigger box
    bool playerTriggerBox = false;
    bool pecheTriggerBox = false;
    bool chateauTriggerBox = false;
    bool pnjTriggerBox = false;

    string dernierPnjRecontré;
    public GameObject boiteDeDialogue;

    public Text actionText;


    private GameObject LastEncounteredPlayer;

    private void OnEnable()
    {
        movement = GetComponent<PlayerMovement>();
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
            LastEncounteredPlayer.GetComponent<TimmyBehavior>().enabled = true;
            LastEncounteredPlayer.GetComponent<PlayerMovement>().enabled = true;
        }
    }

    void Update()
    {
        UpdateTextActionButton();

        
        if (Input.GetKeyDown(KeyCode.Space) && movement.enabled)
            ActionButton();
        //si le mouvement est désactivé et que la boite de dialogue est active c'est qu'on est en train de parler.
        else if(movement.enabled == false && boiteDeDialogue.activeSelf && Input.GetKeyDown(KeyCode.Space))
        {
            Parler(dernierPnjRecontré);
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
            GetComponent<TimmyBehavior>().enabled = false;
            
        }
        if (pnjTriggerBox)
            Parler(dernierPnjRecontré);
    }

    private void UpdateTextActionButton()
    {
        if (pecheTriggerBox)
        {
            actionText.text = "Pêcher";
        }
        if (chateauTriggerBox)
        {
            actionText.text = "Construire";
        }
        if (playerTriggerBox)
        {
            actionText.text = "Changer de perso";
        }
        if (pnjTriggerBox)
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
            dernierPnjRecontré = collision.name;
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
        var dialogues = GameObject.Find(nomPNJ).GetComponent<Dialogues>();

        if(dialogues.index == -1)
        {
            movement.enabled = false;
            boiteDeDialogue.SetActive(true);
        }
        if(dialogues.DialogueSuivant())
        {
            boiteDeDialogue.GetComponentInChildren<Text>().text = dialogues.dialogues[dialogues.index];
        }
        else
        {
            boiteDeDialogue.SetActive(false);
            movement.enabled = true;
            dialogues.index = -1;
        }
    }

    void PlacerChateauDeSable()
    {
        var _castlePlacement = transform.position + Vector3.down;

        var _castle = Instantiate(castlePrefab, _castlePlacement, Quaternion.identity);
    }
}
