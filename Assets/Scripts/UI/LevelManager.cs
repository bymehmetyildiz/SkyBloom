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

        if(endPanel != null )
            endPanel.SetActive(false);

 
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

        StartCoroutine(AudioManager.instance.FadeOutBGM(AudioManager.instance.levelBGM));
        StartCoroutine(LoadNewScene());

        SaveManager.instance.SaveGame();
    }


    private IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(LoadScreenWithFadeEffect(1, sceneIndex));
        
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
            this.sceneIndex = _data.sceneIndex;
        else
            this.sceneIndex = 1;
    }

    public void SaveData(ref GameData _data)
    {
        _data.sceneIndex = sceneIndex;
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
