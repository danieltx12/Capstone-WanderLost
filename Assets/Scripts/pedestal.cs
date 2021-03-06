using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pedestal : MonoBehaviour
{
    Movement movement;
    public GameObject idolText;
    Animator animator;
    public GameObject finalDoorClosed;
    public GameObject idolRoom;
    public AudioClip clip;
    public AudioSource source;
    void Start()
    {
        animator = GetComponent<Animator>();
        idolText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            movement = collision.GetComponent<Movement>();
            if(movement.hasKey)
            {
                source.clip = clip;
                source.Play();
                animator.SetTrigger("hasTotem");
                finalDoorClosed.SetActive(false);
                idolText.SetActive(false);
            }
            else
            {
                finalDoorClosed.SetActive(true);
                idolText.SetActive(true);
                idolRoom.SetActive(false);
            }
        }
    }
}
