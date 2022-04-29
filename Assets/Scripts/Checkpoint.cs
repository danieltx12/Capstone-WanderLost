using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    GameController gameController;
    HP hp;
    public AudioSource audioSource;
    public AudioClip audioClip;

    private void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gameController.lastCheckPointPos = transform.position;
            hp = collision.gameObject.GetComponent<HP>();
            hp.Heal(100);
            audioSource.clip = audioClip;
            audioSource.Play();
        }
    }
}
