using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 0f;
    public bool isPlayer = false;

    [SerializeReference]
    private float currentHealth;

    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = health;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void UpdateHealth(float mod)
    {
        currentHealth -= mod;
        StartCoroutine(GotHit());

        if (currentHealth <= 0f) 
        {
            currentHealth = 0f;
            Debug.Log(gameObject.name + " DIED!!");

            if (isPlayer)
            {
                Debug.Log("GAME OVER!");
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }

    IEnumerator GotHit()
    {
        var spriteCol = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(1f);
        sprite.color = spriteCol;
    }
}
