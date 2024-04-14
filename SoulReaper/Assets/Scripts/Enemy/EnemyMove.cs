using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyMove : MonoBehaviour
{

    public float speed;
    public Animator anim;
    
    
    private float stoppingRad;

    private GameObject target;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        target = player;
        stoppingRad = GetComponent<EnemyAttack>().attackRadius;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("Ally Creature"))
        {
            target = GameObject.FindGameObjectWithTag("Ally Creature");
        }
        else
        {
            target = player;
        }

        if (Vector2.Distance(transform.position, target.transform.position) > stoppingRad)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            anim.SetTrigger("Moving");
        }
    }
}
