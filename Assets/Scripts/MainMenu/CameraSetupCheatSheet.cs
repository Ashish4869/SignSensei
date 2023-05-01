using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSetupCheatSheet : MonoBehaviour
{
    Animator _cheatSheetAnim;
    private void Awake()
    {
        _cheatSheetAnim = GetComponent<Animator>();
    }
    public void ShowCheatSheet()
    {
        AudioManager.Instance.PlaySFX("Paper");
        _cheatSheetAnim.SetTrigger("Show");
    }
    public void HideCheatSheet()
    {
        AudioManager.Instance.PlaySFX("Paper");
        _cheatSheetAnim.SetTrigger("Hide");
    }
}
