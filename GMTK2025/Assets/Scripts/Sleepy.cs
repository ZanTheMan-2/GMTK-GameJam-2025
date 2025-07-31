using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sleepy : MonoBehaviour
{
    public GameObject topBlink, bottomBlink;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            topBlink.gameObject.SetActive(true);
            bottomBlink.gameObject.SetActive(true);
        }
    }
}
