using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTriggers : MonoBehaviour
{
    int numEnemies;
    Collider2D spawnTrigger;
    public GameObject[] enemy;

    private void Start()
    {
        spawnTrigger = GetComponent<Collider2D>();
        numEnemies = enemy.Length;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < numEnemies; i++)
            {
                enemy[i].SetActive(true);
                Debug.Log(enemy[i] + "is active");
            }
                
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < numEnemies; i++)
            {
                enemy[i].SetActive(false);
            }

        }
    }
}
