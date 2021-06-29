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
    public BossScript ctrl_bossEnemy;
    public FlierEnemy ctrl_flierEnemy;
    //--------------------
    public float timeToDestory = 0.5f;
    public float speed = 20f;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        //Assignment of the control variables
        ctrl_enemy = FindObjectOfType<EnemyMelee>();
        ctrl_ShootingEnemy = FindObjectOfType<ShootingEnemy>();
        ctrl_Player = FindObjectOfType<CharacterController2D>();
        ctrl_bossEnemy = FindObjectOfType<BossScript>();
        ctrl_flierEnemy = FindObjectOfType<FlierEnemy>();

        rb.velocity = transform.right * speed;
        Destroy(this.gameObject, timeToDestory); // destroy after shooting in 1 second
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
        if (Col.CompareTag("FlierEnemy"))
        {
            ctrl_flierEnemy.TakeDamage(ctrl_Player.bulletDmg);
            Destroy(this.gameObject);

        }
        if (Col.CompareTag("Boss"))
        {
            if (ctrl_bossEnemy != null) // for the purpose of not getting the NullReferenceExeption
            {
                ctrl_bossEnemy.TakeDamage(ctrl_Player.bulletDmg);
            }
                
            Destroy(this.gameObject);
            
        }
        else
        {
            Debug.Log("Daneben!");
        }
    }
}
