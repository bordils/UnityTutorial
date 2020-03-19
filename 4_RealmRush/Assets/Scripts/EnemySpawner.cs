using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    [Range(0.1f, 120)]
    [SerializeField] float secondsBetweenSpawns;
    [SerializeField] EnemyMovement enemy;
    [SerializeField] Transform EnemyParentTransform;
    [SerializeField] Text scoreText;
    [SerializeField] AudioClip spawnEnemySFX;
    int score;

    void Start()
    {
        score = 0;
        StartCoroutine(SpawnEnemy());
        scoreText.text = score.ToString();
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            score++;
            scoreText.text = score.ToString();
            GetComponent<AudioSource>().PlayOneShot(spawnEnemySFX);
            var newEnemy = Instantiate(enemy, gameObject.transform.position, Quaternion.identity);
            newEnemy.transform.parent = EnemyParentTransform;
            yield return new WaitForSeconds(secondsBetweenSpawns);
        }
    }
}
