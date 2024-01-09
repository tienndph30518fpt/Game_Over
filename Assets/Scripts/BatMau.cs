using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BatMau : MonoBehaviour
{
    public GameObject panelMau;
   
    

    public void Mau()
    {
        panelMau.SetActive(true);
        Time.timeScale = 0;
    }
    public void Continue()
    {
        Time.timeScale = 1;
        panelMau.SetActive(false);
    }
    public void loadScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    
    public void loadPlay()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ExitGame()
    {
        Application.Quit();
    }

  

    
}
