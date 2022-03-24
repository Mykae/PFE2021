using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    public GameObject menuCredits;

    private void Start()
    {
        FindObjectOfType<DontDestroyOnLoad>().currentEnding = 0;
    }

    public void loadGameScene()
    {
        
        SceneManager.LoadScene(1);
    }

    public void Credits()
    {

        if (menuCredits.activeInHierarchy)
            menuCredits.SetActive(false);
        else
            menuCredits.SetActive(true);
    }

    public void QuitterLeJeu()
    {
        Application.Quit();
    }
}
