using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    static List<EnemyAI> allEnemies;
    Unit myUnit;

    public GameObject target;

    public float flockRadius;

    public EnemyWeaponController weapons;

    public float actionCooldown;
    float actionCooldownTimer;
    // Start is called before the first frame update
    void Start()
    {        
        if(allEnemies == null)
        {
            allEnemies = new List<EnemyAI>();
        }
        allEnemies.Add(this);
        myUnit = GetComponent<Unit>();
        target = GameObject.Find("Unit_Player");
    }

    // Update is called once per frame
    void Update()
    {
        combatMovement();
        combatAim();
        actionCooldownTimer -= Time.deltaTime;
        if(actionCooldownTimer <= 0)
        {
            if(Random.value > 0.4f)
            {
                checkForAttack();
            } else {
                useShield();
            }
            actionCooldownTimer = actionCooldown;
        }
    }

    void combatAim()
    {
        
    Vector2 lookVec =  target.transform.position -  transform.position;
    if(myUnit.HealthPercentage() > 0.2f)
        {
            myUnit.aimLook(lookVec);
        } else {
            myUnit.aimLook(-lookVec);
        }
    }


    // Trying to get something a little bit boidy
    void combatMovement()
    {
        Vector2 combinedMovementVector = Vector3.zero;
        // Move relative player
        
        if(myUnit.HealthPercentage() > 0.4f && (target.transform.position - transform.position).sqrMagnitude > 0.3f)
        {
            combinedMovementVector = (target.transform.position - transform.position).normalized;
        } else {
            combinedMovementVector = -(target.transform.position - transform.position).normalized;
        }

        // GO THROUGH ALL ENEMIES and create a weighted avoidance vector.
        Vector2 avoidanceVec = Vector2.zero;
        for (int i = 0; i < allEnemies.Count; i++)
        {
            if(allEnemies[i] == this)
            {
                continue;
            }
            float dist = Vector3.Distance(allEnemies[i].transform.position, transform.position);
            if(dist < flockRadius)
            {
                avoidanceVec +=  (Vector2)(-(allEnemies[i].transform.position - transform.position).normalized * ((flockRadius - dist)*2));
            }
        }
        combinedMovementVector += avoidanceVec;
        myUnit.Move(combinedMovementVector.normalized);
    }

    void checkForAttack()
    {
        if(Random.value < 0.8 && Vector3.Distance(transform.position, target.transform.position) < 1.2f)
        {
            weapons.Attack();
        } 
    }

    void useShield()
    {
        weapons.Shield();
    }


    public void OnDeath()
    {
        allEnemies.Remove(this);
    }
}
