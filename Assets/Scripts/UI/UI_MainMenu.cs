using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private RectTransform warningPanel;
    [SerializeField] private RectTransform creditsPanel;
    [SerializeField] private ScrollRect scrollRect;

    private void Start()
    {
        if (!SaveManager.instance.HasSavedData())
            continueButton.SetActive(false);

        warningPanel.localScale = Vector2.zero;
        creditsPanel.localScale = Vector2.zero;
    }

    public void ContinueGame()
    {
        StartCoroutine(LoadScreenWithFadeEffect(2f));
        AudioManager.instance.PlaySFX(58, null);
    }

    public void NewGame()
    {
        if (continueButton.activeSelf == false)        
            AcceptNewGame();        
        else
        {
            creditsPanel.localScale = Vector2.zero;
            if (warningPanel.localScale != Vector3.one)
                StartCoroutine(ScalePanel(warningPanel));
        }
        AudioManager.instance.PlaySFX(58, null);
    }

    public void Credits()
    {
        AudioManager.instance.PlaySFX(58, null);
        warningPanel.localScale = Vector2.zero;

        if(creditsPanel.localScale != Vector3.one)
            StartCoroutine(ScalePanel(creditsPanel));
    }



    private IEnumerator ScalePanel(RectTransform _panel)
    {
        float duration = 0.3f; // Duration of the tween in seconds
        float elapsed = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            _panel.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        _panel.localScale = endScale; // Ensure it's fully scaled at the end

        // Reset scroll to top
        if (scrollRect != null)
            scrollRect.verticalNormalizedPosition = 1f;
    }

    public void AcceptNewGame()
    {
        AudioManager.instance.PlaySFX(58, null);
        SaveManager.instance.DeleteSavedData();
        StartCoroutine(LoadScreenWithFadeEffect(2));
    }

    public void Reject(RectTransform _panel)
    {
        AudioManager.instance.PlaySFX(58, null);
        _panel.localScale = Vector3.zero;
    }


    public void ExitGame()
    {
        AudioManager.instance.PlaySFX(58, null);
        Application.Quit();
    }

    IEnumerator LoadScreenWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        if (AudioManager.instance != null)
            yield return StartCoroutine(AudioManager.instance.FadeOutBGM(AudioManager.instance.menuBGM));

        yield return new WaitForSeconds(_delay);

        if (LevelManager.instance.sceneIndex <= 0)
            SceneManager.LoadScene(sceneName);
        else
            SceneManager.LoadScene(LevelManager.instance.sceneIndex);
    }


}
