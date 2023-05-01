using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRobotDialougeInMainMenu : MonoBehaviour
{
    //cache reference to the animator of the bot
    [SerializeField] Animator _orangeBotComment;
   

    public void TriggerRobotDialougeAnimation()
    {
        if (_orangeBotComment == null) return;

        _orangeBotComment.SetTrigger(gameObject.name);
    }
}
