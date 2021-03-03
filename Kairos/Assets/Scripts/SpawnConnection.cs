using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnConnection : MonoBehaviour
{
    public GameObject[] standardObjects;
    public GameObject[] specialObjects;
    public int maxObjects;
    public static int standardObjectCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(standardObjectCount < maxObjects)
        {
            int rand = Random.Range(0, standardObjects.Length);
            Instantiate(standardObjects[rand], transform.position, Quaternion.identity);
        }
        else
        {
            int rand = Random.Range(0, specialObjects.Length);
            Instantiate(specialObjects[rand], transform.position, Quaternion.identity);
        }

        standardObjectCount++;
    }
}
