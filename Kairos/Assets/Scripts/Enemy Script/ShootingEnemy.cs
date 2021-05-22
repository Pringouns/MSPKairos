using UnityEngine;
using System.Collections;

public class ShootingEnemy : MonoBehaviour, State_Pattern
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    [SerializeField]  int currentHealth;
    [SerializeField] int maxHealth = 100;
    StateMachine stateMachine = new StateMachine();

    public Transform player;

    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timeBtwShots = startTimeBtwShots;

    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.ChangeState(new TestState(this), currentHealth);
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

               
              
                Die();


    }
      
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
           
        }
    }

    void Die()
    {
        Debug.Log("Enemy died!");

        Destroy(gameObject);
    }

    public void Alive()
    {
        Debug.Log("GameObject is alive");
    }

    public void Dead()
    {
        Debug.Log("GameObject is dead");
    }
}
