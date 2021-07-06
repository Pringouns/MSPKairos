using UnityEngine;
using System.Collections;
using System;

public class BossScript : EnemyBase
{

    protected GameObject playerObj;
    public float aggroRange;

    public float attackRange;
    public int damage;
    private float lastAttackTime;
    public float attackDelay;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject bossProjectile;
    public float stoppingDistance;
    public float retreatDistance;
    public LayerMask whatIsPlayer;
    Vector2 Direction;
    public Animator animator;


    // Start is called before the first frame update
    override protected void onStart()
    {
        timeBtwShots = startTimeBtwShots;
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    void Update()
    {
       if (isAlive())
       {

        //Distance to player
        float distToPlayer = Vector2.Distance(this.transform.position, player.position);

        Vector2 targetPos = player.position;

        Direction = targetPos - (Vector2)this.transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(this.transform.position, Direction, aggroRange);



            // flip
            if (this.transform.position.x < player.position.x)
            {
                //enemy is to the left side of the player, so move right

                transform.localScale = new Vector2(-1, 1);
            }
            else if (this.transform.position.x > player.position.x)
            {
                //enemy is to the right side of the player, so move left

                transform.localScale = new Vector2(1, 1);
            }




            if (distToPlayer <= aggroRange)
            {
                ChasePlayer();

                //Check to see if enough time passed after the last attack
                if (Time.time > lastAttackTime + attackDelay)
                {

                Attack();
                //Record the Time we attacked
                lastAttackTime = Time.time;

                }


            }
              
            else if (distToPlayer > aggroRange) // this one was just to test if the Boss would stop chasing and shooting the player when the player is out of the room
            {
                 //Stop chasing player
                 StopChasingPlayer();

                float distanceToPlayer = Vector3.Distance(this.transform.position, player.position);

                if (distanceToPlayer < attackRange)                                           
                {
                    //Check to see if enough time passed after the last attack
                    if (Time.time > lastAttackTime + attackDelay)
                    {
                        playerObj.SendMessage("TakeDamage", damage);
                        //Record the Time we attacked
                        lastAttackTime = Time.time;
                    }
                }




            }
       }
    }
    

    private void startShootingPlayer()
    {

        if (Vector2.Distance(this.transform.position, player.position) > stoppingDistance)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else if (Vector2.Distance(this.transform.position, player.position) < stoppingDistance && Vector2.Distance(this.transform.position, player.position) > retreatDistance)
        {
            this.transform.position = this.transform.position;
        }
        else if (Vector2.Distance(this.transform.position, player.position) < retreatDistance)
        {
            this.transform.position = Vector2.MoveTowards(this.transform.position, player.position, -moveSpeed * Time.deltaTime);
        }
       else  if (Vector2.Distance(this.transform.position, player.position) < attackRange)
        {
            StopChasingPlayer();
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(bossProjectile, this.transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
       
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            //enemy is to the left side of the player, so move right
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (transform.position.x > player.position.x)
        {
            //enemy is to the right side of the player, so move left
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void StopChasingPlayer()
    {
        rb2d.velocity = Vector2.zero;
        startShootingPlayer();
        animator.SetTrigger("Attack");

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
        Collider2D[] damageToPlayer = Physics2D.OverlapCircleAll(this.transform.position, attackRange, whatIsPlayer);

        for (int i = 0; i < damageToPlayer.Length; i++)
        {
            if (damageToPlayer[i].GetType() == typeof(UnityEngine.EdgeCollider2D)){
                damageToPlayer[i].GetComponent<CharacterController2D>().TakeDamage(damage);
                animator.SetTrigger("Attack");
                Debug.Log("do Melee Attack");
            }
        }

    }

}
