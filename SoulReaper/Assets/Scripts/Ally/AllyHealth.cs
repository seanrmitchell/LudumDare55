using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class AllyHealth : MonoBehaviour
{
    public float health = 0f;
    public Animator anim;

    [SerializeReference]
    private float currentHealth;

    private SpriteRenderer sprite;
    private bool isAlive = true;

    public void UpdateHealth(float mod)
    {
        if (isAlive)
        {
            currentHealth -= mod;
            StartCoroutine(GotHit());

            if (currentHealth <= 0f)
            {
                isAlive = false;
                currentHealth = 0f;
                Debug.Log(gameObject.name + " DIED!!");
                StartCoroutine(WaitDeath());

            }
        }
    }

    IEnumerator GotHit()
    {
        var spriteCol = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = spriteCol;
    }

    IEnumerator WaitDeath()
    {
        gameObject.GetComponent<AllyAttack>().enabled = false;
        anim.SetTrigger("Dying");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
