using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMelee : EnemyBase
{

    [SerializeField]
    protected float aggroRange;

    public float attackRange = 0.5f;
    public int damage = 10;
    private float lastAttackTime;
    public float attackDelay;
    public Animator animator;
    public LayerMask whatIsPlayer;

    // Update is called once per frame
    void Update()
    {
        checkHealth();

        if (isAlive())
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
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < attackRange)
            {
                //Check to see if enough time passed after the last attack
                if (Time.time > lastAttackTime + attackDelay)
                {
                    Attack();
                    //Record the Time we attacked
                    lastAttackTime = Time.time;
                }
            }

            filpSprite();
        }
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            //enemy is to the left side of the player, so move right
            rb2d.velocity = new Vector2(moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (transform.position.x > player.position.x)
        {
            //enemy is to the right side of the player, so move left
            rb2d.velocity = new Vector2(-moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void StopChasingPlayer()
    {
        rb2d.velocity = new Vector2(0f, rb2d.velocity.y);
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        if (Col.CompareTag("StandardAttack"))
            TakeDamage(damage);
    }

    void Attack()
    {
        //attack enemys in range
        Collider2D[] damageToPlayer = Physics2D.OverlapCircleAll(transform.position, attackRange, whatIsPlayer);

        for (int i = 0; i < damageToPlayer.Length; i++)
        {

            damageToPlayer[i].GetComponent<CharacterController2D>().TakeDamage(damage);
            Debug.Log("attacking melee");


        }

    }
}
