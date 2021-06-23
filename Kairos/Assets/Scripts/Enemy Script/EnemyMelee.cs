using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : MonoBehaviour
{
    [SerializeField]
    Transform player;
    
    [SerializeField]
    float aggroRange;
 
    [SerializeField]
    float moveSpeed;

    Rigidbody2D rb2d;

    SpriteRenderer spriteRenderer;

    public Transform target;
    public int maxHealth = 100;
    int currentHealth = 0;

    public float attackRange = 0.5f;
    public int damage = 10;
    private float lastAttackTime;
    public float attackDelay;
    public Animator animator;
    public LayerMask whatIsPlayer;


    // Start is called before the first frame update
    void Start()
    {   
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
       
       if (spriteRenderer.enabled)
       {
          //Distance to player
          float distToPlayer = Vector2.Distance(transform.position, player.position);

          if (distToPlayer < aggroRange)
          {
             //code to chase player
             ChasePlayer();
                animator.SetBool("Chasing", true);
          }
          else
          {
             //Stop chasing player
             StopChasingPlayer();
                animator.SetBool("Chasing", false);

            }
            //Attacking AI

            //Check the distance between Enemy and player
            float distanceToPlayer = Vector3.Distance(transform.position, target.position);
          if (distanceToPlayer < attackRange)
          {
             //Check to see if enough time passed after the last attack
             if (Time.time > lastAttackTime + attackDelay)
             {
                target.SendMessage("TakeDamage", damage);
                //Record the Time we attacked
                lastAttackTime = Time.time;
             }
          }
       }
       else 
       {
         //death
       }


        // test for flip
        if (transform.position.x < player.position.x)
        {
            //enemy is to the left side of the player, so move right

            transform.localScale = new Vector2(-1, 1);
        }
        else if (transform.position.x > player.position.x)
        {
            //enemy is to the right side of the player, so move left

            transform.localScale = new Vector2(1, 1);
        }
    } 

    void ChasePlayer()
    {   
        if(transform.position.x < player.position.x)
        {
            //enemy is to the left side of the player, so move right
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else if(transform.position.x > player.position.x)
        {
            //enemy is to the right side of the player, so move left
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void StopChasingPlayer(){
       rb2d.velocity = Vector2.zero;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        //check enemy health
        if (currentHealth <= 0)
        {
            die();
        }
    }

    void die()
    {
        spriteRenderer.enabled = false;
    }

    public void GetDamage(int damage) // Remove Damage from actual LifePoints
    {
        currentHealth -= damage;
    }
    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.CompareTag("StandardAttack"))
            GetDamage(damage);
    }


    void Attack()
    {
        //attack enemys in range
        Collider2D[] damageToPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsPlayer);

        for (int i = 0; i <= damageToPlayer.Length; i++)
        {

            damageToPlayer[i].GetComponent<CharacterController2D>().TakeDamage(damage);
            

        }

    }
}
