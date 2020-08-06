using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnCollision : MonoBehaviour
{
    Collider2D myCol;
    // Start is called before the first frame update
    void Start()
    {
        myCol = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "PlayerCollider")
        {
            Vector3 contactPos = collision.GetContact(0).point;
            Unit guy = collision.gameObject.GetComponentInParent<Unit>();
            guy.TakeDamage(1, contactPos);
            guy.Move(guy.transform.position - contactPos);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log(col.gameObject.name);
        if (col.gameObject.name == "PlayerCollider")
        {
            //Vector3 contactPos = col.GetContact(0).point;
            Unit guy = col.gameObject.GetComponentInParent<Unit>();
            guy.TakeDamage(1, col.gameObject.transform.position);
            guy.Move((guy.transform.position - transform.position) * 25);
            Camera.main.GetComponent<CameraController>().hitCamera();
        }
    }
}
