using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Awake : MonoBehaviour
{
    public TMP_Text text;
    public GameObject blink1, blink2, blank;
    public GameObject bedroomBG, bathroomBG, bathroom, office;
    public DirtSpawner dirtSpawner;
    bool dirtSpawned;
    public GameObject cloth, cursor;
    public Dialog officemanger;

    //Driving
    public GameObject driveScene, cookScene, driveCam, cookCam;

    public bool cleaned = false; // when this is true transition does

    private void Start()
    {
        blank.gameObject.SetActive(true);
        blink1.gameObject.SetActive(false);
        blink2.gameObject.SetActive(false);
        text.gameObject.SetActive(true);
        bathroomBG.gameObject.SetActive(false);
        bedroomBG.gameObject.SetActive(true);
        dirtSpawned = false;
        cloth.SetActive(false);
        cursor.SetActive(false);
        officemanger.enabled = false;
        office.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            blank.gameObject.SetActive(false);
            blink1.gameObject.SetActive(true);
            blink2.gameObject.SetActive(true);
            text.gameObject.SetActive(false);
            StartCoroutine(waiter());
        }

        if (cleaned == true)   // goes to office
        {
            cleaned = false;
            StartCoroutine(transitionWaiter(bathroom, office));
            officemanger.enabled = true;
        }
    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(2.5f);  //blinks
        blink1.gameObject.SetActive(false);
        blink2.gameObject.SetActive(false);
        blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0);
        blank.gameObject.SetActive(true);

        StartCoroutine(transitionWaiter(bedroomBG, bathroomBG));

    }

    IEnumerator transitionWaiter(GameObject currentBG, GameObject newBG)
    {
        for (float i = 0; i < 2; i += 0.05f) // fades black to transition
        {
            yield return new WaitForSeconds(0.05f);
            blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }
        currentBG.gameObject.SetActive(false);
        newBG.gameObject.SetActive(true);

        for (float i = 2; i > 0; i -= 0.05f) // open eyes
        {
            yield return new WaitForSeconds(0.05f);
            blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
            //Debug.Log(i);
            if (i >= 0.00000001f && newBG == bathroomBG && !dirtSpawned)
            {
                dirtSpawned = true;
                dirtSpawner.SpawnDirt();
                cloth.SetActive(true);
                cursor.SetActive(true);
            }
        }
    }

    public void DriveEnd()
    {
        StartCoroutine(DriveEndWaiter());
    }

    IEnumerator DriveEndWaiter()
    {
        driveScene.gameObject.SetActive(false);
        driveCam.gameObject.SetActive(false);
        cookScene.gameObject.SetActive(true);
        cookCam.gameObject.SetActive(true);

        for (float i = 2; i > 0; i -= 0.05f) // open eyes
        {
            yield return new WaitForSeconds(0.05f);
            blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }

        blank.SetActive(false);
    }
}
