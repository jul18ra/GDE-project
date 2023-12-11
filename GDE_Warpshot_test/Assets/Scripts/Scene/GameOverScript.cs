using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public TMP_Text waveText;
    private AudioSource audioSource;
    public AudioClip startGameSound;
    public AudioClip buttonSound;

    private int finalWaveCount;
    private int mainMenu = 0;
    private int levelScene = 1;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        finalWaveCount = PlayerPrefs.GetInt("finalWaveCount");
        waveText.SetText($"You got to wave {finalWaveCount}");

    }

    public void OpenMainMenu()
    {
        StartCoroutine(NextScene(buttonSound, mainMenu));
    }

    public void Retry()
    {
        PlayerPrefs.SetInt("finalWaveCount", 0);
        StartCoroutine(NextScene(startGameSound, levelScene));

    }

    private IEnumerator NextScene(AudioClip sound, int sceneNumber)
    {
        audioSource.PlayOneShot(sound);
        yield return new WaitForSeconds(sound.length);
        SceneManager.LoadScene(sceneNumber);
    }
}
