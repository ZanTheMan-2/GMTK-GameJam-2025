using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToothDirt : MonoBehaviour
{
    int health;

    // Start is called before the first frame update
    void Start()
    {
        health = Random.Range(3, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0) Destroy(gameObject);
    }

    public void Clean()
    {
        health--;
    }
}
