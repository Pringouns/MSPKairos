using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
<<<<<<< Updated upstream
   public CharacterController2D ctrl_Player;
   public cannonScript ctrl_cannon; // zum testen wegen dmg
=======
   //-------------
   // this is the script of the player attack - its for the weapon!!
   //-------------
   public CharacterController2D ctrl_Player;
   public EnemyMelee ctrl_enemy;
>>>>>>> Stashed changes
   public Transform fireWeapon;

   public Transform attackPoint;
   public LayerMask enemyLayers;



   public GameObject fireAttackPrefab;

    // Start is called before the first frame update
    void Start()
    { 
       ctrl_Player = FindObjectOfType<CharacterController2D>();
<<<<<<< Updated upstream
       ctrl_cannon = FindObjectOfType<cannonScript>();
=======
       ctrl_enemy = FindObjectOfType<EnemyMelee>();
>>>>>>> Stashed changes
    }
    void FixedUpdate()
    {
       if (ctrl_Player.getFireAttack()) 
       {
          Shoot();
       }
       if (ctrl_Player.getMelee()) 
       {
          Attack();
          ctrl_Player.nextAttackTime = Time.time + 1f / ctrl_Player.attackRate;
       }
    }

    void Shoot() 
    {
       Instantiate(fireAttackPrefab, fireWeapon.position, fireWeapon.rotation);
<<<<<<< Updated upstream
       ctrl_Player.GetDamage(ctrl_Player.bulletLPremove); // 20 dmg bei fernkampfangriff
=======
       ctrl_Player.GetDamage(ctrl_Player.bulletLPremove); // 20 dmg - remote combat
>>>>>>> Stashed changes
    }

    void Attack() 
    {
       //attack enemys in range
       Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, ctrl_Player.attackRange, enemyLayers);

       //damaga them
       foreach (Collider2D Enemy in hitEnemies)
       {
        // enemy.GetComponent<Enemy>().TakeDamage(ctrl_Player.attackDamage);
<<<<<<< Updated upstream
          ctrl_cannon.m_CannonLP -= ctrl_Player.m_MeleeDamage;
=======
          ctrl_enemy.TakeDamage(ctrl_Player.m_MeleeDamage);
>>>>>>> Stashed changes
       }
    }

  
    void OnTriggerEnter2D(Collider2D Col)
    {
       if (Col.CompareTag("Enemy"))
       {
          if (ctrl_Player.getMelee())
          {
<<<<<<< Updated upstream
             ctrl_cannon.m_CannonLP -= 50;
=======
             ctrl_enemy.TakeDamage(ctrl_Player.m_MeleeDamage);
>>>>>>> Stashed changes
          }
       }
    }
}
