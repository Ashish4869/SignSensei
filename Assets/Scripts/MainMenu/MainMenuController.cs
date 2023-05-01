using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Navigation system
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu;
    [SerializeField] GameObject _StatMenu;
    [SerializeField] GameObject _tutorial;
    [SerializeField] GameObject _settingsPage;
    [SerializeField] GameObject _cameraSetUp;
    [SerializeField] GameObject _creditsPage;

    LevelLoader _levelLoader;
    private void Awake()
    {
        _levelLoader = FindObjectOfType<LevelLoader>();
    }

    //Go to Training
    public void GoTraining()
    {
        _levelLoader.LoadNextLevel("Training");
        SetGameMode(true);
    }

    public void GoChallenge()
    {
        _levelLoader.LoadNextLevel("Challenge");
        SetGameMode(false);
    }

    //Quit game
    public void QuitGame()
    {
        KillBackEnd.QuitBackEnd();
        Application.Quit();
    }

    //Back to Main Menu
    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        _levelLoader.LoadNextLevel("MainMenu");
    }

    //Set Training Values
    public void SetGameMode(bool IsTrainingMode)
    {
        GameManager.Instance.SetGameMode(IsTrainingMode);
    }

    public void PlayHoverSFX()
    {
        GameManager.Instance.PlayHoverMusic();
    }

    public void PlayPressSound()
    {
        //AudioManager.Instance.PlaySFX("ButtonPress");
    }

    //Load Stats
    public void LoadStats()
    {
        _mainMenu.SetActive(false);
        _StatMenu.SetActive(true);
    }


    //Show Tutorial
    public void LoadTutorial()
    {
        _tutorial.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void LoadCameraSetUp()
    {
        GameManager.Instance.EnterCameraSetup(true);
        _cameraSetUp.SetActive(true);
        _mainMenu.SetActive(false);
    }

  
    public void LoadSettingsPage()
    {
        _settingsPage.SetActive(true);
        _mainMenu.SetActive(false);
    }

    public void LoadCreditsPage()
    {
        _creditsPage.SetActive(true);
        _mainMenu.SetActive(false);
    }

    

    //Route to main menu screen
    public void ReturnToMainMenu()
    {
        _mainMenu.SetActive(true);
        _StatMenu.SetActive(false);
        _tutorial.SetActive(false);
        _settingsPage.SetActive(false);
        _cameraSetUp.SetActive(false);
        _creditsPage.SetActive(false);
        GameManager.Instance.EnterCameraSetup(false);
    }
    
}
