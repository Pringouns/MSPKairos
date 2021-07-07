using UnityEngine;
using System.Collections;

public class BossProjectile : MonoBehaviour
{
    public float speed;
    private Transform player;
    private Vector2 target;
    public int damage;
   
    



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
