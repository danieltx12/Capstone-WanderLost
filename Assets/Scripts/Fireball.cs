using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Rigidbody2D FireballProj;
    public Rigidbody2D MeleeProj;
    public Transform LaunchOffset;
    public bool canShoot = false;
    public bool canMelee = false;
    Animator animator;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void Shoot()
    {
        if (canShoot)
        {
            Rigidbody2D clone;
            clone = Instantiate(FireballProj, LaunchOffset.position, transform.rotation);
            StartCoroutine(CooldownProj(1f));
        }
    }
    public void Melee()
    {
        if (canMelee)
        { 
        Rigidbody2D clone;
        clone = Instantiate(MeleeProj, LaunchOffset.position, transform.rotation);
        StartCoroutine(CooldownMelee(0.5f));
        }
    }

    IEnumerator CooldownProj(float cd)
    {
        canShoot = !canShoot;
        yield return new WaitForSeconds(cd);
        canShoot = !canShoot;
    }

    IEnumerator CooldownMelee(float cd)
    {
        canMelee = !canMelee;
        animator.SetBool("canMelee", false);
        yield return new WaitForSeconds(cd);
        canMelee = !canMelee;
        animator.SetBool("canMelee", true);
    }
}
