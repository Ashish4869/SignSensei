using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Shows feedback for each robot
/// </summary>

public class RobotFeedback : MonoBehaviour
{
    Animator _chatBubble;

    private void Awake()
    {
        _chatBubble = GetComponent<Animator>();
    }

    public void ShowChatBubble(int mode)
    {
        if (mode == 0) _chatBubble.SetTrigger("Perfect");
        else if (mode == 1) _chatBubble.SetTrigger("Great");
        else _chatBubble.SetTrigger("Missed");

    }
}
