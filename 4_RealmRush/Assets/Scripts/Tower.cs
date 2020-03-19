using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform objectToPan;
    [SerializeField] float attackRange;
    [SerializeField] ParticleSystem projectileParticle;

    public Waypoint waypoint;

    Transform target;

    void Update()
    {
        SetTarget();
        if (target)
        {
            objectToPan.LookAt(target);
            FireAtEnemy();
        }
        else
        {
            Shoot(false);
        }
    }

    private void SetTarget()
    {
        var enemies = FindObjectsOfType<EnemyDamage>();
        if(enemies.Length == 0) { return; }

        Transform closestEnemy = enemies[0].transform;

        foreach(EnemyDamage testEnemy in enemies)
        {
            closestEnemy = getClosest(closestEnemy, testEnemy.transform);
        }
        target = closestEnemy;
    }

    private Transform getClosest(Transform closestEnemy, Transform testEnemy)
    {
        float d1 = Vector3.Distance(gameObject.transform.position,closestEnemy.position);
        float d2 = Vector3.Distance(gameObject.transform.position, testEnemy.position);
        if(d1 < d2)
        {
            return closestEnemy;
        }
        else
        {
            return testEnemy;
        }
    }

    private void FireAtEnemy()
    {
        float distanceToEnemy = Vector3.Distance(gameObject.transform.position,target.transform.position);
        if(distanceToEnemy < attackRange)
        {
            Shoot(true);
        } else
        {
            Shoot(false);
        }
    }

    private void Shoot(bool v)
    {
        var emissionModule = projectileParticle.emission;
        emissionModule.enabled = v;
    }
}
