using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyHitScript : MonoBehaviour 
{
   public CharacterController2D ctrl_Player;
   public cannonScript ctrl_cannon;
   public FireBullet ctrl_bullet;
    void Start()
    {
       ctrl_cannon = FindObjectOfType<cannonScript>();
       ctrl_Player = FindObjectOfType<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
       if (ctrl_cannon.m_CannonLP <= 0)
       {
          ctrl_cannon.DestroyCannon();
       }
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
       if (Col.CompareTag("PlayerFireAttack"))
       {
          ctrl_cannon.m_CannonLP -= ctrl_Player.bulletDmg;
       }
       if (Col.CompareTag("PlayerMeleeAttack")) 
       {
          if (ctrl_Player.getMelee())
          {
             ctrl_cannon.m_CannonLP -= ctrl_Player.m_MeleeDamage;
            
          }
       }
    }
}
