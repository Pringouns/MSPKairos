using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    public GameObject[] doors;
    public GameObject[] mobSpawns;
    public List<GameObject> mobs;

    private CameraControl cam;

    public bool isCleared = false;
    public bool hasSpawned = false;

    void Start()
    {
        cam = Camera.main.GetComponent<CameraControl>();
    }

    void FixedUpdate()
    {
        if (hasSpawned == true && isCleared == false)
        {
            bool mobsAlive = false;
            foreach(GameObject mob in mobs)
            {
                EnemyBase mobScript = mob.GetComponent<EnemyBase>();
                if (mobScript.isAlive())
                {
                    mobsAlive = true;
                }
            }

            if (!mobsAlive)
            {
                isCleared = true;
                activateDoors();
            }
        }
    }

    public void onPlayerEnter()
    {
        cam.moveCamera(this.transform.position);

        if (hasSpawned == false)
        {
            activateDoors();
            foreach (GameObject mobSpawn in mobSpawns)
            {
                SpawnpointControl spawnController = mobSpawn.GetComponent<SpawnpointControl>();
                mobs.Add(spawnController.spawn());
            }

            this.hasSpawned = true;
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
