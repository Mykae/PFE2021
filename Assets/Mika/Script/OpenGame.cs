using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGame : MonoBehaviour
{
    public GameObject mg;
    public GameObject player;
    public bool isIn = false;
    public PlayerBehavior playerStatus;
    public bool canGameBeReplayed = true;
    private bool isItTheFuckingFishGame = false;
    public float distance;

    private void Awake()
    {
        playerStatus = player.GetComponent<PlayerBehavior>();
        isItTheFuckingFishGame = (name == "OpenGameFish");
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == player.name)
            isIn = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == player.name)
            isIn = false;
    }

    public void Update()
    {
        distance = Vector3.Distance(player.transform.position, transform.position);

        if (isIn == true && (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("Fire1")) && playerStatus.showMonologue == false && canGameBeReplayed)
        {
            mg.SetActive(true);
            player.GetComponent<PlayerMovement>().enabled = false;
            //Time.timeScale = 0;
        }
    }

    public void ExitSucessfullFishGame()
    {
        StartCoroutine(AttendsDeuxSecAvantDePecherANouveauWesh());
    }

    private IEnumerator AttendsDeuxSecAvantDePecherANouveauWesh()
    {
        canGameBeReplayed = false;
        yield return new WaitForSeconds(2);
        canGameBeReplayed = true;
    }
}
