using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private int levelScene = 1;
    public GameObject controlInfoPanel;
    private AudioSource audioSource;
    public AudioClip startGameSound;
    public AudioClip buttonSound;

    private void Start()
    {
        audioSource = Camera.main.GetComponent<AudioSource>();
        PlayerPrefs.SetInt("finalWaveCount", 0);
    }

    public void PlayGame()
    {
        audioSource.PlayOneShot(buttonSound);
        StartCoroutine("StartGame");
    }

    public void QuitGame()
    {
        audioSource.PlayOneShot(buttonSound);
        Application.Quit();
    }

    public void OpenControls()
    {
        audioSource.PlayOneShot(buttonSound);
        controlInfoPanel.SetActive(true);
    }

    public void CloseControls()
    {
        audioSource.PlayOneShot(buttonSound);
        controlInfoPanel.SetActive(false);
    }

    private IEnumerator StartGame()
    {
        audioSource.PlayOneShot(startGameSound);
        yield return new WaitForSeconds(startGameSound.length);
        SceneManager.LoadScene(levelScene);
    }
}
