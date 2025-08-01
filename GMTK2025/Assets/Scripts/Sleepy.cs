using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleepy : MonoBehaviour
{
    public GameObject topBlink, bottomBlink, text;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            text.gameObject.SetActive(false);
            topBlink.gameObject.SetActive(true);
            bottomBlink.gameObject.SetActive(true);
        }
    }
}
