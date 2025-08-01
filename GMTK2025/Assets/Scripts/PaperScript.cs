using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaperScript : MonoBehaviour
{
    public Awake manager;
    public int moneyAmount, sanityAmount, initMoney, initSanity;

    public TMP_Text mainText, bonusText, budgetTXT, sanityTXT;
    public string[] mainString;
    public int[] amountString;
    public int amountCost;

    public GameObject blank, officeScene, drivingScene, arms, cam;
    public int paperAmount;

    public Transform putDownSpot, paperParent;
    public bool paperInPlace;

    public int[] moneyGainIfYes, sanityGainIfYes, moneyGainIfNo, sanityGainIfNo;

    private void Start()
    {
        moneyAmount = initMoney;
        sanityAmount = initSanity;
    }

    private void Update()
    {
        sanityAmount = Mathf.Clamp(sanityAmount, 0, 100);

        mainText.text = mainString[paperAmount];
        bonusText.text = amountString[paperAmount].ToString();
        amountCost = amountString[paperAmount];
        budgetTXT.text = "Budget: " + moneyAmount.ToString();
        sanityTXT.text = "Sanity: " + sanityAmount.ToString();

        if (Input.GetKeyDown(KeyCode.L)) paperChange();
        if (paperAmount == mainString.Length) manager.OfficeWin();
    }

    public void paperChange()
    {
        if (paperAmount < mainString.Length - 1) paperAmount += 1;
        else
        {
            manager.OfficeWin();
        }
    }

    public void paperReset()
    {
        transform.parent = paperParent;
        transform.localPosition = Vector3.zero;
        GetComponent<Collider2D>().enabled = true;
        putDownSpot.GetComponent<Collider2D>().enabled = true;
    }

    public void Yes(Transform stamp)
    {
        if ((moneyAmount + moneyGainIfYes[paperAmount]) >= 0)
        {
            //Add money
            moneyAmount += moneyGainIfYes[paperAmount];
            //Add sanity
            sanityAmount += sanityGainIfYes[paperAmount];

            paperInPlace = false;
            GetComponentInParent<Animator>().SetTrigger("paperRight");
            stamp.GetComponent<Interactable>().paperInPlace = false;
            stamp.GetComponent<Interactable>().currentYesBox = null;

        }
        else
        {
            Debug.Log("too poor");
            return;
        }
    }

    public void No()
    {
        //Add money
        moneyAmount += moneyGainIfNo[paperAmount];
        //Add sanity
        sanityAmount += sanityGainIfNo[paperAmount];

        paperInPlace = false;
    }
}
