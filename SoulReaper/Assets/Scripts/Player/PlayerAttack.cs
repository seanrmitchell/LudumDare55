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
    
    public Transform attackLoc;
    public Animator animPlayer;
    public Animator animSlash;

    public LayerMask enemyLayer;

    public Score score;

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
            attackLoc.rotation = Quaternion.identity;
            attackLoc.position = transform.position + Vector3.ClampMagnitude(mousePos - attackLoc.position, attackDistance);
            
            Vector2 objPos;
            objPos.x = mousePos.x - attackLoc.position.x;
            objPos.y = mousePos.y - attackLoc.position.y;
            var targetAngle = Mathf.Atan2(objPos.x, objPos.y) * Mathf.Rad2Deg;
            Debug.Log(targetAngle);
            attackLoc.rotation = Quaternion.Euler(0f, 0f, targetAngle);
            

            animPlayer.SetTrigger("Attacking");

            RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, mousePos - transform.position, attackDistance, enemyLayer);
            
            foreach(RaycastHit2D obj in hit)
            {
                if (Vector3.Distance(obj.collider.transform.position, transform.position) <= attackDistance)
                {
                    Debug.Log(obj.collider.gameObject.name + " GOT HIT!!");
                    obj.collider.gameObject.GetComponent<EnemyHealth>().UpdateHealth(attackDamage);

                    if (!obj.collider.gameObject.GetComponent<EnemyHealth>().isAlive)
                    {
                        score.soulsCollected++;
                    }
                }
            }

            StartCoroutine(WaitAttack());
        }

    }

    public void StartSlash()
    {
        animSlash.SetTrigger("Attacking");
    }

    IEnumerator WaitAttack()
    {
        hasAttack = false;
        yield return new WaitForSeconds(animPlayer.GetCurrentAnimatorStateInfo(0).length);
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
