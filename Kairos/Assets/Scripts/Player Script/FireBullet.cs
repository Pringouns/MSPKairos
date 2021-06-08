using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
   //--------------------
   // this script is just for the bullet of the player fire weapon!!
   //--------------------
   //---ctrl variables--- 
   public CharacterController2D ctrl_Player;
   public EnemyMelee ctrl_enemy; // zum testen wegen dmg
   public ShootingEnemy ctrl_ShootingEnemy;
   //public BossScript ctrl_bossEnemy;
   //--------------------

   public float speed = 20f;
   public Rigidbody2D rb;

   // Start is called before the first frame update
   void Start()
   {
      //Assignment of the control variables
      ctrl_enemy = FindObjectOfType<EnemyMelee>();
      ctrl_ShootingEnemy = FindObjectOfType<ShootingEnemy>();
      ctrl_Player = FindObjectOfType<CharacterController2D>();
      //ctrl_bossEnemy = FindObjectOfType<BossScript>();

      rb.velocity = transform.right * speed;
      Destroy(this.gameObject, 1); // destroy after shooting in 1 second
   }
   
   void OnTriggerEnter2D(Collider2D Col)
   {
      if (Col.CompareTag("Enemy"))
      {
         ctrl_enemy.TakeDamage(ctrl_Player.bulletDmg);
         Destroy(this.gameObject);
         Debug.Log("Komm mir nicht zu nahe!");
      }
      if (Col.CompareTag("EnemyDistance"))
      {
         ctrl_ShootingEnemy.TakeDamage(ctrl_Player.bulletDmg);
         Destroy(this.gameObject);
         Debug.Log("Camper!");
      }
      //if (Col.CompareTag("Boss")) 
      //{
      //   ctrl_bossEnemy.currentHealth -= ctrl_Player.bulletLPremove; 
      //}
      else 
      {
         Debug.Log("Daneben!");
      }
   }
}
