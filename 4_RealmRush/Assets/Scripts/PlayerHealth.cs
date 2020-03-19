using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int healthPoints = 10;
    [SerializeField] int healthDecrease = 1;
    [SerializeField] Text healthText;
    [SerializeField] AudioClip enemyHitSXF;

    private void Start()
    {
        healthText.text = healthPoints.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        print("what the fuck bro");
        GetComponent<AudioSource>().PlayOneShot(enemyHitSXF);
        //Debug.Break();

        healthPoints -= healthDecrease;
        healthText.text = healthPoints.ToString(); 
    }
}
