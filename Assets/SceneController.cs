using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public void playgame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        
    }
    public void QuitGame()
    {
        Application.Quit();
        
    }
}
