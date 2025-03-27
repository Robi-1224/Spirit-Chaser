using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialText;

    public void RetsartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlayTheGame()
    {
        for(int i =0; i< tutorialText.Length; i++)
        {
            switch(tutorialText[i].activeSelf) {

                case true: tutorialText[i].SetActive(false); break;

                case false: tutorialText[i].SetActive(true); break;
            }
        }
    }
}
