using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraControl : MonoBehaviour
{
    
    public void moveCamera(Vector3 newPos)
    {
        newPos.z = -10;
        transform.position = newPos;
    }
    public void pause()
    {
        Time.timeScale = 0;
    }

    public void unpause()
    {
        Time.timeScale = 1;
    }

    void Start(){
       // pause();
    }

    void Update(){
        int sceneIndex = SceneManager.GetActiveScene().buildIndex;
         if(sceneIndex == 0 ){
           //pause();
        }
    }

    public void camToStart()
    {
        Vector3 zero = new Vector3(0, 0, -10);
        moveCamera(zero);
    }
}
