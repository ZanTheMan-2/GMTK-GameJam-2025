using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Beer : MonoBehaviour
{
    public Slider slider;
    public float drinkAmount, initValue,     refillSpeed, thirstThreshold, thirstMultiplier;
    public Car carScript;
    public Animator Shake2D, Shake3D;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = initValue;
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
            Shake3D.SetBool("Shake", true);
        }
        else
        {
            carScript.beerMultiplier = 1;
            //Unshake screen
            Shake2D.SetBool("Shake", false);
            Shake3D.SetBool("Shake", false);
        }
    }

    public void Drink()
    {
        slider.value -= drinkAmount;
    }
}
