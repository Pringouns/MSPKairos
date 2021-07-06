using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    int state;

    [SerializeField]
    protected float moveSpeed;

    protected Transform player;

    protected Rigidbody2D rb2d;

    protected SpriteRenderer spriteRenderer;

    public int maxHealth;
    public int currentHealth;

    public GameObject[] dropItems;
    public float dropChance = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        changeState("alive");
        onStart();
    }

    protected virtual void onStart() { }
    protected virtual void onDeath() { }


    protected void filpSprite()
    {
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

    public bool isAlive()
    {
        return this.state == 0;
    }

    protected void changeState(string state)
    {
        switch (state)
        {
            case "alive":
                this.state = 0;
                break;

            case "dead":
                this.state = 1;
                die();
                break;
            default:
                break;
        }
    }

    protected void checkHealth()
    {
        if (isAlive() && currentHealth <= 0)
        {
            changeState("dead");
        }
    }

    protected void die()
    {
        spriteRenderer.enabled = false;
        CircleCollider2D[] circleCol = this.GetComponents<CircleCollider2D>();
        if (circleCol != null)
        {
            foreach (CircleCollider2D c in circleCol)
                Destroy(c);
        }
        Rigidbody2D[] rigid = this.GetComponents<Rigidbody2D>();
        if (rigid != null)
        {
            foreach (Rigidbody2D r in rigid)
                Destroy(r);
        }
        BoxCollider2D[] boxCol = this.GetComponents<BoxCollider2D>();
        if (boxCol != null)
        {
            foreach (BoxCollider2D b in boxCol)
                Destroy(b);
        }
        onDeath();
        dropItem();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        checkHealth();
    }

    protected void dropItem()
    {
        float randF = Random.Range(0, 100) / 100f;
        if (randF <= dropChance && dropItems.Length > 0)
        {
            int rand = Random.Range(0, dropItems.Length);
            Instantiate(dropItems[rand], transform.position, Quaternion.identity);
        }
    }
}

