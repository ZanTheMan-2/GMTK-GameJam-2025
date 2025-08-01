using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaperScript : MonoBehaviour
{
    public int moneyAmount, sanityAmount;

    public TMP_Text mainText, bonusText;
    public string[] mainString;
    public int[] amountString;
    public int amountCost;

    public GameObject blank, officeScene, drivingScene, arms, cam;
    public int paperAmount;

    public Transform putDownSpot, paperParent;
    public bool paperInPlace;

    public int[] moneyGainIfYes, sanityGainIfYes, moneyGainIfNo, sanityGainIfNo;

    private void Update()
    {
        mainText.text = mainString[paperAmount];
        bonusText.text = amountString[paperAmount].ToString();
        amountCost = amountString[paperAmount];

        if (Input.GetKeyDown(KeyCode.L)) paperChange();
        if (paperAmount == 8) StartCoroutine(win(officeScene, drivingScene));
    }

    public void paperChange()
    {
        if (paperAmount < mainString.Length - 1) paperAmount += 1;
        else
        {
            StartCoroutine(win(officeScene, drivingScene));
        }
    }

    public void paperReset()
    {
        transform.parent = paperParent;
        transform.localPosition = Vector3.zero;
        GetComponent<Collider2D>().enabled = true;
        putDownSpot.GetComponent<Collider2D>().enabled = true;
    }

    public void Yes()
    {
        //Add money
        moneyAmount += moneyGainIfYes[paperAmount];
        //Add sanity
        sanityAmount += sanityGainIfYes[paperAmount];
        
        Debug.Log("Oooo you're insane");
        paperInPlace = false;
    }

    public void No()
    {
        //Add money
        moneyAmount += moneyGainIfNo[paperAmount];
        //Add sanity
        sanityAmount += sanityGainIfNo[paperAmount];

        Debug.Log("Oooo you're poor");
        paperInPlace = false;
    }


    IEnumerator win(GameObject currentBG, GameObject newBG)
    {
        cam.gameObject.SetActive(false);
        
        for (float i = 0; i < 2; i += 0.05f) // fades black to transition
        {
            yield return new WaitForSeconds(0.05f);
            blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }

        newBG.gameObject.SetActive(true);
        arms.gameObject.SetActive(false);

        for (float i = 2; i > 0; i -= 0.05f) // open eyes
        {
            yield return new WaitForSeconds(0.05f);
            blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }
        currentBG.gameObject.SetActive(false);
    }
}
