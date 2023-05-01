using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Marquee : MonoBehaviour
{
    float _speed = 10f;

    private void Update()
    {
        transform.position = new Vector3(transform.position.x + -40 * Time.deltaTime,
            transform.position.y,
            transform.position.z);
    }
}
