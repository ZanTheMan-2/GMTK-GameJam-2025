using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotDivider : MonoBehaviour
{
    public Transform leftSegment, rightSegment;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cut()
    {
        rightSegment.position += Vector3.left * -3;
    }
}
