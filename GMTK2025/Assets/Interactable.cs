using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public bool isHoldable, beingHeld;
    public enum ObjectType
    {
        Toothbrush,
        Wheel,
        Paper,
        Stamp,
        Knife,
        Booze
    }
    public ObjectType thisType;
    public GameObject putDownSpot;

    [Header("Toothbrush")]
    bool brushMode;

    [Header("Driving")]
    public bool handOnWheel;

    [Header("Paperwork")]
    public bool paperInPlace;
    Transform currentYesBox, currentNoBox;
    public bool overNoBox, overYesBox;

    [Header("Carrots")]
    bool overCarrot;
    Transform currentCarrotDivider;

    [Header("Booze")]
    public bool overPutDown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!beingHeld) return;

        if(thisType == ObjectType.Toothbrush)
        {
            if (Input.GetMouseButtonDown(0))
            {
                brushMode = true;
            }
            if (Input.GetMouseButtonUp(0))
            {
                brushMode = false;
            }
        }

        if (thisType == ObjectType.Wheel)
        {
            //Rotate
        }

        if (thisType == ObjectType.Stamp)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (overNoBox && currentNoBox.GetComponentInParent<PaperStamp>().paperInPlace)
                {
                    //No
                    currentNoBox.GetComponentInParent<PaperStamp>().No();
                    //Move paper away...

                    overNoBox = false;
                    currentNoBox = null;
                    putDownSpot.GetComponent<Collider2D>().enabled = true;
                }
                if (overYesBox && currentYesBox.GetComponentInParent<PaperStamp>().paperInPlace)
                {
                    //Yes
                    currentYesBox.GetComponentInParent<PaperStamp>().Yes();
                    //Move paper away...

                    overYesBox = false;
                    currentYesBox = null;
                    putDownSpot.GetComponent<Collider2D>().enabled = true;
                }
            }
        }

        if (thisType == ObjectType.Knife)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (overCarrot)
                {
                    //Cut
                    //currentCarrotDivider.GetComponent<SpriteRenderer>().enabled = false;
                    currentCarrotDivider.GetComponent<CarrotDivider>().Cut();
                    //currentCarrotDivider.GetComponent<Collider2D>().enabled = false;
                    overCarrot = false;
                    currentCarrotDivider = null;
                }
            }
        }

        if (thisType == ObjectType.Booze)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (!overPutDown)
                {
                    //Drink
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(thisType == ObjectType.Toothbrush && brushMode && collision.CompareTag("Dirt"))
        {
            collision.GetComponent<ToothDirt>().Clean();
        }

        if(thisType == ObjectType.Stamp && collision.CompareTag("NoBox"))
        {
            overNoBox = true;
            currentNoBox = collision.transform;
        }
        if (thisType == ObjectType.Stamp && collision.CompareTag("YesBox"))
        {
            overYesBox = true;
            currentYesBox = collision.transform;
        }

        if (thisType == ObjectType.Knife && collision.CompareTag("CarrotDivider"))
        {
            overCarrot = true;
            currentCarrotDivider = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (thisType == ObjectType.Stamp && collision.CompareTag("NoBox"))
        {
            overNoBox = false;
            currentNoBox = null;
        }
        if (thisType == ObjectType.Stamp && collision.CompareTag("YesBox"))
        {
            overYesBox = false;
            currentYesBox = null;
        }

        if (thisType == ObjectType.Knife && collision.CompareTag("CarrotDivider"))
        {
            overCarrot = false;
            currentCarrotDivider = null;
        }
    }
}
