using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Unit myUnitMotorThing;

    public KeyCode up,down,left,right,shoot;

    // Start is called before the first frame update
    void Start()
    {
        myUnitMotorThing = GetComponent<Unit>();
        if (myUnitMotorThing == null)
        {
            Debug.LogAssertion("Player Controller does not have [Unit]");
            this.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        directionalInput();
        rotationInput();
        weaponControl();
    }

    void directionalInput()
    {
        Vector2 moveVec = new Vector2();

        if(Input.GetKey(up))
        {
            moveVec.y += 1;
        }
        if(Input.GetKey(down))
        {
            moveVec.y -= 1;
        }
        if(Input.GetKey(left))
        {
            moveVec.x -= 1;
        }
        if(Input.GetKey(right))
        {
            moveVec.x += 1;
        }

        myUnitMotorThing.Move(moveVec.normalized);
    }

    void rotationInput()
    {
        Vector2 lookVec = Camera.main.ScreenToWorldPoint(Input.mousePosition) -  transform.position;
       myUnitMotorThing.aimLook(lookVec);

    }

    void weaponControl()
    {
        if(Input.GetKeyUp(shoot)){
            // Fire, we released
            myUnitMotorThing.FireWeapon();

        } else if (Input.GetKey(shoot)) {
            // go down and charge
            myUnitMotorThing.ChargeWeapon();
        }

    }


}
