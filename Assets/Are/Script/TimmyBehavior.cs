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

    void Update()
    {
        movement = GetComponent<PlayerMovement>();
        UpdateTextActionButton();

        if (Input.GetKeyDown(KeyCode.Space))
            ActionButton();

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
            movement.enabled = false;
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            transform.Find("Main Camera").SetParent(LastEncounteredPlayer.transform);
            LastEncounteredPlayer.transform.Find("Main Camera").transform.localPosition = new Vector3(0, 0, -10);
            LastEncounteredPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            LastEncounteredPlayer.GetComponent<TimmyBehavior>().enabled = true;
            LastEncounteredPlayer.GetComponent<PlayerMovement>().enabled = true;

        }
        if (pnjTriggerBox)
        {
            if (movement.enabled)
            {
                Parler(dernierPnjRecontré);
            }
            else
            {
                boiteDeDialogue.SetActive(false);
                movement.enabled = true;
            }
            
        }
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

    private void OnTriggerEnter2D(Collider2D collision)
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
        movement.enabled = false;
        boiteDeDialogue.SetActive(true);
        switch (nomPNJ)
        {
            case "PNJ1":
                boiteDeDialogue.GetComponentInChildren<Text>().text = "Salut mon ami, tu vas bien ? Pourrais-tu aller me cherche 3 poissons ?";
                break;
            case "PNJ2":
                boiteDeDialogue.GetComponentInChildren<Text>().text = "Hé toi ! Aboule flouze !";
                break;
            case "PNJ3":
                boiteDeDialogue.GetComponentInChildren<Text>().text = "Moi j'aime trop les châteaux de sables <3";
                break;
            default:
                break;
        }
    }

    void PlacerChateauDeSable()
    {
        var _castlePlacement = transform.position + Vector3.down;

        var _castle = Instantiate(castlePrefab, _castlePlacement, Quaternion.identity);
    }
}
