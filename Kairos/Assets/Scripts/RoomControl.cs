using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomControl : MonoBehaviour
{
    public GameObject[] doors;
    private bool wasActive = false;

    private void activateDoors()
    {
        foreach(GameObject door in doors)
        {
            DoorControl doorController = door.GetComponent<DoorControl>();
            doorController.activateDoor();
        }
    }
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("Player") && trigger.GetType() == typeof(UnityEngine.BoxCollider2D) && wasActive == false)
        {
            Debug.Log("Activated");
            wasActive = true;
            activateDoors();
            StartCoroutine(waiter());
        }
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(10);
        activateDoors();
    }
}
