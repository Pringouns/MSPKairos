using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHorizontalControl : MonoBehaviour, DoorControl
{
    public GameObject left;
    public GameObject right;
    public bool isOpen = true;

    private DoorPartControl leftDoor;
    private DoorPartControl rightDoor;

    private void Start()
    {
        leftDoor = left.GetComponent<DoorPartControl>();
        rightDoor = right.GetComponent<DoorPartControl>();
    }

    public void activateDoor()
    {
        if (isOpen)
        {
            isOpen = false;
            leftDoor.moveRight();
            rightDoor.moveLeft();
        }
        else
        {
            isOpen = true;
            leftDoor.moveLeft();
            rightDoor.moveRight();
        }
    }
}
