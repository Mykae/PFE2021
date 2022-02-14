using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{


    //Le joueur a 2 trigger box donc j'ai bricolé pour s'assurer que ce soit bien le corps du joueur
    //qui touche l'objet pour le ramassage, pas juste la trigger box d'interaction

    private bool actualPlayer = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && actualPlayer)
        {
            Destroy(this.gameObject);
        }
        actualPlayer = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            actualPlayer = false;
    }
}
