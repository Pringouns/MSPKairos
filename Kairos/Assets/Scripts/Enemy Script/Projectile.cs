using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;


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

        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);

        intTime = Time.time;
    }
   
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
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

