using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    [SerializeField] GameObject explosion;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        Invoke("ReloadScene", levelLoadDelay);
    }

    private void StartDeathSequence()
    {
        print("Player dying");
        SendMessage("OnPlayerDeath");
        explosion.SetActive(true);
        
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }
}
