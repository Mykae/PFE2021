using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPadMinigame : MonoBehaviour
{

    public Text cardCode;
    public Text inputCode;
    public int longueurCode = 5;
    public float codeResetTime = 0.5f;
    public GameObject player;

    private bool isResetting = false;
    public bool isCleared = false;

    private RandomSound soundToPlay;

    private bool waitForLastDigit = true;

    private void OnEnable()
    {
        soundToPlay = GetComponent<RandomSound>();
        isCleared = false;
        string code = string.Empty;

        for (int i = 0; i < longueurCode; i++)
        {
            code += Random.Range(1, 10);
        }

        cardCode.text = code;
        inputCode.text = string.Empty;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            this.gameObject.SetActive(false);
        }
        }

    public void ButtonClick(int number)
    {
        if (!waitForLastDigit)
            return;
        soundToPlay.PlayRandomSound();
        if (isResetting) return;
        inputCode.text += number;

        if ((inputCode.text == cardCode.text || inputCode.text.Length >= longueurCode))
            StartCoroutine(WaitForTextToBeVisible());
    }

    private IEnumerator ResetCode()
    {
        isResetting = true;
        float pauseEndTime = Time.realtimeSinceStartup + 1;
        while (Time.realtimeSinceStartup < pauseEndTime)
        {
            yield return 0;
        }
        if(isCleared == true)
        {
            player.GetComponent<PlayerMovement>().enabled = true;
            this.gameObject.SetActive(false);
        }
        inputCode.text = string.Empty;
        isResetting = false;
    }

    private IEnumerator WaitForTextToBeVisible()
    {
        waitForLastDigit = false;
        yield return new WaitForSeconds(0.5f);
        if (inputCode.text == cardCode.text)
        {
            inputCode.text = "Correct";
            isCleared = true;
            var i = player.GetComponent<PlaySound>();
            i.Play(0);
            StartCoroutine(ResetCode());
        }
        else if (inputCode.text.Length >= longueurCode)
        {
            inputCode.text = "Failed";
            StartCoroutine(ResetCode());
        }
        waitForLastDigit = true;
    }
}
