using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonBehaviour : MonoBehaviour
{
    public void OnNextButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void OnTutorialButtonPressed()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
    public void OnMenuButtonPressed()
    {
        SceneManager.LoadScene("Start");
    }
}
