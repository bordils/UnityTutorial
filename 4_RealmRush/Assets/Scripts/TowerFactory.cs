using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] int towerLimit;
    [SerializeField] Tower tower;
    [SerializeField] Transform TowerParentTransform;

    Queue<Tower> towerQueue = new Queue<Tower>();

    public void AddTower(Waypoint baseWaypoint)
    {
        int numTowers = towerQueue.Count;

        if (numTowers < towerLimit)
        {
            InstantiateNewTower(baseWaypoint);
            numTowers = numTowers + 1;
        }
        else
        {
            MoveExistingTower(baseWaypoint);
        }     
    }

    private void MoveExistingTower(Waypoint baseWaypoint)
    {
        var oldTower = towerQueue.Dequeue();
        oldTower.waypoint.isPlaceable = true;

        oldTower.waypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        oldTower.transform.position = baseWaypoint.transform.position;
        towerQueue.Enqueue(oldTower);
    }

    private void InstantiateNewTower(Waypoint baseWaypoint)
    {
        var towerInstance = Instantiate(tower, baseWaypoint.transform.position, Quaternion.identity);
        towerInstance.transform.parent = TowerParentTransform;
        towerInstance.waypoint = baseWaypoint;
        baseWaypoint.isPlaceable = false;

        towerQueue.Enqueue(towerInstance);
    }
}
