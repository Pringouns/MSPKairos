using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifePointsUI_Script : MonoBehaviour
{
   Text text;
   public CharacterController2D ctrl_Player;

    // Start is called before the first frame update
    void Start()
    {
       ctrl_Player = FindObjectOfType<CharacterController2D>();
       text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
       text.text = ctrl_Player.GetLifePoints().ToString();
    }
}
