using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class Butto : MonoBehaviour
{
    public TMP_Text triggerText;

 public void playButton()
    {
        SceneManager.LoadScene("Week 1");
    }

    public void triggerButton()
    {
        triggerText.enabled = true;
    }
}
