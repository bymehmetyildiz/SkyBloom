using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour, ISaveManager
{
    public static LevelManager instance;
    public int sceneIndex;

    private void Awake()
    {
        if (instance != null) 
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            sceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            SaveManager.instance.SaveGame();

            StartCoroutine(LoadNewScene());
        } 
    }

    private IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(sceneIndex);
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
}
