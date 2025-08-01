using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollide : MonoBehaviour
{

    public GameObject blank;
    public Awake manager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WinBox"))
        {
            Debug.Log("win");
            StartCoroutine(transitionWaiter());
        }
        if (other.CompareTag("LoseBox"))
        {
            //Lose
        }
    }

    IEnumerator transitionWaiter()
    {
        blank.SetActive(true);
        for (float i = 0; i < 2; i += 0.05f) // fades black to transition
        {
            yield return new WaitForSeconds(0.05f);
            blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }

        manager.DriveEnd();
    }
}
