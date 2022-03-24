using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    public Image imageToFade;
    private Color tempColor;
    private float targetAlpha;
    private int timeToFade = 10;

    private void Start()
    {
        tempColor = imageToFade.color;
    }

    public void TriggerFadeIn()
    {
        Time.timeScale = 1;
        imageToFade.gameObject.SetActive(true);
        StartCoroutine(FadeIn(imageToFade));
    }

    public void TriggerFadeOut()
    {
        Time.timeScale = 1;
        imageToFade.gameObject.SetActive(true);
        StartCoroutine(FadeOut(imageToFade));
    }

    private YieldInstruction fadeInstruction = new YieldInstruction();
    IEnumerator FadeOut(Image image)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < timeToFade)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime;
            c.a = 1.0f - Mathf.Clamp01(elapsedTime / timeToFade);
            image.color = c;
        }
    }

    IEnumerator FadeIn(Image image)
    {
        float elapsedTime = 0.0f;
        Color c = image.color;
        while (elapsedTime < timeToFade)
        {
            yield return fadeInstruction;
            elapsedTime += Time.deltaTime * 2;
            c.a = Mathf.Clamp01(elapsedTime / timeToFade);
            image.color = c;
        }
    }
}
