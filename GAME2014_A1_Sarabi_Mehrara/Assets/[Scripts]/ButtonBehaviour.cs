/*
 ButtonBehaviour.cs
Author: Mehrara Sarabi 
Student ID: 101247463
Last modified: 2021-10-04
Description: This program contains all the button behaviours needed for the game. Including menu, tutorial, next, back, exit buttons.
 
 */

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
