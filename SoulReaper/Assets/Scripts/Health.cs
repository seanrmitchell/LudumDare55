using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float health = 0f;
    public bool isPlayer = false;

    [SerializeReference]
    private float currentHealth;

    // Start is called before the first frame update
    void Awake()
    {
        currentHealth = health;
    }

    public void UpdateHealth(float mod)
    {
        currentHealth -= mod;

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
}
