using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGame : MonoBehaviour
{
    public GameObject mg;
    public GameObject player;
    public bool isIn = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        isIn = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        isIn = false;
    }

    public void Update()
    {
        if (isIn == true && Input.GetKeyDown(KeyCode.E))
        {
            mg.SetActive(true);
            player.GetComponent<PlayerMovement>().enabled = false;
            //Time.timeScale = 0;
        }
    }
}
