using UnityEngine;
using System.Collections;

public class ShootingEnemy : EnemyBase
{
    
    //.......This Script is for the Ranged Enemy.........//

    public float stoppingDistance;
    public float retreatDistance;
    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;

    // Update is called once per frame
    void Update()
    {
        checkHealth();
         if (isAlive())
         {

            // for flip
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


            // for chasing the player
            if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -moveSpeed * Time.deltaTime);
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
}
