using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject panlelPause;
    public void loadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
    
  
    
    
}