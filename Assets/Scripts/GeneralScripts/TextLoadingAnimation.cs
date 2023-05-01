using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextLoadingAnimation : MonoBehaviour
{
    [SerializeField] string _stringToAnimate;
    TextMeshProUGUI _loading;

    private void Start()
    {
        _loading = GetComponent<TextMeshProUGUI>();
        StartCoroutine(AnimateText(_stringToAnimate));
    }

    IEnumerator AnimateText(string loader)
    {
        int dotCount = 1;
        while (true)
        {
            if (dotCount == 4)
            {
                dotCount = 1;
                _loading.text = loader + " .";
            }


            yield return new WaitForSeconds(0.5f);
            _loading.text += " .";
            dotCount++;
        }
    }
}
