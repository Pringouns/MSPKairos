using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    //--------------------
    // this script is just for the bullet of the player fire weapon!!
    //--------------------
    public CharacterController2D ctrl_Player;
    public EnemyMelee ctrl_enemy; // zum testen wegen dmg
    public BossScript ctrl_bossEnemy;
    public float speed = 20f;

    //public GameObject Enemy;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        ctrl_enemy = FindObjectOfType<EnemyMelee>();
        ctrl_Player = FindObjectOfType<CharacterController2D>();
        ctrl_bossEnemy = FindObjectOfType<BossScript>();
        rb.velocity = transform.right * speed;
        Destroy(this.gameObject, 1); // destroy after 5 seconds
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.CompareTag("Enemy"))
        {
            //ctrl_Player.GetDamage(bulletLPremove); // 20 dmg bei fernkampfangriff

            ctrl_enemy.TakeDamage(20);
            Destroy(this.gameObject);
        }
        if (Col.CompareTag("Boss"))
        {
            ctrl_bossEnemy.GetDamage(20);
            Destroy(this.gameObject);
        }
    }
}
