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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            
                jump = true;

        }
        if (Input.GetButtonUp("Jump"))
        {

            glide = true;
            controller.StopGlide();

        }
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
        if (Input.GetKey(KeyCode.J) && canAttack)
        {
            fireball.Shoot();
        }
        if (Input.GetKey(KeyCode.K) && canAttack)
        {
            fireball.Melee();
        }
    }

    private void FixedUpdate()
    {
            controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
    }
}
