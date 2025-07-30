using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    Transform cursorTransform;
    bool overInteractable, overPutDownSpot;
    Transform currentObject, currentPutDownSpot;

    // Start is called before the first frame update
    void Start()
    {
        cursorTransform = transform;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        cursorTransform.position = Input.mousePosition;
        if (overInteractable && currentObject.GetComponent<Interactable>().isHoldable)
        {
            //Change sprite to point
            if (Input.GetMouseButtonDown(0))
            {
                //Pick up
                currentObject.GetComponent<Interactable>().beingHeld = true;
                if(currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Toothbrush)
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
                currentObject.GetComponent<Interactable>().beingHeld = false;
                if (currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Wheel)
                {
                    //Change to open sprite
                }
                else 
                {
                    currentObject.parent = currentPutDownSpot;
                    currentObject.localPosition = Vector3.zero;
                }
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
        if (collision.gameObject.CompareTag("Interactable"))
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
