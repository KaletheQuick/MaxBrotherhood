using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_Ranged : MonoBehaviour
{
    public GameObject projectilePrefab;

    public float chargeTime, charge;
    bool isCharging;

    public float minDamage = 1, maxDamage = 10, minSpeed = 4, maxSpeed = 10;

    AudioSource aud;
    public AudioClip chargeSound, fireSound;
    

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Charge()
    {
        if(charge < chargeTime)
        {
            charge += Time.deltaTime;
        }

        if(isCharging == false)
        {
            isCharging = true;
            aud.pitch = 1;
            aud.Play();
        }
    }

    public void Fire()
    {
        fireProjectile();
    }

    void fireProjectile()
    {
        float chargePercent = (charge/chargeTime);
        GameObject newProGO = Instantiate(projectilePrefab, transform.position, Quaternion.identity) as GameObject;
        projectile2D newPro = newProGO.GetComponent<projectile2D>();
        //debug.log
        newPro.vel = transform.forward * Mathf.Lerp(minSpeed, maxSpeed, chargePercent);
        newPro.flying = true;
        newPro.damage = Mathf.Lerp(minDamage, maxDamage, chargePercent);
        charge = 0;

        // audio
        aud.Stop();
        isCharging = false;
        aud.pitch = 1.5f - chargePercent;
        aud.PlayOneShot(fireSound);

    }
}
