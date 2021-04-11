using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamereChangeRoom : MonoBehaviour
{
    private CameraControl cam;
    public Vector3 moveCam;
    public Vector3 movePlayer;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D trigger)
    {
        if (trigger.CompareTag("Player") && trigger.GetType() == typeof(UnityEngine.BoxCollider2D))
        {
            cam.moveCamera(moveCam);
            CharacterController2D playerControl = trigger.GetComponent<CharacterController2D>();
            playerControl.Stop();
            trigger.transform.position = this.transform.position + movePlayer;
            StartCoroutine(pause());
        }
    }

    IEnumerator pause()
    {
        cam.pause();
        yield return new WaitForSecondsRealtime(1);
        cam.unpause();
    }
}
