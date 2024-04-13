using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed;
    public Animator anim;

    private Vector2 move;
    private float hor_move;
    private float ver_move;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        hor_move = Input.GetAxis("Horizontal");
        ver_move = Input.GetAxis("Vertical");

        move = new Vector2(hor_move, ver_move);
        anim.SetFloat("Speed", move.magnitude);

        if (move.magnitude > 0 )
        {
            transform.Translate(speed * move * Time.deltaTime);
        }
    }
}
