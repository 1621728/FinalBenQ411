using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverplay : MonoBehaviour
{
    public AudioSource hover;
    public void ButtonHighlighted(int track)
    {
        if (track == 1)
        {
            hover.Play();
            Debug.Log("Play button highlighted");
        }
    }

    
    void OnMouseOver()
    {
        
    }

}
