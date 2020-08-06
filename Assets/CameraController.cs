using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform followObj;
    public Vector3 followOffset;

    float smack;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followObj.position + followOffset, smack);
        smack += Time.deltaTime * 2;
    }

    public void hitCamera()
    {
        smack = 0.01f;
    }
}
