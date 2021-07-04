using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackScript : MonoBehaviour
{
    //-------------
    // this is the script of the player attack - its for the weapon!!
    //-------------
    public CharacterController2D ctrl_Player;
    public BossScript ctrl_boss;
    public EnemyMelee ctrl_enemy;
    public ShootingEnemy ctrl_ShootingEnemy;
    public FlierEnemy ctrl_flierEnemy;
    public Transform fireWeapon;

    //melee attack
    public Transform attackPosition;
    public LayerMask whatIsEnemie;
    public float attackRange = 1.0f;
    private float timeBtwAttack = 0.0f;
    public float startTimeBtwAttack = 0.3f;

    public GameObject fireAttackPrefab;
    public Animator animator;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        ctrl_boss = FindObjectOfType<BossScript>();
        ctrl_Player = FindObjectOfType<CharacterController2D>();
        ctrl_enemy = FindObjectOfType<EnemyMelee>();
        ctrl_ShootingEnemy = FindObjectOfType<ShootingEnemy>();
        ctrl_flierEnemy = FindObjectOfType<FlierEnemy>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        if (ctrl_Player.getPlayerState()) // true = player enabled false = Disabled
        {
            if (ctrl_Player.getFireAttack())  // if user press fireattack button
            {
                Shoot();
            }
            if (timeBtwAttack <= 0)
            {
                if (ctrl_Player.getMelee())  // if user press melee button
                {
                    Attack();
                    animator.SetTrigger("isMelee");
                }
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;

            }
        }
        else
        {
            spriteRenderer.enabled = false;
        }
    }

    void Shoot()
    {
        Instantiate(fireAttackPrefab, fireWeapon.position, fireWeapon.rotation);
        ctrl_Player.TakeDamage(ctrl_Player.bulletLPremove); // 20 dmg - remote combat

        Debug.Log("FireAttack!");
    }

    void Attack()
    {
        //attack enemys in range
        Collider2D[] damageToEnemies = Physics2D.OverlapCircleAll(attackPosition.position, attackRange, whatIsEnemie);

        for (int i = 0; i < damageToEnemies.Length; i++)
        {
            if (damageToEnemies[i].CompareTag("Boss"))
            {
                damageToEnemies[i].GetComponent<BossScript>().TakeDamage(ctrl_Player.attackDamage);
            }
            if (damageToEnemies[i].CompareTag("Enemy"))
            {
                damageToEnemies[i].GetComponent<EnemyBase>().TakeDamage(ctrl_Player.attackDamage);
            }
        }
    }
    void OnDrawGizmosSelected()  // Red attack range circle and scene view
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPosition.position, attackRange);
    }
}
