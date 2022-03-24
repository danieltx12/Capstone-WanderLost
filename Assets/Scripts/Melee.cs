using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{
    public float Speed = 4.5f;
    public int dmg = 10;
    HP hp;

    private void Update()
    {
        transform.position += transform.right * Time.deltaTime * Speed;
        StartCoroutine(DestroyTime());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 8)
        {
            hp = collision.gameObject.GetComponent<HP>();
            hp.Damage(dmg);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.layer == 8)
        {
            Debug.Log("IGNORING  COLLISION");
        }
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(0.001f);
        Destroy(this.gameObject);
    }
}
