using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeManeger : MonoBehaviour
{
    public Dialog talked;
    public GameObject currentBG, newBG, textBox, blank;
    private bool ran = true;


    private void Start()
    {
        currentBG.gameObject.SetActive(true);
        newBG.gameObject.SetActive(false);
        textBox.gameObject.SetActive(true);
        blank.gameObject.SetActive(false);
        blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0);

    }
    void Update()
    {
        if (talked.doneTalking && ran) StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        ran = false;
        blank.gameObject.SetActive(true);
        textBox.gameObject.SetActive(false);
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
            Debug.Log(i);
        }
    }
}

