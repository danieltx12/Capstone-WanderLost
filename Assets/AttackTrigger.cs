using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackTrigger : MonoBehaviour
{
    public float speedMult;
    public EnemyAI enemyAI;

 
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //enemyAI.animator.SetTrigger("Attack");
            StartCoroutine("Attack");

        }
    }

    IEnumerator Attack()
    {
        Debug.Log("ATTACKING");
        enemyAI.speed = enemyAI.originalSpeed * speedMult;
        yield return new WaitForSeconds(0.2f);
        Debug.Log("DONE");
        enemyAI.speed = enemyAI.originalSpeed;
    }
}
