using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class door : MonoBehaviour
{
    Movement movement;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            movement = collision.gameObject.GetComponent<Movement>();
            if(movement.hasKey)
            {
                movement.hasKey = !movement.hasKey;
                Destroy(this.gameObject);
            }
        }
    }
}
