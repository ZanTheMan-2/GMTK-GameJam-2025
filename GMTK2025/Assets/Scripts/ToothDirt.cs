using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothDirt : MonoBehaviour
{
    public int health;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health == 0)
        {
            GetComponentInParent<DirtSpawner>().dirts.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void Clean()
    {
        health--;
    }
}
