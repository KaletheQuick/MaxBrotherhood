using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile2D : MonoBehaviour
{
    public Vector3 vel;
    public float damage;


    public bool flying;
    public float flightTime = 10;
    float flightTimer;

    // Previous hit debug 
    RaycastHit2D oldHit;

    List<GameObject> thingsIHaveAlreadyHit;

    // Audio
    AudioSource aud;
    public AudioClip[] deflectionSounds;
    public AudioClip[] hitSounds; 
    

    // Start is called before the first frame update
    void Start()
    {
        thingsIHaveAlreadyHit = new List<GameObject>();
        aud = GetComponent<AudioSource>();

        // HACKY
        Destroy(gameObject, 10);
    }

    void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (vel * 0.1f));
        if(oldHit)
        {            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(oldHit.point, oldHit.point + oldHit.normal);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(flying)
        {
            flightTimer += Time.deltaTime;
            if(flightTimer > flightTime)
                flying = false;
            stepProjectileMove();
        }
    }

    void stepProjectileMove()
    {
        // Cast a ray straight down.
        RaycastHit2D hit = Physics2D.Raycast(transform.position, vel, vel.magnitude * Time.deltaTime);

        // If it hits something...
        if (hit.collider != null && thingsIHaveAlreadyHit.Contains(hit.collider.gameObject) == false)
        {
            // Check if damageable guy
            Unit hitMan = hit.collider.transform.parent.gameObject.GetComponent<Unit>();
            if(hitMan != null && damage != 0)
            {
                // Hurt him!
                aud.volume = 4;
                soundRandomFromArray(hitSounds);
                hitMan.TakeDamage(damage, hit.point);
                // disable arrow
                this.enabled = false;
            } else {   
                if(damage > 0)
                {
                    damage -= 1;
                    if (damage < 0)
                    {                    
                        // disable arrow
                        this.enabled = false;
                    }
                }
                
                // Ricochet
                transform.position = hit.point;
                vel = Vector3.Reflect(vel, hit.normal);
                transform.position = transform.position + (vel * Time.deltaTime * 0.5f);
                //thingsIHaveAlreadyHit.Add(hit.collider.gameObject);

                soundRandomFromArray(deflectionSounds);
            }
            // TODO check to damage or ricochet.
            oldHit = hit;
        } else {
            // Move onward! 
            transform.position = transform.position + (vel * Time.deltaTime);
        }
    }


    void soundRandomFromArray(AudioClip[] soundLib)
    {
        if(soundLib.Length == 0)
            return;
        aud.pitch = 0.9f + (Random.value * 0.2f);
        aud.PlayOneShot(soundLib[Random.Range(0, soundLib.Length)]);
    }
}
