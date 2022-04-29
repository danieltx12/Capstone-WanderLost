using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject Door;
    Animator animator;
    bool isPressed = false;
    public AudioSource audioSource;
    public AudioClip audioClip;
    void Start()
    {
        animator = GetComponent<Animator>();
        Door.SetActive(true);
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isPressed)
        {
            audioSource.clip = audioClip;
            audioSource.Play();
            animator.SetTrigger("isPressed");
            Door.SetActive(false);
            isPressed = true;
        }
    }
}
