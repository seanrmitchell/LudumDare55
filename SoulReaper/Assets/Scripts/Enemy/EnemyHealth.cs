using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public float health;
    public Animator anim;
    public GameObject preFabObj;
    public EnemySpawner spawner;
    public bool isAlive = true;

    [SerializeField]
    private AnimationClip animClip;

    [SerializeReference]
    private float currentHealth;

    private SpriteRenderer sprite;
    private Summoning summons;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = health;

        spawner = GameObject.Find("Enemy Spawner").GetComponent<EnemySpawner>();
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

                EnemyDeath();
            }
        }
    }

    void EnemyDeath()
    {
        // Passes coordinating Ally object to Player Summons Class
        summons.SetSummonCreature(preFabObj);

        // Sets -1 to number of enemies in scene, from EnemySpawner class
        spawner.currentNumOfEnemies--;

        // Begins Enemy Death Animation
        anim.SetTrigger("Dying");
        gameObject.GetComponent<EnemyAttack>().enabled = false;
        gameObject.GetComponent<EnemyMove>().enabled = false;
        gameObject.GetComponent<EnemyHealth>().enabled = false;
        
    }

    IEnumerator GotHit()
    {
        // Colors the obj sprite red when it takes damage
        var spriteCol = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = spriteCol;
    }
}
