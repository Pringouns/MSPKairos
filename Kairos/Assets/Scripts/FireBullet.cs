using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
<<<<<<< Updated upstream
   public CharacterController2D ctrl_Player;
   public cannonScript ctrl_cannon; // zum testen wegen dmg
=======
   //--------------------
   // this script is just for the bullet of the player fire weapon!!
   //--------------------
   public CharacterController2D ctrl_Player;
   public EnemyMelee ctrl_enemy; // zum testen wegen dmg
>>>>>>> Stashed changes
   public float speed        = 20f;

   //public GameObject Enemy;
   public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
       ctrl_cannon = FindObjectOfType<cannonScript>();
=======
       ctrl_enemy = FindObjectOfType<EnemyMelee>();
>>>>>>> Stashed changes
       ctrl_Player = FindObjectOfType<CharacterController2D>();
       rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
       if (Col.CompareTag("Enemy"))
       {
<<<<<<< Updated upstream
          ctrl_cannon.m_CannonLP -= 50;
          //ctrl_Player.GetDamage(bulletLPremove); // 20 dmg bei fernkampfangriff
=======
          ctrl_enemy.TakeDamage(20);
>>>>>>> Stashed changes
          Destroy(this.gameObject);
       }
    }
}
