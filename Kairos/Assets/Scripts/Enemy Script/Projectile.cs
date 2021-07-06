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
    ArrayList groundLayers = new ArrayList();


    private CharacterController2D characterController2D;

    public int damage = 10;
    public int flightTime = 4;
    private float intTime;
    public bool noClip = false;



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
        this.transform.Rotate(0, 0, Mathf.Atan2(yDist, xDist) * Mathf.Rad2Deg);

        intTime = Time.time;

        groundLayers = new ArrayList();
        groundLayers.Add(0);  //Default
        groundLayers.Add(11); //Floor
        groundLayers.Add(13); //Platform
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

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.tag == "Player")
        {
            other.GetComponent<CharacterController2D>().TakeDamage(damage);
            DestroyProjectile();
        }

        if ((!noClip && groundLayers.Contains(other.gameObject.layer)) || other.gameObject.layer == 14)
        {
            DestroyProjectile();
        }
    }
    void DestroyProjectile()
    {
        Object.Destroy(this.gameObject);
    }


}

