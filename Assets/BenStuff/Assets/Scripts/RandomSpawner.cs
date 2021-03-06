using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public GameObject ItemPrefab;
    public float outerRadius = 100;
    public int spawnnumber;
    public float distanceflt;
    public int objectlimit = 2;
    private int asn;
    public Collider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Spawn
        if (EverythingCounter.astroidsnum < 50)
        {   
            if(Score.boidNumber <= Score.targetscore)
            {
                //EverythingCounter.astroidsnum++;
                Invoke("SpawnTime", 2);
            }
              
        }

        colliders = Physics2D.OverlapCircleAll (transform.position, outerRadius);
            
    }

    void SpawnTime()
    {
        SpawnObjectAtRandom();
    }

    void SpawnObjectAtRandom()
    {
        Vector2 randomPos = new Vector2(transform.position.x, transform.position.y) + Random.insideUnitCircle * outerRadius;

        Instantiate(ItemPrefab, randomPos, Quaternion.identity);
    }

    //Draw Circle
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, outerRadius);
    }
}
