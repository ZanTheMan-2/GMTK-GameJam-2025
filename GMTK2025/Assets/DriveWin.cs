using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DriveWin : MonoBehaviour
{
    public GameObject blank, driveScene, coockSceene, cam;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("win");
        StartCoroutine(transitionWaiter(driveScene, coockSceene));
    }

    IEnumerator transitionWaiter(GameObject currentBG, GameObject newBG)
    {
        for (float i = 0; i < 2; i += 0.05f) // fades black to transition
        {
            yield return new WaitForSeconds(0.05f);
            blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }

        newBG.gameObject.SetActive(true);
        cam.gameObject.SetActive(true);

        for (float i = 2; i > 0; i -= 0.05f) // open eyes
        {
            yield return new WaitForSeconds(0.05f);
            blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }
        currentBG.gameObject.SetActive(false);
    }
}
