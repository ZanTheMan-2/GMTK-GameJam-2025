using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    public TMP_Text dialogText;
    public string[] dialog;
    public AudioClip[] speach;
    private AudioSource source;

    public UnityEngine.Sprite[] sprites;
    public SpriteRenderer spriteRenderer;


    private int t;
    private bool talking = true;
    public bool isTalking, doneTalking = false;
    public GameObject textWindow;


    public void Awake()
    {
        source = this.GetComponent<AudioSource>();
    }
    public void Update()
    {
       
            if (t <= dialog.Length)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    t += 1;
                    talking = true;
                    source.Stop();
                }
                dialogText.text = dialog[t];
                UnityEngine.Sprite sprity = sprites[t];
                spriteRenderer.sprite = sprity;


                if (talking)
                {
                    source.clip = speach[t];
                    source.PlayOneShot(source.clip);
                    source.Play();
                    talking = false;
                }
            }
            else
            {
                doneTalking = true;
                this.enabled = false;
                spriteRenderer.enabled = false;
                 Debug.Log("Array problem");

        }
    }
}
