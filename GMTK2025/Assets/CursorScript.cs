using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    Transform cursorTransform;
    public bool overInteractable, overPutDownSpot, holdingObject;
    public Transform currentObject, currentPutDownSpot;

    // Start is called before the first frame update
    void Start()
    {
        cursorTransform = transform;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingObject && currentObject == null)
        {
            currentObject = transform.GetChild(1);
            overInteractable = true;
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        cursorTransform.position = new Vector3(mousePos.x, mousePos.y, 0);
        if (overInteractable && currentObject.GetComponent<Interactable>().isHoldable)
        {
            //Change sprite to point
            if (Input.GetMouseButtonDown(0) && !holdingObject)
            {
                //Pick up
                currentObject.GetComponent<Interactable>().beingHeld = true;
                holdingObject = true;
                if (currentObject.GetComponent<Interactable>().putDownSpot != null) currentObject.GetComponent<Interactable>().putDownSpot.GetComponent<Collider2D>().enabled = true;
                if (currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Toothbrush)
                {
                    //Change sprite to holding cloth
                }
                if (currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Wheel)
                {
                    //Change sprite to hand on wheel
                }
                if (currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Knife)
                {
                    //Change sprite to holding knife
                }
                else
                {
                    currentObject.parent = transform;
                    currentObject.localPosition = Vector3.zero;
                }
            }
            if (Input.GetMouseButtonDown(0) && overPutDownSpot)
            {
                //Put down
                if (currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Wheel)
                {
                    currentObject.GetComponent<Interactable>().beingHeld = false;
                    holdingObject = false;
                    //Change to open sprite
                }
                if (currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Paper)
                {
                    if (currentPutDownSpot = currentObject.GetComponent<Interactable>().putDownSpot.transform)
                    {
                        currentObject.GetComponent<Interactable>().beingHeld = false;
                        holdingObject = false;
                        currentObject.parent = currentPutDownSpot;
                        currentObject.localPosition = Vector3.zero;
                        currentObject.GetComponent<Interactable>().putDownSpot.GetComponent<Collider2D>().enabled = false;
                        currentObject.GetComponent<PaperStamp>().paperInPlace = true;
                        currentObject.GetComponent<Collider2D>().enabled = false;
                        return;
                    }
                }
                else if (currentPutDownSpot = currentObject.GetComponent<Interactable>().putDownSpot.transform)
                {
                    Debug.Log("Put");
                    currentObject.GetComponent<Interactable>().beingHeld = false;
                    holdingObject = false;
                    currentObject.parent = currentPutDownSpot;
                    currentObject.localPosition = Vector3.zero;
                }
                else
                {
                    currentObject.GetComponent<Interactable>().beingHeld = true;
                    holdingObject = true;
                    return;
                }
                if (currentObject.GetComponent<Interactable>().putDownSpot != null && !holdingObject) currentObject.GetComponent<Interactable>().putDownSpot.GetComponent<Collider2D>().enabled = false;
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Change to fist sprite
            }
            if (Input.GetMouseButtonUp(0))
            {
                //Change to open sprite
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable") && !holdingObject)
        {
            overInteractable = true;
            currentObject = collision.transform;
        }
        if (collision.gameObject.CompareTag("PutDownSpot"))
        {
            overPutDownSpot = true;
            if (currentObject != null) currentObject.GetComponent<Interactable>().overPutDown = true;
            currentPutDownSpot = collision.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            overInteractable = false;
            currentObject = null;
        }
        if (collision.gameObject.CompareTag("PutDownSpot"))
        {
            overPutDownSpot = false;
            if (currentObject != null) currentObject.GetComponent<Interactable>().overPutDown = false;
            currentPutDownSpot = null;
        }
    }
}
