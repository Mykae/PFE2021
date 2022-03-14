using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuPause : MonoBehaviour
{
    // TODO public GameObject victoryScreen;
    public GameObject pauseScreen;

    public bool onPause = false;

    public void Awake()
    {
        Time.timeScale = 1;
    }


    public void EnablePause()
    {
        Time.timeScale = 0;
        onPause = true;
        pauseScreen.SetActive(true);
    }

    public void DisablePause()
    {
        Time.timeScale = 1;
        onPause = false;
        pauseScreen.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (onPause)
            {
                DisablePause();
            }
            else
            {
                EnablePause();
            }
        }
    }

}
