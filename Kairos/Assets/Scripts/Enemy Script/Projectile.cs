using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;
    private Vector2 start;
    private Rigidbody2D rb2d;


    private CharacterController2D characterController2D;

    public float attackRange = 0.5f;
    public int damage = 10;
    private float lastAttackTime;
    public float attackDelay;
    public int flightTime = 4;
    private float intTime;



    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
        start  = new Vector2(this.transform.position.x, this.transform.position.y);

        float xDist = target.x - start.x;
        float yDist = target.y - start.y;
        float tDist = Mathf.Sqrt(Mathf.Pow(xDist, 2) + Mathf.Pow(yDist, 2));
        float xVecPart = xDist / tDist;
        float yVecPart = yDist / tDist;

        rb2d.velocity = new Vector2(speed * xVecPart, speed * yVecPart);

        intTime = Time.time;
    }
   
    // Update is called once per frame
    void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (Time.time > intTime + flightTime)
        {
            DestroyProjectile();
        }
        //Attacking AI

        //Check the distance between Enemy and player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer < attackRange)
        {
            //Check to see if enough time passed after the last attack
            if (Time.time > lastAttackTime + attackDelay)
            {
                player.SendMessage("TakeDamage", damage);
                DestroyProjectile();

                //Record the Time we attacked
                lastAttackTime = Time.time;
            }
        }


        // flip
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<CharacterController2D>().TakeDamage(damage);
            Object.Destroy(this.gameObject);
        }
       
    }
    void DestroyProjectile()
    {
        Object.Destroy(this.gameObject);
    }


}

