using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    public GameObject[] doors;
    private bool wasActive = false;
    private CameraControl cam;

    private void Start()
    {
        cam = Camera.main.GetComponent<CameraControl>();
    }

    public void onPlayerEnter()
    {
        cam.moveCamera(this.transform.position);
        activateDoors();
        StartCoroutine(waiter());
    }

    private void activateDoors()
    {
        foreach(GameObject door in doors)
        {
            DoorControl doorController = door.GetComponent<DoorControl>();
            doorController.activateDoor();
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(10);
        activateDoors();
    }
}
