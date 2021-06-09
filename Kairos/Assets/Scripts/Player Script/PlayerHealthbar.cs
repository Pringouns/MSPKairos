using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthbar : MonoBehaviour
{
   public CharacterController2D ctrl_Player;
   public Sprite Healthbar5;
   public Sprite Healthbar4;
   public Sprite Healthbar3;
   public Sprite Healthbar2;
   public Sprite Healthbar1;
   public Sprite Healthbar0;
    // Start is called before the first frame update
    void Start()
    {
       ctrl_Player = FindObjectOfType<CharacterController2D>();
    }

    // Update is called once per frame
    void Update()
    {
       // 125 - 100 - 75 - 50 - 25 - 0
       if (ctrl_Player.GetLifePoints() <= 125 && ctrl_Player.GetLifePoints() > 100) 
       {
          this.gameObject.GetComponent<SpriteRenderer>().sprite = Healthbar5;
       }
       if (ctrl_Player.GetLifePoints() <= 100 && ctrl_Player.GetLifePoints() > 75)
       {
          this.gameObject.GetComponent<SpriteRenderer>().sprite = Healthbar4;
       }
       if (ctrl_Player.GetLifePoints() <= 75 && ctrl_Player.GetLifePoints() > 50)
       {
          this.gameObject.GetComponent<SpriteRenderer>().sprite = Healthbar3;
       }
       if (ctrl_Player.GetLifePoints() <= 50 && ctrl_Player.GetLifePoints() > 25)
       {
          this.gameObject.GetComponent<SpriteRenderer>().sprite = Healthbar2;
       }
       if (ctrl_Player.GetLifePoints() <= 25 && ctrl_Player.GetLifePoints() > 0)
       {
          this.gameObject.GetComponent<SpriteRenderer>().sprite = Healthbar1;
       }
       if (ctrl_Player.GetLifePoints() <= 0)
       {
          this.gameObject.GetComponent<SpriteRenderer>().sprite = Healthbar0;
       }
    }
}
