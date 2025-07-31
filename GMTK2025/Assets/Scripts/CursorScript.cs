using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{
    Transform cursorTransform;
    public bool overInteractable, overPutDownSpot, holdingObject, holdingObjectNoSprite;
    public Transform currentObject, currentPutDownSpot;
    public Sprite[] sprites;
    public bool handOnWheel;
    public GameObject spriteObject;

    // Start is called before the first frame update
    void Start()
    {
        cursorTransform = transform;
        Cursor.visible = false;
        sprites = transform.GetChild(0).GetComponentsInChildren<Sprite>();
        foreach (Sprite currentSprite in sprites)
        {
            if (currentSprite.thisSprite == Sprite.Sprites.OpenHand)
            {
                currentSprite.GetComponent<SpriteRenderer>().enabled = true;
                //currentSprite.GetComponent<Collider2D>().enabled = true;
            }
            else
            {
                currentSprite.GetComponent<SpriteRenderer>().enabled = false;
                if (currentSprite.GetComponent<Collider2D>() != null) currentSprite.GetComponent<Collider2D>().enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(handOnWheel && Input.GetMouseButtonDown(0) && !overInteractable)
        {
            handOnWheel = false;
            GameObject.Find("Wheel").GetComponent<Interactable>().beingHeld = false;
            holdingObject = false;
            //Change to open sprite
            spriteObject.SetActive(true);
            foreach (Sprite currentSprite in sprites)
            {
                if (currentSprite.thisSprite == Sprite.Sprites.OpenHand)
                {
                    currentSprite.GetComponent<SpriteRenderer>().enabled = true;
                    //currentSprite.GetComponent<Collider2D>().enabled = true;
                }
                else
                {
                    currentSprite.GetComponent<SpriteRenderer>().enabled = false;
                    if (currentSprite.GetComponent<Collider2D>() != null) currentSprite.GetComponent<Collider2D>().enabled = false;
                }
            }
            //Do wheel stuff
            GameObject.Find("Wheel").GetComponent<Car>().handOnWheel = false;
            GameObject.Find("Wheel").GetComponent<Car>().SwitchSprite(false);
        }

        if (holdingObjectNoSprite && currentObject == null)
        {
            currentObject = transform.GetChild(1);
            overInteractable = true;
        }
        else if(currentObject == null && !Input.GetMouseButton(0))
        {
            foreach (Sprite currentSprite in sprites)
            {
                if (currentSprite.thisSprite == Sprite.Sprites.OpenHand)
                {
                    currentSprite.GetComponent<SpriteRenderer>().enabled = true;
                    //currentSprite.GetComponent<Collider2D>().enabled = true;
                }
                else
                {
                    currentSprite.GetComponent<SpriteRenderer>().enabled = false;
                    if (currentSprite.GetComponent<Collider2D>() != null) currentSprite.GetComponent<Collider2D>().enabled = false;
                }
            }
        }
        else if(currentObject == null && Input.GetMouseButton(0))
        {
            foreach (Sprite currentSprite in sprites)
            {
                if (currentSprite.thisSprite == Sprite.Sprites.ClosedHand)
                {
                    currentSprite.GetComponent<SpriteRenderer>().enabled = true;
                    //currentSprite.GetComponent<Collider2D>().enabled = true;
                }
                else
                {
                    currentSprite.GetComponent<SpriteRenderer>().enabled = false;
                    if (currentSprite.GetComponent<Collider2D>() != null) currentSprite.GetComponent<Collider2D>().enabled = false;
                }
            }
        }

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); 
        cursorTransform.position = new Vector3(mousePos.x, mousePos.y, 0);
        if (overInteractable && currentObject.GetComponent<Interactable>().isHoldable)
        {
            //Change sprite to point
            foreach (Sprite currentSprite in sprites)
            {
                if (currentSprite.thisSprite == Sprite.Sprites.PointingHand && !holdingObject)
                {
                    currentSprite.GetComponent<SpriteRenderer>().enabled = true;
                    //currentSprite.GetComponent<Collider2D>().enabled = true;
                }
                else if (!holdingObject)
                {
                    currentSprite.GetComponent<SpriteRenderer>().enabled = false;
                    if (currentSprite.GetComponent<Collider2D>() != null) currentSprite.GetComponent<Collider2D>().enabled = false;
                }
            }
            if (Input.GetMouseButtonDown(0) && handOnWheel && currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Wheel)
            {
                handOnWheel = false;
                currentObject.GetComponent<Interactable>().beingHeld = false;
                holdingObject = false;
                //Change to open sprite
                spriteObject.SetActive(true);
                foreach (Sprite currentSprite in sprites)
                {
                    if (currentSprite.thisSprite == Sprite.Sprites.OpenHand)
                    {
                        currentSprite.GetComponent<SpriteRenderer>().enabled = true;
                        //currentSprite.GetComponent<Collider2D>().enabled = true;
                    }
                    else
                    {
                        currentSprite.GetComponent<SpriteRenderer>().enabled = false;
                        if (currentSprite.GetComponent<Collider2D>() != null) currentSprite.GetComponent<Collider2D>().enabled = false;
                    }
                }
                //Do wheel stuff
                currentObject.GetComponent<Car>().handOnWheel = false;
                currentObject.GetComponent<Car>().SwitchSprite(false);
            }
            if (Input.GetMouseButtonDown(0) && !holdingObject && currentObject != null)
            {
                //Pick up
                currentObject.GetComponent<Interactable>().beingHeld = true;
                if (currentObject.GetComponent<Interactable>().putDownSpot != null) currentObject.GetComponent<Interactable>().putDownSpot.GetComponent<Collider2D>().enabled = true;
                if (currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Toothbrush)
                {
                    //Change sprite to holding cloth
                    currentObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
                    currentObject.GetComponent<Collider2D>().enabled = false;
                    holdingObjectNoSprite = false;
                    holdingObject = true;
                    foreach (Sprite currentSprite in sprites)
                    {
                        if (currentSprite.thisSprite == Sprite.Sprites.HoldingCloth)
                        {
                            currentSprite.GetComponent<SpriteRenderer>().enabled = true;
                            currentSprite.GetComponent<Collider2D>().enabled = true;
                            currentObject = currentSprite.transform;
                        }
                        else
                        {
                            currentSprite.GetComponent<SpriteRenderer>().enabled = false;
                            if (currentSprite.GetComponent<Collider2D>() != null) currentSprite.GetComponent<Collider2D>().enabled = false;
                        }
                    }
                }
                else if (currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Wheel)
                {
                    //Change sprite to hand on wheel
                    spriteObject.SetActive(false);
                    currentObject.GetComponent<Car>().handOnWheel = true;
                    currentObject.GetComponent<Car>().SwitchSprite(true);
                    handOnWheel = true;
                    //holdingObject = true;
                }
                else if (currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Knife)
                {
                    //Change sprite to holding knife
                    currentObject.GetComponent<SpriteRenderer>().enabled = false;
                    currentObject.GetComponent<Collider2D>().enabled = false;
                    holdingObjectNoSprite = false;
                    holdingObject = true;
                    foreach (Sprite currentSprite in sprites)
                    {
                        if (currentSprite.thisSprite == Sprite.Sprites.HoldingKnife)
                        {
                            currentSprite.GetComponent<SpriteRenderer>().enabled = true;
                            currentSprite.GetComponent<Collider2D>().enabled = true;
                            currentObject = currentSprite.transform;
                        }
                        else
                        {
                            currentSprite.GetComponent<SpriteRenderer>().enabled = false;
                            if (currentSprite.GetComponent<Collider2D>() != null) currentSprite.GetComponent<Collider2D>().enabled = false;
                        }
                    }
                }
                else
                {
                    holdingObjectNoSprite = true;
                    holdingObject = true;
                    currentObject.parent = transform;
                    currentObject.localPosition = Vector3.zero;
                    Debug.Log("closey");
                    foreach (Sprite currentSprite in sprites)
                    {
                        if (currentSprite.thisSprite == Sprite.Sprites.ClosedHand)
                        {
                            Debug.Log("sprite closey");
                            currentSprite.GetComponent<SpriteRenderer>().enabled = true;
                            //currentSprite.GetComponent<Collider2D>().enabled = true;
                        }
                        else
                        {
                            currentSprite.GetComponent<SpriteRenderer>().enabled = false;
                            if (currentSprite.GetComponent<Collider2D>() != null) currentSprite.GetComponent<Collider2D>().enabled = false;
                        }
                    }
                }
            }
            if (Input.GetMouseButtonDown(0) && overPutDownSpot)
            {
                //Put down
                if (currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Paper)
                {
                    if (currentPutDownSpot = currentObject.GetComponent<Interactable>().putDownSpot.transform)
                    {
                        currentObject.GetComponent<Interactable>().beingHeld = false;
                        holdingObject = false;
                        holdingObjectNoSprite = false;
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
                    Debug.Log("Put - " + currentPutDownSpot + " - " + currentObject.GetComponent<Interactable>().putDownSpot.transform);
                    currentObject.GetComponent<Interactable>().beingHeld = false;
                    holdingObject = false;
                    holdingObjectNoSprite = false;
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
        else if (overInteractable && currentObject.GetComponent<Interactable>().thisType == Interactable.ObjectType.Booze && Input.GetMouseButtonDown(0)) currentObject.GetComponent<Beer>().Drink();
        else if (!holdingObject && !overInteractable)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //Change to fist sprite
                foreach (Sprite currentSprite in sprites)
                {
                    if (currentSprite.thisSprite == Sprite.Sprites.ClosedHand)
                    {
                        currentSprite.GetComponent<SpriteRenderer>().enabled = true;
                        //currentSprite.GetComponent<Collider2D>().enabled = true;
                    }
                    else
                    {
                        currentSprite.GetComponent<SpriteRenderer>().enabled = false;
                        if (currentSprite.GetComponent<Collider2D>() != null) currentSprite.GetComponent<Collider2D>().enabled = false;
                    }
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                //Change to open sprite
                foreach (Sprite currentSprite in sprites)
                {
                    if (currentSprite.thisSprite == Sprite.Sprites.OpenHand)
                    {
                        currentSprite.GetComponent<SpriteRenderer>().enabled = true;
                        //currentSprite.GetComponent<Collider2D>().enabled = true;
                    }
                    else
                    {
                        currentSprite.GetComponent<SpriteRenderer>().enabled = false;
                        if (currentSprite.GetComponent<Collider2D>() != null) currentSprite.GetComponent<Collider2D>().enabled = false;
                    }
                }
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
