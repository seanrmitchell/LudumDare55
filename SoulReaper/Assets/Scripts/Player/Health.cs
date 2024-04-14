using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 0f;

    [SerializeReference]
    private float currentHealth;

    private SpriteRenderer sprite;
    private Summoning summons;

    

    public void UpdateHealth(float mod)
    {
        currentHealth -= mod;
        StartCoroutine(GotHit());

        if (currentHealth <= 0f) 
        {
            currentHealth = 0f;
            Debug.Log(gameObject.name + " DIED!!");
        }
    }

    IEnumerator GotHit()
    {
        var spriteCol = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = spriteCol;
    }

}
