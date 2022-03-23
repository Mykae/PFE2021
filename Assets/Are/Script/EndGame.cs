using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Fade lancerFadeOut;
    public GameObject boutonRetour;

    public Image background;
    public Sprite[] imageDeFin;
    //0 = Ending : Default
    //1 = Ending : party with 4 and cake cooked

    private void Start()
    {
        background.sprite = imageDeFin[GameObject.FindGameObjectWithTag("DontDestroy").GetComponent<DontDestroyOnLoad>().currentEnding];
        lancerFadeOut.TriggerFadeOut();
    }

    private void Update()
    {

        if (boutonRetour.activeInHierarchy)
            enabled = false;
        if(Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) || lancerFadeOut.imageToFade.color.a == 0)
        {
                boutonRetour.SetActive(true);
        }
    }

    public void retourMenu()
    {
        SceneManager.LoadScene(0);
    }
}
