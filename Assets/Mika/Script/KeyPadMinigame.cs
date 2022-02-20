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

    private bool isResetting = false;

    private void OnEnable()
    {
        string code = string.Empty;

        for (int i = 0; i < longueurCode; i++)
        {
            code += Random.Range(1, 10);
        }

        cardCode.text = code;
        inputCode.text = string.Empty;
    }

    public void ButtonClick(int number)
    {
        if (isResetting) return;
        inputCode.text += number;
        
        if(inputCode.text == cardCode.text)
        {
            inputCode.text = "Correct";
            StartCoroutine(ResetCode());
        }
        else if (inputCode.text.Length >= longueurCode)
        {
            inputCode.text = "Failed";
            StartCoroutine(ResetCode());
        }
    }

    private IEnumerator ResetCode()
    {
        isResetting = true;
        yield return new WaitForSeconds(codeResetTime);
        inputCode.text = string.Empty;
        isResetting = false;
    }

}
