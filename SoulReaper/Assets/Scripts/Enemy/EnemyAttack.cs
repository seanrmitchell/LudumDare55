using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage;
    public float attackSpeed;
    public float attackRadius;
    private bool hasAttack = true;
    public LayerMask allyLayer;

    private void Update()
    {
        if (hasAttack)
        {
            foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, attackRadius, allyLayer))
            {
                Debug.Log(col.gameObject.name + "GOT HIT!");
                col.gameObject.GetComponent<Health>().UpdateHealth(attackDamage);
                StartCoroutine(AttackCycle(attackSpeed));
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
