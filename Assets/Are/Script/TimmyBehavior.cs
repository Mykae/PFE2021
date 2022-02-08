using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimmyBehavior : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;

    enum Direction { North, East, South, West };
    Direction playerDirection;

    [SerializeField]
    Transform castlePrefab;

    //Différents types de trigger box
    bool playerTriggerBox = false;
    bool pecheTriggerBox = false;
    bool chateauTriggerBox = false;
    bool pnjTriggerBox = false;

    string dernierPnjRecontré;
    public GameObject boiteDeDialogue;
    bool isPlayerTalking = false;

    public Text actionText;


    private GameObject LastEncounteredPlayer;

    void Update()
    {
        //Interdiction de bouger si le joueur est en train de parler avec quelqu'un
        if(!isPlayerTalking)
            MovementInput();

        UpdateTextActionButton();

        if (Input.GetKeyDown(KeyCode.Space))
            ActionButton();

    }

    private void FixedUpdate()
    {
        rb.velocity = movement * moveSpeed;
    }

    void MovementInput()
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");

        movement = new Vector2(mx, my).normalized;

        facingDirection(mx, my);
    }

    void facingDirection (float mx, float my)
    {
        if (mx > 0)
        {
            playerDirection = Direction.East;
            //gameObject.GetComponent<SpriteRenderer>().sprite = PlayerSprites[0]; -> faudra faire quelque chose du style pour avoir les bonnes sprites
        }
        if (mx < 0)
        {
            playerDirection = Direction.West;
        }
        if (my > 0)
        {
            playerDirection = Direction.North;
        }
        if (my < 0)
        {
            playerDirection = Direction.South;
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
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            transform.Find("Main Camera").SetParent(LastEncounteredPlayer.transform);
            LastEncounteredPlayer.transform.Find("Main Camera").transform.localPosition = new Vector3(0, 0, -10);
            LastEncounteredPlayer.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            LastEncounteredPlayer.GetComponent<TimmyBehavior>().enabled = true;
        }
        if (pnjTriggerBox)
        {
            if (isPlayerTalking)
            {
                boiteDeDialogue.SetActive(false);
                isPlayerTalking = false;
            }
            else
            {
                Parler(dernierPnjRecontré);
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
        isPlayerTalking = true;
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
        var _castlePlacement = new Vector3(1, 0, 0) + transform.position;
        if (playerDirection == Direction.West)
        {
            _castlePlacement = new Vector3(-1, 0, 0) + transform.position;
        }
        else if (playerDirection == Direction.South)
        {
            _castlePlacement = new Vector3(0, -1, 0) + transform.position;
        }
        else if (playerDirection == Direction.North)
        {
            _castlePlacement = new Vector3(0, 1, 0) + transform.position;
        }

        var _castle = Instantiate(castlePrefab, _castlePlacement, Quaternion.identity);
    }
}
