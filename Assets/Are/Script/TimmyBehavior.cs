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

    public Text actionText;


    void Update()
    {
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
            transform.Find("Main Camera").gameObject.SetActive(false);
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
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && collision.name != name)
        {
            playerTriggerBox = true;
        }
        if (collision.tag == "Peche")
        {
            pecheTriggerBox = true;
        }
        if (collision.tag == "Chateau")
        {
            chateauTriggerBox = true;
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
