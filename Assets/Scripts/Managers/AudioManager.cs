using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour, ISaveManager
{
    public static AudioManager instance;
    public AudioSource menuBGM;
    public AudioSource levelBGM;
    public AudioSource[] sfx;
    [SerializeField] private float sfxMinDist;
    public bool isPlaying;
    public bool isPaused;
    private float musicLevel = 0.3f;

    

    [SerializeField] private float fadeDuration = 2f;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }


    }

    private void Start()
    {
        SaveManager.instance.LoadGame();

        if (!isPlaying && SceneManager.GetActiveScene().buildIndex == 1)
            StartCoroutine(FadeInBGM(menuBGM));
        else if (!isPlaying && SceneManager.GetActiveScene().buildIndex > 0 && SceneManager.GetActiveScene().buildIndex < 11)
            StartCoroutine(FadeInBGM(levelBGM));

        isPaused = false;
    }

 

    public void PlaySFX(int index, Transform source)
    {
        if (sfx[index].isPlaying)
            return;

        if (source != null && Vector2.Distance(PlayerManager.instance.player.transform.position, source.position) > sfxMinDist)
            return;
        
        if (index < sfx.Length)
        {
            if (index != 0)
                sfx[index].pitch = UnityEngine.Random.Range(0.85f, 1.1f);

            sfx[index].Play();
        }
    }

    public void StopSFX(int index)
    {
        if(sfx[index] != null && sfx[index].isPlaying)
            sfx[index].Stop();
    }

    public void PauseAllSFX()
    {
        for (int i = 0; i < sfx.Length; i++)
        {
            if(sfx[i] != null)
                sfx[i].Pause();
        }
        isPaused = true;
    }


    private IEnumerator FadeInBGM(AudioSource _BGM)
    {
        isPlaying = true;
        _BGM.volume = 0f;
        _BGM.Play();

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            _BGM.volume = Mathf.Lerp(0f, musicLevel, timer / fadeDuration);
            yield return null;
        }

        _BGM.volume = musicLevel;
    }

    public IEnumerator FadeOutBGM(AudioSource _BGM)
    {
        float startVolume = _BGM.volume;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            _BGM.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }

        _BGM.Stop();
        isPlaying = false;
    }

    public void LoadData(GameData _data)
    {
        if (_data != null)
            this.musicLevel = _data.musicVolume;
        else
            this.musicLevel = 0.3f;
    }

    public void SaveData(ref GameData _data)
    {
        //implemented
    }
}
