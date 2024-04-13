using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage;
    public float attackSpeed;
    public float attackRadius;
    public GameObject target;

    private bool hasAttack = true;



    private void Update()
    {
        if (hasAttack && Vector3.Distance(target.transform.position, transform.position) <= attackRadius)
        {
            Debug.Log("Attack Ready!");
            
            foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, attackRadius))
            {
                if (col.gameObject == target)
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
