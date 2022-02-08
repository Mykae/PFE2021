using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float currentSpeed;
    public float walkSpeed;
    public float runSpeed;
    public Rigidbody2D rb;
    private bool shift;
    Vector2 movement;

    private void Start()
    {
        shift = false;
    }

    void Update()
    {
        MovementInput();
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
}
