using UnityEngine;
using System.Collections;

public class BossProjectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;


    private CharacterController2D characterController2D;

    public float attackRange;
    public int damage;
    private float lastAttackTime;
    public float attackDelay;
    



    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.FindGameObjectWithTag("Player").transform;

        target = new Vector2(player.position.x, player.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (transform.position.x == target.x && transform.position.y == target.y)
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
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        CharacterController2D player = other.GetComponent<CharacterController2D>();
       if (player != null) {
            player.TakeDamage(damage);
        }
   

    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
