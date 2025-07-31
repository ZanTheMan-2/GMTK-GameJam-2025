using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public TMP_Text dialogText;
    public string[] dialog;
    public AudioClip[] speach;
    private AudioSource source;

    private int t;
    private bool talking = true;
    public bool isTalking, doneTalking = false;
    public GameObject textWindow;


    public void Start()
    {
        source = this.GetComponent<AudioSource>();
    }
    public void Update()
    {
        try{
            if (Input.GetKeyDown(KeyCode.Space))
            {
                t += 1;
                talking = true;
                source.Stop();
            }
            dialogText.text = dialog[t];

            if (talking)
            {
                source.clip = speach[t];
                source.PlayOneShot(source.clip);
                source.Play();
                talking = false;
            }
        }catch 
        {
            Debug.Log("Array problem");
            doneTalking = true;
            this.enabled = false;
        }
    }
}
