using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class LoadingManager : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusic("MainMenu",true);
        StartBackEnd.RunBackEnd();
    }
    public void LoadMainGame()
    {
        AudioManager.Instance.PlayMusic("MainMenu", false);
        FindObjectOfType<LevelLoader>().LoadNextLevel("MainMenu");
    }

}
