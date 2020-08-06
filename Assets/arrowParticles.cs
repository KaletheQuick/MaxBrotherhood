using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class arrowParticles : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float minSpeed, maxSpeed, minFly, maxFly;
    public int minParticle, maxParticle;

    public bool testBurst;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(testBurst)
        {
            testBurst = false;
            Burst();
        }
    }

    public void Burst()
    {
        int rando = Random.Range(minParticle, maxParticle+1);
        for (int i = 0; i < rando; i++)
        {
            fireProjectile();
        }
    }

    void fireProjectile()
    {
        GameObject newProGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        projectile2D newPro = newProGO.GetComponent<projectile2D>();
        //debug.log
        newPro.vel = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0).normalized;
        newPro.vel = newPro.vel * Random.Range(minSpeed, maxSpeed);
        newPro.flightTime = Random.Range(minFly, maxFly);
        newPro.flying = true;


    }
}
