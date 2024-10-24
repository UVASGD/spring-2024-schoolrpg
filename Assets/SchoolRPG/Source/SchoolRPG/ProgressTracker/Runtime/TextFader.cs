using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class TextFader : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textDisplay;
    [SerializeField] private float fadeDuration = 1.0f;

    public void Start()
    {
        /*Color color = textDisplay.color;
        color.a = 0;
        textDisplay.color = color;*/
        textDisplay.CrossFadeAlpha(0, 0.001f, true);
        //textDisplay.enabled = false;
        //StartCoroutine(FadeInAndOut());
    }

    public IEnumerator FadeInAndOut()
    {
        Debug.Log("FadeInAndOutTriggered");
        //textDisplay.enabled = true;
        textDisplay.CrossFadeAlpha(1, fadeDuration, true);
        yield return new WaitForSeconds(2f);
        textDisplay.CrossFadeAlpha(0, fadeDuration, true);
        Debug.Log(textDisplay.color);
        //textDisplay.enabled = false;
    }

    /*
    public IEnumerator FadeInAndOut()
    {
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(4f);
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeIn()
    {
        myTextMeshProGUI.CrossFadeAlpha(0, 2f, true);
    }

    public IEnumerator FadeOut()
    {
        myTextMeshProGUI.CrossFadeAlpha(0, 2f, true);
    }*/
}
