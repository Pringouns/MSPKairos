using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorVerticalControl : MonoBehaviour, DoorControl
{
    public GameObject top;
    public GameObject bottom;
    public bool isOpen = true;

    private DoorPartControl topDoor;
    private DoorPartControl bottomDoor;

    private void Start()
    {
        topDoor = top.GetComponent<DoorPartControl>();
        bottomDoor = bottom.GetComponent<DoorPartControl>();
    }

    public void activateDoor()
    {
        if (isOpen)
        {
            isOpen = false;
            topDoor.moveDown();
            bottomDoor.moveUp();
        }
        else
        {
            isOpen = true;
            topDoor.moveUp();
            bottomDoor.moveDown();
        }
    }
}
