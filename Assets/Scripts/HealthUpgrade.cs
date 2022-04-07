using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour
{

    HP player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HP>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.Upgrade();
            Destroy(this.gameObject);
        }
    }
}
