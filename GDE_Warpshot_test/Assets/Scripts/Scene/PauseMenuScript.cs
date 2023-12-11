using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuScript : MonoBehaviour
{
    public GameObject pauseMenu;
    private int mainMenu = 0;
    private bool paused = false;
    private AudioSource audioSource;
    public AudioClip buttonSound;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if(paused) 
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    private void PauseGame()
    {
        AudioListener.volume = 0;
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        AudioListener.volume = 1;
        audioSource.PlayOneShot(buttonSound);
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void ToMainMenu()
    {
        StartCoroutine("MainMenu");
    }

    private IEnumerator MainMenu()
    {
        Time.timeScale = 1;
        AudioListener.volume = 1;
        audioSource.PlayOneShot(buttonSound);
        yield return new WaitForSeconds(buttonSound.length);
        SceneManager.LoadScene(mainMenu);
    }
}
