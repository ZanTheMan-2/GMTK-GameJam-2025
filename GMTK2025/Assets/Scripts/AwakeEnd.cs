using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeEnd : MonoBehaviour
{
    bool spaceClicked1, spaceClicked2;
    public bool canCursor;
    public GameObject blink1, blink2, blank, blank2;
    public GameObject bedroomBG, bridgeBG, waterBG;
    public GameObject sleepText, bridgeText, credits;
    public AudioSource audioSource;
    bool onBridge;

    // Start is called before the first frame update
    void Start()
    {
        spaceClicked1 = false;
        spaceClicked2 = false;
        Cursor.visible = false;
        canCursor = false;
        blank.gameObject.SetActive(true);
        blink1.gameObject.SetActive(false);
        blink2.gameObject.SetActive(false);
        bridgeBG.gameObject.SetActive(false);
        bedroomBG.gameObject.SetActive(true);

        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !GetComponent<AudioSource>().isPlaying && !spaceClicked1)
        {
            spaceClicked1 = true;
            sleepText.SetActive(false);
            blank.gameObject.SetActive(false);
            blink1.gameObject.SetActive(true);
            blink2.gameObject.SetActive(true);
            StartCoroutine(waiter());

            audioSource.Stop();
            GetComponent<AudioSource>().Play();
        }

        if (Input.GetKeyDown(KeyCode.Space) && onBridge && !spaceClicked2)
        {
            spaceClicked2 = true;
            StartCoroutine(bridgeWaiter(bridgeBG, waterBG));
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(2.5f);  //blinks
        blink1.gameObject.SetActive(false);
        blink2.gameObject.SetActive(false);
        blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0);
        blank.gameObject.SetActive(true);

        StartCoroutine(transitionWaiter(bedroomBG, bridgeBG));

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
        }
        bridgeText.SetActive(true);
        onBridge = true;
    }

    IEnumerator bridgeWaiter(GameObject currentBG, GameObject newBG)
    {
        bridgeText.SetActive(false);
        onBridge = false;
        blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0);
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
        }

        credits.SetActive(true);
    }
}
