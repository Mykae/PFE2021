using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerFruit : MonoBehaviour
{
    public float speed;
    private int fruitCount = 0;
    public int goalFruit;
    public Text fruitCountText;
    public GameObject winPanel;
    private float input;
    private Rigidbody2D rb;
    public Transform wallLeft;
    public Transform wallRight;
    public GameObject minigame;
    public GameObject player;
    private bool isEnding = false;
    private bool isExit = false;

    [SerializeField] private GameObject fruitsToInstantiate;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        fruitCountText.text = "Fruits : " + fruitCount.ToString() + "/" + goalFruit.ToString();
    }

    private void OnEnable()
    {
        input = 0;
        fruitCount = 0;
        winPanel.SetActive(false);
        isEnding = false;
        fruitCountText.text = "Fruits : " + fruitCount.ToString() + "/" + goalFruit.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        input = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(input * speed, rb.velocity.y);
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, wallLeft.position.x, wallRight.position.x), transform.position.y, transform.position.z);
        if (Input.GetKeyDown(KeyCode.Escape)) isExit = true;
        CheckEnding();
    }

    public void AddFruit(int number)
    {
        if((fruitCount + number) <= 0)
        {
            fruitCount = 0;
        }
        else
        {
            fruitCount += number;
        }
        fruitCountText.text = "Fruits : " + fruitCount.ToString() + "/" + goalFruit.ToString();
        if (fruitCount >= goalFruit && !isEnding)
        {
            winPanel.SetActive(true);
            StartCoroutine(EndMinigame());
        }
    }

    public void CheckEnding()
    {
        if (isEnding == true || isExit == true)
        {
            if (isEnding)
            {
                for (int i = 0; i < fruitCount; i++)
                {
                    Instantiate(fruitsToInstantiate, player.transform.position, player.transform.rotation);
                }
                var k = player.GetComponent<PlaySound>();
                k.Play(0);
            }

            player.GetComponent<PlayerMovement>().enabled = true;
            GameObject[] allObjects = GameObject.FindGameObjectsWithTag("SpawnedFruit");
            foreach (GameObject obj in allObjects)
            {
                Destroy(obj);
            }
            isEnding = false;
            isExit = false;
            minigame.gameObject.SetActive(false);
        }
    }

    private IEnumerator EndMinigame()
    {
        
        float pauseEndTime = Time.realtimeSinceStartup + 1;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        isEnding = true;

    }
}
