using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Awake : MonoBehaviour
{
    public GameObject weekText, sleepText;
    bool ableToSpace;
    bool spaceClicked;
    public bool canCursor;
    public TMP_Text text, carCrashTXT;
    public GameObject blink1, blink2, blank, blank2, driveBlank;
    public GameObject bedroomBG, bathroomBG, bathroom, office;
    public DirtSpawner dirtSpawner;
    bool dirtSpawned;
    public GameObject cloth, cursor;
    public Dialog officemanger;
    public AudioSource audioSource, carSource, zangySource;
    //Driving
    public GameObject driveScene, cookScene, driveCam, driveCam3D, cookCam;

    //Office
    public GameObject officeText, officeScene, paperCanva;

    public bool cleaned = false; // when this is true transition does

    private void Start()
    {
        sleepText.SetActive(false);
        weekText.SetActive(false);
        ableToSpace = false;
        spaceClicked = false;
        canCursor = false;
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

        audioSource.Play();
        Color TXTcolor = carCrashTXT.color;
        TXTcolor.a = 0;
        carCrashTXT.color = TXTcolor;
        StartCoroutine(weekTextVisible());
    }

    IEnumerator weekTextVisible()
    {
        weekText.SetActive(true);
        TMP_Text weekTXT = weekText.GetComponentInChildren<TMP_Text>();
        for (float i = 0; i < 1; i += 0.05f) // text fade
        {
            yield return new WaitForSeconds(0.05f);
            Color TXTcolor = weekTXT.color;
            TXTcolor.a = i;
            weekTXT.color = TXTcolor;
        }
        yield return new WaitForSeconds(0.5f);
        sleepText.SetActive(true);
        ableToSpace = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GetComponent<AudioSource>().isPlaying && !spaceClicked && ableToSpace)
        {
            spaceClicked = true;
            weekText.SetActive(false);
            sleepText.SetActive(false);
            blank.gameObject.SetActive(false);
            blink1.gameObject.SetActive(true);
            blink2.gameObject.SetActive(true);
            text.gameObject.SetActive(false);
            StartCoroutine(waiter());

            audioSource.Stop();
            GetComponent<AudioSource>().Play();
        }

        if (cleaned == true)   // goes to office
        {
            cleaned = false;
            StartCoroutine(transitionWaiterToOffice());
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
        dirtSpawned = true;
        dirtSpawner.SpawnDirt();
        cloth.SetActive(true);
        cursor.SetActive(true);

        for (float i = 2; i > 0; i -= 0.05f) // open eyes
        {
            yield return new WaitForSeconds(0.05f);
            blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }

        canCursor = true;
    }

    IEnumerator transitionWaiterToOffice()
    {
        yield return new WaitForSeconds(1.5f);

        blank2.SetActive(true);
        blank2.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0);
        canCursor = false;

        for (float i = 0; i < 2; i += 0.05f) // fades black to transition
        {
            yield return new WaitForSeconds(0.05f);
            blank2.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }

        bathroom.gameObject.SetActive(false);
        office.gameObject.SetActive(true);
        officeText.SetActive(false);
        officemanger.gameObject.SetActive(false);

        for (float i = 2; i > 0; i -= 0.05f) // open eyes
        {
            yield return new WaitForSeconds(0.05f);
            blank2.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
            GetComponent<AudioSource>().volume = i / 2;
        }

        GetComponent<AudioSource>().Pause();
        blank2.SetActive(false);
        officeText.SetActive(true);
        officemanger.gameObject.SetActive(true);
    }

    public void DriveEnd()
    {
        StartCoroutine(DriveEndWaiter());
    }

    IEnumerator DriveEndWaiter()
    {
        driveBlank.gameObject.SetActive(true);
        driveBlank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0);
        for (float i = 0; i < 2; i += 0.05f) // fades black to transition
        {
            yield return new WaitForSeconds(0.05f);
            driveBlank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }

        blank2.SetActive(true);
        blank2.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 2);
        driveScene.gameObject.SetActive(false);
        driveCam.gameObject.SetActive(false);
        driveCam3D.SetActive(false);
        cookCam.gameObject.SetActive(true);
        cookScene.gameObject.SetActive(true);

        for (float i = 2; i > 0; i -= 0.05f) // open eyes
        {
            yield return new WaitForSeconds(0.05f);
            blank2.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }

        driveBlank.SetActive(false);
        blank2.SetActive(false);
        canCursor = true;
    }

    public void OfficeWin()
    {
        blank2.gameObject.SetActive(true);
        blank2.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0);
        StartCoroutine(OfficeBlink());
    }

    IEnumerator OfficeBlink()
    {
        canCursor = false;
        paperCanva.SetActive(false);
        //arms.gameObject.SetActive(false);

        for (float i = 0; i < 2; i += 0.05f) // fades black to transition
        {
            yield return new WaitForSeconds(0.05f);
            blank2.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }

        driveCam.SetActive(true);
        driveCam3D.SetActive(true);
        driveBlank.SetActive(true);
        driveBlank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1);
        cookCam.gameObject.SetActive(false);
        blank2.SetActive(false);
        officeScene.gameObject.SetActive(false);
        driveScene.gameObject.SetActive(true);

        if (zangySource.clip != null)
        {
            for (float i = 2; i > 0; i -= 0.05f) // open eyes
            {
                yield return new WaitForSeconds(0.05f);
                driveBlank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
                GetComponent<AudioSource>().volume = i / 2;
            }
            GetComponent<AudioSource>().Pause();
            zangySource.Play();
            canCursor = true;
            driveBlank.SetActive(false);
            yield return new WaitForSeconds(zangySource.clip.length);
            GetComponent<AudioSource>().UnPause();
            for (float i = 0; i < 2; i += 0.05f) // open eyes
            {
                yield return new WaitForSeconds(0.05f);
                GetComponent<AudioSource>().volume = i / 2;
            }
        }
        else
        {
            canCursor = true;
            driveBlank.SetActive(false);
        }
    }

    public void DriveDeath()
    {
        StartCoroutine(DriveDeathWaiter());
    }

    IEnumerator DriveDeathWaiter()
    {
        driveBlank.gameObject.SetActive(true);
        driveBlank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1);
        carSource.Play();

        driveScene.gameObject.SetActive(false);

        carCrashTXT.transform.parent.gameObject.SetActive(true);
        carCrashTXT.gameObject.SetActive(true);
        for (float i = 0; i < 1; i += 0.05f) // text fade
        {
            yield return new WaitForSeconds(0.05f);
            Color TXTcolor = carCrashTXT.color;
            TXTcolor.a = i;
            carCrashTXT.color = TXTcolor;
        }

        yield return new WaitForSeconds(5);
        for (float i = 1; i > 0; i -= 0.05f) // text fade
        {
            yield return new WaitForSeconds(0.05f);
            Color TXTcolor = carCrashTXT.color;
            TXTcolor.a = i;
            carCrashTXT.color = TXTcolor;
        }
        driveBlank.SetActive(false);
        carCrashTXT.transform.parent.gameObject.SetActive(false);
        carCrashTXT.gameObject.SetActive(false);
        blank2.SetActive(true);
        blank2.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1);
        driveCam.gameObject.SetActive(false);
        driveCam3D.SetActive(false);
        cookCam.gameObject.SetActive(true);
        //Set death scene to true
        cookScene.SetActive(true);

        for (float i = 2; i > 0; i -= 0.05f) // open eyes
        {
            yield return new WaitForSeconds(0.05f);
            blank2.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }

        blank2.SetActive(false);
        canCursor = true;
    }
}
