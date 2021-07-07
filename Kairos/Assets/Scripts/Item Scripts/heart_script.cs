using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart_script : MonoBehaviour
{
   public CharacterController2D ctrl_Player;
   [SerializeField] private int m_AddLifePoints = 50;
   void Start() 
   {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        ctrl_Player = player.GetComponent<CharacterController2D>();
    }
   void OnTriggerEnter2D(Collider2D col) 
   {
      if (col.CompareTag("Player"))
      {
         ctrl_Player.GetHealth(m_AddLifePoints);
         Destroy(this.gameObject);
      }
   }
}
