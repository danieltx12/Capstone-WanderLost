using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vines : MonoBehaviour
{
    Collider2D vineCollider;
    Animator animator;
    bool destroyed = false;
    void Start()
    {
        vineCollider = GetComponent<Collider2D>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Fireball" && !destroyed)
        {
            animator.SetTrigger("Destroy");
            vineCollider.isTrigger = true;
            StartCoroutine("Delete");
        }
    }

    IEnumerator Delete()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(this);
    }
}
