using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollide : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinBox"))
        {
            //Win
        }
        if (other.CompareTag("LoseBox"))
        {
            //Lose
        }
    }
}
