using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerFruit : MonoBehaviour
{
    public float speed;
    public int fruitCount;
    public int goalFruit;
    public Text fruitCountText;
    public GameObject winPanel;
    private float input;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
    }

    public void AddFruit(int number)
    {
        fruitCount += number;
        fruitCountText.text = fruitCount.ToString();
        if (fruitCount >= goalFruit)
        {
            //wait (x) seconds
            winPanel.SetActive(true);
        }
    }
}
