using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class PlayerAttack : MonoBehaviour
{
    public float attackRad;
    public float attackDistance;
    public float attackDamage;
    public float attackSpeed;
    
    public Transform attackPos;
    public LayerMask enemyLayer;
    public Animator anim;

    private Vector3 mousePos;
    private bool hasAttack = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        if (hasAttack && Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attacking");
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, mousePos - transform.position, attackDistance, enemyLayer);
            
            foreach(RaycastHit2D obj in hit)
            {
                if (Vector3.Distance(obj.collider.transform.position, transform.position) <= attackDistance)
                {
                    Debug.Log(obj.collider.gameObject.name + " GOT HIT!!");
                    obj.collider.gameObject.GetComponent<EnemyHealth>().UpdateHealth(attackDamage);
                }
            }

            StartCoroutine(WaitAttack());
        }

    }

    IEnumerator WaitAttack()
    {
        hasAttack = false;
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        hasAttack = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(mousePos, 1f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, mousePos - transform.position);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
