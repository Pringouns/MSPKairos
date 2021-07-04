using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnpointControl : MonoBehaviour
{
    public GameObject[] objects;

    // Start is called before the first frame update
    public GameObject spawn()
    {
        int rand = Random.Range(0, objects.Length);
        return Instantiate(objects[rand], transform.position, Quaternion.identity);
    }
}
