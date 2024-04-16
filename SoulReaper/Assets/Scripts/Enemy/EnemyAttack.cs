using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackDamage;
    public float attackSpeed;
    public float attackRadius;
    public ParticleSystem part;
    private bool hasAttack = true;
    public LayerMask allyLayer;

    private void Update()
    {
        if (hasAttack)
        {
            part.Play();
            foreach (Collider2D col in Physics2D.OverlapCircleAll(transform.position, attackRadius, allyLayer))
            {
                Debug.Log(col.gameObject.name + "GOT HIT!");

                try { col.gameObject.GetComponent<Health>().UpdateHealth(attackDamage); }

                catch { col.gameObject.GetComponent<AllyHealth>().UpdateHealth(attackDamage);  }
                
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
