using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    Movement movement;
    public AudioSource audioSource;
    public AudioClip audioClip;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            movement = collision.gameObject.GetComponent<Movement>();
            if(movement.hasKey)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
                movement.hasKey = !movement.hasKey;
                Destroy(this.gameObject);
            }
        }
    }
}
