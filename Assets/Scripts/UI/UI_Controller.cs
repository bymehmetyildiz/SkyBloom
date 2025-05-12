using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour
{
    [Header("End Screen")]
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject returnMenuButton;
    [SerializeField] private RectTransform controlsPanel;
    [SerializeField] private RectTransform settingsPanel;
    [Space]

    public static UI_Controller instance;

    [SerializeField] private GameObject[] inventoryElements;
    public GameObject inGameUI;

    public UI_ItemToolTip itemToolTip;
    public UI_StatToolTip statToolTip;
    public UI_SkillToolTip skillToolTip;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance.gameObject);

    }

    private void Start()
    {
        fadeScreen.gameObject.SetActive(true);

        Switch(inGameUI);

        itemToolTip.gameObject.SetActive(false);
        statToolTip.gameObject.SetActive(false);
        skillToolTip.gameObject.SetActive(false);
        settingsPanel.localScale = Vector3.zero;
        controlsPanel.localScale = Vector3.zero;

    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && PlayerManager.instance.player.stats.isDead == false)
            SwitchWithKey(inventoryElements[0]);
    }

    public void Switch(GameObject _menu)
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null; // This is to keep FadeScreen active.

            if(!fadeScreen)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
        {
            _menu.SetActive(true);
        }   
        
        if (GameManager.instance != null)
        {
            if (_menu == inGameUI)
                GameManager.instance.PauseGame(false);
            else
                GameManager.instance.PauseGame(true);

        }

    }

    private void CheckInGameUI()
    {
        for (int i = 0; i < inventoryElements.Length; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
                return;

        }
        Switch(inGameUI);        
    }

    public void SwitchWithKey(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckInGameUI();
            return;
        }
        Switch(_menu);

    }

    public void SwitchOnEndScreen()
    {
        SaveManager.instance.SaveGame();
        fadeScreen.FadeOut();
        StartCoroutine(EndScreen());
    }

    IEnumerator EndScreen()
    {
        yield return new WaitForSeconds(1.5f);
        endText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        returnMenuButton.SetActive(true);
    }

    public void RestartGameButton() => GameManager.instance.RestartScene();
    public void ReturnToMenu()
    {
        Time.timeScale = 1f; // Resume time just in case it's frozen
        GameManager.instance.ReturnToMenu();
    }

    public void Settings()
    {     
        controlsPanel.localScale = Vector3.zero;
        StartCoroutine(ScalePanel(settingsPanel, Vector3.zero, Vector3.one));
    }

    public void Controls()
    { 
        settingsPanel.localScale = Vector3.zero;
        StartCoroutine(ScalePanel(controlsPanel, Vector3.zero, Vector3.one));
    }


    public IEnumerator ScalePanel(RectTransform _panel, Vector3 _startScale, Vector3 _endScale)
    {
        float duration = 0.3f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime; // Use unscaled time
            float t = Mathf.Clamp01(elapsed / duration);
            _panel.localScale = Vector3.Lerp(_startScale, _endScale, t);
            yield return null;
        }

        _panel.localScale = _endScale;
    }

    public void Save() => SaveManager.instance.SaveGame();
    public void Resume() => CheckInGameUI();
}
