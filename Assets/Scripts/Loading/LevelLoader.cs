using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator _loader;
    float _transitionTime = 2f;
    

    public void LoadNextLevel(string level)
    {
        StartCoroutine(LoadLevel(level));
    }

    IEnumerator LoadLevel(string levelName)
    {
        //Stop this scene music
        AudioManager.Instance.PlayMusic(SceneManager.GetActiveScene().name, false);

        //Transition
        _loader.SetTrigger("Start");
        yield return new WaitForSeconds(_transitionTime);
        SceneManager.LoadScene(levelName);

        //Play NextSceneMusic
        AudioManager.Instance.PlayMusic(levelName, true);
    }
}
