using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Invoke("LoadnextScene", levelLoadDelay);
    }

    private void LoadnextScene()
    {
        SceneManager.LoadScene(1);
    }
}
