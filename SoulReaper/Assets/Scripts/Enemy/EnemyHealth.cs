using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public Animator anim;
    public GameObject preFabObj;

    [SerializeReference]
    private float currentHealth;

    private SpriteRenderer sprite;
    private Summoning summons;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = health;
        sprite = GetComponent<SpriteRenderer>();
        summons = GameObject.Find("Player").GetComponent<Summoning>();
    }

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
                summons.SetSummonCreature(preFabObj);
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
        gameObject.GetComponent<EnemyAttack>().enabled = false;
        gameObject.GetComponent<EnemyMove>().enabled = false;
        anim.SetTrigger("Dying");
        yield return new WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        Destroy(gameObject);
    }
}
