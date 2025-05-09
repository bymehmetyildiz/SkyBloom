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
    [Space]

    public static UI_Controller instance;

    [SerializeField] private GameObject[] inventoryElemnets;
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
        
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Tab) && PlayerManager.instance.player.stats.isDead == false)
            SwitchWithKey(inventoryElemnets[0]);
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
        for (int i = 0; i < inventoryElemnets.Length; i++)
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
    public void ReturnToMenu() => GameManager.instance.ReturnToMenu();
    public void Save() => SaveManager.instance.SaveGame();
    public void Resume() => CheckInGameUI();
}
