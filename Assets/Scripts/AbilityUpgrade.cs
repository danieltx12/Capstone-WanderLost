using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUpgrade : MonoBehaviour
{

    [SerializeField] bool isGlideUpgrade;
    [SerializeField] bool isAttackUpgrade;
    [SerializeField] bool isDashUpgrade;
    [SerializeField] bool isKey;
    Fireball fireball;
    CharacterController characterController;
    DashAbility dashAbility;
    Movement movement;
    public AudioClip audioClip;
    public AudioSource audioSource;
    public GameObject tutorial;
   
    void Start()
    {
        tutorial.SetActive(false);
        fireball = GameObject.FindGameObjectWithTag("Player").GetComponent<Fireball>();
        characterController = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        dashAbility = GameObject.FindGameObjectWithTag("Player").GetComponent<DashAbility>();
        movement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        audioSource.clip = audioClip;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            tutorial.SetActive(true);
            audioSource.Play();
            if(isGlideUpgrade)
            {
                movement.canGlide = true;
                characterController.canGlide = true;
                Destroy(this.gameObject);
            }
            else if(isAttackUpgrade)
            {
                movement.canAttack = true;
                Destroy(this.gameObject);
            }
            else if(isDashUpgrade)
            {
                dashAbility.canDash = true;
                Destroy(this.gameObject);
            }
            else if(isKey)
            {
                movement.hasKey = true;
                Destroy(this.gameObject);
            }
        }
    }
}
