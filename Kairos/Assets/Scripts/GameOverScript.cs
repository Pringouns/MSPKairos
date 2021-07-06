﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public void BackToMain(){
        SceneManager.LoadScene(0);
    }


    public void QuitGame() {
        Application.Quit();
    }
}
