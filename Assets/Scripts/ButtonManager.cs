
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialText;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject creditsPanel;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] AudioClip selectSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();
    }

    public void PauseTheGame()
    {
        if (pauseMenu.activeSelf)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
       
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void RetsartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenSettingss()
    {
        if (settingsPanel.activeSelf)
        {
            settingsPanel.SetActive(false);
        }
        else
        {
            settingsPanel.SetActive(true);
        }
        audioSource.PlayOneShot(selectSound, 1);
    }

    public void OpenCredits()
    {
        if (creditsPanel.activeSelf)
        {
            creditsPanel.SetActive(false);
        }
        else
        {
            creditsPanel.SetActive(true);
        }
        audioSource.PlayOneShot(selectSound, 1);
    }

    public void HowToPlayTheGame()
    {
        audioSource.PlayOneShot(selectSound, 1);
        for (int i =0; i< tutorialText.Length; i++)
        {
            switch(tutorialText[i].activeSelf) {

                case true: tutorialText[i].SetActive(false); break;

                case false: tutorialText[i].SetActive(true); break;
            }
        }
    }




    // for the graphic settings
    public void LowGraphics()
    {
        QualitySettings.SetQualityLevel(0);
        audioSource.PlayOneShot(selectSound,1);
    }

    public void MediumGraphics()
    {
        QualitySettings.SetQualityLevel(1);
        audioSource.PlayOneShot(selectSound, 1);
    }

    public void HighGraphics()
    {
        QualitySettings.SetQualityLevel(2);
        audioSource.PlayOneShot(selectSound, 1);
    }

    public void UltraGraphics()
    {
        QualitySettings.SetQualityLevel(3);
        audioSource.PlayOneShot(selectSound, 1);
    }
}
