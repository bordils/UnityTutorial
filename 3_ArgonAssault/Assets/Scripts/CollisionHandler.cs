using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [Tooltip("in s")][SerializeField] float loadDelay = 2f;
    [Tooltip("FX Particle on plane")][SerializeField] GameObject explosionFX;

    private void OnTriggerEnter(Collider other)
    {
        StartDeathSequence();
        explosionFX.SetActive(true);
        Invoke("ReloadScene",loadDelay);
    }

    private void ReloadScene()
    {
        SceneManager.LoadScene(1);
    }

    private void StartDeathSequence()
    {
        gameObject.SendMessage("StopMotion");
    }
}
