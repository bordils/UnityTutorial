using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour
{
    [SerializeField] Vector3 movementDirection;
    [SerializeField] float period = 2f;

    // todo remove from inspector later
    Vector3 startingPosition;

    void Start()
    {
        startingPosition = transform.position;
    }

    void Update()
    {
        if (Mathf.Abs(period) <= Mathf.Epsilon) { return; }
        float cycle = Time.time / period;
        float pi = Mathf.PI;
        float sinWave = (Mathf.Sin(2*pi*cycle) + 1f) / 2f;
        
        Vector3 offset = movementDirection * sinWave;
        transform.position = startingPosition + offset;
    }
}
