using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve Curve;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FateTo (string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        float t = 1f;

        while (t > 0)
        {
            t -= Time.deltaTime;
            float a = Curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a); // t alpha kanalı
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        float t = 0f;

        while (t < 1)
        {
            t += Time.deltaTime;
            float a = Curve.Evaluate(t);
            img.color = new Color(0f, 0f, 0f, a); // t alpha kanalı
            yield return 0;
        }

        SceneManager.LoadScene(scene);
    }
}
