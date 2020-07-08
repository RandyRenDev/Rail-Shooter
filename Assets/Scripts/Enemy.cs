using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyDeathFX;
    [SerializeField] Transform parent;

    public void Start()
    {
        BoxCollider boxCollider = gameObject.AddComponent<BoxCollider>();
        boxCollider.isTrigger = false;
    }
    private void OnParticleCollision(GameObject other)
    {
        GameObject explosionFX = Instantiate(enemyDeathFX, transform.position, Quaternion.identity);
        explosionFX.transform.parent = parent;
        ScoreBoard.updateScore();
        Destroy(gameObject);
        
    }
}
