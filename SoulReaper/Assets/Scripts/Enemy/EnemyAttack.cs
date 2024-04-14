using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage;
    public float attackSpeed;
    public float attackRadius;
    private bool hasAttack = true;

    private void Update()
    {
        if (hasAttack)
        {
            foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, attackRadius))
            {
                if (!col.CompareTag("Enemy") && Vector2.Distance(col.transform.position, transform.position) <= attackRadius)
                {
                    Debug.Log(col.gameObject.name + "GOT HIT!");
                    col.gameObject.GetComponent<Health>().UpdateHealth(attackDamage);
                    StartCoroutine(AttackCycle(attackSpeed));
                }
            }
        }
    }

    IEnumerator AttackCycle(float waitTime)
    {
        hasAttack = false;
        yield return new WaitForSeconds(waitTime);
        hasAttack = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
