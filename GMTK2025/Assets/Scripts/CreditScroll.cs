using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditScroll : MonoBehaviour
{
    public float scrollSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.up * scrollSpeed * Time.deltaTime;
    }
}
