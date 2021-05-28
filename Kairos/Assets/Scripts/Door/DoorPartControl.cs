using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPartControl : MonoBehaviour
{
    public float size;

    public void moveLeft()
    {
        this.transform.position += new Vector3(-size, 0);
    }

    public void moveRight()
    {
        this.transform.position += new Vector3(size, 0);
    }

    public void moveUp()
    {
        this.transform.position += new Vector3(0, size);
    }

    public void moveDown()
    {
        this.transform.position += new Vector3(0, -size);
    }

}
