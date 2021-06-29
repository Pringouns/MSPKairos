using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlierEnemy : MonoBehaviour
{

    //.......This Script is for the flier Enemy.....//



    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    [SerializeField] int currentHealth = 100;
    [SerializeField] int maxHealth = 100;
    SpriteRenderer spriteRenderer;
    public AIPath aiPath;


    // Use this for initialization
    void Start()
    {
        currentHealth = maxHealth;
        timeBtwShots = startTimeBtwShots;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (spriteRenderer.enabled)
        {
            // for flip

            if (aiPath.desiredVelocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (aiPath.desiredVelocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }

            // to manage the time of shooting 

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

        if (currentHealth <= 0)
        {
            die();
        }
    }

    void die()
    {
        spriteRenderer.enabled = false;
    }

}
