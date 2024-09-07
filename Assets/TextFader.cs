using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TextFader : MonoBehaviour
{
    [SerializeField] public TextMeshProUGUI textDisplay;
    [SerializeField] private float fadeDuration = 1.0f;

    public static TextFader Instance { get; private set; }

    public void Start()
    {
        textDisplay.CrossFadeAlpha(0, 0.01f, true);
        StartCoroutine(FadeInAndOut());
    }

    public IEnumerator FadeInAndOut()
    {
        textDisplay.CrossFadeAlpha(1, 2f, true);
        yield return new WaitForSeconds(2f);
        textDisplay.CrossFadeAlpha(0, 2f, true);
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
