﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] string levelToLoad;


    public void StartGame()
    {

        SceneManager.LoadScene(levelToLoad);
    }




    public void QuitGame()
    {
        Application.Quit();
    } 

}