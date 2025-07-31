using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public bool handOnWheel;
    public Transform wheel, cam;
    public float maxRotation, driftDelay, minDrift, maxDrift, steerSensitivity, camSensitivity, carSpeed;
    float rotationVelocity;
    int rotationDirection; //1 is left, -1 is right
    float currentRotation, currentCamRotation;
    public Collider2D normalCollider, bigCollider;
    public float beerMultiplier;
    float multipliedDelay;

    [Header("Sprites")]
    public SpriteRenderer spriteRenderer;
    public UnityEngine.Sprite handWheel, justWheel;

    // Start is called before the first frame update
    void Start()
    {
        rotationDirection = Random.Range(0, 1);
        StartCoroutine(Drift());
        currentRotation = 0;
        beerMultiplier = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotationDirection == 0) rotationDirection = -1;

        currentRotation += rotationVelocity * rotationDirection * Time.deltaTime;
        if(handOnWheel) currentRotation += Input.GetAxis("Mouse X") * steerSensitivity * -1;

        wheel.rotation = Quaternion.Euler(0, 0, Mathf.Clamp(currentRotation, maxRotation * -1, maxRotation));
        currentCamRotation += Mathf.Clamp(currentRotation, maxRotation * -1, maxRotation) * camSensitivity;
        cam.rotation = Quaternion.Euler(0, Mathf.Clamp(currentCamRotation * -1, -90, 90), 0);
        cam.Translate(cam.transform.forward * carSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        multipliedDelay = driftDelay / beerMultiplier;
    }

    IEnumerator Drift()
    {
        for (int x = 1; x > 0;)
        {
            yield return new WaitForSeconds(multipliedDelay);
            Debug.Log("meow");
            rotationDirection = Random.Range(0, 1);
            rotationVelocity = Random.Range(minDrift, maxDrift);
            rotationVelocity *= beerMultiplier;
        }
    }

    public void SwitchSprite(bool switchingToHold)
    {
        if (switchingToHold)
        {
            //Switch to holding wheel
            spriteRenderer.sprite = handWheel;
            normalCollider.enabled = false;
            bigCollider.enabled = true;
        }
        else
        {
            //Switch to not holding wheel
            spriteRenderer.sprite = justWheel;
            normalCollider.enabled = true;
            bigCollider.enabled = false;
        }
    }
}
