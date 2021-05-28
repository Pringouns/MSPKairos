using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] mobSpawns;

    private CameraControl cam;

    private bool isCleared = false;
    private bool hasSpawned = false;

    private void Start()
    {
        cam = Camera.main.GetComponent<CameraControl>();
    }

    private void FixedUpdate()
    {
        if (hasSpawned == true && isCleared == false)
        {
            GameObject[] mobs = GameObject.FindGameObjectsWithTag("Enemy");
            if (mobs.Length == 0)
            {
                isCleared = true;
                activateDoors();
            }
        }
    }

    public void onPlayerEnter()
    {
        cam.moveCamera(this.transform.position);
        activateDoors();

        if(hasSpawned == false)
        {
            foreach(GameObject mobSpawn in mobSpawns)
            {
                SpawnpointControl spawnController = mobSpawn.GetComponent<SpawnpointControl>();
                spawnController.spawn();
            }

            hasSpawned = true;
        }
    }

    private void activateDoors()
    {
        foreach(GameObject door in doors)
        {
            DoorControl doorController = door.GetComponent<DoorControl>();
            doorController.activateDoor();
        }
    }
}
