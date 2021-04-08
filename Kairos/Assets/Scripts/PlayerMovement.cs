using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   public CharacterController2D controller;
   public float runSpeed   = 40f;              // runSpeed from CharacterController
   float horizontalMove    = 0f;
   bool jump   = false;
   bool crouch = false;
   bool melee  = false;
   bool fire   = false;

    // Update is called once per frame
    void Update()
    {
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

       if(Input.GetButtonDown("Jump")) //Vorgefertig in Projekt einstellung - Unity
       {
         jump = true;
       }

       if(Input.GetButtonDown("Crouch")) //Vorgefertig in Projekt einstellung - Unity
       {
          crouch = true;
       }
       else if(Input.GetButtonUp("Crouch")) //Damit man solange crouchen kann wie man den Button gedrückt hält und wenn man los lässt hört man auf
       {
          crouch = false;
       }

       if (Input.GetButtonDown("melee")) // "c"
       {
          melee = true;
       }
       else if (Input.GetButtonUp("melee")) //"C" los lassen
       {
          melee = false;
       }
       if (Input.GetButtonDown("Fire")) // "v"
       {
          fire = true;
       }
       if (controller.GetLifePoints() <= 0)
       {
          controller.PlayerRespawn();
       }
    }

    void FixedUpdate() 
    {
       controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
       controller.MeleeAttack(melee);
       controller.FireAttack(fire);
       fire = false;
       jump = false;
    }
}
