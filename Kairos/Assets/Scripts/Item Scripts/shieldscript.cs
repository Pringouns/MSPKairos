using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shieldscript : MonoBehaviour
{
   public CharacterController2D ctrl_Player;
   [SerializeField]private int m_AddShieldPoints = 25;
    // Start is called before the first frame update
    void Start()
    {
       ctrl_Player = FindObjectOfType<CharacterController2D>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
       if (col.CompareTag("Player"))
       {
          ctrl_Player.AddShieldPoints(m_AddShieldPoints);
          Destroy(this.gameObject);
       }
    }
}
