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
    }

    public void Credits()
    {
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
        SaveManager.instance.DeleteSavedData();
        StartCoroutine(LoadScreenWithFadeEffect(2));
    }

    public void Reject(RectTransform _panel) => _panel.localScale = Vector3.zero;


    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator LoadScreenWithFadeEffect(float _delay)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        if (LevelManager.instance.sceneIndex <= 0)
            SceneManager.LoadScene(sceneName);
        else
            SceneManager.LoadScene(LevelManager.instance.sceneIndex);
    }

   
}
