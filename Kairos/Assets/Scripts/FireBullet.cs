using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullet : MonoBehaviour
{
   public CharacterController2D ctrl_Player;
   public cannonScript ctrl_cannon; // zum testen wegen dmg
   public float speed        = 20f;

   //public GameObject Enemy;
   public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
       ctrl_cannon = FindObjectOfType<cannonScript>();
       ctrl_Player = FindObjectOfType<CharacterController2D>();
       rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D Col)
    {
       if (Col.CompareTag("Enemy"))
       {
          ctrl_cannon.m_CannonLP -= 50;
          //ctrl_Player.GetDamage(bulletLPremove); // 20 dmg bei fernkampfangriff
          Destroy(this.gameObject);
       }
    }
}
