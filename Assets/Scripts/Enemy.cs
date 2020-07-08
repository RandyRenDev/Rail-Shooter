using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathFX;
    [SerializeField] Transform parent;
    [SerializeField] int health;
    ScoreBoard score;

    PlayerController playerController;
    public void Start()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
        score = FindObjectOfType<ScoreBoard>();

        playerController = FindObjectOfType<PlayerController>();
    }

    private void OnParticleCollision(GameObject other)
    {
        if(health <= 0)
        {
            GameObject explosionFX = Instantiate(enemyDeathFX, transform.position, Quaternion.identity);
            explosionFX.transform.parent = parent;
            score.updateScore();
            Destroy(gameObject);
        }
        else
        {
            health -= playerController.GetDamage();
            print(gameObject + " health is: " + health);
        }

        
    }
}
