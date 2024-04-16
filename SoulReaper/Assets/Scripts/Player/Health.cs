using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 0f;
    public Animator anim;
    public GameObject gameOverScreen;
    public bool isAlive = true;

    [SerializeReference]
    private float currentHealth;

    private SpriteRenderer sprite;
    private bool gotHit = false;

    void Awake()
    {
        currentHealth = health;
        sprite = GetComponent<SpriteRenderer>();
    }

    public void UpdateHealth(float mod)
    {
        if (isAlive && !gotHit)
        {
            currentHealth -= mod;
            StartCoroutine(GotHit());
            gotHit = true;
            if (currentHealth <= 0f)
            {
                isAlive = false;
                currentHealth = 0f;
                Debug.Log("GAME OVER!");
                gameOverScreen.SetActive(true);
                Time.timeScale = 0f;
            }
        }
    }

    IEnumerator GotHit()
    {
        var spriteCol = sprite.color;
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = spriteCol;
        yield return new WaitForSeconds(2f);
        gotHit = false;
    }

}
