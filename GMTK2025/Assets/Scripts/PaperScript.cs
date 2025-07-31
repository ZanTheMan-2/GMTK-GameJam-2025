using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaperScript : MonoBehaviour
{
    public TMP_Text mainText, bonusText;
    public string[] mainString;
    public int[] amountString;
    public int amountCost;

    public int paperAmount;


    private void Update()
    {
        mainText.text = mainString[paperAmount];
        bonusText.text = amountString[paperAmount].ToString();
        amountCost = amountString[paperAmount];

        if (Input.GetKeyDown(KeyCode.L)) paperChange();

    }

    public void paperChange()
    {
        paperAmount += 1;
    }
}
