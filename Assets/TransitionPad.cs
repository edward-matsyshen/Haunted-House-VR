using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;

public class TransitionPad : MonoBehaviour
{
    public string targetSceneName;
    public float transitionDelay = 0.5f;  // A small delay before the scene starts transitioning
    public float fadeDuration = 1.0f;  // Duration of the fade effect

    private bool isTransitioning = false;
    private Texture2D fadeTexture;
    private float fadeAlpha = 0.0f;

    private void Start()
    {
        // Create a black texture
        fadeTexture = new Texture2D(1, 1);
        fadeTexture.SetPixel(0, 0, Color.black);
        fadeTexture.Apply();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(TransitionAfterDelay());
        }
    }

    private IEnumerator TransitionAfterDelay()
    {
        isTransitioning = true;

        // Start fade to black
        yield return StartCoroutine(FadeIn());

        // Wait for the specified delay before transitioning
        yield return new WaitForSeconds(transitionDelay);

        // Load the target scene
        SceneManager.LoadScene(targetSceneName);
    }

    private IEnumerator FadeIn()
    {
        float elapsedTime = 0;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            fadeAlpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = fadeDuration;
        while (elapsedTime > 0)
        {
            elapsedTime -= Time.deltaTime;
            fadeAlpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            yield return null;
        }
    }

    private void OnGUI()
    {
        if (fadeAlpha > 0)
        {
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, fadeAlpha);
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeTexture);
        }
    }
}