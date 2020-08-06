using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    public GameObject shield;
    public GameObject spear;

    public Transform spearAttack, spearIdle, shieldBlock, shieldIdle;

    public float spearOutTime, spearTimer, shieldOutTime, shieldTimer;

    public bool blocking, spearing;

    public AudioClip spearThrust;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        manageSpear();
        manageShield();
    }

    public void Attack()
    {
        spearing = true;
        GetComponentInParent<AudioSource>().PlayOneShot(spearThrust);
    }

    void manageSpear()
    {
        if(spearing)
        {
            spearTimer += Time.deltaTime;
            spear.transform.position = Vector3.Lerp(spear.transform.position, spearAttack.position, 0.6f);
            spear.transform.rotation = Quaternion.Lerp(spear.transform.rotation, spearAttack.rotation, 0.6f);
        } else {
            spear.transform.position = Vector3.Lerp(spear.transform.position, spearIdle.position, 0.1f);
            spear.transform.rotation = Quaternion.Lerp(spear.transform.rotation, spearIdle.rotation, 0.1f);
        }
        if(spearTimer > spearOutTime)
        {
            spearing = false;
            spearTimer = 0;
        }
    }

    public void Shield()
    {
        blocking = true;
    }

    void manageShield()
    {
        if(blocking)
        {
            shieldTimer += Time.deltaTime;
            shield.transform.position = Vector3.Lerp(shield.transform.position, shieldBlock.position, 0.6f);
            shield.transform.rotation = Quaternion.Lerp(shield.transform.rotation, shieldBlock.rotation, 0.6f);
        } else {
            shield.transform.position = Vector3.Lerp(shield.transform.position, shieldIdle.position, 0.1f);
            shield.transform.rotation = Quaternion.Lerp(shield.transform.rotation, shieldIdle.rotation, 0.1f);
        }
        if(shieldTimer > shieldOutTime)
        {
            blocking = false;
            shieldTimer = 0;
        }
    }
}
