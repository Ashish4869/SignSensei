using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public bool _music = true, _sfx = true, _powerUps = true;
    public int _difficulty = 0;

    public void ChangeMusicSetting(bool condition) => _music = condition;
    public void ChangeSFXSetting(bool condition) => _sfx = condition;
    public void ChangePowerUpSetting(bool condition) => _powerUps = condition;
    public void ChangeDifficultySetting(int condition) => _difficulty = condition;

}
