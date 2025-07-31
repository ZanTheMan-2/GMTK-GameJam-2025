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

    }

    public void paperChange()
    {
        if (paperAmount < mainString.Length - 1) paperAmount += 1;
        else
        {
            Debug.Log("You win!");
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
}
