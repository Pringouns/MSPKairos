using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public CharacterController2D controller;
    [SerializeField] public Vector3 spawn;// Set a vector3 named spawn;
    // Update is called once per frame
    void Update()
    {
       if (controller.GetLifePoints() <= 0) 
       {
          transform.position = spawn;
       }
    }
}
