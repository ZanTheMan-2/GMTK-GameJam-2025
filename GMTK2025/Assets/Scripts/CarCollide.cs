using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollide : MonoBehaviour
{
    public Awake manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinBox"))
        {
            Debug.Log("win");
            manager.DriveEnd();
        }
        if (other.CompareTag("LoseBox"))
        {
            Debug.Log("lose");
            manager.DriveDeath();
        }
    }

   
}
