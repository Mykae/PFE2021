using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitFall : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    public playerFruit playerFruit;
    public int value;
    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        playerFruit = GameObject.FindGameObjectWithTag("PlayerFruit").GetComponent<playerFruit>();
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "PlayerFruit")
        {
            playerFruit.AddFruit(value);
            Destroy(this.gameObject);
        }
        if (collision.tag == "GroundFruit")
        {

            Destroy(this.gameObject);
        }
    }
}
