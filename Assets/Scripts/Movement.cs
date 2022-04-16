using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController controller;

    public Fireball fireball;

    public float runSpeed = 40f;

    bool jump = false;

    bool glide = false;

    bool doublejump = false;

    public bool canGlide = false;

    public bool canAttack = false;

    float horizontalMove = 0f;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            animator.SetTrigger("isJump");
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
            fireball.Shoot();
        }
        if (Input.GetKeyDown(KeyCode.K) && canAttack)
        {
            animator.SetTrigger("isAttack");
            fireball.Melee();
        }

        if (horizontalMove != 0)
        {
            animator.SetBool("isRun", true);
        }
        else
        {
            animator.SetBool("isRun", false);
        }
    }

    private void FixedUpdate()
    {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
    }
}
