using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private float currentSpeed;
    public float walkSpeed = 5;
    public float runSpeed = 7.5f;
    private Rigidbody2D rb;
    private bool shift;
    public int height = 5;
    Vector2 movement;
    public TilemapCollider2D Layer0;
    public TilemapCollider2D Layer1;


    enum Direction { North, East, South, West };
    Direction playerDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shift = false;
    }

    void Update()
    {
        MovementInput();
        CheckHeight();
    }

    private void FixedUpdate()
    {
        if(shift == true)
        {
            currentSpeed = runSpeed;
        }
        else if(shift == false)
        {
            currentSpeed = walkSpeed;
        }
        rb.velocity = movement * currentSpeed;
    }

    void MovementInput()
    {
        float mx = Input.GetAxisRaw("Horizontal");
        float my = Input.GetAxisRaw("Vertical");
        shift = Input.GetKey(KeyCode.LeftShift);

        movement = new Vector2(mx, my).normalized;
        facingDirection(mx, my);
    }
    
    void CheckHeight()
    {
        if(height == 5)
        {
            Layer1.gameObject.SetActive(false);
            Layer0.gameObject.SetActive(true);
        }
        else if (height == 15)
        {
            Layer1.gameObject.SetActive(true);
            Layer0.gameObject.SetActive(false);
        }
    }

    void facingDirection(float mx, float my)
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

    public void SetHeight(int layer)
    {
        height = layer;
        GetComponent<SpriteRenderer>().sortingOrder = height;
    }
}
