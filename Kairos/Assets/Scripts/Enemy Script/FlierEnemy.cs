using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class FlierEnemy : EnemyBase
{

    //.......This Script is for the flier Enemy.....//



    private float timeBtwShots;
    public float startTimeBtwShots;
    public GameObject projectile;
    public AIPath aiPath;
    AIDestinationSetter aiSetter;


    // Use this for initialization
    override protected void onStart()
    {
        timeBtwShots = startTimeBtwShots;
        aiSetter = GetComponent<AIDestinationSetter>();
        aiSetter.target = player;
    }

    override protected void onDeath()
    {
        this.aiSetter.target = null;
    }

    // Update is called once per frame
    void Update()
    {
        checkHealth();
        if (isAlive())
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
}
