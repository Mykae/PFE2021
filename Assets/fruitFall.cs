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
        speed = Random.Range(minSpeed, maxSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerFruit.AddFruit(value);
            Destroy(gameObject);
        }
        if (collision.tag == "ground")
        {

            Destroy(gameObject);
        }
    }
}
