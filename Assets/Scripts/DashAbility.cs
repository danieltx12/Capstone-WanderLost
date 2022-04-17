using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : MonoBehaviour
{
    public DashState dashState;
    public float dashTimer;
    public float maxDash = 20f;
    public float dashForce = 250f;
    public float horizontalForce = 20f;
    public bool canDash = false;

    //public Vector2 savedVelocity;
    public Rigidbody2D rigid;

    void Update()
    {
        switch (dashState)
        {
            case DashState.Ready:
                var isDashKeyDown = Input.GetKeyDown(KeyCode.E);
                if (isDashKeyDown && canDash)
                {
                    //savedVelocity = rigid.velocity;
                    float horizontal = Input.GetAxisRaw("Horizontal");
                    float vertical = Input.GetAxisRaw("Vertical");
                    if(horizontal == 0 && vertical == 0)
                    {
                        horizontal = transform.right.x;
                    }
                    rigid.AddForce(new Vector2(horizontal * dashForce * horizontalForce, vertical * dashForce));
                    dashState = DashState.Dashing;
                }
                break;
            case DashState.Dashing:
                dashTimer += Time.deltaTime * 3;
                if (dashTimer >= maxDash)
                {
                    dashTimer = maxDash;
                    //rigid.velocity = savedVelocity;
                    dashState = DashState.Cooldown;
                }
                break;
            case DashState.Cooldown:
                dashTimer -= Time.deltaTime;
                if (dashTimer <= 0)
                {
                    dashTimer = 0;
                    dashState = DashState.Ready;
                }
                break;
            default:
                break;
        }
    }
}

public enum DashState
{
    Ready,
    Dashing,
    Cooldown
}

