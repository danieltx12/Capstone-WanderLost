using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackHitbox : MonoBehaviour
{
    Collider2D hitbox;
    public int dmg;
    HP hp;

    public void Start()
    {
        hitbox = GetComponent<Collider2D>();
    }

   void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hp = collision.gameObject.GetComponent<HP>();
            hp.Damage(dmg);
            
        }
    }
}
