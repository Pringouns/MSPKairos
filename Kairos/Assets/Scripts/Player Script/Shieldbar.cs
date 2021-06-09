using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shieldbar : MonoBehaviour
{
   public CharacterController2D ctrl_Player;
   public Sprite Shieldbar5;
   public Sprite Shieldbar4;
   public Sprite Shieldbar3;
   public Sprite Shieldbar2;
   public Sprite Shieldbar1;
   public Sprite Shieldbar0;
    // Start is called before the first frame update
    void Start()
    {
       ctrl_Player = FindObjectOfType<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // 125 - 100 - 75 - 50 - 25 - 0
       if (ctrl_Player.m_shieldPoints <= 125 && ctrl_Player.m_shieldPoints > 100)
       {
          this.gameObject.GetComponent<SpriteRenderer>().sprite = Shieldbar5;
       }
       if (ctrl_Player.m_shieldPoints <= 100 && ctrl_Player.m_shieldPoints > 75)
       {
          this.gameObject.GetComponent<SpriteRenderer>().sprite = Shieldbar4;
       }
       if (ctrl_Player.m_shieldPoints <= 75 && ctrl_Player.m_shieldPoints > 50)
       {
          this.gameObject.GetComponent<SpriteRenderer>().sprite = Shieldbar3;
       }
       if (ctrl_Player.m_shieldPoints <= 50 && ctrl_Player.m_shieldPoints > 25)
       {
          this.gameObject.GetComponent<SpriteRenderer>().sprite = Shieldbar2;
       }
       if (ctrl_Player.m_shieldPoints <= 25 && ctrl_Player.m_shieldPoints > 0)
       {
          this.gameObject.GetComponent<SpriteRenderer>().sprite = Shieldbar1;
       }
       if (ctrl_Player.m_shieldPoints <= 0)
       {
          this.gameObject.GetComponent<SpriteRenderer>().sprite = Shieldbar0;
       }
    }
}
