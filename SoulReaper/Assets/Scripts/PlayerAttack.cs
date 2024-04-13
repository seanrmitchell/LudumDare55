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
    public Animator anim;

    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("Attacking");
            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, mousePos - transform.position);
            
            foreach(RaycastHit2D obj in hit)
            {
                if (obj.collider.gameObject.CompareTag("Enemy") && Vector3.Distance(obj.collider.transform.position, transform.position) <= attackDistance)
                {
                    Debug.Log(obj.collider.gameObject.name + " GOT HIT!!");
                    obj.collider.gameObject.GetComponent<Health>().UpdateHealth(attackDamage);
                }
            }
        }

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
