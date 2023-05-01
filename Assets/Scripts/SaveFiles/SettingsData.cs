using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SettingsData
{
    public bool _music, _sfx, _powerUps;
    public int _difficulty;


    public SettingsData(Settings s)
    {
        _music = s._music;
        _sfx = s._sfx;
        _powerUps = s._powerUps;
        _difficulty = s._difficulty;
    }

}
