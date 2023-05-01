using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Spins the handle of the UI.
/// </summary>


public class TimerHandle : MonoBehaviour
{
    float _currentTime = 0f;

    private void Awake()
    {
        EventManager.OnHandSignMatchedinTraining += ResetHandle;
    }
    private void Update()
    {
        _currentTime += Time.deltaTime;

        if (_currentTime >= 1)
        {
            AudioManager.Instance.PlaySFX("Tick");
            _currentTime = 0f;
        }

        gameObject.transform.rotation = Quaternion.Euler(0, 0, _currentTime * -360);
    }

    void ResetHandle()
    {
        _currentTime = 0f;
    }

    private void OnDestroy()
    {
        EventManager.OnHandSignMatchedinTraining -= ResetHandle;
    }

    public void StopTimerSound()
    {
        _currentTime = -100;
    }


}
