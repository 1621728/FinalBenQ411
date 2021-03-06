using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyScript : MonoBehaviour
{
    public GameObject boidguts;
    public GameObject dust;
    public int agro = 10;
    int randomTarget;
    public Transform target;
    private Rigidbody2D rb2;
    public float thrustScale;
    public float distance;
    private GameObject chase;
    public float multisize = 2f;
    // Start is called before the first frame update
    void Start()
    {
        
        
        //chase = GameObject.FindGameObjectWithTag("Clone");
        GetNewTarget();
        rb2 = GetComponent<Rigidbody2D>();
        if(Score.boidNumber <= Score.targetscore)
        {
            //distance = Vector2.Distance(this.transform.position, target.transform.position);
        }
        
    }
    void GetNewTarget()
    {
        GameObject[] varyTargets;
        varyTargets = GameObject.FindGameObjectsWithTag("Clone");
        if (varyTargets.Length > 0)
        {
            if(Score.boidNumber <= Score.targetscore)
            {
                randomTarget = Random.Range(0, varyTargets.Length);
                target = varyTargets[randomTarget].transform;
            }
            
        }
        else
        {
            target = null;
        }
    }
    void followMouse()
        {

            Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y);
            transform.up = direction;

            //MoveForward
            rb2.AddForce(transform.up * thrustScale * Time.deltaTime);
        }


    // Update is called once per frame
    void Update()
    {

        if (Score.boidNumber > Score.targetscore)
        {
            this.gameObject.SetActive(false);
        }

        if (target == null)
        {
            GetNewTarget();
        }

        if (target != null)
        {
            followMouse();
        }
        


        if (distance < agro)
        {
            
        }

        
    }

    public void OnCollisionEnter2D(Collider2D collision)
    {

        

        if (collision.gameObject.tag == "Astroid")
        {
            Instantiate(dust, this.transform.position, Quaternion.identity);
        }
        if (collision.gameObject.tag == "Clone")
        {
            Instantiate(boidguts, this.transform.position, Quaternion.identity);
            this.transform.localScale = Vector2.one * multisize;
        }
    }
}
