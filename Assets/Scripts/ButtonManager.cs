
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialText;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] AudioClip selectSound;

    private AudioSource audioSource;


    private void Awake()
    {
        audioSource= GetComponent<AudioSource>();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
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
