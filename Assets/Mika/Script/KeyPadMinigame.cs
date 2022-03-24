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

    public GameObject zoneDinteractionAFermerSiCoffreOuvert;

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
            player.GetComponent<PlayerBehavior>().restartMonologue(new string[1] { "Voilà ! Le coffre est déverrouillé ! Je me demande bien ce qu’il y a dedans… Mais je préfère laisser Topaze découvrir par lui-même. Dans tous les cas, maintenant il pourra être présent ce soir ! " });
            GameObject.Find("Topaze").GetComponent<DialoguesAvecTimmy>().dialogues = new string[2] { "Quoi, tu as réussi ?! Tu es trop fort Timmy, merci ! ", "Sois certain que je serai là ce soir !" };
            zoneDinteractionAFermerSiCoffreOuvert.GetComponent<OpenGame>().canGameBeReplayed = false;
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
