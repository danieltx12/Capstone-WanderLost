using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public Fireball fireball;

    public AudioClip fireballAudio;

    public AudioClip attackAudio;

    public AudioSource audioSource;

    public float runSpeed = 40f;

    bool jump = false;

    bool glide = false;

    bool doublejump = false;

    public bool canGlide = false;

    public bool canAttack = false;

    float horizontalMove = 0f;

    public bool hasKey = false;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("canMelee", true);
    }

    // Update is called once per frame
    void Update()
    {
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;

        }
       /* if (Input.GetButtonUp("Jump"))
        {
          
            glide = true;
            controller.StopGlide();

        }*/
        if (Input.GetButton("Jump") && glide && jump && canGlide)
        {

            controller.Glide();

        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            runSpeed = 60f;
        }
        else
        {
            runSpeed = 40f;
        }
        if (Input.GetKeyDown(KeyCode.J) && canAttack)
        {
            if (fireball.canShoot)
            {
                animator.SetTrigger("Fire");
                audioSource.clip = fireballAudio;
                audioSource.Play();
            }
            fireball.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.K) && canAttack)
        {
            if (fireball.canMelee)
            {
                animator.SetTrigger("isAttack");
                audioSource.clip = attackAudio;
                audioSource.Play();
                fireball.Melee();
            }
            
        }

        if (horizontalMove != 0 && controller.m_Grounded)
        {
           animator.SetBool("isRun", true);
            
        }
        else
        {
            animator.SetBool("isRun", false);
        }

        if(controller.m_Grounded)
            animator.SetBool("isFalling", false);
        else if(!controller.m_Grounded)
        {
            animator.SetBool("isFalling", true);
            
        }
    }

    private void FixedUpdate()
    {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
    }
}
