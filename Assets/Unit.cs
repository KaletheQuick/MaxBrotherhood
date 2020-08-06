using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	// HACK
    public Vector3 test;
	public float speed = 1;

    public Weapon_Ranged weapon;


    // Audio
    AudioSource aud;
    public AudioClip[] hurtSounds;
    public AudioClip[] deathSounds; 

    // HP
    public float hitPointsMax = 10, hitPointsCurrent = 10;
    public GameObject hurtEffect;
    float invulnerabilityTimer;
    public float iFrames = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        aud = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        invulnerabilityTimer -= Time.deltaTime;
    }
    
    /// This will take in a normalized 2d vector and then attempt to move the unit
    public void Move(Vector2 moveVec)
    {
        if(hitPointsCurrent <= 0)
            return;
        transform.position = transform.position + ((Vector3)moveVec * speed * Time.deltaTime);
    }

    /// Take in 2D vector and face/aim in that direction
    public void aimLook(Vector2 lookDir)
    {
        if(hitPointsCurrent <= 0)
            return;
        transform.LookAt(transform.position + (Vector3)lookDir); //
    }

    // Experience pain, possibly die
    public void TakeDamage(float damage, Vector3 impactPos)
    {
        if(invulnerabilityTimer > 0)
            return;

        if(hitPointsCurrent > 0)
        {
            hitPointsCurrent -= damage;
            GameObject newProGO = Instantiate(hurtEffect, impactPos + new Vector3(0,0,0.5f), Quaternion.identity) as GameObject;
            Destroy(newProGO, 0.1f);
            if(hitPointsCurrent <= 0)
            {
                //die
                die();
                return;
            }
            
        }
        soundRandomFromArray(hurtSounds);
        if(iFrames > 0)
        {
            invulnerabilityTimer = iFrames;
        }
    }

    void die()
    {
        // die
        soundRandomFromArray(deathSounds);
        Destroy(this.gameObject, 10);
        BroadcastMessage("OnDeath");
    }

    public void ChargeWeapon() 
    {
        if(weapon)
        {
            weapon.Charge();
        }
    }

    public void FireWeapon()
    {
        if(weapon)
        {
            weapon.Fire();
        }
    }


    void soundRandomFromArray(AudioClip[] soundLib)
    {
        aud.pitch = 0.9f + (Random.value * 0.2f);
        aud.PlayOneShot(soundLib[Random.Range(0, soundLib.Length)]);
    }

    public float HealthPercentage()
    {
        return hitPointsCurrent/hitPointsMax;
    }
}
