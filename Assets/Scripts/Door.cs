using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    PlayerManager tyler;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        tyler = other.GetComponent<PlayerManager>();

        if (other.CompareTag("Player"))
        {
            if (tyler.checkForKey() == true)
            {
                SceneManager.LoadScene(2);
            } 
        }
    }
}
