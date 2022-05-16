using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class BoidUnit : MonoBehaviour
{
    public GameObject CloneEffect;
    public float range = 10;
    public bool autobehave = false;
    private float time = 0.0f;
    public float interpolationPeriod = 0.1f;
    private Transform target;
    int randomTarget;
    public AudioSource die;
    public float rotateScale;
    public float thrustScale;
    private Rigidbody2D rb2;
    public GameObject flames;
    public GameObject boid;
    public AudioSource touch;
    public GameObject collide;
    public GameObject collideSparks;
    public float ouchSpeed;
    public float dizzySpeed;
    public AudioSource dizzySound;
    public bool isSelected;
    public int isFed;
    public AudioSource hey;
    public AudioSource hi;
    public bool unlimitedPower;
    public int maxSpeed;
    //Stop At Mouse
    public bool sam;
    //Slow Down At Mouse
    public bool sdam;
    //Divide number
    public float sdamn;
    private GameObject targetObj;

    // Start is called before the first frame update
    void Start()
    {
        GetNewTarget();
        //Get RigidBody 2D
        rb2 = GetComponent<Rigidbody2D>();
       // isSelected = true;
        isSelected = false;
        isFed = 0;
        //GameObject.Find("Cm vcam1").GetComponent<CinemachineVirtualCamera>().Follow = this.transform;
    }

    void GetNewTarget()
    {
        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, range);
        foreach (Collider2D collider2D in colliderArray)
        {
            if(collider2D.gameObject.tag == "Food")
            {
                target = collider2D.gameObject.transform;
            }
            else
            {
                GameObject[] varyTargets;

                varyTargets = GameObject.FindGameObjectsWithTag("Food");

                if (varyTargets.Length > 0)
                {
                    randomTarget = Random.Range(0, varyTargets.Length);
                    target = varyTargets[randomTarget].transform;
                }
                else
                {
                    target = null;
                }
            }
        }     
    }

    void Seek()
    {
        Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
        transform.up = direction;

        //MoveForward
        rb2.AddForce(transform.up * thrustScale * Time.deltaTime);
    }

    void Targets()
    {
        GetNewTarget();
    }
    //navigation to mouse////////////////////////////////////////
    void followMouse()
    {
        //Point to Mouse
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;

        //MoveForward
        rb2.AddForce(transform.up * thrustScale * Time.deltaTime);
    }


    // Update is called once per frame
    void Update()
    {
        
        //If selected = false.
        if(autobehave == true)
        {
            if (isSelected == false)
            {
                time += Time.deltaTime;
                if (time >= interpolationPeriod)
                {
                    GetNewTarget();
                    time = 0.0f;
                }
                if (target != null)
                {
                    Debug.Log(Physics2D.OverlapCircleAll(transform.position, range));
                    Seek();
                }
            }          
        }
        
        //Select all
        if (Input.GetKeyDown("z"))
        {
            isSelected = true;
        }

        //Turn off at win
        if (Score.boidNumber >= Score.targetscore)
        {
            this.gameObject.SetActive(false);
        }

        //limity velocity magnitude
        if (rb2.velocity.magnitude > maxSpeed)
        {
            rb2.velocity = Vector2.ClampMagnitude(rb2.velocity, maxSpeed);
        }

        //Reset Rotation/Stabilize boids.
        if (Input.GetMouseButtonDown(0))
        {
            rb2.angularVelocity = 0;
        }

        //When RightClick
        if (Input.GetMouseButton(1))
        {
            isSelected = false;
        }

        //Follow Mouse
        if (Input.GetMouseButton(0) && isSelected == true)
        {
            followMouse();
        }

            ///boid Highlight
            if (isSelected == true)
        {
            boid.transform.GetChild(0).gameObject.SetActive(true);
            GameObject.Find("CM vcam1").GetComponent<CinemachineVirtualCamera>().Follow = this.transform;
        }
        else
        {
            boid.transform.GetChild(0).gameObject.SetActive(false);
        }

        ////Rotate
        //float rotation = rotateScale * Input.GetAxis("Horizontal");
        //transform.Rotate(new Vector3(0, 0, -rotation));

        ////Move Foreward and backward
        //float thrust = thrustScale * Input.GetAxis("Vertical");
        //rb2.AddForce(transform.up * thrust);

        //Clone Power
        if (Input.GetKeyDown("space") && isFed > 0)
        {
            isFed--;
            Instantiate(boid);
            Instantiate(CloneEffect, this.transform.position, Quaternion.identity);
        }

        //Unlimited Power
        if (unlimitedPower == true)
        {
            if (Input.GetKeyDown("space"))
            {
                Instantiate(boid);
                Instantiate(CloneEffect, this.transform.position, Quaternion.identity);
            }
        }
        

        //Outline
        if (isFed > 0)
        {
            boid.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            boid.transform.GetChild(1).gameObject.SetActive(false);
        }

        //Dizzy
        if (rb2.angularVelocity > dizzySpeed)
        {
            Debug.Log("Dizzy");
            //dizzySound.Play();
        }
        if (rb2.angularVelocity < -dizzySpeed)
        {
            Debug.Log("Dizzy");
            //dizzySound.Play();
        }

    }

    public void Killob()
    {
        Destroy(this.gameObject);
    }

    //Ouch protocol
    void Ouch()
    {
        touch.pitch = Random.Range(0.8f, 1.3f);
        touch.Play();
        Instantiate(collide, boid.transform);
        Instantiate(collideSparks, boid.transform);
        Invoke("Killob", .3f);
    }

    //On Collision
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    GameObject cw = collision.gameObject;
    //    Rigidbody2D cwrb = cw.GetComponent<Rigidbody2D>();

    //    //Collision Effects/Ouch
    //    if (rb2.velocity.magnitude - cwrb.velocity.magnitude > ouchSpeed)
    //    {
    //        Debug.Log("Contact!!!");
    //        Ouch();
    //    }

    //    //Die on touch
    //    if (collision.gameObject.tag == "Enemy")
    //    {
    //        Ouch();
    //    }

    //    //teamUp
    //    if (collision.gameObject.tag.Equals("Clone") && collision.gameObject.GetComponent<BoidUnit>().isSelected == true && isSelected == false)
    //    {
    //        hi.pitch = Random.Range(0.8f, 1.3f);
    //        hi.Play();
    //        isSelected = true;
    //    }

    //    //Reach Mouse
    //    if (collision.gameObject.name.Contains("Mouse"))
    //    {
    //        //Slow Down At Mouse
    //        if (sdam == true)
    //        {
    //            rb2.velocity = Vector2.one / sdamn;
    //        }

    //        //Stop At Mouse
    //        if (sam == true)
    //        {
    //            rb2.velocity = Vector2.zero;
    //        }
    //    }

    //    //Eat Food
    //    if (collision.gameObject.tag.Equals("Food"))
    //    {
    //        isFed++;
    //    }
    //}

    public void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject cw = collision.gameObject;
        Rigidbody2D cwrb = cw.GetComponent<Rigidbody2D>();

        //Collision Effects/Ouch
        if (rb2.velocity.magnitude - cwrb.velocity.magnitude > ouchSpeed)
        {
            Debug.Log("Contact!!!");
            Ouch();
        }

        //Die on touch
        if (collision.gameObject.tag == "Enemy")
        {
            Ouch();
        }

        //teamUp
        if (collision.gameObject.tag.Equals("Clone") && collision.gameObject.GetComponent<BoidUnit>().isSelected == true && isSelected == false)
        {
            hi.pitch = Random.Range(0.8f, 1.3f);
            hi.Play();
            isSelected = true;
        }

        //Reach Mouse
        if (collision.gameObject.name.Contains("Mouse"))
        {
            //Slow Down At Mouse
            if (sdam == true)
            {
                rb2.velocity = Vector2.one / sdamn;
            }

            //Stop At Mouse
            if (sam == true)
            {
                rb2.velocity = Vector2.zero;
            }
        }

        //Eat Food
        if (collision.gameObject.tag.Equals("Food"))
        {
            isFed++;
            Instantiate(CloneEffect, this.transform.position, Quaternion.identity);
        }
    }
}
