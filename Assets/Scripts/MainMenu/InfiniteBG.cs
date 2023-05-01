using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfiniteBG : MonoBehaviour
{
    Image rightSign, leftSign;
    float slideValue = 10;
    private void Awake()
    {
        Image[] images = GetComponentsInChildren<Image>();
        rightSign = images[0];
        leftSign = images[1];

    }
    // Update is called once per frame
    void Update()
    {
        if(rightSign.transform.position.x > Screen.width*1.5)
        {
            rightSign.transform.position = new Vector3(-Screen.width/2, transform.position.y, transform.position.z);
        }

        if (leftSign.transform.position.x > Screen.width*1.5)
        {
            leftSign.transform.position = new Vector3(-Screen.width/2, transform.position.y, transform.position.z);
        }

        rightSign.transform.position = new Vector3(rightSign.transform.position.x + slideValue * Time.deltaTime, transform.position.y, transform.position.z);
        leftSign.transform.position = new Vector3(leftSign.transform.position.x + slideValue * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
