using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public void moveCamera(Vector3 newPos)
    {
        transform.position += newPos;
    }
    public void pause()
    {
        Time.timeScale = 0;
    }

    public void unpause()
    {
        Time.timeScale = 1;
    }
}
