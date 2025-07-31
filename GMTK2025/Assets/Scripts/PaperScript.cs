using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PaperScript : MonoBehaviour
{
    public TMP_Text mainText, bonusText;
    public string[] mainString;
    public string[] amountString;

    private void Start()
    {
        mainText.text = mainString[0];
        bonusText.text = amountString[0];
    }
}
