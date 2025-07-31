using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaperStamp : MonoBehaviour
{
    public int moneyGainIfYes, sanityGainIfYes, moneyGainIfNo, sanityGainIfNo;
    public bool paperInPlace;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Yes()
    {
        //Add money
        //Add sanity
        Debug.Log("Oooo you're insane");
    }
    
    public void No()
    {
        //Add money
        //Add sanity
        Debug.Log("Oooo you're poor");
    }
}
