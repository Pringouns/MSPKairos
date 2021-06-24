using UnityEngine;
using System.Collections;
using Pathfinding;

public class ShootingEnemy : MonoBehaviour
{
    //public float speed;
    //public float stoppingDistance;
    //public float retreatDistance;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    [SerializeField]  int currentHealth = 100;
    [SerializeField] int maxHealth = 100;
    SpriteRenderer spriteRenderer;
    public AIPath aiPath;
    Transform player;
    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
         if (currentHealth <= 0)
         {
          die();
         }
         if (spriteRenderer.enabled)
         {
            // for fliping
            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
    


            //if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            //{
            //   transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            //}
            //else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            //{
            //   transform.position = this.transform.position;
            //}
            //else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            //{
            //   transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
            //}


            if (timeBtwShots <= 0)
            {
               Instantiate(projectile, transform.position, Quaternion.identity);
               timeBtwShots = startTimeBtwShots;
            }
            else
            {
               timeBtwShots -= Time.deltaTime;
            }
         }
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
    }

    void die()
    {
        spriteRenderer.enabled = false;
    }



}
