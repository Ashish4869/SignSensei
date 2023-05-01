using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the triggering for animation for all Powerup related UI.
/// </summary>

public class PowerUpUIManager : MonoBehaviour
{
    [SerializeField] Animator _inactiveBlue;
    [SerializeField] Animator _activeBlue;
    [SerializeField] Animator _inactiveGreen;
    [SerializeField] Animator _activeGreen;
    [SerializeField] Animator _inactiveRed;
    [SerializeField] Animator _activeRed;

    Animator _TvScreen, _Veins, _flag;
    private void Awake()
    {
        //cache values
        _flag = GameObject.Find("Flag").GetComponent<Animator>();
        _TvScreen = transform.Find("TV/OffScreen").gameObject.GetComponent<Animator>();
        _Veins = transform.Find("Veins").gameObject.GetComponent<Animator>();
    }

    public void ActivatePowerUp(int PowerUp)
    {
        if(PowerUp == 1) //Slow power up
        {
            _inactiveBlue.SetTrigger("InactiveFadeOut");
            _activeBlue.SetTrigger("ActiveFadeIn");
        }
        else if(PowerUp == 2) //2X power up
        {
            _inactiveRed.SetTrigger("InactiveFadeOut");
            _activeRed.SetTrigger("ActiveFadeIn");

            _Veins.SetTrigger("Show");
            _flag.SetTrigger("Raise");
        }
        else // TV power up
        {
            _inactiveGreen.SetTrigger("InactiveFadeOut");
            _activeGreen.SetTrigger("ActiveFadeIn");
            _TvScreen.SetTrigger("ON");
        }
    }


    public void DeactivatePowerUp(int PowerUp)
    {
        if (PowerUp == 1) //Slow power up
        {
            _inactiveBlue.SetTrigger("InactiveFadeIn");
            _activeBlue.SetTrigger("ActiveFadeOut");
        }
        else if (PowerUp == 2) // 2X power up
        {
            _inactiveRed.SetTrigger("InactiveFadeIn");
            _activeRed.SetTrigger("ActiveFadeOut");

            _Veins.SetTrigger("Hide");
            _flag.SetTrigger("Drop");
        }
        else //TV power up
        {
            _inactiveGreen.SetTrigger("InactiveFadeIn");
            _activeGreen.SetTrigger("ActiveFadeOut");
            _TvScreen.SetTrigger("OFF");
        }
    }
}
