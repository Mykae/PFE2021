using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerminerLeJeu : MonoBehaviour
{
    public MenuPause gameMenu;
    public bool isIn = false;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            isIn = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            isIn = false;
    }

    public void Update()
    {
        if (isIn == true && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) && Time.timeScale != 0)
        {
            gameMenu.PromptfinDePartie();
        }
    }
}
