using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoverplay : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource hover;
    void Start()
    {
        
    }

    void OnMouseOver()
    {
        hover.Play();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
