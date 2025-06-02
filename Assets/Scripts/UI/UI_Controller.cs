using CrazyGames;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Controller : MonoBehaviour, ISaveManager
{
    public static UI_Controller instance;

    [Header("End Screen")]
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject diedText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject returnMenuButton;    
    [SerializeField] private RectTransform controlsPanel;
    [SerializeField] private RectTransform settingsPanel;
    [Space]

    [Header("Sound Effects")]  
    [SerializeField] private Slider bgmSlider;
    [Space]

    [Header("Skill Panel")]
    [SerializeField] private RectTransform warningPanel;
    [SerializeField] private TMP_Text warningMessage;

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
        AudioManager.instance.StopSFX(58);

        itemToolTip.gameObject.SetActive(false);
        statToolTip.gameObject.SetActive(false);
        skillToolTip.gameObject.SetActive(false);
        settingsPanel.localScale = Vector3.zero;
        controlsPanel.localScale = Vector3.zero;

        SaveManager.instance.LoadGame();
        bgmSlider.onValueChanged.AddListener(delegate { SetMusicVolume(); });
        warningPanel.localScale = Vector3.zero;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && PlayerManager.instance.player.stats.isDead == false)
        {
            SwitchWithKey(inventoryElements[0]);
            AudioManager.instance.PauseAllSFX();
            
        }

        if (Input.GetKeyDown(KeyCode.Escape) && PlayerManager.instance.player.stats.isDead == false)
        {
            SwitchWithKey(inventoryElements[2]);           
            AudioManager.instance.PauseAllSFX();
            
        }
        SetMusicVolume();
    }

    public IEnumerator ScalePanel(string _text)
    {
        float duration = 0.3f; // Duration of the tween in seconds
        float elapsed = 0f;
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        warningMessage.text = _text;
        while (elapsed < duration)
        {
            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / duration;
            warningPanel.localScale = Vector3.Lerp(startScale, endScale, t);
            yield return null;
        }
        
        warningPanel.localScale = endScale;
        yield return new WaitForSecondsRealtime(2.0f);
        CloseWarningPanel();
    }

    public void CloseWarningPanel()
    {
        warningPanel.localScale = Vector2.zero;      
    }

    public void SetMusicVolume()
    {
        if (bgmSlider != null)
        {
            if(AudioManager.instance.levelBGM.isPlaying)
                AudioManager.instance.levelBGM.volume = bgmSlider.value;
        }

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
        AudioManager.instance.PlaySFX(58, null);
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
        //yield return new WaitForSeconds(1.5f);
        diedText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        // Now pause the game
        GameManager.instance.PauseGame(true);
        yield return new WaitForSecondsRealtime(1.0f);
        restartButton.SetActive(true);
        yield return new WaitForSecondsRealtime(0.25f);
        returnMenuButton.SetActive(true);
    }

    // Show MidGame Ad
    public void RestartGameButton()
    {
        if (PlayerManager.instance.player.stats.isDead)
        {
            ShowMidgameAd();
        }
        else
            GameManager.instance.RestartScene();
    }
    public void ShowMidgameAd()
    {
        CrazySDK.Ad.RequestAd(
            CrazyAdType.Midgame,
            () =>
            {
                Debug.Log("Midgame ad started");
            },
            (error) =>
            {
                Debug.Log("Midgame ad error: " + error);
                // Fallback: restart scene even if ad fails (important for QA)
                GameManager.instance.RestartScene();
            },
            () =>
            {
                Debug.Log("Midgame ad finished");
                GameManager.instance.RestartScene();
            }
        );
    }
    /**********************/

    public void ReturnToMenu()
    {
        Time.timeScale = 1f; // Resume time just in case it's frozen
        GameManager.instance.ReturnToMenu();
    }

    public void Settings()
    {     
        controlsPanel.localScale = Vector3.zero;
        StartCoroutine(ScalePanel(settingsPanel, Vector3.zero, Vector3.one));
        AudioManager.instance.PlaySFX(58, null);
    }

    public void Controls()
    { 
        settingsPanel.localScale = Vector3.zero;
        StartCoroutine(ScalePanel(controlsPanel, Vector3.zero, Vector3.one));
        AudioManager.instance.PlaySFX(58, null);
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

    public void Save()
    {
        SaveManager.instance.SaveGame();
        AudioManager.instance.PlaySFX(58, null);
    }
    public void Resume() => CheckInGameUI();

    public void LoadData(GameData _data)
    {
        if (_data != null)
        {
            bgmSlider.value = _data.musicVolume;            
        }
        else
            bgmSlider.value = 0.3f;
          
    }

    public void SaveData(ref GameData _data)
    {
        _data.musicVolume = bgmSlider.value;       
    }
}
