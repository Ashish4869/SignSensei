using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraSetupManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _handSignText;
    [SerializeField] TextMeshProUGUI _accuracyText;

    private void Update()
    {
        _handSignText.text = GameManager.Instance.GetCurrentCharacterForCameraSetup();
        _accuracyText.text = " Accuracy: "
            + decimal.Round((decimal)GameManager.Instance.GetCurrentAccuracyForCameraSetup(),1) 
            + "%";
    }
}
