using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour
{
   public CharacterController2D ctrl_Player;
    // Start is called before the first frame update
    void Start()
    {
       ctrl_Player = FindObjectOfType<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D Col) 
    {
        Destroy(this.gameObject);

       //if collision = player??
        ctrl_Player.GetDamage();
    }
}
