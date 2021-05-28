using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
   //----- PlayerMovement-----
   // this script observes the activity of the player and reacts to key presses 
   //-----------------------------------

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

       if(Input.GetButtonDown("Jump")) // defined in the unity settings
       {
         jump = true;
       }

       if(Input.GetButtonDown("Crouch")) // defined in the unity settings
       {
          crouch = true;
       }
       else if(Input.GetButtonUp("Crouch")) //defined in the unity settings - when you release the button it stops to crouch
       {
          crouch = false;
       }

       if (Input.GetButtonDown("melee")) // defined in the unity settings
       {
          melee = true;
       }
       else if (Input.GetButtonUp("melee")) //defined in the unity settings - when you release the button it stops to melee
       {
          melee = false;
       }
       if (Input.GetButtonDown("Fire")) // defined in the unity settings
       {
          fire = true;
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
