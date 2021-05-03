using UnityEngine;
using System.Collections;

public class ShootingEnemy : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    [SerializeField]  int currentHealth;
    [SerializeField] int maxHealth = 100;
    public EnemyState currentState;
  

    public Transform player;
    public enum EnemyState
    {
        alive,
        death
    }


    // Use this for initialization
    void Start()
    {
        currentState = EnemyState.alive;
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;

    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.alive:
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
                    Instantiate(projectile, transform.position, Quaternion.identity);
                    timeBtwShots = startTimeBtwShots;
                }
                else
                {
                    timeBtwShots -= Time.deltaTime;
                }

                break;
            case EnemyState.death:
                Debug.Log("Enemy state is death");
                Die();
                break;
        }
    }
      
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentState = EnemyState.death;
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        Destroy(gameObject);
    }



}
