using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRandomsize : MonoBehaviour
{
    public float minVal;
    public float maxVal;
    private Vector3 v3;
    public float rn;
    public float distance;
    private GameObject other;
    // Start is called before the first frame update
    void Start()
    {
        other = GameObject.FindGameObjectWithTag("Astroid");

        rn = Random.Range(minVal, maxVal);
        this.gameObject.transform.localScale = Vector2.one * (rn);
        this.gameObject.GetComponent<Rigidbody2D>().mass = rn;
        distance = Vector2.Distance(this.transform.position, other.transform.position);
        if(distance < rn * 5)
        {
            rn = rn / 5;
        }
    }
    // Start is called before the first frame update

    //v3 = transform.localScale;
    //v3.x = Random.Range(minVal, maxVal);
    //v3.y = v3.x;
    // Update is called once per frame
    void Update()
    {
        
    }
}
