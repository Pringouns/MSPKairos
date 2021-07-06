using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnter : MonoBehaviour
{
    public GameObject room;
    public GameObject playerSpawn;

    RoomControl roomControl;

    private void Start()
    {
        roomControl = room.GetComponent<RoomControl>();
    }
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("Player") && trigger.GetType() == typeof(UnityEngine.EdgeCollider2D))
        {
            CharacterController2D playerControl = trigger.GetComponent<CharacterController2D>();
            playerControl.Stop();
            trigger.transform.position = playerSpawn.transform.position;
            roomControl.onPlayerEnter();
        }
    }
}
