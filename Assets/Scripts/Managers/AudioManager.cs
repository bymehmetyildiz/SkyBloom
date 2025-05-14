using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource BGM;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private float sfxMinDist;
    public bool isPlaying;

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

        // No DontDestroyOnLoad here — BGM will reset each scene
    }

    private void Start()
    {
        if (!isPlaying && SceneManager.GetActiveScene().buildIndex == 0)
            StartCoroutine(FadeInBGM());
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
                sfx[index].pitch = Random.Range(0.85f, 1.1f);

            sfx[index].Play();
        }
    }

    public void StopSFX(int index) => sfx[index].Stop();

    private IEnumerator FadeInBGM()
    {
        isPlaying = true;
        BGM.volume = 0f;
        BGM.Play();

        float timer = 0f;
        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            BGM.volume = Mathf.Lerp(0f, 1f, timer / fadeDuration);
            yield return null;
        }

        BGM.volume = 1f;
    }

    public IEnumerator FadeOutBGM()
    {
        float startVolume = BGM.volume;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            BGM.volume = Mathf.Lerp(startVolume, 0f, timer / fadeDuration);
            yield return null;
        }

        BGM.Stop();
        isPlaying = false;
    }
}
