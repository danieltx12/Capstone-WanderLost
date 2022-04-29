using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthUpgrade : MonoBehaviour
{

    HP player;
    public AudioClip audioClip;
    public AudioSource audioSource;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<HP>();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
        if (collision.CompareTag("Player"))
        {
            player.Upgrade();
            player.Heal(100);
            Destroy(this.gameObject);
        }
    }
}
