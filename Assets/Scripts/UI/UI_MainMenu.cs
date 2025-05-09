using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject continueButton;
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private RectTransform warningPanel;

    private void Start()
    {
        if (!SaveManager.instance.HasSavedData())
            continueButton.SetActive(false);

        warningPanel.localScale = Vector2.zero;
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
            StartCoroutine(WarningPanel());
        
    }


    private IEnumerator WarningPanel()
    {
        float duration = 0.3f; // Duration of the tween in seconds
        float elapsed = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            warningPanel.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }

        warningPanel.localScale = endScale; // Ensure it's fully scaled at the end
    }

    public void AcceptNewGame()
    {
        SaveManager.instance.DeleteSavedData();
        StartCoroutine(LoadScreenWithFadeEffect(2));
    }

    public void RejectNewGame() => warningPanel.localScale = Vector3.zero;


    public void ExitGame()
    {
        Debug.Log("Exit Game");
        //Application.Quit();
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
