using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimmyBehavior : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    Vector2 movement;

    enum Direction { North, East, South, West };
    Direction playerDirection;

    [SerializeField]
    Transform castlePrefab;


    void Update()
    {
        MovementInput();

        if (Input.GetKeyDown(KeyCode.Space))
            PlacerChateauDeSable();

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
