using UnityEngine;
using System.Collections;
using System;

public class BossScript : MonoBehaviour
{
    [SerializeField]
    Transform player;

    [SerializeField]
    float aggroRange;

    [SerializeField]
    float moveSpeed;

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


    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
    }
    // Update is called once per frame
    void Update()
    {
        //Distance to player
        float distToPlayer = Vector2.Distance(transform.position, player.position);
       
            if(distToPlayer < aggroRange)
        {
            //code to chase player
            ChasePlayer();
        }
        else
        {
            //Stop chasing player
            StopChasingPlayer();
             
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
    }
    

    private void stopShooting()
    {

        throw new NotImplementedException();
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


        if (timeBtwShots <= 0)
        {
            Instantiate(bossProjectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        if (Vector2.Distance(transform.position, player.position) < 5) {
            Debug.Log(Vector2.Distance(transform.position, player.position));
           StopChasingPlayer();
        }
        throw new NotImplementedException();
    }

    void ChasePlayer()
    {
        if (transform.position.x < player.position.x)
        {
            //enemy is to the left side of the player, so move right
            rb2d.velocity = new Vector2(moveSpeed, 0);
            transform.localScale = new Vector2(1, 1);
        }
        else if (transform.position.x > player.position.x)
        {
            //enemy is to the right side of the player, so move left
            rb2d.velocity = new Vector2(-moveSpeed, 0);
            transform.localScale = new Vector2(-1, 1);
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
        Debug.Log("Enemy died!");

        GetComponent<Collider2D>().enabled = false;
        this.enabled = false;
    }
}
