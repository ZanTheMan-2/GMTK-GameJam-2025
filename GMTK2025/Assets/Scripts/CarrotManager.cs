using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotManager : MonoBehaviour
{
    public GameObject[] dividers;
    public GameObject blank, carrotScene, BedScene, arms;
    public SpriteRenderer spriteRenderer, carrotRender;
    public UnityEngine.Sprite[] sprites;
    private bool ready = true;

    public AudioSource sound;

    private void Start()
    {
        BedScene.gameObject.SetActive(false);
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
        else
        {
            if (ready == true)
            {
                ready = false;
                sound.Play();
                StartCoroutine(transitionWaiter(carrotScene, BedScene));
                blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0);
                blank.gameObject.SetActive(true);
                Debug.Log("blinked");
            }
        }
    }
    IEnumerator transitionWaiter(GameObject currentBG, GameObject newBG)
    {
        for (float i = 0; i < 2; i += 0.05f) // fades black to transition
        {
            yield return new WaitForSeconds(0.05f);
            blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }

        newBG.gameObject.SetActive(true);
        arms.gameObject.SetActive(false);
        carrotRender.enabled = false;

        for (float i = 2; i > 0; i -= 0.05f) // open eyes
        {
            yield return new WaitForSeconds(0.05f);
            blank.GetComponent<SpriteRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, i);
        }
        currentBG.gameObject.SetActive(false);
    }
}
