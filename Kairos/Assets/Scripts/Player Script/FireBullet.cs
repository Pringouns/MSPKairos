using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
    public float timeToDestory = 0.5f;
    public float speed = 20f;
    public int damage = 100;
    Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        //Assignment of the control variables
        rb2d = GetComponent<Rigidbody2D>();

        rb2d.velocity = transform.right * speed;
        this.transform.localScale = new Vector2(-1, 1);
        Destroy(this.gameObject, timeToDestory); // destroy after shooting in 1 second
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
        Debug.Log(Col.gameObject.layer);
        if (Col.gameObject.layer != 8)
        {
            EnemyBase eb = Col.GetComponent<EnemyBase>();
            if (eb != null)
            {
                eb.TakeDamage(damage);
            }
            Destroy(this.gameObject);
        }
    }
}
