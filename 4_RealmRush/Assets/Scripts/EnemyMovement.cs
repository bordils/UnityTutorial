using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float dwellTime = 1f;
    [SerializeField] List<Waypoint> Path;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PrintAllWaypoints());
    }

    IEnumerator PrintAllWaypoints()
    {
        foreach(Waypoint block in Path)
        {
            transform.position = block.transform.position;
            yield return new WaitForSeconds(dwellTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
