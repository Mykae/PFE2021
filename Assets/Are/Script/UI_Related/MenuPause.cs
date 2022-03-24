using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    // TODO public GameObject victoryScreen;
    [SerializeField] private GameObject actionButton, pauseScreen, ouiNonPrompt;

    private bool isGamePaused;

    private RandomSound soundToPlay;

    public void Awake()
    {
        Time.timeScale = 1;
        soundToPlay = GetComponent<RandomSound>();
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

    public void PromptfinDePartie()
    {
        PlaySound();
        Time.timeScale = 0;
        isGamePaused = true;
        ouiNonPrompt.SetActive(true);
        actionButton.SetActive(false);
    }

    public void GoToEndingScreen()
    {
        Invoke("LoadEndScene", 5);
    }

    public void LoadEndScene()
    {
        SceneManager.LoadScene(2);
    }

    public void EnablePause()
    {
        PlaySound();
        Time.timeScale = 0;
        isGamePaused = true;
        pauseScreen.SetActive(true);
        actionButton.SetActive(false);

    }


    public void DisablePause()
    {
        PlaySound();
        Time.timeScale = 1;
        isGamePaused = false;
        pauseScreen.SetActive(false);
        ouiNonPrompt.SetActive(false);
    }

    public void PlaySound()
    {
        soundToPlay.PlayRandomSound();
    }


}
