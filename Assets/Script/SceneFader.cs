using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while(t > 0)
        {
            t -= Time.deltaTime * 0.5f;
            float a = curve.Evaluate(t);
            img.color = new Color(255f, 255f, 255f, a);

            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime * 0.5f;
            float a = curve.Evaluate(t);
            img.color = new Color(255f, 255f, 255f, a);

            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
