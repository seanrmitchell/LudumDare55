using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.Audio;

public class PlayerAttack : MonoBehaviour
{
    public float attackRad;
    public float attackDistance;
    public float attackDamage;
    public float attackSpeed;
    
    public Transform attackLoc;

    public Animator animPlayer;
    public Animator animSlash;
    public AudioSource attackSound;

    public LayerMask enemyLayer;

    public Score score;

    private Vector3 mousePos;
    private bool hasAttack = true;

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
            var targetAngle = Mathf.Atan2(objPos.y, objPos.x) * Mathf.Rad2Deg;
            Debug.Log(targetAngle);
            targetAngle -= 90f;
            attackLoc.rotation = Quaternion.Euler(new Vector3(0f, 0f, targetAngle));


            animPlayer.SetTrigger("Attacking");
            StartCoroutine(WaitAttack());
        }

    }

    public void StartSlash()
    {
        animSlash.SetTrigger("Attacking");
        attackSound.Play();

        RaycastHit2D[] hit = Physics2D.RaycastAll(transform.position, mousePos - transform.position, attackDistance, enemyLayer);

        foreach (RaycastHit2D obj in hit)
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
