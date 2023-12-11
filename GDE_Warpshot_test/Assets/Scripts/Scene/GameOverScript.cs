using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public TMP_Text waveText;
    private int finalWaveCount;
    private int mainMenu = 0;
    private int levelScene = 1;

    // Start is called before the first frame update
    void Start()
    {
        finalWaveCount = PlayerPrefs.GetInt("finalWaveCount");
        waveText.SetText($"You got to wave {finalWaveCount}");

    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    public void Retry()
    {
        SceneManager.LoadScene(levelScene);
        PlayerPrefs.SetInt("finalWaveCount", 0);
    }

}
