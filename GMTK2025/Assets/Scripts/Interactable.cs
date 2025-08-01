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
    public Transform currentYesBox, currentNoBox;
    public bool overNoBox, overYesBox, clicked;

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
                if (overNoBox && currentNoBox.GetComponentInParent<PaperScript>().paperInPlace)
                {
                    //No
                    currentNoBox.GetComponentInParent<PaperScript>().No();
                    //Move paper away...
                    currentNoBox.GetComponentInParent<Animator>().SetTrigger("paperLeft");
                    paperInPlace = false;
                    overNoBox = false;
                    currentNoBox = null;
                }
                if (overYesBox && currentYesBox.GetComponentInParent<PaperScript>().paperInPlace)
                {
                    //Yes
                    currentYesBox.GetComponentInParent<PaperScript>().Yes(transform);
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
