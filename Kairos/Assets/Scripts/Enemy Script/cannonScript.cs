﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonScript : MonoBehaviour
{

    public float Range;
    private GameObject player;
    [SerializeField]public int m_CannonLP = 500;        
    bool Detected = false;
    public GameObject Cannonball;
    public float FireRate;
    float nextTimeToFire = 0;
    public float Force;
    public Transform shootPoint;
    Vector2 Direction;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindWithTag("Player");
        Vector2 targetPos = player.transform.position;

        Direction = targetPos - (Vector2)transform.position;

        RaycastHit2D rayInfo = Physics2D.Raycast(transform.position,Direction,Range);


        if (rayInfo) 
        {
            if (rayInfo.collider.gameObject.tag == "Player")
            {
                if (Detected == false)
                {
                    Detected = true;
                }
            }

            else
            {
                if (Detected == true) 
                {
                    Detected = false;
                }
            }

        }

        if (Detected) 
        {
            gameObject.transform.right = Direction * -1;
            if (Time.time > nextTimeToFire) 
            {
                nextTimeToFire = Time.time + 1 / FireRate;
                shoot();
            }

            Detected = false;
        }

        if (m_CannonLP <= 0) 
        {
           DestroyCannon();
        }
    }

    void shoot() 
    {
        GameObject CannonballIns = Instantiate(Cannonball, shootPoint.position, Quaternion.identity);
        CannonballIns.GetComponent<Rigidbody2D>().AddForce(Direction * Force);
    }


    void OnDrawGizmosSelected() 
    {
        Gizmos.DrawWireSphere(transform.position, Range);
    }

    public void DestroyCannon() 
    {
       Destroy(this.gameObject);
    }
}
