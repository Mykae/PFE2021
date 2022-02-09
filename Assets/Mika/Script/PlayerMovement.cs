using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    private float currentSpeed;
    public float walkSpeed;
    public float runSpeed;
    public Rigidbody2D rb;
    private bool shift;
    public int height;
    Vector2 movement;
    public TilemapCollider2D Layer0;
    public TilemapCollider2D Layer1;

    private void Start()
    {
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

    public void SetHeight(int layer)
    {
        height = layer;
        GetComponent<SpriteRenderer>().sortingOrder = height;
    }
}
