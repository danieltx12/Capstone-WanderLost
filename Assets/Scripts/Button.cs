using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject Door;
    Animator animator;
    bool isPressed = false;
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
            animator.SetTrigger("isPressed");
            Door.SetActive(false);
            isPressed = true;
        }
    }
}
