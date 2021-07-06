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
        float divider = ctrl_Player.GetMaxLifePoints() / 5;

        // 125 - 100 - 75 - 50 - 25 - 0
        if (ctrl_Player.GetLifePoints() / divider > 4)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Healthbar5;
        }
        else if (ctrl_Player.GetLifePoints() / divider > 3)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Healthbar4;
        }
        else if (ctrl_Player.GetLifePoints() / divider > 2)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Healthbar3;
        }
        else if (ctrl_Player.GetLifePoints() / divider > 1)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Healthbar2;
        }
        else if (ctrl_Player.GetLifePoints() / divider > 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Healthbar1;
        }
        else if (ctrl_Player.GetLifePoints() <= 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().sprite = Healthbar0;
        }
    }
}
