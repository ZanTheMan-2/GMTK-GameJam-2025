using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class Beer : MonoBehaviour
{
    public Slider slider;
    public float drinkAmount, initValue,     refillSpeed, thirstThreshold, thirstMultiplier;
    public Car carScript;
    public Animator Shake2D;
    public bool beerActive;
    public Volume beerVolume;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = initValue;
    }

    private void Awake()
    {
        if (!beerActive)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = Mathf.Clamp(slider.value, 0, 1);

        slider.value += refillSpeed * Time.deltaTime;

        if (slider.value >= thirstThreshold)
        {
            //Thirst.
            carScript.beerMultiplier = thirstMultiplier;
            //Screen Shake
            Shake2D.SetBool("Shake", true);
            beerVolume.weight = 1;
        }
        else
        {
            carScript.beerMultiplier = 1;
            //Unshake screen
            Shake2D.SetBool("Shake", false);
            beerVolume.weight = 0;
        }
    }

    public void Drink()
    {
        slider.value -= drinkAmount;
    }
}
