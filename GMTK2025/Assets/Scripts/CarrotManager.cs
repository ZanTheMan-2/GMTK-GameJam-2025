using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotManager : MonoBehaviour
{
    public GameObject[] dividers;
    public SpriteRenderer spriteRenderer;
    public UnityEngine.Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DividerDestroyed(int dividerNumber)
    {
        dividers[dividerNumber - 1].GetComponent<SpriteRenderer>().enabled = false;
        dividers[dividerNumber - 1].GetComponent<Collider2D>().enabled = false;
        spriteRenderer.sprite = sprites[dividerNumber - 1];
        if (dividerNumber > 1)
        {
            dividers[dividerNumber - 2].GetComponent<SpriteRenderer>().enabled = true;
            dividers[dividerNumber - 2].GetComponent<Collider2D>().enabled = true;
        }
    }
}
