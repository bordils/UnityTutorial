using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;

    void Start()
    {
        Invoke("LoadnextScene", levelLoadDelay);
    }

    private void LoadnextScene()
    {
        SceneManager.LoadScene(1);
    }
}
