using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipal : MonoBehaviour
{

    public void loadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitterLeJeu()
    {
        Application.Quit();
    }
}
