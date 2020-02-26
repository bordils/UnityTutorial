using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scoreOnHit = 12;
    [SerializeField] int maxHits = 10;


    ScoreBoard scoreBoard;


    void Start()
    {
        AddNonTriggerBoxCollider();   
        scoreBoard = FindObjectOfType<ScoreBoard>();
    }

    private void AddNonTriggerBoxCollider()
    {
        Collider boxColldier = gameObject.AddComponent<BoxCollider>();
        boxColldier.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other)
    {
        OnHit();
        
        if (maxHits <= 0)
        {
            KillEnemy();
        }
    }

    private void OnHit()
    {
        // to do - add hit effect
        scoreBoard.ScoreHit(scoreOnHit);
        maxHits--;
    }

    private void KillEnemy()
    {
        GameObject fx = Instantiate(deathFX, transform.position, Quaternion.identity);
        fx.transform.parent = parent;
        Destroy(gameObject);
    }
}
