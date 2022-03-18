using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    // TODO public GameObject victoryScreen;
    [SerializeField] private GameObject actionButton, pauseScreen;

    private bool isGamePaused;

    public void Awake()
    {
        Time.timeScale = 1;
    }

    private void Start()
    {
        isGamePaused = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isGamePaused)
                DisablePause();
            else
                EnablePause();
        }
    }

    public void loadMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    public void loadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void EnablePause()
    {
        Time.timeScale = 0;
        isGamePaused = true;
        pauseScreen.SetActive(true);
        actionButton.SetActive(false);
        /*isActionButtonEnabled = actionButton.activeInHierarchy ? true : false;
        if (isActionButtonEnabled)
            actionButton.SetActive(false);*/

    }

    public void DisablePause()
    {
        Time.timeScale = 1;
        isGamePaused = false;
        pauseScreen.SetActive(false);
        /*
        if (isActionButtonEnabled)
        {
            actionButton.SetActive(true);
        }*/
    }

}
