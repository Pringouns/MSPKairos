using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heart_script : MonoBehaviour
{
   public CharacterController2D ctrl_Player;
   [SerializeField] private int m_AddLifePoints = 50;
   void Start() 
   {
      ctrl_Player = FindObjectOfType<CharacterController2D>();
   }
   void OnTriggerEnter2D(Collider2D col) 
   {
      ctrl_Player.GetHealth(m_AddLifePoints);
      Destroy(this.gameObject);
   }
}
