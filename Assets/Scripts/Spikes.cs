using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public int dmg = 500000;
    HP hp;
    private void OnCollisionEnter2D(Collision2D collision)
    {
            hp = collision.gameObject.GetComponent<HP>();
            hp.Damage(dmg);
    }
}
