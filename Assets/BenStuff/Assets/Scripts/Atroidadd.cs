using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Atroidadd : MonoBehaviour
{
    public GameObject dust;
    // Start is called before the first frame update
    void Start()
    {
        EverythingCounter.astroidsnum++;
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EverythingCounter.astroidsnum--;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            //if (collision.gameObject.tag == "Astroid")
            //{
            // Instantiate(dust, contact.point, Quaternion.identity);
            // Instantiate(dust, contact.point, Quaternion.identity);
            // Instantiate(dust, contact.point, Quaternion.identity);
            //}

            //if (collision.gameObject.tag == "Enemy")
            //{
            //    Instantiate(dust, contact.point, Quaternion.identity);
            //    Instantiate(dust, contact.point, Quaternion.identity);
            //    Instantiate(dust, contact.point, Quaternion.identity);
            //}
            if (collision.gameObject)
            {
                Instantiate(dust, contact.point, Quaternion.identity);
                //Instantiate(dust, contact.point, Quaternion.identity);
                //Instantiate(dust, contact.point, Quaternion.identity);
                //&& GetComponent<Rigidbody2D>().velocity.magnitude - collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 15
            }
        }
    }
}
