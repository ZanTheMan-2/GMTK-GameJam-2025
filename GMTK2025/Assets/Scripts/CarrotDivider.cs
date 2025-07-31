using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotDivider : MonoBehaviour
{
    public int dividerNumber;

    public void Cut()
    {
        GetComponentInParent<CarrotManager>().DividerDestroyed(dividerNumber);
    }
}
