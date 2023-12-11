using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    private int levelScene = 1;
    public GameObject controlInfoPanel;

    private void Start()
    {
        PlayerPrefs.SetInt("finalWaveCount", 0);
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(levelScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenControls()
    {
        controlInfoPanel.SetActive(true);
    }

    public void CloseControls()
    {
        controlInfoPanel.SetActive(false);
    }
}
