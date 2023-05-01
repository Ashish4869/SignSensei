using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMainMenuAftertCredits : MonoBehaviour
{
    [SerializeField] GameObject _mainMenuButton;

    public void ShowMainMenuButton()
    {
        _mainMenuButton.SetActive(true);
    }

    public void HideMainMenuButton()
    {
        _mainMenuButton.SetActive(false);
    }
}
