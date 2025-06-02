using CrazyGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, ISaveManager
{

    
    public static LevelManager instance;
    [SerializeField] private UI_FadeScreen fadeScreen;
    public int sceneIndex;

    [Header("End Panel")]
    [SerializeField] private GameObject endPanel;
    [Space]    
    [Header("Controller Panel")]
    [SerializeField] private GameObject controllersPanel;
    private bool isControlShown;

    private void Awake()
    {
        if (instance != null) 
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    private void Start()
    {
        fadeScreen = FindObjectOfType<UI_FadeScreen>();

        if (endPanel != null)
            endPanel.SetActive(false);

        SaveManager.instance.LoadGame();

        // Only show the panel if on scene 2 and it hasn't been shown before
        if (SceneManager.GetActiveScene().buildIndex == 2 && !isControlShown)
        {
            if (controllersPanel != null)
                controllersPanel.SetActive(true);
        }
        else
        {
            if (controllersPanel != null)
                controllersPanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CloseControlPanel();
        }
    }

    public void CloseControlPanel()
    {
        if (controllersPanel != null && controllersPanel.activeSelf)
        {
            controllersPanel.SetActive(false);
            isControlShown = true;
            SaveManager.instance.SaveGame(); // Save the state so it persists
        }
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() == null)
            return;

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextIndex = currentIndex + 1;

        // If player is at the last scene
        if (nextIndex >= SceneManager.sceneCountInBuildSettings)
        {
            SwitchOnEndScreen();
            AudioManager.instance.menuBGM.Play();
            SaveManager.instance.SaveGame();
            return;
        }
        sceneIndex = nextIndex;
        SaveManager.instance.SaveGame();

        StartCoroutine(AudioManager.instance.FadeOutBGM(AudioManager.instance.levelBGM));
        ShowMidgameAdAndLoadScene();
    }

    private void ShowMidgameAdAndLoadScene()
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
                // Fallback: load scene even if ad fails
                StartCoroutine(LoadScreenWithFadeEffect(1, sceneIndex));
            },
            () =>
            {
                Debug.Log("Midgame ad finished");
                StartCoroutine(LoadScreenWithFadeEffect(1, sceneIndex));
            }
        );
    }

    IEnumerator LoadScreenWithFadeEffect(float _delay, int _sceneIndex)
    {
        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(_sceneIndex);
    }

    public void LoadData(GameData _data)
    {
        if (_data != null)
        {
            this.sceneIndex = _data.sceneIndex;
            this.isControlShown = _data.isControlShown;
        }
        else
        {
            this.sceneIndex = 2;
            isControlShown = false;
        }

    }

    public void SaveData(ref GameData _data)
    {
        _data.sceneIndex = sceneIndex;
        _data.isControlShown = isControlShown;
    }

    public void SwitchOnEndScreen()
    {
        fadeScreen.FadeOut();
        StartCoroutine(EndScreen());
    }

    IEnumerator EndScreen()
    {
        yield return new WaitForSeconds(1.5f);
        fadeScreen.FadeIn();
        if (endPanel != null)
            endPanel.SetActive(true);
    }

    public void ReturnMenu()
    {
        SaveManager.instance.DeleteSavedData();
        StartCoroutine(LoadScreenWithFadeEffect(1, 0));
    }

    public void NewGamePlus()
    {
        sceneIndex = 2;
        StartCoroutine(LoadScreenWithFadeEffect(1, sceneIndex));
    }
}
