using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyHealth : MonoBehaviour
{
    public float health = 0f;
    public Animator anim;
    public AnimationClip deathAnim;

    [SerializeReference]
    private float currentHealth;

    private SpriteRenderer sprite;
    private Summoning summons;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = health;
        sprite = GetComponent<SpriteRenderer>();
        summons = GameObject.Find("Player").GetComponent<Summoning>();
    }

    public void UpdateHealth(float mod)
    {
        currentHealth -= mod;
        StartCoroutine(GotHit());

        if (currentHealth <= 0f)
        {
            currentHealth = 0f;
            summons.SetSummonCreature(gameObject.layer);
            StartCoroutine(WaitDeath());
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
        gameObject.GetComponent<EnemyAttack>().enabled = false;
        gameObject.GetComponent<EnemyMove>().enabled = false;
        anim.SetTrigger("Dying");
        yield return new WaitForSeconds(deathAnim.length);
        Destroy(gameObject);
    }
}
