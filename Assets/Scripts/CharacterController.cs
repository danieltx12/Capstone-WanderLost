using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterController : MonoBehaviour
{

    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private float m_DoubleJumpForce = 400f;// Amount of force added when the player jumps.
    [Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
    [SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
    [SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
    [SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings

    const float k_GroundedRadius = .5f; // Radius of the overlap circle to determine if grounded
    private bool m_Grounded;            // Whether or not the player is grounded.
    const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
    private Rigidbody2D m_Rigidbody2D;
    private bool m_FacingRight = true;  // For determining which way the player is currently facing.
    private Vector3 m_Velocity = Vector3.zero;
    private bool m_doubleJump = false;
    private bool m_jumping = false;
    private float m_vel;
    public bool canGlide = false;
    private static CharacterController instance;
    [Header("Events")]
    [Space]

    public UnityEvent OnLandEvent;

    [System.Serializable]
    public class BoolEvent : UnityEvent<bool> { }

    private void Awake()
    {
        m_Rigidbody2D = GetComponent<Rigidbody2D>();

        if (OnLandEvent == null)
            OnLandEvent = new UnityEvent();
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = m_Grounded;
        bool doubleJump = m_doubleJump;
        m_Grounded = false;
        m_vel = m_Rigidbody2D.velocity.y;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
        // This can be done using layers instead but Sample Assets will not overwrite your project settings.
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                m_Grounded = true;
                m_doubleJump = true;
                m_jumping = false;
                if (!wasGrounded)
                    OnLandEvent.Invoke();
                m_Rigidbody2D.gravityScale = 3;
            }
        }

       
    }


    public void Move(float move, bool jump)
    {
   

            // Move the character by finding the target velocity
            Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
            // And then smoothing it out and applying it to the character
            m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref m_Velocity, m_MovementSmoothing);

            // If the input is moving the player right and the player is facing left...
           if (move > 0 && !m_FacingRight)
            {
                // ... flip the player.
                FlipRight();
            }
            // Otherwise if the input is moving the player left and the player is facing right...
            else if (move < 0 && m_FacingRight)
            {
                // ... flip the player.
                FlipLeft();
            } 
        // If the player should jump...
        if (m_Grounded && jump)
        {
            // Add a vertical force to the player.
            m_Grounded = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
            m_jumping = true;
        }
        if(!m_Grounded && jump && m_doubleJump && canGlide)
        {
            // Add a vertical force to the player.
            m_doubleJump = false;
            m_Rigidbody2D.AddForce(new Vector2(0f, m_DoubleJumpForce));
            if (m_Rigidbody2D.velocity.y < 0)
                {
                m_vel = m_Rigidbody2D.velocity.y * -3;
                }
            m_vel = Mathf.Clamp(m_vel, -1000, 2);
            

           
        }
    }


    private void FlipLeft()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        //Vector3 theScale = transform.localScale;
        // theScale.x *= -1;
        //transform.localScale = theScale;

        transform.rotation = Quaternion.Euler(0, 180, 0);
        
    }
    private void FlipRight()
    {
        // Switch the way the player is labelled as facing.
        m_FacingRight = !m_FacingRight;

        // Multiply the player's x local scale by -1.
        //Vector3 theScale = transform.localScale;
        // theScale.x *= -1;
        //transform.localScale = theScale;

        transform.rotation = Quaternion.Euler(0, 0, 0);

    }

    public void Glide()
    {
        //m_Rigidbody2D.gravityScale = 0.5f;
    }
    public void StopGlide()
    {
        m_Rigidbody2D.gravityScale = 3f;
    }
    
}