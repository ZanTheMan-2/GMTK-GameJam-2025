using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotDivider : MonoBehaviour
{
    public int dividerNumber;

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
        GetComponentInParent<CarrotManager>().DividerDestroyed(dividerNumber);
    }
}
