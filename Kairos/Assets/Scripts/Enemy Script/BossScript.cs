using UnityEngine;
using System.Collections;
using System;

public class BossScript : MonoBehaviour
{
    //[SerializeField]
    Transform player;

    
    public float aggroRange;

    Rigidbody2D rb2d;
    public int maxHealth = 100;
    int currentHealth;
    public float attackRange;
    public int damage;
    private float lastAttackTime;
    public float attackDelay;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject bossProjectile;
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public LayerMask whatIsPlayer;
    Vector2 Direction;

    SpriteRenderer m_ObjectRenderer; // to invisible the object

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;

        m_ObjectRenderer = GetComponent<SpriteRenderer>(); // to invisible the object 

    }
    // Update is called once per frame
    void Update()
    {
        //Distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        Vector2 targetPos = player.transform.position;

        Direction = targetPos - (Vector2)transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, Direction, aggroRange);



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
        else if (distToPlayer > aggroRange && player.position.y > -7 && player.position.x < 17)
        {
            //Stop chasing player
            StopChasingPlayer();


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








            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer < attackRange)
            {
                //Check to see if enough time passed after the last attack
                if (Time.time > lastAttackTime + attackDelay)
                {
                    player.SendMessage("TakeDamage", damage);
                    //Record the Time we attacked
                    lastAttackTime = Time.time;
                }
            }

            //else if (distanceToPlayer >= attackRange)
            //{
            //    Attack();
            //}



        }
    }
    

    private void startShootingPlayer()
    {

            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }
       else  if (Vector2.Distance(transform.position, player.position) < attackRange)
        {
            StopChasingPlayer();
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(bossProjectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        
       
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x && transform.position.x > 0.1)
        {
            //enemy is to the left side of the player, so move right
            rb2d.velocity = new Vector2(speed, 0);
            transform.localScale = new Vector2(-1, 1);
        }
        else if (transform.position.x > player.position.x && transform.position.x < 0.1)
        {
            //enemy is to the right side of the player, so move left
            rb2d.velocity = new Vector2(-speed, 0);
            transform.localScale = new Vector2(1, 1);
        }
    }

    private void StopChasingPlayer()
    {
        rb2d.velocity = Vector2.zero;
        startShootingPlayer();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            die();
        }
    }

    void die()
    {
        m_ObjectRenderer.enabled = false; // to invisible the object 

        Debug.Log("Enemy died!");
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

        for (int i = 0; i < damageToPlayer.Length; i++)
        {
            
            damageToPlayer[i].GetComponent<CharacterController2D>().TakeDamage(damage);
           
            
        }

    }


}
