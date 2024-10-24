using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public int lvlToLoad;

    private void LoadLevel() 
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);    
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
            other.GetComponent<GatherInput1>().DisableControls();
            LoadLevel();
        }
    }
}

